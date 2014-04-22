using CoinTNet.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoinTNet.DAL
{
    /// <summary>
    /// Contains methods for managing user profiles
    /// </summary>
    class AuthenticationService
    {
        private const string ProfilesFileName = "profiles.txt";
        private const string Separator = ":";

        /// <summary>
        /// Creates a new profile and logs the user in
        /// </summary>
        /// <param name="profileName">The name of the profile</param>
        /// <param name="password">The user's password</param>
        public void CreateProfile(string profileName, string password)
        {
            string salt = SecurityHelper.GenerateSalt();
            string s = string.Format("{0}:{1}:{2}\n", profileName, salt, SecurityHelper.HashSha512(profileName, salt, password));
            File.AppendAllText(ProfilesFileName, s);
            UserContext.CurrentContext.ProfileName = profileName;
            UserContext.CurrentContext.PasswordHash = SecurityHelper.HashSha512(password, profileName);
        }

        /// <summary>
        /// Tries to login the user
        /// </summary>
        /// <param name="profileName">The profile name</param>
        /// <param name="password">The user's password</param>
        /// <returns></returns>
        public bool Login(string profileName, string password)
        {
            if (!File.Exists(ProfilesFileName))
            {
                return false;
            }
            var savedProfileLine = File.ReadAllLines(ProfilesFileName).Where(l => l.Split(new[] { ':' })[0] == profileName).FirstOrDefault();

            if (savedProfileLine != null)
            {
                var buf = savedProfileLine.Split(new[] { ':' });
                if (SecurityHelper.HashSha512(profileName, buf[1], password) == buf[2])
                {
                    UserContext.CurrentContext.ProfileName = profileName;
                    UserContext.CurrentContext.PasswordHash = SecurityHelper.HashSha512(password, profileName);
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Retrives a list of available profiles
        /// </summary>
        /// <returns>The saved profiles</returns>
        public IList<string> GetProfiles()
        {
            if (!File.Exists(ProfilesFileName))
            {
                return new List<string>();
            }
            return File.ReadAllLines(ProfilesFileName).Select(l => l.Split(new[] { ':' })[0]).ToList();
        }
    }
}
