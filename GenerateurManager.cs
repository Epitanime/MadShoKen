using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

namespace TimingMadShoken
{
    
    public class Generateur 
    {
        String name;
        String beforeArgs;
        String path;
        volatile Process p;
        int childNum = 0;
        Boolean runProc;
        Boolean runError;
        

        String toyundaPath = "";

        public Generateur(String pathValue, String beforeArgsValue, String nameValue)
        {
            this.path = pathValue;
            this.name = nameValue;
            this.beforeArgs = beforeArgsValue;
            
            p = null;
        }
       
        public void runProcess()
        {

            string data;


            StreamWriter write = new StreamWriter(toyundaPath, false, System.Text.Encoding.Default);
            
            p.StandardOutput.BaseStream.Flush();
            
                while ((data = p.StandardOutput.ReadLine()) != null)
                {
                    //data = data.Replace('ÿ', '\x408');
                    //String encoded = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(data));



                    write.WriteLine(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(data)));
                    
                    //this.consoleErreur.AppendText(data);
                    //UpdateTextBox(textBoxMessage, data); // this is a call to an
                }
                write.Close();
        }

        public void runProcessExit()
        {

            string data;
            
            StreamWriter write = new StreamWriter(toyundaPath + ".log");

            p.StandardError.BaseStream.Flush();
                while ((data = p.StandardError.ReadLine()) != null)
                {
                    write.Write(data + Environment.NewLine);
                    //UpdateTextBox(textBoxMessage, data); // this is a call to an
                }
            write.Close();
            
        }

