using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fa镜头视野计算
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            //item 填入相机靶面范围
            for (int i = 0; i < TargetSurfaceRange.Length; i++)
            {
                this.comboBoxCameraTargetSurface.Items.Add(TargetSurfaceRange[i]);
            }

            //打开默认显示选项
            this.comboBoxCameraTargetSurface.SelectedIndex = 0;
            this.comboBoxFaLens.SelectedIndex = 0;
        }

        /// <summary>
        /// 相机靶面范围
        /// </summary>
        string[] TargetSurfaceRange = new string[] {
            #region 范围
            "1/6(2.3*1.73)",
             "1/4-640*480(3.1*2.3)",
             "1/4(3.2*2.4)",
             "1/4(3.6*2.7)",
             "1/3.6(4*3)",
             "1/3.2(4.536*3.416)",
             "1/3-1280*960(4.8*3.6)",
             "1/2.9-720*540(5.0*3.7)",
             "1/2.9-1440*1080(5.0*3.7)",
             "1/2.7-1280*1024(5.1*4.1)",
             "1/2.7(5.27*3.96)",
             "1/2.7(5.371*4.035)",
             "1/2.5-2592*1944(5.76*4.29)",
             "1/2.3-3840*2748(6.4*4.6)",
             "1/2.3(6.16*4.62)",
             "1/2-1280*1024(6.1*4.9)",
             "1/2(6.4*4.8)",
             "1/1.8-2048*2536(7.176*5.319)",
             "1/1.7-4024*3036(7.4*5.6)",
             "1/1.7(7.6*5.7)",
             "1/1.6(8.08*6.01)",
             "1/1.2-1920*1200(11.3*7)",
             "2/3(8.8*6.6)",
             "2/3-1920*1200(9.2*5.8)",
             "1''-2048*2048(11.3*11.3)",
             "1''-2592*2048(12.4*9.8)",
             "1''-1280*1024(12.8*9.6)",
             "1''-5472*3684(13.1*8.8)",
             "1''-4096*2160(14.1*7.5)",
             "1.1''(12.8*12.8)",
             "1.1''-4096*3000(14.1*10.4)",
             "4/3(17.307*12.98)",
             "4/3(17.824*13.368)",
             "4/3-3296*2472(18*13.5)",
             "FoveonX3(20.7*13.8)",
             "1.8''(21.5*14.4)",
             "1.8''(22.2*14.8)",
             "1.8''(22.5*15)",
             "1.8''(22.7*15.1)",
             "1.8''(23.6*15.5)",
             "1.8''(23.7*15.8)",
             "1.8''(25.1*16.7)",
             "2''(23*23)",
             "Canon's APS-H(27*17.8)",
             "Canon's APS-H(28.7*19.1)",
             "35mm film(36*24)",
             "Leica S2(45*30)",
             "Kodak KAF 3900(50.7*39)"
	#endregion
        };

        /// <summary>
        /// 点击计算按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            calculation();
        }

        /// <summary>
        /// 主要计算方法 逻辑
        /// </summary>
        private void calculation()
        {
            try
            {
                #region 非空验证
                if (this.comboBoxCameraTargetSurface.Text.Trim().Length == 0 || this.comboBoxFaLens.Text.Trim().Length == 0 || this.TBDistance.Text.Trim().Length == 0)
                {
                    MessageBox.Show("注意某个参数为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                double num = 0.0;
                double.TryParse(this.TBDistance.Text, out num);
                bool flag = num < 1.0;

                #region 主要计算逻辑
                if (flag)
                {
                    this.TBDistance.Text = "物距参数不合法！";
                    return;
                }
                else
                {
                    int num2 = this.comboBoxCameraTargetSurface.Text.IndexOf('(') + 1;
                    int num3 = this.comboBoxCameraTargetSurface.Text.IndexOf(')');
                    string text = this.comboBoxCameraTargetSurface.Text.Substring(num2, num3 - num2);
                    string[] array = text.Split(new char[] { '*' });
                    double num4 = Convert.ToDouble(array[0]);
                    double num5 = Convert.ToDouble(array[1]);
                    double num6 = Convert.ToDouble(this.comboBoxFaLens.Text);
                    double num7 = num * num4 / num6;
                    double num8 = num * num5 / num6;
                    this.TBField.Text = num7.ToString("0.0") + "*" + num8.ToString("0.0");
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }

        }

        /// <summary>
        /// 计时器  定时清除 物距框内的 错误提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 5000;
            this.TBDistance.Text = "";
        }

        /// <summary>
        /// 超链接跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://e-yuansu.com/207/");
        }
    }
}
