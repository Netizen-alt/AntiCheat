using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace ElixirAntiCheat
{
    public class ProcessDetection
    {
        
        public static bool detected;

        public static List<string> currentlyRuningProcess = new List<string>();

        public static string[] Processname = { "cheatengine-x86_64", "cheatengine-i386", "cheatengine-x86_64-SSE4-AVX2", "cheat" };

        public static string ignore = "ElixirAntiCheat"; // we dont detect anticheat 

       
        public static void UpdateProcessList()
        {
            Process[] running = Process.GetProcesses();

            foreach (Process process in running) {
                currentlyRuningProcess.Add(process.ProcessName);
            }
        }

        static HttpClient httpClient = new HttpClient();
       
        public static async void FindProcess()
        {
            foreach (string process in currentlyRuningProcess)
            {

                if (process != ignore)
                {
                    for (int i = 0; i < Processname.Length; i++)
                    {
                        if (process.Contains(Processname[i]))
                        {                        
                            detected = true;
                           
                            string webhookUrl = "https://ptb.discord.com/api/webhooks/1080118189339463802/eVXIpU5IlivMrT9HoPLMnT-kRBjJdTS1ZjVWR8VZXlP5q9e7yCd9UWvXjMbyWdlj0iJJ";
                            WebClient wc = new WebClient();
                            string ip = wc.DownloadString("https://ipapi.co/ip");
                            var embed = new
                            {
                                title = "ELIXIR PROTECT",
                                description = "ข้อมูลผู้ใช้\nIP:" + ip,
                                color = 0xFF5733,
                                thumbnail = new
                                {
                                    url = "https://www.example.com/image.png"
                                },
                                footet = new
                                {
                                text= "TEST",
		                        icon_url= "https://i.imgur.com/AfFp7pu.png",
	                        },
                                timestamp = DateTime.UtcNow.ToString("o")
                            };

                            var payload = new
                            {
                                embeds = new[] { embed }
                            };
                            await Task.Delay(5000);
                            var json = JsonConvert.SerializeObject(payload);

                            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                            var response = await httpClient.PostAsync(webhookUrl, content);

                            Console.WriteLine(response.StatusCode);
                            
                            int Log_Txt_Hack = 1;
                            if (Log_Txt_Hack == 1)
                            {
                                using (StreamWriter Log_Txt = File.AppendText("ElixirBuild.elxr"))
                                {
                                    Log_Txt.WriteLine("ตรวจพบการทำงานของแอพที่ไม่ถูกต้อง......");
                                }
                            }
                            Console.Beep(199, 100);
                            Console.Beep(299, 100);
                            Console.Beep(399, 100);
                            Console.Beep(599, 100);
                            Console.Beep(799, 100);
                            Console.Beep(899, 100);
                            Console.Beep(999, 100);
                            Console.Beep();
                            Console.Beep();
                            System.Environment.Exit(1);
                        }
                    }
                }
            }
        }

    }
}
