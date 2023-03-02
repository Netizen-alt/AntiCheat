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
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

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
       

        public static async void GetProfile() {

            

        }


        static ulong getSteamID64(uint accountID)
        {
            return accountID + 76561197960265728UL;
        }

        static string dec2hex(string dec)
        {
            ulong value = ulong.Parse(dec);
            return value.ToString("X");
        }

        const uint REG_DWORD = 4;
        const int ERROR_SUCCESS = 0;
        const int KEY_READ = 0x20019;

        [System.Runtime.InteropServices.DllImport("advapi32.dll", EntryPoint = "RegOpenKeyEx")]
        public static extern int RegOpenKeyEx(
            RegistryHive hKey,
            string lpSubKey,
            int ulOptions,
            int samDesired,
            out IntPtr phkResult
            );

        [System.Runtime.InteropServices.DllImport("advapi32.dll", EntryPoint = "RegQueryValueEx")]
        public static extern int RegQueryValueEx(
            IntPtr hKey,
            string lpValueName,
            int lpReserved,
            ref uint lpType,
            out uint lpData,
            ref uint lpcbData
            );







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

                            int lReg;
                            IntPtr hKey;

                            lReg = RegOpenKeyEx(RegistryHive.CurrentUser, "Software\\Valve\\Steam\\ActiveProcess", 0, KEY_READ, out hKey);

                            if (lReg != 0)
                            {
                                Console.WriteLine("Please load the Steam client and complete the login.");
                                Console.ReadKey();
                                Environment.Exit(0);
                            }

                            if (lReg == ERROR_SUCCESS)
                            {
                                uint dwType = REG_DWORD;
                                uint dwSize = sizeof(uint);
                                uint dwValue = 0;

                                lReg = RegQueryValueEx(hKey,
                                                       "ActiveUser",
                                                       0,
                                                       ref dwType,
                                                       out dwValue,
                                                       ref dwSize);
                                uint accountID = dwValue;
                                ulong steamID64 = getSteamID64(accountID);
                                Console.WriteLine("Account ID: " + accountID);
                                Console.WriteLine("SteamID64: " + steamID64);

                                Console.WriteLine(dec2hex(steamID64.ToString()));

                                var steamapi = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=4A6A1730731B87CCB3E9AE467FBA4B68&steamids={steamID64}";

                                WebClient wcX = new WebClient();
                                string res = wcX.DownloadString(steamapi);
                                JObject jsonx = JObject.Parse(res);

                                string playerName = (string)jsonx["response"]["players"][0]["personaname"];
                                string steamId = (string)jsonx["response"]["players"][0]["steamid"];
                                string avatar = (string)jsonx["response"]["players"][0]["avatarfull"];
                                string profileURl = (string)jsonx["response"]["players"][0]["profileurl"];

                                if (lReg == ERROR_SUCCESS)
                                {
                                    Console.WriteLine(lReg);
                                    if (dwValue == 0)
                                    {
                                        Console.WriteLine("Please login to Steam before entering the game.");
                                        Console.ReadKey();
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You are logged in to Steam.");



                                        string webhookUrl = "https://ptb.discord.com/api/webhooks/1080118189339463802/eVXIpU5IlivMrT9HoPLMnT-kRBjJdTS1ZjVWR8VZXlP5q9e7yCd9UWvXjMbyWdlj0iJJ";
                                        WebClient wc = new WebClient();
                                        string ip = wc.DownloadString("https://ipapi.co/ip");
                                        var embed = new
                                        {
                                            title = "ELIXIR PROTECT",
                                            author = new
                                            {
                                                name = $"{playerName}",
                                                icon_url = $"{avatar}",
                                                url= $"{profileURl}"
                                            },
                                            description = $"ข้อมูลผู้ใช้: {playerName}\nสตรีมไอดี: {steamId}\nโปรแกรมที่เปิด: {Processname[i]}\nIP: {ip}",
                                            color = 0xFF5733,
                                            footer = new
                                            {
                                                text = "ElixirProtect By: HELLO JJ#2631"
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
                                    }
                                }
                            }
                            


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
                            return;
                            //System.Environment.Exit(1);
                        }
                    }
                }
            }
        }

    }
}
