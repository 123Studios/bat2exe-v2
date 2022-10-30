using System;
using System.IO;
using System.Diagnostics;
using System.Net;
namespace bat2exeV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start(args[0]);
        }

        static void Start(string path)
        {
            try
            {
                if (path == String.Empty)
                {
                    put("[-] Arguments are empty!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    put("[[[Startup Process]]]");
                    WebClient wc = new WebClient();
                    string url = "https://raw.githubusercontent.com/EscaLag/Abobus-obfuscator/main/AbobusObf.py";
                    put("[+] Got new version of Abobus by EscaLag (1/5)");
                    path = path.Replace(@"\", "/");
                    string editAbobus = wc.DownloadString(url);
                   // editAbobus = editAbobus.Replace("status_china_symbol        = False", "status_china_symbol        = True");
                    editAbobus = editAbobus.Replace("    file = input('Drag your Batch file: ')", "    file = '" + path + "'");
                    editAbobus = editAbobus.Replace("    name = file[:-4]+'-obf'", "    name = 'output'");


                    put("[+] Set 'Chinese' char obfuscation true and file (2/5)");

                    File.WriteAllText(Environment.CurrentDirectory + "/abobus.py", editAbobus);

                    put("[+] Wrote Abobus.py (3/5)");
                    try
                    {
                        ProcessStartInfo start = new ProcessStartInfo(); //credit: https://stackoverflow.com/questions/22694028/wpf-check-if-python-installed-on-system
                        start.FileName = @"cmd.exe";
                        start.Arguments = "python --version";
                        start.UseShellExecute = false;
                        start.RedirectStandardError = true;
                        start.CreateNoWindow = true;
                        Process.Start(start);


                    }
                    catch
                    {
                        put("[-] Python not installed or error happen!\nPress any key to exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }



                    put("[+] Python was found (4/5)");

                    if (path.Contains(".bat")) put("[+] Input file is bat file (5/5)");
                    else
                    {
                        put("[-] Not batch file");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }



                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //Obfuscation process

                    put("[[[Obfuscation Process]]]");
                        ProcessStartInfo start1 = new ProcessStartInfo(); 
                        start1.FileName = @"python.exe";
                        start1.Arguments = Environment.CurrentDirectory + "/abobus.py";
                        start1.UseShellExecute = false;
                        start1.RedirectStandardError = true;
                        start1.CreateNoWindow = true;
                        Process.Start(start1);

                    Console.ForegroundColor = ConsoleColor.Magenta;

                    put("[+] Applied obfuscator (1/1)");
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    //Obfuscation process
                    put("[[[Compile Process]]]");
                    System.Threading.Thread.Sleep(2000);
                    string data2 = File.ReadAllText(Environment.CurrentDirectory + "/output.bat");

                    var our = Base64Encode("cls\n" + data2 + "\nDel %~0");

                    var s1 = "@echo off\nCERTUTIL -f -decode \"%~f0\" \"%Temp%/test.bat\" >nul 2>&1 \ncls\n\"%Temp%/test.bat\"\nExit\n-----BEGIN CERTIFICATE-----\n" + our.ToString() + "\n-----END CERTIFICATE-----";
                    File.WriteAllText(Environment.CurrentDirectory + "/b64.bat", s1);
                    put("Finished obfuscation. Compile process come next update!");
                    Console.ReadKey();


                }
            }
            catch(Exception ex)
            {
                put("[-] Error Occur: " + ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        public static void put(string t) { Console.WriteLine(t); }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

}

