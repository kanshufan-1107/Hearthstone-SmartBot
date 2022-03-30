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
        /// 防检测程序文件夹
        /// </summary>
        private static readonly string BasePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "Temp" + Path.DirectorySeparatorChar.ToString() + "name.temp";
        /// <summary>
        /// 启动器所在地址
        /// </summary>
        private static readonly string LauncherPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar.ToString() + "Launcher.exe";

        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 确定系统是否认为指定的应用程序没有响应
        /// </summary>
        /// <param name="hWnd">要测试的窗口的句柄</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool IsHungAppWindow(IntPtr hWnd);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //初始化页面信息
            textBox1.Text = DetectionFrequency.ToString();
            textBox2.Text = lscs_ckmc;
            bool flag = IsNumberic(textBox1.Text);
            if (flag)
            {
                timer1.Interval = 1000 * 60 * int.Parse(textBox1.Text);
            }
            else
            {
                timer1.Stop();
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //防止输入框初始焦点
            groupBox1.Focus();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            /*string text = string.Empty;
            if (File.Exists(BasePath))
            {
                text = File.ReadAllText(BasePath);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                Process[] processesByName = Process.GetProcessesByName(text.Replace(".exe", ""));
                foreach (Process process in processesByName)
                {
                    bool processHungFlag = IsHungAppWindow(process.Handle);
                    if (processHungFlag)
                    {
                        //关闭SB
                        process.Kill();
                        //打开SB
                        Process newProcess = new Process
                        {
                            StartInfo = new ProcessStartInfo(LauncherPath)
                        };
                        newProcess.Start();
                    }
                }
            }*/
            /*//获取炉石传说句柄
            IntPtr lscs = FindWindow(null, lscs_ckmc);
            bool hungFlag = IsHungAppWindow(lscs);
            if (hungFlag)
            {
                //关闭炉石传说
                System.Threading.Thread.Sleep(5000);
                //开启炉石传说
            }*/
        }

        private void Text1_change(object sender, EventArgs e)
        {
            lscs_ckmc = textBox2.Text;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            if (File.Exists(BasePath))
            {
                text = File.ReadAllText(BasePath);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                Process[] processesByName = Process.GetProcessesByName(text.Replace(".exe", ""));
                foreach (Process process in processesByName)
                {
                    //关闭SB
                    process.Kill();
                    System.Threading.Thread.Sleep(2000);
                    //打开SB
                    Process newProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo(LauncherPath)
                    };
                    newProcess.Start();
                }
            }
        }

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
    }
}
