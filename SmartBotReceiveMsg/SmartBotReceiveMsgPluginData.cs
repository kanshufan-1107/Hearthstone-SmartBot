using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.SmartBotReceiveMsg
{
    [Serializable]
    class SmartBotReceiveMsgPluginData : PluginDataContainer
    {
        public SmartBotReceiveMsgPluginData()
        {
            Name = "SmartBotReceiveMsg";
            Enabled = true;
            AutoOpenHSAndInject = true;
            AutoStart = true;
        }

        [Category("Plugin")]
        [DisplayName("功能说明")]
        public string Description
        {
            get
            {
                return "SmartBot接收中控信息，以及SmartBot必须的设置";
            }
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅰ.炉石传说自启并注入脚本")]
        public bool AutoOpenHSAndInject
        {
            get; set;
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅱ.插件自动开始")]
        public bool AutoStart
        {
            get; set;
        }
    }
}
