using CoinTNet.DO;

namespace CoinTNet.UI.Interfaces
{
    /// <summary>
    /// Interface for all controls providing strategy settings
    /// </summary>
    interface IStrategySettingsControl
    {
        /// <summary>
        /// Assigns strategy-specific settings to the class
        /// </summary>
        /// <param name="settings"></param>
        void FillSettings(StrategySettings settings);
    }
}
