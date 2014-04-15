using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTNet.BLL.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    class StrategyAttribute : Attribute
    {
        public StrategyAttribute(string name, Type settingsType)
        {
            Name = name;
            SettingsType = settingsType;
        }
        public string Name { get; set; }
        public Type SettingsType { get; set; }
    }
}
