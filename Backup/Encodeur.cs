using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Collections;

namespace TimingMadShoken
{
    public class Encodeur
    {


       public static void execute(String executablePath, String arguments)
        {
            Process p;
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = executablePath;
            info.Arguments = arguments;
            info.UseShellExecute = true;
            info.RedirectStandardOutput = false;
            info.RedirectStandardInput = false;
            info.RedirectStandardError = false;
            info.CreateNoWindow = true;

            try
            {
                p = new Process();
                p.StartInfo = info;
                p.EnableRaisingEvents = true;
                p.Start();

                p.WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
      
        internal static string encode(string videoPath)
        {
            String mediaCoderPath = "";
            String mediaCoderSetting = "";
            String returnStr = "";
            if (!File.Exists(videoPath))
            {
                returnStr = "Impossible de trouver la vidéo à l'adresse " + videoPath + "\n";
            }
            if (File.Exists(Environment.GetEnvironmentVariable("ProgramFiles") + "\\MediaCoder\\mediacoder.exe"))
            {
                mediaCoderPath = Environment.GetEnvironmentVariable("ProgramFiles") + "\\MediaCoder\\mediacoder.exe";
            }
            else if   (File.Exists(System.Configuration.ConfigurationSettings.AppSettings["MediaCoder"]))
            {
                mediaCoderPath = System.Configuration.ConfigurationSettings.AppSettings["MediaCoder"];
            }
            else
            {
                returnStr += "Impossible de trouver media coder\n";
            }
            if (File.Exists(Application.StartupPath + "\\timingMediaCoderConf.xml"))
            {
                mediaCoderSetting = Application.StartupPath + "\\timingMediaCoderConf.xml";
            }
            else
            {
                returnStr += "Impossible de trouver la configuration media coder à l'adresse " + Application.StartupPath + "\\timingMediaCoderConf.xml" + "\n";
            }
            if (returnStr.Length == 0)
            {   
                execute("\"" + mediaCoderPath + "\"",
                    "-start -exit -preset \"" + mediaCoderSetting + "\" "
                    + " -ui 0 " + "\"" + videoPath + "\"");
                if (! File.Exists(videoPath.Replace(".avi", "") + "_transcoded.avi"))
                {
                    returnStr += "Impossible de trouver la vidéo réencodée à l'adresse " + videoPath + "_transcoded" + "\n";
                }
            }
            
            return returnStr;
        }
    }
}
