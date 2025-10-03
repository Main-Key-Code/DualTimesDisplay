using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace test01
{
    public partial class mainF : Form
    {
        private Timer clockTimer;

        public mainF()
        {
            InitializeComponent();
        }

        private void mainF_Shown(object sender, EventArgs e)
        {
            decimal screenWidth = Screen.PrimaryScreen.Bounds.Width;

            int setLocation = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            
            this.Location = new Point(setLocation, 0);

#if DEBUG
            Debug.WriteLine($"Screen.PrimaryScreen.Bounds.Width: {Screen.PrimaryScreen.Bounds.Width}");
            Debug.WriteLine($"Screen.PrimaryScreen.Bounds.Height: {Screen.PrimaryScreen.Bounds.Height}");
            
            Debug.WriteLine($"{this.Size}");
#endif

            this.TopMost = true;
           
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;

            clockTimer = new Timer();
            clockTimer.Interval = 50;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }
        
        public void ActivateClock()
        {
            while (true)
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke(new Action(() =>
                    {
                        label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }));
                }
                else
                {
                    label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                System.Threading.Thread.Sleep(50);
            }
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // 한국 시간 (KST, UTC+9)
            var koreaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"));

            // 헝가리 시간 (Central Europe Standard Time)
            var hungaryTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time"));

            label1.Text = $"한  국: {koreaTime: tt hh:mm:ss}";
            label1.ForeColor = Color.Goldenrod;
            label2.Text = $"헝가리: {hungaryTime:tt hh:mm:ss}";
            label2.ForeColor = Color.DarkCyan;
        }

    }
}
