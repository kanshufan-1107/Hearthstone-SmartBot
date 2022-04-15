using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.AutoInjectAndStart
{
    [Serializable]
    class AutoInjectAndStartPluginData : PluginDataContainer
    {
        public AutoInjectAndStartPluginData()
        {
            Name = "AutoInjectAndStart";
            Enabled = true;
        }

        [Category("Plugin")]
        [DisplayName("功能说明")]
        public string Description
        {
            get
            {
                return "SmartBot自动启动炉石传说,注入并开始!";
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅰ.炉石传说自启并注入脚本")]
        public bool AutoOpenHSAndInject
        {
            get; set;
        }

        [Category("Setting")]
        [DisplayName("Ⅱ.插件自动开始")]
        public bool AutoStart
        {
            get; set;
        }
    }
}