        public void ExecuteSeperated(String arguments, String toyundaPath)//RichTextBox consoleValue, RichTextBox consoleErreurValue)
        {
            this.toyundaPath = toyundaPath;
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = this.path;
            info.Arguments = arguments;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = false;
            info.RedirectStandardInput = false;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;


            /*string output = string.Empty;
            string  errorOutput = string.Empty;*/
            try
            {
                Thread processThread2 = new Thread(new ThreadStart(runProcessExit));
                p = new Process();
                p.StartInfo = info;
                p.EnableRaisingEvents = true;
                p.Start();
                
                processThread2.Start();
                while (!(processThread2.IsAlive)) ;
                while ((processThread2.IsAlive)) ;
                p.WaitForExit(2000);
                p.WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Execute(String arguments, String toyundaPath)//RichTextBox consoleValue, RichTextBox consoleErreurValue)
        {
            this.toyundaPath = toyundaPath;
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = this.path;
            info.Arguments =this.beforeArgs + arguments;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardInput = false;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;


            /*string output = string.Empty;
            string  errorOutput = string.Empty;*/
            try
            {
                Thread processThread = new Thread(new ThreadStart(runProcess));
                Thread processThread2 = new Thread(new ThreadStart(runProcessExit));
                p = new Process();
                p.StartInfo = info;
                p.EnableRaisingEvents = true;
                p.Start();
                    
                
                    processThread.Start();
                    processThread2.Start();
                    while (!(processThread.IsAlive && processThread2.IsAlive)) ;
                    p.WaitForExit(2000);
                    p.WaitForExit();
                    while ((processThread.IsAlive || processThread2.IsAlive)) ;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public string getName()
        {
            return this.name;
        }

        public String getDisplay()
        {
            return this.name;
        }
    }
    
    class GenerateurManager
    {
        Boolean displayInfo;
        List<Generateur> genList;
RichTextBox console;
        RichTextBox consoleErreur;

        public String findBinPath(String name)
        {
            String bouh = "bouh"; //lachieuse touch
            String result = "";
            String[] pathList = Environment.GetEnvironmentVariable("path").Split(new char[] { ';' });
            result = "";

            foreach (String path in pathList)
            {
                if (File.Exists(path.Trim() + "\\" + name))
                    result = path.Trim() + "\\" + name;
            }
            return result;
        }

        public GenerateurManager(RichTextBox consoleValue, RichTextBox consoleErreurValue)
        {
            genList = new List<Generateur>();
            this.console = consoleValue;
            this.consoleErreur = consoleErreurValue;
            this.displayInfo = false;
            //generateur ruby de rubix
            genList.Add(new Generateur(
                findBinPath("ruby.exe")
                , " \"" + Environment.GetEnvironmentVariable("TOYUNDA_TOOLS") + "\\generator\\toyunda-gen.rb" + "\" "
                //System.Configuration.ConfigurationSettings.AppSettings["Ruby"] 
                , "Ruby (from rubix)"));
        }

        private void storeGeneratedFile(String generation, String toyundaFilePath)
        {
            StreamWriter streamWriter = new StreamWriter(toyundaFilePath);

                streamWriter.Write(generation);
                   
                // Fermeture du StreamReader (attention très important) 
                streamWriter.Flush();
                streamWriter.Close();
            
        }

        public void launchGeneration(int i, String lyricFilePath, String frameFilePath, String toyundaFilePath)
        {
            console.Clear();
            consoleErreur.Clear();
            /*if (formatForDos(Application.ExecutablePath).IndexOf(" ") > 0)
            {
                MessageBox.Show("L'executable contient un espace non géré dans le chemin d'accès, l'execution des génerateurs risque de ne pas fonctionner. Veuillez remplacer les espaces par des '_'.", "Attention : erreur de chemin");
            }*/
            if (i >= 0 && i < genList.Count)
            {
                Generateur gen = genList[i];
                String beforeArg = "";
                String afterArg = "";
               
                //gen.Execute(beforeArg + " \"" + lyricFilePath + "\"  \"" + frameFilePath + "\" " + afterArg + " ", this.console, this.consoleErreur);

                gen.Execute(" \"" + lyricFilePath + "\"  \"" + frameFilePath + "\" " + afterArg + " ", toyundaFilePath);

                if (!displayConsoles(toyundaFilePath))
                {
                    gen.ExecuteSeperated(beforeArg + " \"" + lyricFilePath + "\"  \"" + frameFilePath + "\" " + afterArg + " ", toyundaFilePath);
                    displayConsoles(toyundaFilePath);
                }
                
                
                //afterArg = " > \"D:\\Documents and Settings\\dumestier\\Mes documents\\EPITANIME\\Timing\\ding_dong_song\\tmp.txt\"";
                //gen.Execute(beforeArg + " \""
                //  + "D:\\Documents and Settings\\dumestier\\Mes documents\\EPITANIME\\Timing\\ding_dong_song\\Gunther & The Sunshine Girls - Ding Dong Song.txt"
                //+ "\"  \"" + "D:\\Documents and Settings\\dumestier\\Mes documents\\EPITANIME\\Timing\\ding_dong_song\\Gunther & The Sunshine Girls - Ding Dong Song.frm"
                //+ "\" " + afterArg + " ",
                //"D:\\Documents and Settings\\dumestier\\Mes documents\\EPITANIME\\Timing\\ding_dong_song\\test.toy");
                //storeGeneratedFile(console.Text, toyundaFilePath);
                
            }
            
        }

        private Boolean displayConsoles(String toyundaFilePath)
        {
            StreamReader reader = null;

            Boolean result = false;
                reader = new StreamReader(toyundaFilePath);
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    console.AppendText(line + Environment.NewLine);
                    result = true;
                }
                reader.Close();
                reader = new StreamReader(toyundaFilePath + ".log");

                while ((line = reader.ReadLine()) != null)
                {
                    bool display = true;
                    
                        consoleErreur.SelectionColor = Color.Black;
                        if (line.Length >= 5 && line.Substring(0, 5).CompareTo("Info:") == 0)
                        {
                            if (!this.displayInfo)
                            {
                                display = false;
                            }
                            else
                            {
                                consoleErreur.SelectionColor = Color.Blue;
                                consoleErreur.AppendText(line.Substring(0, 5));
                                consoleErreur.SelectionColor = Color.Black;
                                consoleErreur.AppendText(line.Substring(5, line.Length - 5) + Environment.NewLine);
                            }
                        }
                        else if (line.Length >= 8 && line.Substring(0, 8).CompareTo("Warning:") == 0)
                        {
                            consoleErreur.SelectionColor = Color.Red;
                            consoleErreur.AppendText(line.Substring(0, 8));
                            consoleErreur.SelectionColor = Color.Black;
                            consoleErreur.AppendText(line.Substring(8, line.Length - 8) + Environment.NewLine);
                        }
                        else
                        {
                            consoleErreur.SelectionColor = Color.Black;
                        }
                        consoleErreur.SelectionColor = Color.Green;
                        
                    result = true;
                }
                consoleErreur.AppendText("Géneration terminée" + Environment.NewLine);
                reader.Close();
                return result;
        }

        public List<Generateur>  getGeneratorList()
        {
            return genList;
        }

        internal void setDisplayInfo(bool p)
        {
            this.displayInfo = p;
        }
    }
}
