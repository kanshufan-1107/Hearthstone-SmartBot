using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.AutoResizeAndStickTop
{
    [Serializable]
    class AutoResizeAndStickTopPluginData : PluginDataContainer
    {
        /// <summary>
        /// 炉石传说窗口名称
        /// </summary>
        public string ckmc_;
        /// <summary>
        /// 宽度
        /// </summary>
        public int width_;
        /// <summary>
        /// 高度
        /// </summary>
        public int height_;
        /// <summary>
        /// X
        /// </summary>
        public int x_;
        /// <summary>
        /// Y
        /// </summary>
        public int y_;

        public AutoResizeAndStickTopPluginData()
        {
            Name = "AutoResizeAndStickTop";
            Enabled = true;
            width_ = 549;
            height_ = 439;
            x_ = 1378;
            y_ = 608;
        }

        [Category("Plugin")]
        [DisplayName("功能说明")]
        public string Description
        {
            get
            {
                return "炉石传说窗口自动调整大小并前置";
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅰ.炉石传说-窗口名称")]
        public string Ckmc
        {
            get
            {
                return ckmc_;
            }
            set
            {
                ckmc_ = value;
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅱ.炉石传说窗口-宽")]
        public int Width
        {
            get
            {
                return width_;
            }
            set
            {
                if (value < 549)
                {
                    width_ = 549;
                    return;
                }
                else if (value > 1600)
                {
                    width_ = 1600;
                    return;
                }

                width_ = value;
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅲ.炉石传说窗口-高")]
        public int Height
        {
            get
            {
                return height_;
            }
            set
            {
                if (value < 439)
                {
                    height_ = 439;
                    return;
                }
                else if (value > 900)
                {
                    height_ = 900;
                    return;
                }

                height_ = value;
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅳ.炉石传说窗口-左上角X坐标")]
        public int X
        {
            get
            {
                return x_;
            }
            set
            {
                int max = 1927 - width_;
                if (value < 0)
                {
                    x_ = 0;
                    return;
                }
                else if (value > max)
                {
                    x_ = max;
                    return;
                }
                x_ = value;
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅴ.炉石传说窗口-左上角Y坐标")]
        public int Y
        {
            get
            {
                return y_;
            }
            set
            {
                int max = 1047 - height_;
                if (value < 0)
                {
                    y_ = 0;
                    return;
                }
                else if (value > max)
                {
                    y_ = max;
                    return;
                }

                y_ = value;
            }
        }
    }
}
