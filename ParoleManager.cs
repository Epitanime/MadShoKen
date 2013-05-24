using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace TimingMadShoken
{
    public class ParoleManager
    {
        String lyricsFilepath;
        String frameFilepath;
        String toyundaFilepath;

        List<LyricLine> lyricList;
        Int32 currentLine;
        //Int32 currentSyl;
        //RichTextBox frameDisplayer;
        //RichTextBox lyricDisplayer;
        //TextBox frameFileText;
        //TextBox lyricFileText;
        //TextBox infoLineText;
        //TextBox infoSylText;
        //TextBox toyundaFileText;
        //Graphics frameGraphic;
        Boolean _frameSaved;
        //private Panel _prevColorPanel;
        //private Panel _duringColorPanel;
        //private Panel _afterColorPanel;
        public EventHandler onCurrentLineChangedEvent;
        public EventHandler onCurrentLineActiveChangedEvent;
        public EventHandler onFrameChangedEvent;
        public EventHandler onFrameFileChangedEvent;
        public EventHandler onLyricFileChangedEvent;
        public EventHandler onToyundaFileChangedEvent;
        public EventHandler onColorChangedEvent;

        private Color[] colorArray;


        public class LyricSyl
        {
           
            String text;
            long beginFrame;
            long endFrame;


            public LyricSyl(String textValue)
            {
                this.text = textValue;
                this.beginFrame = 0;
                this.endFrame = 0;
            }

            public String getText()
            {
                return this.text;
            }

            public String getDisplay()
            {
                return this.text;
            }

            public long getBeginFrame()
            {
                return this.beginFrame;
            }

            public long getEndFrame()
            {
                return this.endFrame;
            }

            public Boolean setBeginFrame(long position)
            {
                /*if (position > this.endFrame && this.endFrame > -1)
                    return false;
                else
                {*/
                this.beginFrame = position;
                return true;
                //}
            }
            public Boolean setEndFrame(long position)
            {
                if (position < this.beginFrame && this.beginFrame > 0)
                    return false;
                else
                {
                    this.endFrame = position;
                    return true;
                }
            }

            internal void save(StreamWriter streamWriter)
            {
                //Paroles

                //
                if (this.getBeginFrame() >= 0 && this.getEndFrame() >= 0)
                {
                    streamWriter.WriteLine(this.getBeginFrame() + " " + this.getEndFrame());
                }
            }

            internal string getProp()
            {
                return this.getBeginFrame() + " "
                        + this.getEndFrame() + " "
                        + this.getText(); ;
            }

            internal void setText(string p)
            {
                this.text = p;
            }

            internal void clearFrame()
            {
                this.beginFrame = 0;
                this.endFrame = 0;
            }
        }

        public class LyricLine
        {
            List<LyricSyl> sylList;
            int currentSyl;

            public LyricLine()
            {
                sylList = new List<LyricSyl>();
                currentSyl = 0;
            }

            public List<LyricSyl> getSylList()
            {
                return this.sylList;
            }

            internal void Add(LyricSyl syl)
            {
                this.sylList.Add(syl);
            }

            internal void displayText(System.Windows.Forms.RichTextBox parolesText)
            {
                foreach (LyricSyl syl in this.sylList)
                {
                    String syltext = syl.getDisplay();
                    parolesText.Text += syltext;
                }
            }

            internal void displayColor(System.Windows.Forms.RichTextBox parolesText, int posBegin, Boolean displayCurrent)
            {
                Color color = Color.Blue;
                for (int i = 0; i < sylList.Count; ++i)
                {
                    LyricSyl syl = sylList[i];
                    String syltext = syl.getDisplay();
                    parolesText.SelectionStart = posBegin;
                    parolesText.SelectionLength = syltext.Length;
                    if (i == currentSyl && displayCurrent)
                    {
                        parolesText.SelectionColor = Color.Green;
                        parolesText.SelectionFont = new Font(FontFamily.GenericSerif, 26, FontStyle.Bold);
                    }
                    else
                    {
                        parolesText.SelectionColor = (i < currentSyl ? Color.Black : color);
                        parolesText.SelectionFont = new Font(FontFamily.GenericSerif, 20, FontStyle.Regular);
                    }
                    color = (color == Color.Blue ? Color.Red : Color.Blue);
                    posBegin += syltext.Length;
                }
            }


            internal bool nextSyl()
            {
                if (this.currentSyl < this.sylList.Count - 1)
                {
                    this.currentSyl++;
                    while (this.currentSyl < this.sylList.Count &&
                        this.sylList[currentSyl].getText().Trim().Length == 0)
                    {
                        this.currentSyl++;
                    }
                    if (this.currentSyl < this.sylList.Count)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }

            internal Boolean setCurrentEndFrame(long position)
            {
                if (this.currentSyl < this.sylList.Count)
                {
                    return this.sylList[currentSyl].setEndFrame(position);
                }
                else
                    return false;
            }

            internal Boolean setCurrentBeginFrame(long position)
            {
                if (this.currentSyl < this.sylList.Count)
                {
                    return this.sylList[currentSyl].setBeginFrame(position);
                }
                else
                    return false;
            }

            internal string getCurrentSylFrameProp()
            {
                if (this.currentSyl < this.sylList.Count)
                {
                    return this.sylList[currentSyl].getProp();
                }
                else
                    return "";
            }

            internal Int32 getNumberSyl()
            {
                Int32 result = 0;
                foreach (LyricSyl syl in sylList)
                {
                    result += 1;
                }
                return result;
            }

            internal void save(StreamWriter streamWriter)
            {
                foreach (LyricSyl syl in sylList)
                {
                    syl.save(streamWriter);
                }
            }
            /*
            internal void displayAllFrames(RichTextBox frameListRichTextBox)
            {
                foreach (LyricSyl syl in sylList)
                {
                    frameListRichTextBox.AppendText((syl.getProp() + Environment.NewLine));
                }
            }*/

            internal void addSylToList(ref List<LyricSyl> sylListTarget)
            {
                foreach (LyricSyl syl in sylList)
                {
                    sylListTarget.Add(syl);
                }
            }

            internal void clearFrameList()
            {
                foreach (LyricSyl syl in sylList)
                {
                    syl.clearFrame();
                    this.currentSyl = 0;
                }
            }

            internal void displayCurrentSyl(TextBox infoSylText)
            {
                infoSylText.Text = (this.currentSyl + 1).ToString();
            }

            internal void setCurrentSyl(int p)
            {
                if (currentSyl < this.sylList.Count)
                    this.currentSyl = p;
                else
                    this.currentSyl = 0;
            }

            internal bool previousSyl()
            {
                if (this.currentSyl > 0)
                {
                    currentSyl--;
                    return true;
                }
                else
                    return false;
            }

            internal void goLastSyl()
            {
                this.currentSyl = this.sylList.Count - 1;
            }

            internal void displayAllFrames(int posline, RichTextBox frameDisplayer)
            {
                int posSyl = 0;
                foreach (LyricSyl syl in this.sylList)
                {
                    //String text =  + " " +  + " " + ;
                    frameDisplayer.SelectionFont = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
                    frameDisplayer.SelectionColor = Color.Red;
                    frameDisplayer.AppendText((posline + 1).ToString());
                    frameDisplayer.AppendText(" ");
                    frameDisplayer.SelectionColor = Color.Blue;
                    frameDisplayer.AppendText((posSyl + 1).ToString());
                    frameDisplayer.AppendText(" ");
                    frameDisplayer.SelectionColor = Color.Green;
                    frameDisplayer.AppendText(syl.getBeginFrame().ToString());
                    frameDisplayer.AppendText(" ");
                    frameDisplayer.SelectionColor = Color.Purple;
                    frameDisplayer.AppendText(syl.getEndFrame().ToString());
                    frameDisplayer.AppendText(" ");
                    frameDisplayer.SelectionColor = Color.Black;
                    frameDisplayer.AppendText(syl.getText());
                    frameDisplayer.AppendText(Environment.NewLine);
                    posSyl++;
                }
            }

            internal string getCurrentSylNumDisplay()
            {
                return (this.currentSyl + 1).ToString();
            }

            internal long getCurrentBeginFrame()
            {
                return this.sylList[currentSyl].getBeginFrame();
            }
            /*
            internal void displayColor(int posline, RichTextBox frameDisplayer)
            {
                int posSyl = 0;
                foreach (LyricSyl syl in this.sylList)
                {
                    int pos = frameDisplayer.SelectionStart;

                    String text = (posline + 1).ToString() + " " + (posSyl + 1) + " " + (syl.getProp() + Environment.NewLine);


                    frameDisplayer.SelectionLength = (posline + 1).ToString().Length;
                    frameDisplayer.SelectionColor = Color.Green;
                    frameDisplayer.SelectionFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);

                    frameDisplayer.SelectionStart = frameDisplayer.SelectionLength;
                    frameDisplayer.SelectionLength = (posSyl + 1).ToString().Length;
                    frameDisplayer.SelectionColor = Color.Red;
                    frameDisplayer.SelectionFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);

                    pos += text.Length - 2;

                    frameDisplayer.SelectionStart = pos;
                    posSyl++;
                }
            }*/

            internal string getFileLine()
            {
                String result = "";

                foreach (LyricSyl syl in this.sylList)
                {
                    result += "&" + syl.getText();
                }
                return result;
            }

            internal int getCurrentSyl()
            {
                return this.currentSyl;
            }

            internal void setBeginFrame(int syl, int beginFrame)
            {
                this.sylList[syl].setBeginFrame(beginFrame);
            }

            internal void setEndFrame(int syl, int endFrame)
            {
                this.sylList[syl].setEndFrame(endFrame);
            }
        }

        public ParoleManager()
        {
            //todo mettre tout a null
        }
               
            public void init(//RichTextBox frameDisplayerValue, //RichTextBox lyricDisplayerValue,
                //TextBox frameFileTextValue, 
                //TextBox lyricFileTextValue, 
                //TextBox toyundaFileTextValue,
                //Panel panel, 
                //TextBox infoLineTextValue,
        //TextBox infoSylTextValue,
             //Panel prevColorPanel,
         //Panel duringColorPanel,
         //Panel afterColorPanel)
                )

            
        {
            lyricList = new List<LyricLine>();
            colorArray = new Color[3];
            //this.frameDisplayer = frameDisplayerValue;
            //this.lyricDisplayer = lyricDisplayerValue;
            //this.frameFileText = frameFileTextValue;
            //this.lyricFileText = lyricFileTextValue;
            //this.toyundaFileText = toyundaFileTextValue;
            //this.frameGraphic = Graphics.FromHwnd(panel.Handle);
            //this.frameGraphic.Clip = new Region(new Rectangle(0, 0, panel.Width, panel.Height));
            //this.infoLineText = infoLineTextValue;
            //this.infoSylText = infoSylTextValue;
            
            
            this.clearFramesFile();
            this.clearLyricsFile();
            this._frameSaved = false;
            //_prevColorPanel = prevColorPanel;
            //_duringColorPanel = duringColorPanel;
            //_afterColorPanel = afterColorPanel;

            this.toyundaFilepath = "";

        }

        private void clearFramesFile()
        {
            this.setFrameFilepath("");
            this.clearFrameList();
        }

        private void clearLyricsFile()
        {
            this.setLyricsFilepath("");
            clearLyrics();
        }

        private void clearLyrics()
        {
            lyricList.Clear();
            currentLine = 0;
        }

        internal Boolean loadLyrics()
        {
            //lyricDisplayer.Clear();
            //onLyricFileChangedEvent.Invoke(this, new EventArgs());//this.lyricFileText.Text = String.Empty;

            if (this.loadLyricsFile())
            {
                onLyricFileChangedEvent.Invoke(this, new EventArgs()); //this.lyricFileText.Text = getDisplayLyricName();
                //onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();

                onFrameChangedEvent.Invoke(this, new EventArgs());                
                onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                //displayCurrentLine(false);
                return true;
            }
            else
            {
                onLyricFileChangedEvent.Invoke(this, new EventArgs());//this.lyricFileText.Text = String.Empty;
                return false;
            }
        }


        public Boolean loadLyricsFile()
        {
            //try
            //{
                String path = lyricsFilepath;
                clearLyricsFile();
                if (path != "" && File.Exists(path))
                {
                    this.setLyricsFilepath(path);
                    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                    StreamReader streamReader = new StreamReader(lyricsFilepath);
                    String ligne = streamReader.ReadLine();
                    // Lecture de toutes les lignes et affichage de chacune sur la page 
                    while (ligne != null)
                    {
                        ligne = ligne.Trim();
                        if (ligne.Length > 0)
                        {
                            if (!ligne.ToCharArray()[0].Equals('%'))
                            {
                                String[] splitted = ligne.Split(new Char[] { '&' });
                                LyricLine lyricLine = new LyricLine();
                                foreach (String syltext in splitted)
                                {
                                    if (syltext.Length > 0)
                                    {
                                        lyricLine.Add(new LyricSyl(syltext));
                                    }
                                }
                                lyricList.Add(lyricLine);
                            }
                            else
                            {
                                if (ligne.Length >= 21 && ligne.Substring(0, 6).CompareTo("%color") == 0)
                                {
                                    String[] splitter = ligne.Substring(6, ligne.Length - 6).Trim().Split(new Char[] { ' ' });
                                    if (splitter.Length >= 3
                                        && splitter[0].Length >= 6
                                        && splitter[1].Length >= 6
                                        && splitter[2].Length >= 6)
                                    {
                                        colorArray[0] = Color.FromArgb(
                                            int.Parse(splitter[0].Substring(4, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[0].Substring(3, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[0].Substring(0, 2), NumberStyles.HexNumber));
                                        colorArray[1] = Color.FromArgb(
                                            int.Parse(splitter[1].Substring(4, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[1].Substring(3, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[1].Substring(0, 2), NumberStyles.HexNumber));
                                        colorArray[2] = Color.FromArgb(
                                            int.Parse(splitter[2].Substring(4, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[2].Substring(3, 2), NumberStyles.HexNumber)
                                            , int.Parse(splitter[2].Substring(0, 2), NumberStyles.HexNumber));
                                        onColorChangedEvent.Invoke(this, new EventArgs());
                                    }
                                }
                            }
                        }
                        ligne = streamReader.ReadLine();
                    }
                    // Fermeture du StreamReader (attention très important) 
                    streamReader.Close();
                    return true;
                }
                else
                    return false;
/*            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture du fichier : " + ex.Message);
                return false;
            }
                */
        }

        private void setLyricsFilepath(string path)
        {
            this.lyricsFilepath = path;
            onLyricFileChangedEvent.Invoke(this, new EventArgs()); //this.lyricFileText.Text = this.getDisplayLyricName();
        }

        public void setToyundaFilepath(string path)
        {
            this.toyundaFilepath = path;
            //todo
            //this.toyundaFileText.Text = this.getDisplayToyundaFilename();
            onToyundaFileChangedEvent.Invoke(this, new EventArgs());
        }


        public bool loadToyundaFile(string path, bool loadLyrics, bool loadFrames)
        {
            this.setToyundaFilepath(path);
            return this.loadToyundaFile(loadLyrics, loadFrames);
        }

        public bool loadToyundaFile(bool loadLyrics, bool loadFrames)
        {
            try
            {
                String path = toyundaFilepath;
                if (path != "" && File.Exists(path))
                {
                    this.setToyundaFilepath(path);
                    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                    StreamReader streamReader = new StreamReader(this.toyundaFilepath);
                    String ligne = streamReader.ReadLine();
                    // Lecture de toutes les lignes et affichage de chacune sur la page 
                    //atteindre la position du fichier lyrics
                    while (ligne != null && ! (ligne.CompareTo("# --- LYRICS - GENERATE AGAIN AFTER YOU EDIT ---") == 0))
                        ligne = streamReader.ReadLine();
                    ligne = streamReader.ReadLine();
                    if (loadLyrics)
                    {
                        StreamWriter streamWriter = new StreamWriter(this.lyricsFilepath);
                        clearLyrics();
                        while (ligne != null
    && ligne.Length >= 2
    && ligne.Substring(0, 2).CompareTo("--") == 0)
                        {
                            streamWriter.WriteLine(ligne.Substring(2, ligne.Length - 2));
                            ligne = streamReader.ReadLine();
                        }
                        streamWriter.Close();
                        this.loadLyrics();
                    }
                    while (ligne != null && !( ligne.CompareTo("# --- TIMING - GENERATE AGAIN AFTER YOU EDIT ---") == 0))
                    {
                        ligne = streamReader.ReadLine();
                    }
                    ligne = streamReader.ReadLine();
                    
                    //                        && !ligne.Equals("# --- SUB - DO NOT EDIT HERE - MODIFICATIONS WILL BE LOST ---") == 0)
                    if (loadFrames)
                    {
                    StreamWriter streamWriter = new StreamWriter(this.frameFilepath);
                    while (ligne != null
                        && ligne.Length >= 2
                        && ligne.Substring(0, 2).CompareTo("==") == 0)
                    {
                        
                        ligne = streamReader.ReadLine();
                        if (ligne != null)
                        {
                            String[] values = ligne.Substring(2, ligne.Length - 2).Split(new Char[] { ' ' });
                            if (values.Length == 2)
                            {
                                int begin, end;
                                int.TryParse(values[0], out begin);
                                int.TryParse(values[1], out end);
                                streamWriter.WriteLine(begin + " " + end);
                            }
                        }
                        ligne = streamReader.ReadLine();
                    }
                    streamWriter.Close();
                    this.loadFrameFile();
                    }
                    // Fermeture du StreamReader (attention très important) 
                    streamReader.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture du fichier : " + ex.Message);
                return false;
            }

        }

        public string getDisplayToyundaFilename()
        {
            string result = "";
            if (this.toyundaFilepath.Length > 0)
            {
                String[] splitted = toyundaFilepath.Split(new Char[] { '/', '\\' });
                result = splitted[splitted.Length - 1];
            }
            return result;
        }

        public string getToyundaFilepath()
        {
            return this.toyundaFilepath;
        }

        public Boolean saveFramesFile()
        {
            try
            {
                if (this.frameFilepath != null && File.Exists(frameFilepath))
                {
                    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                    StreamWriter streamWriter = new StreamWriter(this.frameFilepath);
                    foreach (LyricLine line in lyricList)
                    {
                        line.save(streamWriter);
                    }
                    // Fermeture du StreamReader (attention très important) 
                    streamWriter.Flush();
                    streamWriter.Close();
                    this._frameSaved = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("Le fichier frame est introuvable à l'adresse " + frameFilepath, "Erreur");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture du fichier : " + ex.Message, "Erreur");
                return false;
            }

        }

        public String getLyricsFilepath()
        {
            return lyricsFilepath;
        }

        public String getFramesFilepath()
        {
            return frameFilepath;
        }

        internal void displayCurrentLine(Boolean displayCurrent, RichTextBox box
            , TextBox infoLineText
            , TextBox infoSylText)
        {
            box.Clear();

            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                this.lyricList[this.currentLine].displayText(box);
                this.lyricList[this.currentLine].displayColor(box, 0, displayCurrent);
                infoLineText.Text = (this.currentLine + 1).ToString();
                this.lyricList[this.currentLine].displayCurrentSyl(infoSylText);

            }
        }

        public Boolean goPreviousSyl()
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                if (!this.lyricList[currentLine].previousSyl())
                {
                    if (this.goPreviousLine(true))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    //currentSyl++;
                    onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                    //displayCurrentLine(false);

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool goPreviousLine(Boolean lastPos)
        {
            if (this.currentLine > 0)
            {
                this.currentLine--;
                if (!lastPos)
                    this.lyricList[currentLine].setCurrentSyl(0);
                else
                    this.lyricList[currentLine].goLastSyl();
                onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                //displayCurrentLine(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean goNextSyl()
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                if (!this.lyricList[currentLine].nextSyl())
                {
                    return this.goNextLine();
                }
                else
                {
                    //currentSyl++;
                    onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                    //displayCurrentLine(false);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public Boolean goNextLine()
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count - 1)
            {
                this.currentLine++;

                this.lyricList[currentLine].setCurrentSyl(0);
                onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                //displayCurrentLine(false);
                return true;

            }
            else
            {
                return false;
            }
        }

        internal void setCurrentEndFrame(long position)
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                if (!lyricList[currentLine].setCurrentEndFrame(position))
                {
                    //MessageBox.Show("Position de fin de frame invalide :" + position.ToString());
                }
                else
                {
                    //todo ameliorer ca
                    //onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();
                    //displayLastFrame();
                    onCurrentLineChangedEvent.Invoke(this, new EventArgs());
                    //lyricDisplayer.Clear();
                    //displayCurrentLine(false);
                    this._frameSaved = false;
                }
            }
        }

        internal void setCurrentBeginFrame(long position)
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                if (!lyricList[currentLine].setCurrentBeginFrame(position))
                {
                    //MessageBox.Show("Position de début de frame invalide :" + position.ToString());
                }
                else
                {
                    onCurrentLineActiveChangedEvent.Invoke(this, new EventArgs());
                    //lyricDisplayer.Clear();
                    //displayCurrentLine(true);
                    this._frameSaved = false;
                }
            }
        }

        internal string getCurrentSylFrameProp()
        {
            if (currentLine >= 0 && currentLine < this.lyricList.Count)
            {
                return lyricList[currentLine].getCurrentSylFrameProp();
            }
            else
                return "";
        }

        internal Int32 getNumberSyl()
        {
            Int32 result = 0;
            foreach (LyricLine line in lyricList)
            {
                result += line.getNumberSyl();
            }
            return result;
        }

        /*internal int getCurrentSylNum()
        {
            return this.currentSyl;
        }*/

        internal void createFrameFile(string p)
        {
            FileStream file = File.Create(p);
            file.Close();
            this.setFrameFilepath(p);
        }

        private void setFrameFilepath(string p)
        {
            this.frameFilepath = p;
            onFrameFileChangedEvent.Invoke(this, new EventArgs());
            //this.frameFileText.Text = getDisplayFrameName();
        }

        internal Boolean loadLyricsFile(string p)
        {
            this.setLyricsFilepath(p);
            return this.loadLyrics();
        }

        internal string getDisplayLyricName()
        {
            string result = "";
            if (this.lyricsFilepath.Length > 0)
            {
                String[] splitted = lyricsFilepath.Split(new Char[] { '/', '\\' });
                result = splitted[splitted.Length - 1];
            }
            return result;
        }

        internal string getDisplayFrameName()
        {
            string result = "";
            if (this.frameFilepath.Length > 0)
            {
                String[] splitted = frameFilepath.Split(new Char[] { '/', '\\' });
                result = splitted[splitted.Length - 1];
            }
            return result;
        }

        internal bool loadFrameFile(string p)
        {
            this.setFrameFilepath(p);
            return this.loadFrameFile();
        }

        internal bool loadFrameFile()
        {
            try
            {
                String path = frameFilepath;
                clearFramesFile();
                if (path != "")
                {

                    this.setFrameFilepath(path);
                    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
                    StreamReader streamReader = new StreamReader(frameFilepath);

                    String ligne = streamReader.ReadLine();
                    int noSylSet = 0;
                    int noFrSet = 0;
                    int sylSet = 0;
                    int lineSet = 0;
                    //parcours de toutes les sylabes
                    foreach (LyricLine lyricLine in lyricList)
                    {
                        Boolean set = false;
                        foreach (LyricSyl lyricSyl in lyricLine.getSylList())
                        {
                            if (ligne != null)
                            {

                                ligne = ligne.Trim();
                                if (ligne.Length > 0)
                                {
                                    String[] splitted = ligne.Split(new Char[] { ' ' });
                                    //todo securite
                                    lyricSyl.setBeginFrame(long.Parse(splitted[0]));
                                    lyricSyl.setEndFrame(long.Parse(splitted[1]));
                                    set = true;
                                    sylSet++;
                                }
                                ligne = streamReader.ReadLine();

                            }
                            else
                            {
                                lyricSyl.setBeginFrame(0);
                                lyricSyl.setEndFrame(0);
                                noSylSet++;
                            }
                        }
                        lineSet += (set ? 1 : 0);


                    }
                    while (ligne != null)
                    {
                        ligne = streamReader.ReadLine();
                        noFrSet++;
                    }
                    if (noSylSet > 0)
                    {
                        MessageBox.Show("Attention : " + noSylSet.ToString() + " syllabes n'ont pas eut de frame associées");
                    }
                    if (noFrSet > 0)
                    {
                        MessageBox.Show("Attention : " + noFrSet.ToString() + " frame n'ont pas eut de syllabes associées");
                    }
                    // this.currentSyl = sylSet - 1; 
                    this.goLine(0);
                    this.goSyl(0);
                    onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();

                    onFrameFileChangedEvent.Invoke(this, new EventArgs()); //this.frameFileText.Text = getDisplayFrameName();
                    // Fermeture du StreamReader (attention très important) 
                    streamReader.Close();
                    this._frameSaved = true;
                    return true;
                }
                else
                {
                    this.setFrameFilepath("");
                    onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();
                    onFrameFileChangedEvent.Invoke(this, new EventArgs()); //this.frameFileText.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de lecture du fichier : " + ex.Message);
                return false;
            }
        }

        internal void displayAllFrames(RichTextBox frameBox)
        {

            frameBox.Clear();
            frameBox.SelectionStart = 0;
            int posLine = 0;
            foreach (LyricLine line in this.lyricList)
            {
                line.displayAllFrames(posLine, frameBox);
                posLine++;
            }
            frameBox.SelectionStart = 0;
        }

        /*internal void displayLastFrame()
        {
            frameDisplayer.AppendText(getCurrentSylFrameProp() + Environment.NewLine);
        }*/

        public List<LyricSyl> getSylList()
        {
            List<LyricSyl> sylList = new List<LyricSyl>();
            foreach (LyricLine line in lyricList)
            {
                line.addSylToList(ref sylList);
            }
            return sylList;
        }

        internal void saveManualModifs(RichTextBox frameBox)
        {
            StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

            String[] lineList = frameBox.Text.Split(new String[] { "\n", Environment.NewLine }, option);

            //List<LyricSyl> sylList = this.getSylList();
            Int32 cpt = 0;
            int linePos = 0;
            int lineNum = 0;
            //int lineDec = 0;
            int sylNum = -1;
            int beginAt = 0;
            //int sylDec = 0;
            String line;
            LyricLine lyricLine = null;
            LyricSyl lyricSyl = null;
            int backCurrentLine = this.currentLine;
            int backCurrentSyl = (backCurrentLine < this.lyricList.Count ? lyricList[backCurrentLine].getCurrentSyl() : 0);
            clearLyrics();
            while (linePos < lineList.Length)
            {
                beginAt = 0;
                line = lineList[linePos];

                if (line.Length == 0)
                {
                    //todo : cas vide
                }
                else if (line.ToCharArray()[0].Equals('%'))
                {
                    //TODO: cas information
                }
                else
                {
                    int num;
                    String[] parts = line.Split(new Char[] { ' ' }, 5);
                    //recuperation de la ligne
                    if (parts.Length == 0 || !int.TryParse(parts[0], out num))
                    {
                        //creer une nouvele ligne
                        //lineDec ++;
                        lyricLine = new LyricLine();
                        lineNum = 0;
                        lyricList.Add(lyricLine);
                        //sylDec = 0;
                        sylNum = 0;
                        lyricSyl = new LyricSyl("");
                        lyricLine.Add(lyricSyl);

                    }
                    else
                    {
                        beginAt = 1;
                        if (num != lineNum)
                        {
                            //si on a changé de ligne
                            lyricLine = new LyricLine();
                            lineNum = num;
                            lyricList.Add(lyricLine);
                            sylNum = 0;
                        }
                        if (lyricLine == null)
                        {
                            throw (new Exception("Erreur de parsing"));
                        }
                        //recuperation de la syllabe
                        if (parts.Length < 2 || !int.TryParse(parts[1], out num))
                        {
                            //creer une nouvele ligne
                            //sylDec ++;
                            lyricSyl = new LyricSyl("");
                            sylNum = 0;
                            lyricLine.Add(lyricSyl);
                        }
                        else
                        {
                            beginAt = 2;
                            if (num != sylNum)
                            {
                                //si on a changé de sylabe
                                lyricSyl = new LyricSyl("");
                                sylNum = num;
                                lyricLine.Add(lyricSyl);
                            }
                            if (lyricSyl == null)
                            {
                                throw (new Exception("Erreur de parsing"));
                            }


                            //recuperation des frames
                            if (parts.Length < 3 || !int.TryParse(parts[2], out num))
                            {
                                lyricSyl.setBeginFrame(0);
                            }
                            else
                            {
                                beginAt = 3;
                                //si on est sur la même syl on ne garde que la fin
                                if (lyricSyl.getBeginFrame() <= 0)
                                    lyricSyl.setBeginFrame(num);

                                if (parts.Length < 4 || !int.TryParse(parts[3], out num))
                                {
                                    lyricSyl.setEndFrame(0);
                                }
                                else
                                {
                                    beginAt = 4;
                                    lyricSyl.setEndFrame(num);
                                }
                            }
                        }
                    }
                    for (int i = beginAt; i < parts.Length; ++i)
                    {
                        //espace de fin de ligne
                        if (parts[i].Length == 0)
                            parts[i] = " ";
                        lyricSyl.setText(lyricSyl.getText() + parts[i]);
                    }
                }
                linePos++;
            }

            onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();
            this.currentLine = backCurrentLine;
            if (currentLine < this.lyricList.Count)
            {
                lyricList[currentLine].setCurrentSyl(backCurrentSyl);
            }
            onCurrentLineChangedEvent.Invoke(this, new EventArgs());
            //displayCurrentLine(false);

        }
        /*
        public void displayVisualFrame()
        {
            Brush lineBrush = new SolidBrush(Color.White);
            Pen linePen = new Pen(lineBrush);
            linePen.Color = Color.White;
            RectangleF rect = frameGraphic.ClipBounds;

            //Tracer la ligne du centre
            frameGraphic.DrawLine(linePen,
                new Point(0, (int)rect.Height / 2),
                new Point((int)rect.Width, (int)rect.Height / 2));
        }*/


        internal void clearFrameList()
        {
            //remettre les frame à -1 et recommencer
            foreach (LyricLine line in lyricList)
            {
                line.clearFrameList();
            }
            //this.currentSyl = 0;

            this.currentLine = 0;
            onFrameChangedEvent.Invoke(this, new EventArgs());//displayAllFrames();
            //displayCurrentLine(false);
            onCurrentLineChangedEvent.Invoke(this, null);
        }



        internal void goLine(int lineVal)
        {
            if (lineVal >= 0 && lineVal < this.lyricList.Count)
            {
                this.currentLine = lineVal;
                //displayCurrentLine(false);
                onCurrentLineChangedEvent.Invoke(this, new EventArgs());
            }
        }

        internal void goSyl(int sylVal)
        {
            if (this.currentLine >= 0 && this.currentLine < this.lyricList.Count &&
                sylVal >= 0 && sylVal < this.lyricList[this.currentLine].getSylList().Count)
            {
                this.lyricList[this.currentLine].setCurrentSyl(sylVal);
                //displayCurrentLine(false);
                onCurrentLineChangedEvent.Invoke(this, new EventArgs());
            }
        }

        internal string getCurrentLineNumDisplay()
        {
            if (this.currentLine >= 0 && this.currentLine < this.lyricList.Count)
                return (this.currentLine + 1).ToString();
            else
                return "";
        }

        internal string getCurrentSylNumDisplay()
        {
            if (this.currentLine >= 0 && this.currentLine < this.lyricList.Count)
                return this.lyricList[this.currentLine].getCurrentSylNumDisplay();
            else
                return "";
        }

        internal long getCurrentBeginFrame()
        {
            if (this.currentLine >= 0 && this.currentLine < this.lyricList.Count)
                return this.lyricList[this.currentLine].getCurrentBeginFrame();
            else
                return 0;
        }

        internal Boolean isFrameSaved()
        {
            return this._frameSaved;
        }

        public void setColors(Color[] colorArrayArg)
        {
            colorArray[0] = colorArrayArg[0];
            colorArray[1] = colorArrayArg[1];
            colorArray[2] = colorArrayArg[2];
        }

        internal void saveLyricsFile()
        {
            if (File.Exists(this.lyricsFilepath))
            {


                //todo gerer si non exist
                StreamWriter writer = new StreamWriter(this.lyricsFilepath);

                String color = "%color "
+ colorArray[0].B.ToString("X2")
                    + colorArray[0].G.ToString("X2")
                + colorArray[0].R.ToString("X2")
            + " "
            + colorArray[1].B.ToString("X2")
                + colorArray[1].G.ToString("X2")
                + colorArray[1].R.ToString("X2")
            + " "
            + colorArray[2].B.ToString("X2")
                + colorArray[2].G.ToString("X2")
                + colorArray[2].R.ToString("X2");

                writer.WriteLine(color);
                foreach (LyricLine line in lyricList)
                {
                    writer.WriteLine(line.getFileLine());
                }
                writer.Close();
            }
            else
            {
                MessageBox.Show("Le fichier lyrics " + this.lyricsFilepath + " est introuvable", "Erreur");

            }
        }

        internal void setBeginFrame(int line, int syl, int beginFrame)
        {
            this.lyricList[line].setBeginFrame(syl, beginFrame);
        }

        internal void setEndFrame(int line, int syl, int endFrame)
        {
            this.lyricList[line].setEndFrame(syl, endFrame);
        }

        internal Color[] getColorArray()
        {
            return this.colorArray;
        }
    }
}
