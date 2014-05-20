using System;

namespace CoinTNet.DO.Exchanges
{
    /// <summary>
    /// Class used to wrap the result of proxy calls
    /// </summary>
    /// <typeparam name="T">The type of result returned by the proxy</typeparam>
    public class CallResult<T>
    {
        /// <summary>
        /// Initialises a new instance of the class with an error
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        public CallResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        /// <param name="result">The wrapped result</param>
        /// <param name="errorMessage">An optional error message</param>
        public CallResult(T result, string errorMessage = "")
        {
            Result = result;
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Gets a value indicating if the call was successful
        /// </summary>
        public bool Success
        {
            get { return string.IsNullOrEmpty(ErrorMessage); }

        }
        /// <summary>
        /// Gets or the call's result
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets an error code
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
