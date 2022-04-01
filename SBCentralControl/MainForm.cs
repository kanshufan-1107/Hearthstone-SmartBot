using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SBCentralControl
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 中控检测炉石和SB的频率，默认为5分钟
        /// </summary>
        int DetectionFrequency = 5;
        /// <summary>
        /// 炉石传说窗口名称
        /// </summary>
        string lscs_ckmc = "炉石传说";
        /// <summary>
        /// 中控运行时间
        /// </summary>
        string yxsj = "00:00:00";
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime startTime;
        /// <summary>
        /// SB重启次数
        /// </summary>
        int sbRestartNum = 0;
        /// <summary>
        /// 炉石重启次数
        /// </summary>
        int hsRestartNum = 0;

        /// <summary>
        /// 防检测程序文件夹
        /// </summary>
        private static readonly string BasePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "Temp" + Path.DirectorySeparatorChar.ToString() + "name.temp";
        /// <summary>
        /// 启动器所在地址
        /// </summary>
        private static readonly string LauncherPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "Launcher.exe";
        /// <summary>
        /// 中控通信配置文件
        /// </summary>
        private static readonly string SBCentralControlIniPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "SBCentralControl.ini";

        /// <summary>
        /// 旧输入信息
        /// </summary>
        string OldText1 = "炉石传说";
        string OldText2 = "5";

        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 隐藏光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// 确定系统是否认为指定的应用程序没有响应
        /// </summary>
        /// <param name="hWnd">要测试的窗口的句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool IsHungAppWindow(IntPtr hWnd);

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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //初始化页面信息
            textBox2.Text = lscs_ckmc;
            textBox4.Text = yxsj;
            textBox5.Text = sbRestartNum + "/" + hsRestartNum;

            textBox3.Text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] SB中控开始运行...";
            timer1.Stop();
            Log("以下为配置信息...");
            LogByNoTime("=================================================================");
            Log("炉石窗口名称: " + lscs_ckmc);
            Log("中控检测频率: " + DetectionFrequency + "分钟");
            LogByNoTime("=================================================================");
            bool flag = IsNumberic(textBox1.Text);
            if (flag)
            {
                timer1.Interval = 1000 * 60 * int.Parse(textBox1.Text);
            }
            else
            {
                timer1.Stop();
            }
            Log("中控运行状态: " + (timer1.Enabled ? "运行中" : "已停止"));
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //防止输入框初始焦点
            groupBox1.Focus();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            string text = string.Empty;
            if (File.Exists(BasePath))
            {
                text = File.ReadAllText(BasePath);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                Process[] processesByName = Process.GetProcessesByName(text.Replace(".exe", ""));
                if (processesByName.Length <= 0)
                {
                    //打开SB
                    Process newProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo(LauncherPath)
                    };
                    newProcess.Start();
                    Log("SmartBot未启动,启动中...");
                    System.Threading.Thread.Sleep(10000);
                    Process[] processesByName2 = Process.GetProcessesByName(text.Replace(".exe", ""));
                    if (processesByName2.Length > 0)
                    {
                        Log("SmartBot启动成功...");
                    }
                }
                foreach (Process process in processesByName)
                {
                    bool processHungFlag = IsHungAppWindow(process.Handle);
                    if (processHungFlag)
                    {
                        Log("SmartBot未响应,重启中...");
                        //关闭SB
                        process.Kill();
                        //打开SB
                        Process newProcess = new Process
                        {
                            StartInfo = new ProcessStartInfo(LauncherPath)
                        };
                        newProcess.Start();
                        sbRestartNum++;
                        textBox5.Text = sbRestartNum + "/" + hsRestartNum;
                        Process[] processesByName2 = Process.GetProcessesByName(text.Replace(".exe", ""));
                        if (processesByName2.Length > 0)
                        {
                            Log("SmartBot启动成功...");
                        }
                    }
                }
            }
            //获取炉石传说句柄
            IntPtr lscs = FindWindow(null, lscs_ckmc);
            bool hungFlag = IsHungAppWindow(lscs);
            if (hungFlag)
            {
                Log("炉石传说未响应,重启中...");
                //关闭炉石传说
                SendSmartBotMsg("CloseHs");
                System.Threading.Thread.Sleep(5000);
                //开启炉石传说
                SendSmartBotMsg("StartRelogger");
                hsRestartNum++;
                textBox5.Text = sbRestartNum + "/" + hsRestartNum;
                Process[] processesByName2 = Process.GetProcessesByName("Hearthstone");
                if (processesByName2.Length > 0)
                {
                    Log("炉石传说启动成功...");
                }
            }
        }

        private void Text1_change(object sender, EventArgs e)
        {
            lscs_ckmc = textBox2.Text;
        }

        private void Text1_Leave(object sender, EventArgs e)
        {
            if(OldText1 != textBox2.Text)
            {
                Log("炉石传说窗口名称修改为: " + lscs_ckmc);
            }
        }

        private void TextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            OldText1 = textBox2.Text;
        }

        private void Text2_change(object sender, EventArgs e)
        {
            bool flag = IsNumberic(textBox1.Text);
            if (flag)
            {
                DetectionFrequency = int.Parse(textBox1.Text);
                timer1.Interval = 1000 * 60 * int.Parse(textBox1.Text);
            }
            else
            {
                timer1.Stop();
            }
        }

        private void Text2_Leave(object sender, EventArgs e)
        {
            if(OldText2 != textBox1.Text)
            {
                Log("中控检测频率修改为: " + textBox1.Text + "分钟");
            }
        }

        private void TextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            OldText2 = textBox1.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(SBCentralControlIniPath)) 
            {
                if (!timer1.Enabled)
                {
                    startTime = DateTime.Now;
                    sbRestartNum = 0;
                    hsRestartNum = 0;
                    textBox5.Text = sbRestartNum + "/" + hsRestartNum;
                    timer1.Start();
                    timer2.Start();
                    button1.Text = "停止运行";
                    Log("SB中控已启动...");
                    string text = string.Empty;
                    if (File.Exists(BasePath))
                    {
                        text = File.ReadAllText(BasePath);
                    }
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        Process[] processesByName = Process.GetProcessesByName(text.Replace(".exe", ""));
                        if (processesByName.Length <= 0)
                        {
                            //打开SB
                            Process newProcess = new Process
                            {
                                StartInfo = new ProcessStartInfo(LauncherPath)
                            };
                            newProcess.Start();
                            Log("SmartBot启动中...");
                        }
                    }
                }
                else
                {
                    textBox4.Text = "00:00:00";
                    sbRestartNum = 0;
                    hsRestartNum = 0;
                    textBox5.Text = sbRestartNum + "/" + hsRestartNum;
                    timer1.Stop();
                    timer2.Stop();
                    button1.Text = "开始运行";
                    Log("SB中控已停止...");
                }
            }
            else
            {
                Log("SB中控同级目录不存在[SBCentralControl.ini]文件,请检查!");
            }
            
        }

        private void TextBox3_Enter(object sender, EventArgs e)
        {
            HideCaret(textBox3.Handle);
        }

        private void TextBox3_MouseEnter(object sender, EventArgs e)
        {
            HideCaret(textBox3.Handle);
        }

        private void TextBox4_MouseEnter(object sender, EventArgs e)
        {
            HideCaret(textBox4.Handle);
        }

        private void TextBox4_Enter(object sender, EventArgs e)
        {
            HideCaret(textBox4.Handle);
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            textBox4.Text = Sec2Min(startTime);
        }

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool IsNumberic(string message)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 空白日志打印
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="flag"></param>
        private void LogByNoTime(string msg)
        {
            textBox3.Text += "\r\n" + msg;
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();
        }

        /// <summary>
        /// 带时间的日志打印
        /// </summary>
        /// <param name="msg"></param>
        private void Log(string msg)
        {
            textBox3.Text += "\r\n[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg;
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();
        }

        /// <summary>
        /// 修改中控通信配置文件
        /// </summary>
        /// <param name="methodName"></param>
        public void SendSmartBotMsg(string methodName) 
        {
            WritePrivateProfileString("SmartBot", methodName, true.ToString(), SBCentralControlIniPath);
        }


        private string Sec2Min(DateTime timeA)
        {
            DateTime timeB = DateTime.Now;
            TimeSpan ts = timeB - timeA;
            Int64 sec = (Int64)ts.TotalSeconds;

            Int64 shi;
            Int64 fen;
            Int64 miao;
            if (sec < 0)
                sec = 0;
            miao = sec % 60;
            sec -= miao;
            sec /= 60;
            fen = sec % 60;
            sec -= fen;
            shi = sec / 60;
            return string.Format("{0:00}:{1:00}:{2:00}", shi, fen, miao);
        }
    }
}
