using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace TimingMadShoken
{

    /**
     * Classe chargée de lancer un nouveau thread pour l'upload ftp
     * et de le tuer une fois que le ftp a envoyé les fichier
     */
    class FtpTime
    {
        public class FtpEventArgs : EventArgs
        {
            public string message;

            public FtpEventArgs(string messageS)
            {
                message = messageS;
            }
        }

        public class FtpUploadArgs : EventArgs
        {
            public int pos;
            public int max;

            public FtpUploadArgs(int posS, int maxS)
            {
                pos = posS;
                max = maxS;
            }
        }


        FtpWebRequest request;
        //public EventHandler onMessage;
        //public EventHandler onUpload;
        //public EventHandler onFinish;

        ParoleManager paroleManager;
        String videoFilePath;
        String folderForReceive;
        String name;
        public volatile int max = 0;
        public volatile int min = 0;

        String serveur;
        String id;
        String pass;
        public volatile String message = "";

        public FtpTime(ParoleManager paroleManagerS, String videoFilePathS, String folderForReceiveS)
        {
            paroleManager = paroleManagerS;
            videoFilePath = videoFilePathS;
            folderForReceive = folderForReceiveS;
        }

        public int amountSent()
        {
            return 0;
        }


        private void connect(String serveur, String id, String pass, String path)
        {

            String ftpAdd = "ftp://" + serveur + "/" + name;
            if (path.Length > 0)
                ftpAdd += "/" + Path.GetFileName(path);
            request = (FtpWebRequest)FtpWebRequest.Create(ftpAdd);
            request.Credentials = new NetworkCredential(id, pass);

        }
        private void configMkdir()
        {
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;
        }

        private void configUpload()
        {
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;
        }

        private int getFileSize()
        {
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.UsePassive = false;
            request.UseBinary = true;
            request.KeepAlive = true;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return (int) response.ContentLength; 
        }

        private void configDownload()
        {
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;
        }

        private void configListDir()
        {
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
        }


        private void addMessage(String mes)
        {
            message += mes + (Environment.NewLine);
        }

        private string[] getFileList(String serveur, String id, String pass, String path)
        {
            
            connect(serveur, id, pass, path);
            addMessage("   Recuperation de la liste des fichiers en cours...");
            configListDir();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            String mess = reader.ReadToEnd();
            addMessage(mess);

            string[] res = mess.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return res;
        }

        private void getFile(String serveur, String id, String pass, String path)
        {
            try
            {
            addMessage("   Recuperation du fichier "+path+" en cours...");
            //connect(serveur, id, pass, path);
            //max  = getFileSize();
            connect(serveur, id, pass, path);
            configDownload();
                FileStream outStream = File.OpenWrite(this.folderForReceive + "/" + path);
                int pos = 0;
                int size = 0;
                int bufferSize = 1024;

                addMessage("   Connection en cours...");
                Stream inStream = request.GetResponse().GetResponseStream();
                byte[] buffer = new byte[bufferSize];

                addMessage("   Transfert en cours...");
                
                while ((size = inStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    //size = Math.Min(bufferSize, ((int)inStream.Length) - pos - 1);
                    outStream.Write(buffer, 0, size);
                    //onUpload.Invoke(this, new FtpUploadArgs(pos, (int)inStream.Length));
                    min = pos;
                    
                    pos += size;
                    Application.DoEvents();
                }

                //onUpload.Invoke(this, new FtpUploadArgs(pos, (int)inStream.Length));

                inStream.Close();
                outStream.Close();
                addMessage("   Envoit du fichier terminé");
            }
            catch (Exception e)
            {
                addMessage("   Erreur : " + e.Message);
            }
        }

        private void sendFile(String serveur, String id, String pass, String path)
        {
            try
            {
         
            connect(serveur, id, pass, path);
            configUpload();
            //BufferedStream instream = new BufferedStream(File.OpenRead(path));
            //BufferedStream outstream = new BufferedStream(request.GetRequestStream());

                FileStream inStream = File.OpenRead(path);
                int pos = 0;
                int size = 0;
                int bufferSize = 1024;

                addMessage("   Connection en cours...");
                Stream outStream = request.GetRequestStream();
                byte[] buffer = new byte[bufferSize];

                addMessage("   Transfert en cours...");

                while ((size = inStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    //size = Math.Min(bufferSize, ((int)inStream.Length) - pos - 1);
                    outStream.Write(buffer, 0, size);
                    //onUpload.Invoke(this, new FtpUploadArgs(pos, (int)inStream.Length));
                    min = pos;
                    max = (int)inStream.Length;
                    pos += size;
                    Application.DoEvents();
                }

                //onUpload.Invoke(this, new FtpUploadArgs(pos, (int)inStream.Length));

                inStream.Close();
                outStream.Close();
                addMessage("   Envoit du fichier terminé");
            }
            catch (Exception e)
            {
                addMessage("   Erreur : " + e.Message);
            }
        }

        private void createFolder(String serveur, String id, String pass)
        {
            try
            {
                addMessage("   Création du dossier");
                connect(serveur, id, pass, "");
                configMkdir();
                request.GetResponse();
                request.Abort();
                addMessage("   Dossier créé");
            }
            catch (Exception e)
            {
                addMessage("   Impossible de  créer le dossier (peut être existant)");
            }
        }

        public void init(String nameS, String serveurS, String idS, String passS)
        {
            name = nameS;
            serveur = serveurS;
            id = idS;
            pass = passS;
            message = "";
        }


        public void sendTime()
        {
            //createFolder(serveur, id, pass);
            if (File.Exists(paroleManager.getLyricsFilepath()))
            {
                addMessage("Envoit du fichier parole :");
                sendFile(serveur, id, pass, paroleManager.getLyricsFilepath());
                //TODO : envoyer fichier parole
            }
            if (File.Exists(paroleManager.getFramesFilepath()))
            {
                addMessage("Envoit du fichier frame :");
                //TODO : envoyer fichier frame
                sendFile(serveur, id, pass, paroleManager.getFramesFilepath());
            }
            if (File.Exists(paroleManager.getToyundaFilepath()))
            {
                addMessage("Envoit du fichier toyunda :");
                //TODO : envoyer fichier toyunda
                sendFile(serveur, id, pass, paroleManager.getToyundaFilepath());
            }
            if (File.Exists(videoFilePath))
            {
                addMessage("Envoit du fichier video :");
                //TODO : envoyer fichier video
                sendFile(serveur, id, pass, videoFilePath);
            }
            //onFinish.Invoke(this, new EventArgs());
        }

        public void getime()
        {
            try
            {

                String[] fileList = this.getFileList(serveur, id, pass, "/");

                foreach (String fileName in fileList)
                {
                    String name = fileName;
                    if (name != null && name[0] != '.' && name[0] != '/')
                    {
                        getFile(serveur, id, pass, name);
                    }
                }
            }
            catch (Exception e)
            {
                addMessage("   Impossible de  créer le dossier (peut être existant)");
            }
        }
    }
}
