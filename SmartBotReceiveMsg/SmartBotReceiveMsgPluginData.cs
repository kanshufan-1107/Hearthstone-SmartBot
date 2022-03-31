using System;
using System.Collections.Generic;
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
        }
    }
}
