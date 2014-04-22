using CoinTNet.Common;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace CoinTNet.DAL
{
    /// <summary>
    /// Contains methods to store/retrieve encrypted data
    /// </summary>
    class SecureStorage
    {
        /// <summary>
        /// Saves private data for the current profile. 
        /// </summary>
        /// <typeparam name="T">The type of data to save</typeparam>
        /// <param name="par"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool SaveEncryptedData<T>(T par, string name)
        {
            
            string fileName = UserContext.CurrentContext.ProfileName + "." + name + ".config";

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in properties)
            {
                // Only work with strings
                if (p.PropertyType != typeof(string)) { continue; }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead) { continue; }

                MethodInfo mget = p.GetGetMethod(false);
                MethodInfo mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null) { continue; }
                if (mset == null) { continue; }

                var val = (string)p.GetValue(par, null);

                p.SetValue(par, SecurityHelper.EncryptAES(val, UserContext.CurrentContext.PasswordHash), null);
            }



            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (var sw = new StreamWriter(fileName))
            {
                xs.Serialize(sw, par);
            }
            return true;
        }

        /// <summary>
        /// Gets encryptedinfo (API keys) for a given identifier
        /// </summary>
        /// <typeparam name="T">The type of info to return</typeparam>
        /// <param name="name">The name of the data</param>
        /// <returns></returns>
        public static T GetEncryptedData<T>(string name) where T : class,new()
        {
            string fileName = UserContext.CurrentContext.ProfileName + "." + name + ".config";

            if (!File.Exists(fileName))
            {
                return new T();
            }
            T par = null;
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (var sw = new StreamReader(fileName))
            {
                par = xs.Deserialize(sw) as T;
            }

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in properties)
            {
                // Only work with strings
                if (p.PropertyType != typeof(string)) { continue; }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead) { continue; }

                MethodInfo mget = p.GetGetMethod(false);
                MethodInfo mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null) { continue; }
                if (mset == null) { continue; }

                var val = (string)p.GetValue(par, null);

                p.SetValue(par, SecurityHelper.DecryptAES(val, UserContext.CurrentContext.PasswordHash), null);
            }

            return par;
        }

    }
}
