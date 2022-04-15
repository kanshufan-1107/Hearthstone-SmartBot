using SmartBot.Plugins;
using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.AutoInjectAndStart
{
    class AutoInjectAndStartPlugin : Plugin
    {
        private AutoInjectAndStartPluginData GetPluginData()
        {
            return (AutoInjectAndStartPluginData)DataContainer;
        }
        public override void OnPluginCreated()
        {
            IsDll = true;
            //配置信息
            AutoInjectAndStartPluginData pluginData = GetPluginData();
            if (pluginData.Enabled)
            {
                Bot.Log("[AutoInjectAndStart] - AutoInjectAndStart插件已初始化成功...");
                //获取炉石传说进程 Hearthstone
                Process[] HearthstoneProcess = Process.GetProcessesByName("Hearthstone");
                //启动炉石传说
                if (pluginData.AutoOpenHSAndInject && HearthstoneProcess.Length <= 0)
                {
                    Bot.Log("[AutoInjectAndStart] - 启动炉石传说,脚本注入中...");
                    Bot.StartRelogger();
                }
            }
            base.OnPluginCreated();
        }

        public override void OnInjection()
        {
            //配置信息
            AutoInjectAndStartPluginData pluginData = GetPluginData();
            if (pluginData.Enabled && pluginData.AutoStart)
            {
                Bot.Log("[AutoInjectAndStart] - 脚本已注入,启动中...");
                Bot.StartBot();
            }
            base.OnInjection();
        }
    }
}
