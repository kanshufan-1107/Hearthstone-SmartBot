using SmartBot.Plugins;
using SmartBot.Plugins.API;
using SmartBot.Plugins.SmartBotReceiveMsg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartBotReceiveMsg
{
    class SmartBotReceiveMsg : Plugin
    {
        /// <summary>
        /// 中控通信配置文件
        /// </summary>
        private static readonly string SBCentralControlIniPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "SBCentralControl.ini";

        /// <summary>
        /// 为INI文件中指定的节点取得字符串
        /// </summary>
        /// <param name="lpAppName">欲在其中查找关键字的节点名称</param>
        /// <param name="lpKeyName">欲获取的项名</param>
        /// <param name="lpDefault">指定的项没有找到时返回的默认值</param>
        /// <param name="lpReturnedString">指定一个字串缓冲区，长度至少为nSize</param>
        /// <param name="nSize">指定装载到lpReturnedString缓冲区的最大字符数量</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>复制到lpReturnedString缓冲区的字节数量，其中不包括那些NULL中止字符</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        /// <summary>
        /// 修改INI文件中内容
        /// </summary>
        /// <param name="lpApplicationName">欲在其中写入的节点名称</param>
        /// <param name="lpKeyName">欲设置的项名</param>
        /// <param name="lpString">要写入的新字符串</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public static string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filePath);
            return sb.ToString();
        }

        /// <summary>
        /// 插件配置信息
        /// </summary>
        private SmartBotReceiveMsgPluginData GetPluginData()
        {
            return (SmartBotReceiveMsgPluginData)DataContainer;
        }

        public override void OnPluginCreated()
        {
            IsDll = true;
            Bot.Log("[SmartBotReceiveMsg] - SmartBotReceiveMsg插件已初始化成功...");
            //配置信息
            SmartBotReceiveMsgPluginData pluginData = GetPluginData();

            if (pluginData.Enabled)
            {
                //获取炉石传说进程 Hearthstone
                Process[] HearthstoneProcess = Process.GetProcessesByName("Hearthstone");
                //自动启动炉石传说
                if (pluginData.AutoOpenHSAndInject && HearthstoneProcess.Length <= 0)
                {
                    Bot.Log("[SmartBotReceiveMsg] - 启动炉石传说,准备脚本注入...");
                    Bot.StartRelogger();
                }
            }
            base.OnPluginCreated();
        }

        public override void OnInjection()
        {
            //插件配置信息
            SmartBotReceiveMsgPluginData pluginData = GetPluginData();
            if (pluginData.Enabled)
            {
                if (pluginData.AutoStart)
                {
                    Bot.Log("[SmartBotReceiveMsg] - 脚本已注入,启动中...");
                    Bot.StartBot();
                }
            }
            base.OnInjection();
        }

        public override void OnTick()
        {
            //配置信息
            SmartBotReceiveMsgPluginData pluginData = GetPluginData();

            if (pluginData.Enabled && File.Exists(SBCentralControlIniPath))
            {
                if (bool.Parse(Read("SmartBot", "CloseHs", false.ToString(), SBCentralControlIniPath)))
                {
                    Bot.Log("[SmartBotReceiveMsg] - OnTick - 关闭炉石传说...");
                    Bot.CloseBot();
                    WritePrivateProfileString("SmartBot", "CloseHs", false.ToString(), SBCentralControlIniPath);
                }
                System.Threading.Thread.Sleep(2000);
                if (bool.Parse(Read("SmartBot", "StartRelogger", false.ToString(), SBCentralControlIniPath)))
                {
                    Bot.Log("[SmartBotReceiveMsg] - OnTick - 开启炉石传说...");
                    Bot.StartRelogger();
                    WritePrivateProfileString("SmartBot", "StartRelogger", false.ToString(), SBCentralControlIniPath);
                }
            }
            base.OnTick();
        }
    }
}
