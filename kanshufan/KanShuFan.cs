using SmartBot.Plugins.API;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SmartBot.Plugins.kanshufan
{
    [Serializable]
    class KanShuFanPluginData : PluginDataContainer
    {
        /// <summary>
        /// 德鲁伊卡组
        /// </summary>
        public string deckDruid_;
        /// <summary>
        /// 猎人卡组
        /// </summary>
        public string deckHunter_;
        /// <summary>
        /// 法师卡组
        /// </summary>
        public string deckMage_;
        /// <summary>
        /// 圣骑士卡组
        /// </summary>
        public string deckPaladin_;
        /// <summary>
        /// 牧师卡组
        /// </summary>
        public string deckPriest_;
        /// <summary>
        /// 潜行者卡组
        /// </summary>
        public string deckRogue_;
        /// <summary>
        /// 萨满祭司卡组
        /// </summary>
        public string deckShaman_;
        /// <summary>
        /// 术士卡组
        /// </summary>
        public string deckWarlock_;
        /// <summary>
        /// 战士卡组
        /// </summary>
        public string deckWarrior_;
        /// <summary>
        /// 恶魔猎手卡组
        /// </summary>
        public string deckDemonHunter_;
        /// <summary>
        /// 各个职业上限等级
        /// </summary>
        public int level_;
        /// <summary>
        /// 天梯最高等级
        /// </summary>
        public int rankLevel_;
        /// <summary>
        /// 英雄等级展示
        /// </summary>
        public string heroLevel_;
        /// <summary>
        /// 天梯等级展示
        /// </summary>
        public string rankLevelShow_;
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

        /// <summary>
        /// 初始化
        /// </summary>
        public KanShuFanPluginData()
        {
            Name = "KanShuFan";
            Enabled = true;
            width_ = 549;
            height_ = 439;
            x_ = 1378;
            y_ = 608;
            AutoOpenHSAndInject = true;
            AutoStart = true;
            AutoCheckIsHung = true;
        }

        [Category("Plugin")]
        [DisplayName("功能说明")]
        public string Description
        {
            get
            {
                return "KanShuFan自用整合插件";
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

        [Category("A.核心设置")]
        [DisplayName("Ⅲ.检测炉石传说是否无响应,无响应自动重启")]
        public bool AutoCheckIsHung
        {
            get; set;
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅳ.练级模式")]
        [ItemsSource(typeof(Mode))]
        public Bot.Mode AutoMode
        {
            get; set;
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅴ.各个职业上限等级")]
        public int Level
        {
            get
            {
                return level_;
            }
            set
            {
                if (value < 1)
                {
                    level_ = 1;
                    return;
                }
                else if (value > 60)
                {
                    level_ = 60;
                    return;
                }

                level_ = value;
            }
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅵ.天梯上限等级")]
        public int RankLevel
        {
            get
            {
                return rankLevel_;
            }
            set
            {
                if (value < 1)
                {
                    rankLevel_ = 1;
                    return;
                }
                else if (value > 40)
                {
                    rankLevel_ = 40;
                    return;
                }

                rankLevel_ = value;
            }
        }

        [Category("A.核心设置")]
        [DisplayName("Ⅶ.炉石传说-窗口名称")]
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

        [Category("A.核心设置")]
        [DisplayName("Ⅷ.炉石传说窗口-宽")]
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

        [Category("A.核心设置")]
        [DisplayName("Ⅸ.炉石传说窗口-高")]
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

        [Category("A.核心设置")]
        [DisplayName("Ⅹ.炉石传说窗口-左上角X坐标")]
        public int X
        {
            get
            {
                return x_;
            }
            set
            {
                int max = 1927 - width_;//1378,608
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

        [Category("A.核心设置")]
        [DisplayName("Ⅺ.炉石传说窗口-左上角Y坐标")]
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

        [Category("B.信息展示")]
        [DisplayName("Ⅰ.英雄等级")]
        public string HeroLevel
        {
            get
            {
                return heroLevel_;
            }
        }

        [Category("B.信息展示")]
        [DisplayName("Ⅱ.天梯等级")]
        public string RankLevelShow
        {
            get
            {
                return rankLevelShow_;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅰ.小德卡组")]
        [ItemsSource(typeof(DeckSourceDruid))]
        public string Decks1
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckDruid_))
                {
                    this.deckWarlock_ = "None";
                }
                return this.deckDruid_;
            }
            set
            {
                this.deckDruid_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅱ.猎人卡组")]
        [ItemsSource(typeof(DeckSourceHunter))]
        public string Decks2
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckHunter_))
                {
                    this.deckHunter_ = "None";
                }
                return this.deckHunter_;
            }
            set
            {
                this.deckHunter_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅲ.法师卡组")]
        [ItemsSource(typeof(DeckSourceMage))]
        public string Decks3
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckMage_))
                {
                    this.deckMage_ = "None";
                }
                return this.deckMage_;
            }
            set
            {
                this.deckMage_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅳ.圣骑卡组")]
        [ItemsSource(typeof(DeckSourcePaladin))]
        public string Decks4
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckPaladin_))
                {
                    this.deckPaladin_ = "None";
                }
                return this.deckPaladin_;
            }
            set
            {
                this.deckPaladin_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅴ.牧师卡组")]
        [ItemsSource(typeof(DeckSourcePriest))]
        public string Decks5
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckPriest_))
                {
                    this.deckPriest_ = "None";
                }
                return this.deckPriest_;
            }
            set
            {
                this.deckPriest_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅵ.盗贼卡组")]
        [ItemsSource(typeof(DeckSourceRogue))]
        public string Decks6
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckRogue_))
                {
                    this.deckRogue_ = "None";
                }
                return this.deckRogue_;
            }
            set
            {
                this.deckRogue_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅶ.萨满卡组")]
        [ItemsSource(typeof(DeckSourceShaman))]
        public string Decks7
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckShaman_))
                {
                    this.deckShaman_ = "None";
                }
                return this.deckShaman_;
            }
            set
            {
                this.deckShaman_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅷ.术士卡组")]
        [ItemsSource(typeof(DeckSourceWarlock))]
        public string Decks8
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckWarlock_))
                {
                    this.deckWarlock_ = "None";
                }
                return this.deckWarlock_;
            }
            set
            {
                this.deckWarlock_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅸ.战士卡组")]
        [ItemsSource(typeof(DeckSourceWarrior))]
        public string Decks9
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckWarrior_))
                {
                    this.deckWarrior_ = "None";
                }
                return this.deckWarrior_;
            }
            set
            {
                this.deckWarrior_ = value;
            }
        }

        [Category("C.练级")]
        [DisplayName("Ⅹ.魔猎卡组")]
        [ItemsSource(typeof(DeckSourceDemonHunter))]
        public string Decks10
        {
            get
            {
                if (string.IsNullOrEmpty(this.deckDemonHunter_))
                {
                    this.deckDemonHunter_ = "None";
                }
                return this.deckDemonHunter_;
            }
            set
            {
                this.deckDemonHunter_ = value;
            }
        }

        /// <summary>
        /// 1.德鲁伊卡组
        /// </summary>
        public sealed class DeckSourceDruid : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.DRUID))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 德鲁伊卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 2.猎人
        /// </summary>
        public sealed class DeckSourceHunter : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.HUNTER))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 猎人卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 3.法师卡组
        /// </summary>
        public sealed class DeckSourceMage : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.MAGE))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 法师卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 4.圣骑士卡组
        /// </summary>
        public sealed class DeckSourcePaladin : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.PALADIN))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 圣骑士卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 5.牧师卡组
        /// </summary>
        public sealed class DeckSourcePriest : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.PRIEST))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 牧师卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 6.潜行者卡组
        /// </summary>
        public sealed class DeckSourceRogue : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.ROGUE))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 潜行者卡组查询出错...");
                }
                return itemsTemp;
            }

        }

        /// <summary>
        /// 7.萨满祭司卡组
        /// </summary>
        public sealed class DeckSourceShaman : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.SHAMAN))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 萨满祭司卡组查询出错...");
                }
                return itemsTemp;
            }

        }

        /// <summary>
        /// 8.战士卡组
        /// </summary>
        public sealed class DeckSourceWarlock : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.WARLOCK))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 战士卡组查询出错...");
                }
                return itemsTemp;
            }

        }

        /// <summary>
        /// 9.术士卡组
        /// </summary>
        public sealed class DeckSourceWarrior : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.WARRIOR))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 术士卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 10.恶魔猎手卡组
        /// </summary>
        public sealed class DeckSourceDemonHunter : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { "None", "None" }
                };
                ItemCollection itemsTemp = items;
                try
                {
                    foreach (Deck deck in Bot.GetDecks().FindAll(x => x.Class == Card.CClass.DEMONHUNTER))
                    {
                        itemsTemp.Add(deck.Name, deck.Name);
                    }
                }
                catch (Exception)
                {
                    Bot.Log("[kanshufan] - 恶魔猎手卡组查询出错...");
                }
                return itemsTemp;
            }
        }

        /// <summary>
        /// 练级模式
        /// </summary>
        public sealed class Mode : IItemsSource
        {
            public ItemCollection GetValues()
            {
                ItemCollection items = new ItemCollection
                {
                    { Enum.GetName(typeof(Bot.Mode), Bot.Mode.None), "空" },
                    { Enum.GetName(typeof(Bot.Mode), Bot.Mode.Classic), "经典" },
                    { Enum.GetName(typeof(Bot.Mode), Bot.Mode.Standard), "标准" },
                    { Enum.GetName(typeof(Bot.Mode), Bot.Mode.Wild), "狂野" }
                };
                return items;
            }
        }
    }

    class KanShuFan : Plugin
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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
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

        /// <summary>
        /// 确定系统是否认为指定的应用程序没有响应
        /// </summary>
        /// <param name="hWnd">要测试的窗口的句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool IsHungAppWindow(IntPtr hWnd);

        /// <summary>
        /// 插件配置信息
        /// </summary>
        private KanShuFanPluginData GetPluginData()
        {
            return (KanShuFanPluginData)DataContainer;
        }

        int tickIndex = 1;

        /// <summary>
        /// 插件初始化
        /// </summary>
        public override void OnPluginCreated()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
            pluginData.heroLevel_ = GetHeroLevel();
            pluginData.rankLevelShow_ = GetRankLevelShow();

            Bot.Log("[kanshufan] - kanshufan插件已初始化成功...");

            if (pluginData.Enabled)
            {
                InitWindowsForms();
                //获取炉石传说进程 Hearthstone
                Process[] HearthstoneProcess = Process.GetProcessesByName("Hearthstone");
                //自动启动炉石传说
                if (pluginData.AutoOpenHSAndInject && HearthstoneProcess.Length <= 0)
                {
                    Bot.Log("[kanshufan] - 启动炉石传说,准备脚本注入...");
                    Bot.StartRelogger();
                }
            }
            base.OnPluginCreated();
        }

        /// <summary>
        /// 每300ms调用
        /// </summary>
        public override void OnTick()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();

            if (pluginData.AutoStart && !Bot.IsBotRunning()) 
            {
                Bot.Log("[kanshufan] - SmartBot启动中...");
                Bot.StartBot();
            }

            //每30秒判断一次
            if (tickIndex == 100)
            {
                //获取炉石传说句柄
                IntPtr lscs = FindWindow(null, pluginData.ckmc_);
                bool hungFlag = IsHungAppWindow(lscs);
                if (hungFlag)
                {
                    Bot.Log("[kanshufan] - 炉石传说卡住,重启中...");
                    Bot.CloseHs();
                    System.Threading.Thread.Sleep(5000);
                    Bot.StartRelogger();
                }
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

        /// <summary>
        /// 插件注入
        /// </summary>
        public override void OnInjection()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
            if (pluginData.Enabled)
            {
                InitWindowsForms();
                if (pluginData.AutoStart)
                {
                    Bot.Log("[kanshufan] - 脚本已注入,启动中...");
                    Bot.StartBot();
                }
            }
            base.OnInjection();
        }

        /// <summary>
        /// Smartbot开始
        /// </summary>
        public override void OnStarted()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
            if (pluginData.Enabled)
            {
                InitWindowsForms();

                //玩家信息
                PlayerData player = Bot.GetPlayerDatas();
                //德鲁伊英雄等级
                int heroLevel = player.GetLevel(Card.CClass.DRUID);
                //下一个随机卡组
                String deckName = GetNextDeck(pluginData);

                if (heroLevel >= pluginData.Level)
                {
                    Bot.Log("[kanshufan] - 切换卡组...");
                    Bot.ChangeDeck(deckName);
                }
                //判断当前模式是否符合要求
                if (Bot.CurrentDeck().Name.StartsWith("[C]"))
                {
                    Bot.ChangeMode(Bot.Mode.Classic);
                    pluginData.AutoMode = Bot.Mode.Classic;
                }
                else if (Bot.CurrentDeck().Name.StartsWith("[S]"))
                {
                    Bot.ChangeMode(Bot.Mode.Standard);
                    pluginData.AutoMode = Bot.Mode.Standard;
                }
                else if (Bot.CurrentDeck().Name.StartsWith("[W]"))
                {
                    Bot.ChangeMode(Bot.Mode.Wild);
                    pluginData.AutoMode = Bot.Mode.Wild;
                }
                Bot.Log("[kanshufan] - 切换模式...");
            }
            base.OnStarted();
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        public override void OnGameBegin()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
            //玩家信息
            PlayerData player = Bot.GetPlayerDatas();
            //天梯等级
            int rankLevel = player.GetRank(pluginData.AutoMode);

            if (pluginData.Enabled)
            {
                //判断当前模式天梯等级
                if (rankLevel < pluginData.rankLevel_)
                {
                    Bot.SendEmote(Bot.EmoteType.WellPlayed);
                    Bot.Concede();
                    Bot.Log("[kanshufan] - 天梯等级达到上限,投降...");
                }
            }
            base.OnGameBegin();
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public override void OnGameEnd()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
            //玩家信息
            PlayerData player = Bot.GetPlayerDatas();
            //天梯等级
            int rankLevel = player.GetRank(pluginData.AutoMode);

            if (pluginData.Enabled)
            {

                //防止快速投降导致的报错
                if (rankLevel >= pluginData.rankLevel_)
                {
                    InitWindowsForms();

                    //当前战场
                    Board board = Bot.CurrentBoard;
                    //当前英雄职业
                    Card.CClass heroClass = board.FriendClass;
                    //当前英雄等级
                    int heroLevel = player.GetLevel(heroClass);
                    //下一个随机卡组
                    String deckName = GetNextDeck(pluginData);

                    //判断当前英雄等级是否大于等于上限等级，如果是，则切换卡组
                    if (heroLevel >= pluginData.Level)
                    {
                        Bot.Log("[kanshufan] - 切换卡组...");
                        Bot.ChangeDeck(deckName);
                    }
                    //判断当前模式是否符合要求
                    if (Bot.CurrentDeck().Name.StartsWith("[C]"))
                    {
                        Bot.ChangeMode(Bot.Mode.Classic);
                        pluginData.AutoMode = Bot.Mode.Classic;
                    }
                    else if (Bot.CurrentDeck().Name.StartsWith("[S]"))
                    {
                        Bot.ChangeMode(Bot.Mode.Standard);
                        pluginData.AutoMode = Bot.Mode.Standard;
                    }
                    else if (Bot.CurrentDeck().Name.StartsWith("[W]"))
                    {
                        Bot.ChangeMode(Bot.Mode.Wild);
                        pluginData.AutoMode = Bot.Mode.Wild;
                    }
                    Bot.Log("[kanshufan] - 切换模式...");
                }
            }
            pluginData.heroLevel_ = GetHeroLevel();
            pluginData.rankLevelShow_ = GetRankLevelShow();
            base.OnGameEnd();
        }

        private string GetNextDeck(KanShuFanPluginData pluginData)
        {
            Random rd = new Random();
            switch (rd.Next(0, 9))
            {
                case 0:
                    return pluginData.deckDruid_;
                case 1:
                    return pluginData.deckHunter_;
                case 2:
                    return pluginData.deckMage_;
                case 3:
                    return pluginData.deckPaladin_;
                case 4:
                    return pluginData.deckPriest_;
                case 5:
                    return pluginData.deckRogue_;
                case 6:
                    return pluginData.deckShaman_;
                case 7:
                    return pluginData.deckWarlock_;
                case 8:
                    return pluginData.deckWarrior_;
                case 9:
                    return pluginData.deckDemonHunter_;
                default:
                    return pluginData.deckDruid_;
            }
        }

        private string GetHeroLevel()
        {
            //玩家信息
            PlayerData player = Bot.GetPlayerDatas();
            string str = "小德:" + player.GetLevel(Card.CClass.DRUID) + "; 猎人:" + player.GetLevel(Card.CClass.HUNTER) + "; 法师:" + player.GetLevel(Card.CClass.MAGE) + "; 圣骑:" + player.GetLevel(Card.CClass.PALADIN) + ";\r\n"
                + "牧师:" + player.GetLevel(Card.CClass.PRIEST) + "; 盗贼:" + player.GetLevel(Card.CClass.ROGUE) + "; 萨满:" + player.GetLevel(Card.CClass.SHAMAN) + "; 术士:" + player.GetLevel(Card.CClass.WARLOCK) + ";\r\n"
                + "战士:" + player.GetLevel(Card.CClass.WARRIOR) + "; 魔猎:" + player.GetLevel(Card.CClass.DEMONHUNTER);
            return str;
        }

        private string GetRankLevelShow()
        {
            //玩家信息
            PlayerData player = Bot.GetPlayerDatas();
            string str = "经典:" + player.GetRank(Bot.Mode.Classic) + "; 标准:" + player.GetRank(Bot.Mode.Standard) + "; 狂野:" + player.GetRank(Bot.Mode.Wild);
            return str;
        }

        private void InitWindowsForms()
        {
            //插件配置信息
            KanShuFanPluginData pluginData = GetPluginData();
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
                SetForegroundWindow(lscs);
                Bot.Log("[kanshufan] - 炉石传说 - 窗口大小修改为【宽:" + pluginData.width_ + ",高:" + pluginData.height_ + "】 - 左上角坐标为【" + pluginData.x_ + "," + pluginData.y_ + "】,并置顶");
            }
        }
    }
}
