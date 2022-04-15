using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.AutoResizeAndStickTop
{
    class AutoResizeAndStickTop : Plugin
    {
        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        /// <summary>
        /// 窗口置顶
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        };

        int tickIndex = 1;

        /// <summary>
        /// 插件配置信息
        /// </summary>
        private AutoResizeAndStickTopPluginData GetPluginData()
        {
            return (AutoResizeAndStickTopPluginData)DataContainer;
        }

        public override void OnPluginCreated()
        {
            IsDll = true;
            //插件配置信息
            AutoResizeAndStickTopPluginData pluginData = GetPluginData();
            //获取炉石传说句柄
            IntPtr lscs = FindWindow(null, pluginData.ckmc_);

            if (pluginData.Enabled)
            {
                Bot.Log("[AutoResizeAndStickTop] - AutoResizeAndStickTop插件已初始化成功...");
                //只有插件初始化才会置顶炉石窗口
                SetForegroundWindow(lscs);
                //修改炉石窗口大小
                ResizeWindowsForms();

            }
            base.OnPluginCreated();
        }

        public override void OnTick()
        {
            //每30秒判断一次
            if (tickIndex == 100)
            {
                ResizeWindowsForms();
            }

            if (tickIndex <= 100)
            {
                tickIndex++;
            }
            else
            {
                tickIndex = 0;
            }
            base.OnTick();
        }

        private void ResizeWindowsForms()
        {
            //插件配置信息
            AutoResizeAndStickTopPluginData pluginData = GetPluginData();
            //获取炉石传说句柄
            IntPtr lscs = FindWindow(null, pluginData.ckmc_);

            RECT rect = new RECT();
            GetWindowRect(lscs, ref rect);

            //窗口的宽度
            int width = rect.Right - rect.Left;
            //窗口的高度
            int height = rect.Bottom - rect.Top;
            int x = rect.Left;
            int y = rect.Top;

            if (x != pluginData.x_ || y != pluginData.y_ || width != pluginData.width_ || height != pluginData.height_)
            {
                //修改炉石传说窗体大小
                MoveWindow(lscs, pluginData.x_, pluginData.y_, pluginData.width_, pluginData.height_, false);
                Bot.Log("[AutoResizeAndStickTop] - 炉石传说 - 窗口大小修改为【宽:" + pluginData.width_ + ",高:" + pluginData.height_ + "】 - 左上角坐标为【" + pluginData.x_ + "," + pluginData.y_ + "】");
            }
        }
    }
}
