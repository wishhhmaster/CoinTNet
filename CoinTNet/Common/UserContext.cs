
namespace CoinTNet.Common
{
    /// <summary>
    /// Logged in user context
    /// </summary>
    class UserContext
    {
        /// <summary>
        /// Reference to the unique instance
        /// </summary>
        private static UserContext _instance;

        /// <summary>
        /// Gets the current context
        /// </summary>
        public static UserContext CurrentContext
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserContext();
                }
                return _instance;
            }
        }

        /// <summary>
        /// The user's profile name
        /// </summary>
        public string ProfileName { get; set; }
        /// <summary>
        /// The user's password hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
