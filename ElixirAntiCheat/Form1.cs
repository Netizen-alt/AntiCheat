using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using Microsoft.Win32;
using System;
using System.Threading;

namespace ElixirAntiCheat
{
    public partial class ANITCHEAT : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse);
        public Point mouseLocation;

        public ANITCHEAT()
        {
            InitializeComponent();
            Application.DoEvents();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 5;

            if (panel2.Width >= 640)
            {
                label1.Text = "Success!!!";
                Console.Beep();
                timer1.Stop();
                await Task.Delay(500);

                MessageBox.Show("You are not using Elixir AntiCheat, please download it from our website.", "Elixir AntiCheat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }



        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern uint NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLegth);

        string appPath = Path.GetDirectoryName(Application.ExecutablePath);


        private void ANITCHEAT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
        public static void anti()
        {
            while (true)
            {
                ProcessDetection.UpdateProcessList();
                ProcessDetection.FindProcess();
                Thread.Sleep(3000);
            }
        }




        public static void NONGJJ_PAUSE()
        {
            uint TimeTest1 = 0, TimeTest2 = 0;
            while (true)
            {
                TimeTest1 = TimeTest2;
                TimeTest2 = (uint)Environment.TickCount;
                if (TimeTest1 != 0)
                {
                    Thread.Sleep(100);
                    if ((TimeTest2 - TimeTest1) > 1000)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
        }


        Thread thread1;

        void HProtection()
        {
            thread1 = new Thread(new ThreadStart(NONGJJ_PAUSE));
            thread1.Start();
        }

        void ProcessCheck()
        {
            var mainExe = Process.GetProcessesByName("ElixirAntiCheat").FirstOrDefault();
            string processX = "ElixirAntiCheat";
            if (Process.GetProcessesByName(processX).Length > 1)
            {
                System.Environment.Exit(1);
            }
        }


        public static void SteamCheck()
        {
            int Log_Txt_Hack = 1;
            var process = Process.GetProcessesByName("steam").FirstOrDefault();
            Process[] pname = Process.GetProcessesByName("steam");
            if (pname.Length == 0)
            {
                MessageBox.Show("กรุณาเปิด Steam ก่อนเริ่มเกม", "ELIXIR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Log_Txt_Hack == 1)
                {
                    using (StreamWriter Log_Txt = File.AppendText("ElixirVersion.cache"))
                    {
                        TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");
                        DateTime nowInThailand = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                        string logMessage = string.Format("{0}", "ไม่พบสตรีม");
                        string logDev = $"ElixirAntiCheat {Application.ProductVersion} - Developer By NONGJJ";
                        Log_Txt.WriteLine(logDev + $"\n [{nowInThailand:hh:mm:ss}]" + logMessage);
                    }
                }
                System.Environment.Exit(1);
                return;
            }
        }
        private async void ANITCHEAT_Load(object sender, EventArgs e)
        {


            Thread a = new Thread(anti);
            a.Name = "aNONGJJ";
            a.Start();

            NONGJJBlockInput B = new NONGJJBlockInput();
            SteamCheck();
            HProtection();
            ProcessCheck();
            B.BlockInput(true);

            int Log_Txt_Hack = 1;
            if (Log_Txt_Hack == 1)
            {
                using (StreamWriter Log_Txt = File.AppendText("ElixirVersion.txt"))
                {
                    TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");
                    DateTime nowInThailand = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                    string logMessage = string.Format("{0}", Application.ProductVersion);
                    string logDev = $"ElixirAntiCheat {Application.ProductVersion} - Developer By NONGJJ";
                    Log_Txt.WriteLine(logDev + $"\n[{nowInThailand:hh:mm:ss}]" + logMessage);
                }
            }



            string app = appPath + "\\Launcher\\ConsoleApp1.dll";

            if (!File.Exists(app))
            {
                timer1.Stop();
                MessageBox.Show("ไม่พบไฟล์หลักของเรา ติดต่อทีมผู้พัฒนา", "ELIXIR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Log_Txt_Hack == 1)
                {
                    using (StreamWriter Log_Txt = File.AppendText("ElixirVersion.cache"))
                    {
                        TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");
                        DateTime nowInThailand = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                        string logMessage = string.Format("{0}", "ไม่พบไฟล์บ้างอย่าง");
                        string logDev = $"ElixirAntiCheat {Application.ProductVersion} - Developer By NONGJJ";
                        Log_Txt.WriteLine(logDev + $"\n[{nowInThailand:hh:mm:ss}]" + logMessage);
                    }
                }
                Process[] XXX = Process.GetProcessesByName("Fivem");
                foreach (Process xXXX in XXX)
                    xXXX.Kill();
                System.Environment.Exit(1);
                return;
            }
            else
            {
                Console.WriteLine("TTT");
            }

            timer1.Start();
            label1.Text = "Version : " + Application.ProductVersion;
            label1.Font = new Font(label1.Font, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.BackColor = Color.Transparent;
            label1.Parent = pictureBox1;
            pictureBox1.BackColor = Color.Transparent;

            // text last update
            label2.Text = "Last Update : " + File.GetLastWriteTime(Application.ExecutablePath).ToString("dd/MM/yyyy");
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;
            pictureBox1.BackColor = Color.Transparent;
            await Task.Delay(3000);
            B.BlockInput(false);
        }


        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}