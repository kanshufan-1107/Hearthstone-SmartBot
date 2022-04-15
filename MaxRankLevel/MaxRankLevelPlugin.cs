using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBot.Plugins.MaxRankLevel
{
    class MaxRankLevelPlugin : Plugin
    {
        /// <summary>
        /// 插件配置信息
        /// </summary>
        private MaxRankLevelPluginData GetPluginData()
        {
            return (MaxRankLevelPluginData)DataContainer;
        }

        public override void OnPluginCreated()
        {
            MaxRankLevelPluginData pluginData = GetPluginData();
            IsDll = true;
            if(pluginData.Enabled)
            {
                Bot.Log("[MaxRankLevel] - MaxRankLevel插件已初始化成功...");
            }
            base.OnPluginCreated();
        }

        public override void OnGameBegin()
        {
            MaxRankLevelPluginData pluginData = GetPluginData();
            //玩家信息
            PlayerData player = Bot.GetPlayerDatas();
            //天梯等级
            int rankLevel = player.GetRank(Bot.CurrentMode());
            if (pluginData.Enabled)
            {
                //判断当前模式天梯等级
                if (Bot.CurrentMode() == Bot.Mode.Standard && rankLevel < pluginData.StandardLevel)
                {
                    Bot.Concede();
                    Bot.Log("[MaxRankLevel] - 标准天梯等级达到上限,投降...");
                }
                else if (Bot.CurrentMode() == Bot.Mode.Classic && rankLevel < pluginData.ClassicLevel)
                {
                    Bot.Concede();
                    Bot.Log("[MaxRankLevel] - 经典天梯等级达到上限,投降...");
                }
                else if (Bot.CurrentMode() == Bot.Mode.Wild && rankLevel < pluginData.WildLevel)
                {
                    Bot.Concede();
                    Bot.Log("[MaxRankLevel] - 狂野天梯等级达到上限,投降...");
                }
            }
            base.OnGameBegin();
        }
    }
}
