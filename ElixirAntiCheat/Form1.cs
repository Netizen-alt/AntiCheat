using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using Microsoft.Win32;
using System;

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
            panel2.Width +=5;
           
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

        string appPath = Path.GetDirectoryName(Application.ExecutablePath);

        private async void ANITCHEAT_Load(object sender, EventArgs e)
        {

            NONGJJBlockInput B = new NONGJJBlockInput();



            var process = Process.GetProcessesByName("steam").FirstOrDefault();
            if (process == null)
            {
                MessageBox.Show("Your stream could not be found, please log in.", "ELIXIRTEAM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            int Log_Txt_Hack = 1;
            if (Log_Txt_Hack == 1)
            {
                using (StreamWriter Log_Txt = File.AppendText("ElixirVersion.txt"))
                {
                    Log_Txt.WriteLine(Application.ProductVersion);
                }
            }
            Console.WriteLine(appPath);

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
            int timeTest1 = 0;
            int timeTest2 = 0;

            B.BlockInput(true);
            await Task.Delay(9000);
            B.BlockInput(false);
            while (true)
            {
                ProcessDetection.UpdateProcessList();
                ProcessDetection.FindProcess();
                await Task.Delay(500);
                timeTest1 = timeTest2;
                timeTest2 = Environment.TickCount;
                if (timeTest1 != 0)
                {
                    if ((timeTest2 - timeTest1) > 1000)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }



            //for (int i = 0; i > -1; i++)
            //{
            //    {
            //        foreach (Process clsProcess in Process.GetProcesses())
            //            if (clsProcess.ProcessName.StartsWith("cheat"))//get proces start with cheat
            //            {
            //                Process[] XXX = Process.GetProcessesByName("Fivem");
            //                foreach (Process xXXX in XXX)
            //                    xXXX.Kill();
            //                clsProcess.Kill();//kills proces that starts with cheat
            //                System.Environment.Exit(1);
            //                break;
            //            }
            //    }
            //}
        }


        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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