using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.MaxRankLevel
{
    [Serializable]
    public class MaxRankLevelPluginData : PluginDataContainer
    {
        /// <summary>
        /// 标准等级
        /// </summary>
        private int StandardLevel_;
        /// <summary>
        /// 经典等级
        /// </summary>
        private int ClassicLevel_;
        /// <summary>
        /// 狂野等级
        /// </summary>
        private int WildLevel_;
        public MaxRankLevelPluginData()
        {
            Name = "MaxRankLevel";
            Enabled = false;
            StandardLevel_ = 60;
            ClassicLevel_ = 60;
            WildLevel_ = 60;
        }

        [Category("Plugin")]
        [DisplayName("功能说明")]
        public string Description
        {
            get
            {
                return "设置天梯最高等级";
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅰ.标准上限等级")]
        public int StandardLevel
        {
            get
            {
                return StandardLevel_;
            }
            set
            {
                if(value <= 0)
                {
                    StandardLevel_ = 1;
                }
                else if(value > 60)
                {
                    StandardLevel_ = 60;
                }
                else
                {
                    StandardLevel_ = value;
                }
                
            }
        }

        [Category("Setting")]
        [DisplayName("Ⅱ.经典上限等级")]
        public int ClassicLevel
        {
            get
            {
                return ClassicLevel_;
            }
            set
            {
                if (value <= 0)
                {
                    ClassicLevel_ = 1;
                }
                else if (value > 60)
                {
                    ClassicLevel_ = 60;
                }
                else
                {
                    ClassicLevel_ = value;
                }

            }
        }

        [Category("Setting")]
        [DisplayName("Ⅲ.狂野上限等级")]
        public int WildLevel
        {
            get
            {
                return WildLevel_;
            }
            set
            {
                if (value <= 0)
                {
                    WildLevel_ = 1;
                }
                else if (value > 60)
                {
                    WildLevel_ = 60;
                }
                else
                {
                    WildLevel_ = value;
                }
            }
        }
    }
}
