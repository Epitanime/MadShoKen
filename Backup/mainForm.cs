/****************************************************************************
While the underlying libraries are covered by LGPL, this sample is released 
as public domain.  It is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  
*****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using DirectShowLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TimingMadShoken
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class mainForm : System.Windows.Forms.Form
	{

        IGraphBuilder graph = null;
        IMediaControl mediaControl = null;
        IVideoWindow videoWindow = null;
        IBasicAudio basicAudio = null;
        IMediaSeeking mediaSeeking = null;
        //IMediaPosition mediaPosition = null;
        bool bKeepAspectRatio = true;

        private System.Windows.Forms.Label videoLabel;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TextBox videoFileName;
        private MenuStrip nouveauToyMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem fermerToolStripMenuItem;
        private OpenFileDialog openFileDialog;
        private Label parolesLabel;
        private Label paroleFileLabel;
        private TextBox paroleFileTextBox;
        private IContainer components;
        private RichTextBox parolesText;
        private Button timeButton;
        private TrackBar speedTrackBar;
        private TrackBar videoTrackBar;
        private Label frameLabel;
        private TextBox frameTextbox;
        private System.Windows.Forms.Timer observorTimer;
        private Button saveFrameButton;
        private Label frameFileLabel;
        private TextBox frameFileText;
        private Label generateurLabel;
        private ComboBox generateurComboBox;

        private ParoleManager paroleManager;//gère les fichier parole et frame manipulé
        private Button generateButton;
        private ToolStripMenuItem videoToolStripMenuItem;
        private ToolStripMenuItem ouvrirVideoToolStripMenuItem;
        private ToolStripMenuItem frameToolStripMenuItem;
        private ToolStripMenuItem paroleToolStripMenuItem;
        private ToolStripMenuItem ouvrirFichierParole;
        private ToolStripMenuItem nouveauFichierFrameToolStripMenuItem;
        private ToolStripMenuItem ouvrirFichierToolStripMenuItem;
        private GenerateurManager generateurManager;
        private RichTextBox console;
        private RichTextBox frameRichTextBox;
        private Button SaveModifbutton;
        private Panel wavPanel;
        private Button launchToyButton;
        private ToolStripMenuItem toyundaToolStripMenuItem;
        private ToolStripMenuItem nouveauFichierToolStripMenuItem;
        private ToolStripMenuItem ouvrirFichierToyToolStripMenuItem1;
        private Button cancelModifButton;
        private ToolStripMenuItem rechargerToolStripMenuItem;
        private ToolStripMenuItem recommencerToolStripMenuItem;
        private TextBox lineText;
        private Label label1;
        private Label sylLabel;
        private TextBox sylText;
        private Button downLineButton;
        private Button downSylButton;
        private Button upLineButton;
        private Button upSylButton;
        private Button refreshFrameText;
        private RichTextBox consoleErreur;
        private TextBox rateText;
        private Label toyundaLabel;
        private TextBox toyundaFileText;
        private ToolStripMenuItem nouveauTimeToolStripMenuItem;
        private ToolStripMenuItem ouvrirTimeToolStripMenuItem;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lachieusetouch;
        private Label lachieuseLabel;
        private CheckBox checkBox_gotoposition;
        private GroupBox groupBox1;
        private Label label6;
        private ToolStripMenuItem reencoderLaVidéoToolStripMenuItem; //gere les génerateurs disponibles
        private String videoFilepath;
        private Encodeur encodeur;
        private ToolStripMenuItem rechargerToolStripMenuItem1;
        private Button reloadButton;
        private ColorDialog colorDialog1;
        private Panel prevColorPanel;
        private Panel duringColorPanel;
        private Panel afterColorPanel;
        private CheckBox saveModifCheckBox;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private TrackBar trackBar1;
        private Boolean running;
        private selectName selectNameForm;
        private ToolStripMenuItem pasDeNomToolStripMenuItem;
        private Label timeNameLabel;
        private ToolStripMenuItem genererFichierIniToolStripMenuItem;
        private Button operationButton;
        private selectName.Info infoTime;
        private operation operationForm;
        private Panel panel1;
        private GroupBox couleurgroupBox2;
        private Label label7;
        private Label label9;
        private Label label8;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private CheckBox displayConsoleInfocheckBox1;
        private ToolStripMenuItem reverseTimingToolStripMenuItem;
        private ToolStripMenuItem envoyerParFtpToolStripMenuItem;
        private ToolStripMenuItem récupererParFtpToolStripMenuItem;
        private Boolean frameChanged;
        private FolderBrowserDialog browserDialog;
		public mainForm()
		{
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();
            
			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            paroleManager = new ParoleManager();

            //gestion des events
            paroleManager.onCurrentLineChangedEvent += new System.EventHandler(this.displayCurrentLine);
            paroleManager.onCurrentLineActiveChangedEvent += new System.EventHandler(this.displayCurrentLineActive);
            paroleManager.onFrameChangedEvent += new System.EventHandler(this.displayAllFrames);
            paroleManager.onFrameFileChangedEvent += new System.EventHandler(this.displayFrameFile);
            paroleManager.onLyricFileChangedEvent += new System.EventHandler(this.displayLyricFile);
            paroleManager.onToyundaFileChangedEvent += new System.EventHandler(this.displayToyundFile);
            paroleManager.onColorChangedEvent += new System.EventHandler(this.displayColor);
            paroleManager.init(//this.frameRichTextBox, //this.parolesText,
                //this.frameFileText, 
                //this.paroleFileTextBox, 
                //this.toyundaFileText, 
                //wavPanel, 
                //lineText, 
                //sylText,  
                //prevColorPanel,
          //duringColorPanel,
          //afterColorPanel
          );
            
            
            generateurManager = new GenerateurManager(this.console, this.consoleErreur);
            List<Generateur> generateurList = generateurManager.getGeneratorList();
            generateurComboBox.Items.Clear();
            videoFilepath = "";
            for (int i = 0; i < generateurList.Count; ++i)
            {
                
                generateurComboBox.Items.Add(generateurList[i].getDisplay());
                if (i == 0)
                {
                    generateurComboBox.SelectedIndex = 0;
                }
            }
            encodeur = new Encodeur();
            running = false;
            infoTime = new selectName.Info("", "", 0, "", "", "");
            frameChanged = false;
            this.selectNameForm = null;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
		{
            // Make sure to release the DxPlay object to avoid hanging
            if (m_play != null)
            {
                m_play.Dispose();
            }
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.videoFileName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.videoLabel = new System.Windows.Forms.Label();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.btnPause = new System.Windows.Forms.Button();
            this.nouveauToyMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseTimingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasDeNomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genererFichierIniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.envoyerParFtpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fermerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reencoderLaVidéoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paroleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirFichierParole = new System.Windows.Forms.ToolStripMenuItem();
            this.rechargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauFichierFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirFichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechargerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.recommencerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toyundaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauFichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirFichierToyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.parolesLabel = new System.Windows.Forms.Label();
            this.paroleFileLabel = new System.Windows.Forms.Label();
            this.paroleFileTextBox = new System.Windows.Forms.TextBox();
            this.parolesText = new System.Windows.Forms.RichTextBox();
            this.timeButton = new System.Windows.Forms.Button();
            this.speedTrackBar = new System.Windows.Forms.TrackBar();
            this.videoTrackBar = new System.Windows.Forms.TrackBar();
            this.frameLabel = new System.Windows.Forms.Label();
            this.frameTextbox = new System.Windows.Forms.TextBox();
            this.observorTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFrameButton = new System.Windows.Forms.Button();
            this.frameFileLabel = new System.Windows.Forms.Label();
            this.frameFileText = new System.Windows.Forms.TextBox();
            this.generateurLabel = new System.Windows.Forms.Label();
            this.generateurComboBox = new System.Windows.Forms.ComboBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.frameRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SaveModifbutton = new System.Windows.Forms.Button();
            this.wavPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cancelModifButton = new System.Windows.Forms.Button();
            this.launchToyButton = new System.Windows.Forms.Button();
            this.lineText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sylLabel = new System.Windows.Forms.Label();
            this.sylText = new System.Windows.Forms.TextBox();
            this.downLineButton = new System.Windows.Forms.Button();
            this.downSylButton = new System.Windows.Forms.Button();
            this.upLineButton = new System.Windows.Forms.Button();
            this.upSylButton = new System.Windows.Forms.Button();
            this.refreshFrameText = new System.Windows.Forms.Button();
            this.consoleErreur = new System.Windows.Forms.RichTextBox();
            this.rateText = new System.Windows.Forms.TextBox();
            this.toyundaLabel = new System.Windows.Forms.Label();
            this.toyundaFileText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lachieusetouch = new System.Windows.Forms.Label();
            this.lachieuseLabel = new System.Windows.Forms.Label();
            this.checkBox_gotoposition = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.reloadButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.prevColorPanel = new System.Windows.Forms.Panel();
            this.duringColorPanel = new System.Windows.Forms.Panel();
            this.afterColorPanel = new System.Windows.Forms.Panel();
            this.saveModifCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.timeNameLabel = new System.Windows.Forms.Label();
            this.operationButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.couleurgroupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.displayConsoleInfocheckBox1 = new System.Windows.Forms.CheckBox();
            this.récupererParFtpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauToyMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoTrackBar)).BeginInit();
            this.wavPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.couleurgroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // videoFileName
            // 
            this.videoFileName.Enabled = false;
            this.videoFileName.Location = new System.Drawing.Point(130, 130);
            this.videoFileName.Name = "videoFileName";
            this.videoFileName.Size = new System.Drawing.Size(180, 20);
            this.videoFileName.TabIndex = 9;
            this.videoFileName.TextChanged += new System.EventHandler(this.videoFileName_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(688, 128);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 31);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // videoLabel
            // 
            this.videoLabel.Location = new System.Drawing.Point(22, 130);
            this.videoLabel.Name = "videoLabel";
            this.videoLabel.Size = new System.Drawing.Size(83, 16);
            this.videoLabel.TabIndex = 2;
            this.videoLabel.Text = "Video";
            // 
            // videoPanel
            // 
            this.videoPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.videoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.videoPanel.Location = new System.Drawing.Point(14, 18);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(288, 198);
            this.videoPanel.TabIndex = 10;
            this.videoPanel.MouseLeave += new System.EventHandler(this.videoPanel_MouseLeave);
            this.videoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.videoPanel_Paint);
            this.videoPanel.Click += new System.EventHandler(this.videoPanel_Click);
            this.videoPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.videoPanel_MouseDown);
            this.videoPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.videoPanel_MouseUp);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(688, 165);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(56, 33);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // nouveauToyMenuStrip
            // 
            this.nouveauToyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.paroleToolStripMenuItem,
            this.frameToolStripMenuItem,
            this.toyundaToolStripMenuItem});
            this.nouveauToyMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.nouveauToyMenuStrip.Name = "nouveauToyMenuStrip";
            this.nouveauToyMenuStrip.Size = new System.Drawing.Size(1073, 24);
            this.nouveauToyMenuStrip.TabIndex = 13;
            this.nouveauToyMenuStrip.Text = "test";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauTimeToolStripMenuItem,
            this.ouvrirTimeToolStripMenuItem,
            this.reverseTimingToolStripMenuItem,
            this.pasDeNomToolStripMenuItem,
            this.genererFichierIniToolStripMenuItem,
            this.envoyerParFtpToolStripMenuItem,
            this.récupererParFtpToolStripMenuItem,
            this.fermerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "Time";
            // 
            // nouveauTimeToolStripMenuItem
            // 
            this.nouveauTimeToolStripMenuItem.Name = "nouveauTimeToolStripMenuItem";
            this.nouveauTimeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.nouveauTimeToolStripMenuItem.Text = "Nouveau time";
            this.nouveauTimeToolStripMenuItem.Click += new System.EventHandler(this.nouveauTimeToolStripMenuItem_Click);
            // 
            // ouvrirTimeToolStripMenuItem
            // 
            this.ouvrirTimeToolStripMenuItem.Name = "ouvrirTimeToolStripMenuItem";
            this.ouvrirTimeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.ouvrirTimeToolStripMenuItem.Text = "Ouvrir time";
            this.ouvrirTimeToolStripMenuItem.Click += new System.EventHandler(this.ouvrirTimeToolStripMenuItem_Click);
            // 
            // reverseTimingToolStripMenuItem
            // 
            this.reverseTimingToolStripMenuItem.Name = "reverseTimingToolStripMenuItem";
            this.reverseTimingToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reverseTimingToolStripMenuItem.Text = "Reverse timing";
            this.reverseTimingToolStripMenuItem.Click += new System.EventHandler(this.reverseTimingToolStripMenuItem_Click);
            // 
            // pasDeNomToolStripMenuItem
            // 
            this.pasDeNomToolStripMenuItem.Name = "pasDeNomToolStripMenuItem";
            this.pasDeNomToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.pasDeNomToolStripMenuItem.Text = "Rennommer";
            this.pasDeNomToolStripMenuItem.Click += new System.EventHandler(this.pasDeNomToolStripMenuItem_Click);
            // 
            // genererFichierIniToolStripMenuItem
            // 
            this.genererFichierIniToolStripMenuItem.Name = "genererFichierIniToolStripMenuItem";
            this.genererFichierIniToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.genererFichierIniToolStripMenuItem.Text = "Generer fichier ini";
            this.genererFichierIniToolStripMenuItem.Click += new System.EventHandler(this.genererFichierIniToolStripMenuItem_Click);
            // 
            // envoyerParFtpToolStripMenuItem
            // 
            this.envoyerParFtpToolStripMenuItem.Name = "envoyerParFtpToolStripMenuItem";
            this.envoyerParFtpToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.envoyerParFtpToolStripMenuItem.Text = "Envoyer par ftp";
            this.envoyerParFtpToolStripMenuItem.Click += new System.EventHandler(this.envoyerParFtpToolStripMenuItem_Click);
            // 
            // fermerToolStripMenuItem
            // 
            this.fermerToolStripMenuItem.Name = "fermerToolStripMenuItem";
            this.fermerToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.fermerToolStripMenuItem.Text = "Fermer";
            this.fermerToolStripMenuItem.Click += new System.EventHandler(this.fermerToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirVideoToolStripMenuItem,
            this.reencoderLaVidéoToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.videoToolStripMenuItem.Text = "Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // ouvrirVideoToolStripMenuItem
            // 
            this.ouvrirVideoToolStripMenuItem.Name = "ouvrirVideoToolStripMenuItem";
            this.ouvrirVideoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.ouvrirVideoToolStripMenuItem.Text = "Ouvrir vidéo";
            this.ouvrirVideoToolStripMenuItem.Click += new System.EventHandler(this.ouvrirVideoToolStripMenuItem_Click);
            // 
            // reencoderLaVidéoToolStripMenuItem
            // 
            this.reencoderLaVidéoToolStripMenuItem.Name = "reencoderLaVidéoToolStripMenuItem";
            this.reencoderLaVidéoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.reencoderLaVidéoToolStripMenuItem.Text = "reencoder la vidéo";
            this.reencoderLaVidéoToolStripMenuItem.Click += new System.EventHandler(this.reencoderLaVidéoToolStripMenuItem_Click);
            // 
            // paroleToolStripMenuItem
            // 
            this.paroleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirFichierParole,
            this.rechargerToolStripMenuItem});
            this.paroleToolStripMenuItem.Name = "paroleToolStripMenuItem";
            this.paroleToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.paroleToolStripMenuItem.Text = "Parole";
            // 
            // ouvrirFichierParole
            // 
            this.ouvrirFichierParole.Name = "ouvrirFichierParole";
            this.ouvrirFichierParole.Size = new System.Drawing.Size(180, 22);
            this.ouvrirFichierParole.Text = "Ouvrir fichier parole";
            this.ouvrirFichierParole.Click += new System.EventHandler(this.ouvrirFichierParole_Click);
            // 
            // rechargerToolStripMenuItem
            // 
            this.rechargerToolStripMenuItem.Name = "rechargerToolStripMenuItem";
            this.rechargerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rechargerToolStripMenuItem.Text = "Recharger";
            this.rechargerToolStripMenuItem.Click += new System.EventHandler(this.rechargerToolStripMenuItem_Click);
            // 
            // frameToolStripMenuItem
            // 
            this.frameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauFichierFrameToolStripMenuItem,
            this.ouvrirFichierToolStripMenuItem,
            this.rechargerToolStripMenuItem1,
            this.recommencerToolStripMenuItem});
            this.frameToolStripMenuItem.Name = "frameToolStripMenuItem";
            this.frameToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.frameToolStripMenuItem.Text = "Frame";
            // 
            // nouveauFichierFrameToolStripMenuItem
            // 
            this.nouveauFichierFrameToolStripMenuItem.Name = "nouveauFichierFrameToolStripMenuItem";
            this.nouveauFichierFrameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.nouveauFichierFrameToolStripMenuItem.Text = "Nouveau fichier";
            this.nouveauFichierFrameToolStripMenuItem.Click += new System.EventHandler(this.nouveauFichierFrame_Click);
            // 
            // ouvrirFichierToolStripMenuItem
            // 
            this.ouvrirFichierToolStripMenuItem.Name = "ouvrirFichierToolStripMenuItem";
            this.ouvrirFichierToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ouvrirFichierToolStripMenuItem.Text = "Ouvrir fichier";
            this.ouvrirFichierToolStripMenuItem.Click += new System.EventHandler(this.ouvrirFichierToolStripMenuItem_Click);
            // 
            // rechargerToolStripMenuItem1
            // 
            this.rechargerToolStripMenuItem1.Name = "rechargerToolStripMenuItem1";
            this.rechargerToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.rechargerToolStripMenuItem1.Text = "Recharger";
            this.rechargerToolStripMenuItem1.Click += new System.EventHandler(this.rechargerToolStripMenuItem1_Click);
            // 
            // recommencerToolStripMenuItem
            // 
            this.recommencerToolStripMenuItem.Name = "recommencerToolStripMenuItem";
            this.recommencerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.recommencerToolStripMenuItem.Text = "Recommencer";
            this.recommencerToolStripMenuItem.Click += new System.EventHandler(this.recommencerToolStripMenuItem_Click);
            // 
            // toyundaToolStripMenuItem
            // 
            this.toyundaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauFichierToolStripMenuItem,
            this.ouvrirFichierToyToolStripMenuItem1});
            this.toyundaToolStripMenuItem.Name = "toyundaToolStripMenuItem";
            this.toyundaToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.toyundaToolStripMenuItem.Text = "Toyunda";
            this.toyundaToolStripMenuItem.Click += new System.EventHandler(this.toyundaToolStripMenuItem_Click);
            // 
            // nouveauFichierToolStripMenuItem
            // 
            this.nouveauFichierToolStripMenuItem.Name = "nouveauFichierToolStripMenuItem";
            this.nouveauFichierToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.nouveauFichierToolStripMenuItem.Text = "Nouveau fichier";
            this.nouveauFichierToolStripMenuItem.Click += new System.EventHandler(this.nouveauFichierToolStripMenuItem_Click);
            // 
            // ouvrirFichierToyToolStripMenuItem1
            // 
            this.ouvrirFichierToyToolStripMenuItem1.Name = "ouvrirFichierToyToolStripMenuItem1";
            this.ouvrirFichierToyToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.ouvrirFichierToyToolStripMenuItem1.Text = "Ouvrir fichier";
            this.ouvrirFichierToyToolStripMenuItem1.Click += new System.EventHandler(this.ouvrirFichierToyToolStripMenuItem1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // parolesLabel
            // 
            this.parolesLabel.Location = new System.Drawing.Point(22, 440);
            this.parolesLabel.Name = "parolesLabel";
            this.parolesLabel.Size = new System.Drawing.Size(56, 16);
            this.parolesLabel.TabIndex = 14;
            this.parolesLabel.Text = "Paroles";
            // 
            // paroleFileLabel
            // 
            this.paroleFileLabel.Location = new System.Drawing.Point(22, 158);
            this.paroleFileLabel.Name = "paroleFileLabel";
            this.paroleFileLabel.Size = new System.Drawing.Size(83, 16);
            this.paroleFileLabel.TabIndex = 16;
            this.paroleFileLabel.Text = "Fichier Parole";
            // 
            // paroleFileTextBox
            // 
            this.paroleFileTextBox.Enabled = false;
            this.paroleFileTextBox.Location = new System.Drawing.Point(130, 160);
            this.paroleFileTextBox.Name = "paroleFileTextBox";
            this.paroleFileTextBox.Size = new System.Drawing.Size(180, 20);
            this.paroleFileTextBox.TabIndex = 17;
            this.paroleFileTextBox.TextChanged += new System.EventHandler(this.paroleFileTextBox_TextChanged);
            // 
            // parolesText
            // 
            this.parolesText.Enabled = false;
            this.parolesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parolesText.Location = new System.Drawing.Point(128, 440);
            this.parolesText.Name = "parolesText";
            this.parolesText.ReadOnly = true;
            this.parolesText.Size = new System.Drawing.Size(560, 57);
            this.parolesText.TabIndex = 18;
            this.parolesText.Text = "";
            this.parolesText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.parolesText_MouseDown);
            this.parolesText.TextChanged += new System.EventHandler(this.parolesText_TextChanged);
            // 
            // timeButton
            // 
            this.timeButton.Location = new System.Drawing.Point(699, 440);
            this.timeButton.Name = "timeButton";
            this.timeButton.Size = new System.Drawing.Size(56, 32);
            this.timeButton.TabIndex = 19;
            this.timeButton.Text = "Time";
            this.timeButton.Click += new System.EventHandler(this.label1_Click);
            this.timeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.timeButton_MouseDown);
            this.timeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.timeButton_MouseUp);
            // 
            // speedTrackBar
            // 
            this.speedTrackBar.Location = new System.Drawing.Point(686, 266);
            this.speedTrackBar.Maximum = 40;
            this.speedTrackBar.Name = "speedTrackBar";
            this.speedTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.speedTrackBar.Size = new System.Drawing.Size(45, 97);
            this.speedTrackBar.TabIndex = 0;
            this.speedTrackBar.Value = 20;
            this.speedTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // videoTrackBar
            // 
            this.videoTrackBar.Location = new System.Drawing.Point(362, 367);
            this.videoTrackBar.Maximum = 10000;
            this.videoTrackBar.Name = "videoTrackBar";
            this.videoTrackBar.Size = new System.Drawing.Size(320, 45);
            this.videoTrackBar.TabIndex = 20;
            this.videoTrackBar.Scroll += new System.EventHandler(this.videoTrackBar_Scroll);
            // 
            // frameLabel
            // 
            this.frameLabel.Location = new System.Drawing.Point(22, 337);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new System.Drawing.Size(94, 14);
            this.frameLabel.TabIndex = 21;
            this.frameLabel.Text = "Frame";
            // 
            // frameTextbox
            // 
            this.frameTextbox.Enabled = false;
            this.frameTextbox.Location = new System.Drawing.Point(128, 337);
            this.frameTextbox.Name = "frameTextbox";
            this.frameTextbox.Size = new System.Drawing.Size(118, 20);
            this.frameTextbox.TabIndex = 22;
            this.frameTextbox.TextChanged += new System.EventHandler(this.frameTextbox_TextChanged);
            this.frameTextbox.Leave += new System.EventHandler(this.frameTextbox_Leave);
            // 
            // observorTimer
            // 
            this.observorTimer.Enabled = true;
            this.observorTimer.Tick += new System.EventHandler(this.observor_Tick);
            // 
            // saveFrameButton
            // 
            this.saveFrameButton.Location = new System.Drawing.Point(895, 496);
            this.saveFrameButton.Name = "saveFrameButton";
            this.saveFrameButton.Size = new System.Drawing.Size(64, 34);
            this.saveFrameButton.TabIndex = 24;
            this.saveFrameButton.Text = "Sauver";
            this.saveFrameButton.Click += new System.EventHandler(this.saveFrameButton_Click);
            // 
            // frameFileLabel
            // 
            this.frameFileLabel.Location = new System.Drawing.Point(22, 195);
            this.frameFileLabel.Name = "frameFileLabel";
            this.frameFileLabel.Size = new System.Drawing.Size(83, 16);
            this.frameFileLabel.TabIndex = 25;
            this.frameFileLabel.Text = "Fichier Frame";
            // 
            // frameFileText
            // 
            this.frameFileText.Enabled = false;
            this.frameFileText.Location = new System.Drawing.Point(130, 191);
            this.frameFileText.Name = "frameFileText";
            this.frameFileText.Size = new System.Drawing.Size(180, 20);
            this.frameFileText.TabIndex = 26;
            this.frameFileText.TextChanged += new System.EventHandler(this.frameFileText_TextChanged);
            // 
            // generateurLabel
            // 
            this.generateurLabel.Location = new System.Drawing.Point(22, 261);
            this.generateurLabel.Name = "generateurLabel";
            this.generateurLabel.Size = new System.Drawing.Size(83, 16);
            this.generateurLabel.TabIndex = 27;
            this.generateurLabel.Text = "Generateur";
            // 
            // generateurComboBox
            // 
            this.generateurComboBox.FormattingEnabled = true;
            this.generateurComboBox.Location = new System.Drawing.Point(130, 259);
            this.generateurComboBox.Name = "generateurComboBox";
            this.generateurComboBox.Size = new System.Drawing.Size(222, 21);
            this.generateurComboBox.TabIndex = 28;
            this.generateurComboBox.SelectedIndexChanged += new System.EventHandler(this.generateurComboBox_SelectedIndexChanged);
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(130, 296);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 29;
            this.generateButton.Text = "Generer";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(128, 536);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(267, 100);
            this.console.TabIndex = 30;
            this.console.Text = "";
            this.console.TextChanged += new System.EventHandler(this.console_TextChanged);
            // 
            // frameRichTextBox
            // 
            this.frameRichTextBox.Location = new System.Drawing.Point(768, 121);
            this.frameRichTextBox.Name = "frameRichTextBox";
            this.frameRichTextBox.Size = new System.Drawing.Size(259, 263);
            this.frameRichTextBox.TabIndex = 33;
            this.frameRichTextBox.Text = "";
            this.frameRichTextBox.MouseEnter += new System.EventHandler(this.frameRichTextBox_MouseEnter);
            this.frameRichTextBox.MouseLeave += new System.EventHandler(this.frameRichTextBox_MouseLeave);
            // 
            // SaveModifbutton
            // 
            this.SaveModifbutton.Location = new System.Drawing.Point(32, 72);
            this.SaveModifbutton.Name = "SaveModifbutton";
            this.SaveModifbutton.Size = new System.Drawing.Size(60, 34);
            this.SaveModifbutton.TabIndex = 34;
            this.SaveModifbutton.Text = "Save";
            this.SaveModifbutton.UseVisualStyleBackColor = true;
            this.SaveModifbutton.Click += new System.EventHandler(this.SaveModifbutton_Click);
            // 
            // wavPanel
            // 
            this.wavPanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.wavPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wavPanel.Controls.Add(this.groupBox1);
            this.wavPanel.Location = new System.Drawing.Point(768, 536);
            this.wavPanel.Name = "wavPanel";
            this.wavPanel.Size = new System.Drawing.Size(250, 104);
            this.wavPanel.TabIndex = 35;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancelModifButton);
            this.groupBox1.Controls.Add(this.SaveModifbutton);
            this.groupBox1.Location = new System.Drawing.Point(110, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 72);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modifications manuelles";
            this.groupBox1.Visible = false;
            // 
            // cancelModifButton
            // 
            this.cancelModifButton.Location = new System.Drawing.Point(32, 40);
            this.cancelModifButton.Name = "cancelModifButton";
            this.cancelModifButton.Size = new System.Drawing.Size(60, 32);
            this.cancelModifButton.TabIndex = 37;
            this.cancelModifButton.Text = "Cancel";
            this.cancelModifButton.UseVisualStyleBackColor = true;
            this.cancelModifButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // launchToyButton
            // 
            this.launchToyButton.Location = new System.Drawing.Point(237, 296);
            this.launchToyButton.Name = "launchToyButton";
            this.launchToyButton.Size = new System.Drawing.Size(115, 23);
            this.launchToyButton.TabIndex = 36;
            this.launchToyButton.Text = "Lancer toyunda";
            this.launchToyButton.UseVisualStyleBackColor = true;
            this.launchToyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // lineText
            // 
            this.lineText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lineText.Location = new System.Drawing.Point(128, 367);
            this.lineText.Name = "lineText";
            this.lineText.Size = new System.Drawing.Size(40, 20);
            this.lineText.TabIndex = 39;
            this.lineText.TextChanged += new System.EventHandler(this.lineText_TextChanged);
            this.lineText.Leave += new System.EventHandler(this.lineText_Leave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 367);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 14);
            this.label1.TabIndex = 38;
            this.label1.Text = "Ligne";
            // 
            // sylLabel
            // 
            this.sylLabel.Location = new System.Drawing.Point(22, 392);
            this.sylLabel.Name = "sylLabel";
            this.sylLabel.Size = new System.Drawing.Size(94, 14);
            this.sylLabel.TabIndex = 40;
            this.sylLabel.Text = "Syllabe";
            // 
            // sylText
            // 
            this.sylText.Location = new System.Drawing.Point(128, 392);
            this.sylText.Name = "sylText";
            this.sylText.Size = new System.Drawing.Size(40, 20);
            this.sylText.TabIndex = 41;
            this.sylText.TextChanged += new System.EventHandler(this.sylText_TextChanged);
            this.sylText.Leave += new System.EventHandler(this.sylText_Leave);
            // 
            // downLineButton
            // 
            this.downLineButton.Location = new System.Drawing.Point(176, 368);
            this.downLineButton.Name = "downLineButton";
            this.downLineButton.Size = new System.Drawing.Size(34, 20);
            this.downLineButton.TabIndex = 42;
            this.downLineButton.Text = "<";
            this.downLineButton.Click += new System.EventHandler(this.downLineButton_Click);
            // 
            // downSylButton
            // 
            this.downSylButton.Location = new System.Drawing.Point(176, 393);
            this.downSylButton.Name = "downSylButton";
            this.downSylButton.Size = new System.Drawing.Size(34, 20);
            this.downSylButton.TabIndex = 43;
            this.downSylButton.Text = "<";
            this.downSylButton.Click += new System.EventHandler(this.downSylButton_Click);
            // 
            // upLineButton
            // 
            this.upLineButton.Location = new System.Drawing.Point(216, 368);
            this.upLineButton.Name = "upLineButton";
            this.upLineButton.Size = new System.Drawing.Size(34, 20);
            this.upLineButton.TabIndex = 44;
            this.upLineButton.Text = ">";
            this.upLineButton.Click += new System.EventHandler(this.upLineButton_Click);
            // 
            // upSylButton
            // 
            this.upSylButton.Location = new System.Drawing.Point(216, 393);
            this.upSylButton.Name = "upSylButton";
            this.upSylButton.Size = new System.Drawing.Size(34, 20);
            this.upSylButton.TabIndex = 45;
            this.upSylButton.Text = ">";
            this.upSylButton.Click += new System.EventHandler(this.upSylButton_Click);
            // 
            // refreshFrameText
            // 
            this.refreshFrameText.Location = new System.Drawing.Point(965, 496);
            this.refreshFrameText.Name = "refreshFrameText";
            this.refreshFrameText.Size = new System.Drawing.Size(65, 34);
            this.refreshFrameText.TabIndex = 46;
            this.refreshFrameText.Text = "Rafraichir";
            this.refreshFrameText.Click += new System.EventHandler(this.refreshFrameText_Click);
            // 
            // consoleErreur
            // 
            this.consoleErreur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleErreur.ForeColor = System.Drawing.Color.Red;
            this.consoleErreur.Location = new System.Drawing.Point(448, 536);
            this.consoleErreur.Name = "consoleErreur";
            this.consoleErreur.Size = new System.Drawing.Size(238, 100);
            this.consoleErreur.TabIndex = 47;
            this.consoleErreur.Text = "";
            this.consoleErreur.TextChanged += new System.EventHandler(this.consoleErreur_TextChanged);
            // 
            // rateText
            // 
            this.rateText.Location = new System.Drawing.Point(683, 376);
            this.rateText.Name = "rateText";
            this.rateText.Size = new System.Drawing.Size(39, 20);
            this.rateText.TabIndex = 48;
            this.rateText.TextChanged += new System.EventHandler(this.rateText_TextChanged);
            this.rateText.Validated += new System.EventHandler(this.rateText_Leave);
            this.rateText.Leave += new System.EventHandler(this.rateText_Leave);
            // 
            // toyundaLabel
            // 
            this.toyundaLabel.Location = new System.Drawing.Point(22, 231);
            this.toyundaLabel.Name = "toyundaLabel";
            this.toyundaLabel.Size = new System.Drawing.Size(96, 16);
            this.toyundaLabel.TabIndex = 49;
            this.toyundaLabel.Text = "Fichier Toyunda";
            // 
            // toyundaFileText
            // 
            this.toyundaFileText.Enabled = false;
            this.toyundaFileText.Location = new System.Drawing.Point(130, 227);
            this.toyundaFileText.Name = "toyundaFileText";
            this.toyundaFileText.Size = new System.Drawing.Size(180, 20);
            this.toyundaFileText.TabIndex = 50;
            this.toyundaFileText.TextChanged += new System.EventHandler(this.toyundaFileText_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(128, 512);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 14);
            this.label2.TabIndex = 51;
            this.label2.Text = "Console output";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(448, 512);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 52;
            this.label3.Text = "Console error and info";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(769, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 53;
            this.label4.Text = "Info frames";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(769, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 16);
            this.label5.TabIndex = 54;
            this.label5.Text = "ligne/syllable/début frame/fin frame/texte";
            // 
            // lachieusetouch
            // 
            this.lachieusetouch.Location = new System.Drawing.Point(768, 656);
            this.lachieusetouch.Name = "lachieusetouch";
            this.lachieusetouch.Size = new System.Drawing.Size(248, 32);
            this.lachieusetouch.TabIndex = 57;
            this.lachieusetouch.MouseLeave += new System.EventHandler(this.lachieusetouch_MouseLeave);
            this.lachieusetouch.Click += new System.EventHandler(this.lachieusetouch_Click);
            this.lachieusetouch.MouseHover += new System.EventHandler(this.lachieusetouch_MouseHover);
            this.lachieusetouch.MouseEnter += new System.EventHandler(this.lachieusetouch_MouseEnter);
            // 
            // lachieuseLabel
            // 
            this.lachieuseLabel.AutoSize = true;
            this.lachieuseLabel.Location = new System.Drawing.Point(816, 664);
            this.lachieuseLabel.Name = "lachieuseLabel";
            this.lachieuseLabel.Size = new System.Drawing.Size(138, 13);
            this.lachieuseLabel.TabIndex = 56;
            this.lachieuseLabel.Text = "Lachieuse touch -- BOUH !!";
            this.lachieuseLabel.Visible = false;
            // 
            // checkBox_gotoposition
            // 
            this.checkBox_gotoposition.AutoSize = true;
            this.checkBox_gotoposition.Location = new System.Drawing.Point(128, 416);
            this.checkBox_gotoposition.Name = "checkBox_gotoposition";
            this.checkBox_gotoposition.Size = new System.Drawing.Size(105, 17);
            this.checkBox_gotoposition.TabIndex = 58;
            this.checkBox_gotoposition.Text = "Aller à la position";
            this.checkBox_gotoposition.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(363, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(332, 17);
            this.label6.TabIndex = 60;
            this.label6.Text = "Cliquez sur la vidéo pour marquer une frame";
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(818, 496);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(71, 34);
            this.reloadButton.TabIndex = 61;
            this.reloadButton.Text = "Recharger les fichiers";
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // prevColorPanel
            // 
            this.prevColorPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.prevColorPanel.Location = new System.Drawing.Point(20, 45);
            this.prevColorPanel.Name = "prevColorPanel";
            this.prevColorPanel.Size = new System.Drawing.Size(24, 24);
            this.prevColorPanel.TabIndex = 63;
            this.prevColorPanel.BackColorChanged += new System.EventHandler(this.prevColorPanel_BackColorChanged);
            this.prevColorPanel.DoubleClick += new System.EventHandler(this.prevColorPanel_DoubleClick);
            this.prevColorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.prevColorPanel_Paint);
            // 
            // duringColorPanel
            // 
            this.duringColorPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.duringColorPanel.Location = new System.Drawing.Point(63, 45);
            this.duringColorPanel.Name = "duringColorPanel";
            this.duringColorPanel.Size = new System.Drawing.Size(24, 24);
            this.duringColorPanel.TabIndex = 64;
            this.duringColorPanel.DoubleClick += new System.EventHandler(this.duringColorPanel_DoubleClick);
            // 
            // afterColorPanel
            // 
            this.afterColorPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.afterColorPanel.Location = new System.Drawing.Point(107, 45);
            this.afterColorPanel.Name = "afterColorPanel";
            this.afterColorPanel.Size = new System.Drawing.Size(24, 24);
            this.afterColorPanel.TabIndex = 65;
            this.afterColorPanel.DoubleClick += new System.EventHandler(this.afterColorPanel_DoubleClick);
            this.afterColorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // saveModifCheckBox
            // 
            this.saveModifCheckBox.Location = new System.Drawing.Point(923, 447);
            this.saveModifCheckBox.Name = "saveModifCheckBox";
            this.saveModifCheckBox.Size = new System.Drawing.Size(147, 43);
            this.saveModifCheckBox.TabIndex = 67;
            this.saveModifCheckBox.Text = "Prise en compte des modifications manuelles";
            this.saveModifCheckBox.UseVisualStyleBackColor = true;
            this.saveModifCheckBox.CheckedChanged += new System.EventHandler(this.saveModifCheckBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(322, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 20);
            this.button1.TabIndex = 68;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(322, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 20);
            this.button2.TabIndex = 69;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(322, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 20);
            this.button3.TabIndex = 70;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(322, 226);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 20);
            this.button4.TabIndex = 71;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(723, 266);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 97);
            this.trackBar1.TabIndex = 72;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // timeNameLabel
            // 
            this.timeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeNameLabel.Location = new System.Drawing.Point(19, 37);
            this.timeNameLabel.Name = "timeNameLabel";
            this.timeNameLabel.Size = new System.Drawing.Size(295, 47);
            this.timeNameLabel.TabIndex = 73;
            // 
            // operationButton
            // 
            this.operationButton.Location = new System.Drawing.Point(748, 496);
            this.operationButton.Name = "operationButton";
            this.operationButton.Size = new System.Drawing.Size(64, 34);
            this.operationButton.TabIndex = 74;
            this.operationButton.Text = "Operation";
            this.operationButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.videoPanel);
            this.panel1.Location = new System.Drawing.Point(365, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 256);
            this.panel1.TabIndex = 75;
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // couleurgroupBox2
            // 
            this.couleurgroupBox2.Controls.Add(this.label9);
            this.couleurgroupBox2.Controls.Add(this.label8);
            this.couleurgroupBox2.Controls.Add(this.label7);
            this.couleurgroupBox2.Controls.Add(this.prevColorPanel);
            this.couleurgroupBox2.Controls.Add(this.duringColorPanel);
            this.couleurgroupBox2.Controls.Add(this.afterColorPanel);
            this.couleurgroupBox2.Location = new System.Drawing.Point(768, 402);
            this.couleurgroupBox2.Name = "couleurgroupBox2";
            this.couleurgroupBox2.Size = new System.Drawing.Size(149, 88);
            this.couleurgroupBox2.TabIndex = 76;
            this.couleurgroupBox2.TabStop = false;
            this.couleurgroupBox2.Text = "Couleur";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 68;
            this.label9.Text = "Après";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "Pendant";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "Avant";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(688, 242);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 31);
            this.pictureBox1.TabIndex = 77;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(723, 242);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 31);
            this.pictureBox2.TabIndex = 78;
            this.pictureBox2.TabStop = false;
            // 
            // displayConsoleInfocheckBox1
            // 
            this.displayConsoleInfocheckBox1.AutoSize = true;
            this.displayConsoleInfocheckBox1.Location = new System.Drawing.Point(451, 642);
            this.displayConsoleInfocheckBox1.Name = "displayConsoleInfocheckBox1";
            this.displayConsoleInfocheckBox1.Size = new System.Drawing.Size(103, 17);
            this.displayConsoleInfocheckBox1.TabIndex = 79;
            this.displayConsoleInfocheckBox1.Text = "Afficher les infos";
            this.displayConsoleInfocheckBox1.UseVisualStyleBackColor = true;
            this.displayConsoleInfocheckBox1.CheckedChanged += new System.EventHandler(this.displayConsoleInfocheckBox1_CheckedChanged);
            // 
            // récupererParFtpToolStripMenuItem
            // 
            this.récupererParFtpToolStripMenuItem.Name = "récupererParFtpToolStripMenuItem";
            this.récupererParFtpToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.récupererParFtpToolStripMenuItem.Text = "Récuperer par ftp";
            this.récupererParFtpToolStripMenuItem.Click += new System.EventHandler(this.récupererParFtpToolStripMenuItem_Click);
            // 
            // mainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1073, 704);
            this.Controls.Add(this.displayConsoleInfocheckBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.couleurgroupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.operationButton);
            this.Controls.Add(this.timeNameLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveModifCheckBox);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox_gotoposition);
            this.Controls.Add(this.lachieuseLabel);
            this.Controls.Add(this.lachieusetouch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toyundaFileText);
            this.Controls.Add(this.toyundaLabel);
            this.Controls.Add(this.rateText);
            this.Controls.Add(this.consoleErreur);
            this.Controls.Add(this.refreshFrameText);
            this.Controls.Add(this.upSylButton);
            this.Controls.Add(this.upLineButton);
            this.Controls.Add(this.downSylButton);
            this.Controls.Add(this.downLineButton);
            this.Controls.Add(this.sylText);
            this.Controls.Add(this.sylLabel);
            this.Controls.Add(this.lineText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.launchToyButton);
            this.Controls.Add(this.wavPanel);
            this.Controls.Add(this.frameRichTextBox);
            this.Controls.Add(this.console);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.generateurComboBox);
            this.Controls.Add(this.generateurLabel);
            this.Controls.Add(this.frameFileText);
            this.Controls.Add(this.frameFileLabel);
            this.Controls.Add(this.saveFrameButton);
            this.Controls.Add(this.frameTextbox);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.videoTrackBar);
            this.Controls.Add(this.speedTrackBar);
            this.Controls.Add(this.timeButton);
            this.Controls.Add(this.parolesText);
            this.Controls.Add(this.paroleFileLabel);
            this.Controls.Add(this.paroleFileTextBox);
            this.Controls.Add(this.parolesLabel);
            this.Controls.Add(this.videoLabel);
            this.Controls.Add(this.videoFileName);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.nouveauToyMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.nouveauToyMenuStrip;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timing MadShoKen";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.nouveauToyMenuStrip.ResumeLayout(false);
            this.nouveauToyMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoTrackBar)).EndInit();
            this.wavPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.couleurgroupBox2.ResumeLayout(false);
            this.couleurgroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new mainForm());
		}


        enum State
        {
            Uninit,
            Stopped,
            Paused,
            Playing
        }
        State m_State = State.Uninit;
        DxPlay m_play = null;

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            if (mediaControl != null)
            {
                running = true;
                Thread.Sleep(1000);
                mediaControl.Run();
                
            }
        }

        private void btnPause_Click(object sender, System.EventArgs e)
        {
            if (mediaControl != null)
            {
                mediaControl.Pause();
                running = false;
            }
            if (frameChanged)
            {

                int oldPos = frameRichTextBox.SelectionStart;
                this.ActiveControl = this.btnPause;
                paroleManager.displayAllFrames(this.frameRichTextBox);
                frameRichTextBox.SelectionStart = oldPos;
                frameChanged = false;
            }
             
        }

        // Called when the video is finished playing
        private void m_play_StopPlay(Object sender)
        {
            // This isn't the right way to do this, but heck, it's only a sample
            CheckForIllegalCrossThreadCalls = false;

            btnPause.Enabled = false;
            videoFileName.Enabled = true;
            btnStart.Text = "Start";
            btnPause.Text = "Pause";

            CheckForIllegalCrossThreadCalls = true;

            m_State = State.Stopped;

            // Rewind clip to beginning to allow DxPlay.Start to work again.
            m_play.Rewind();
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }



        private void timeButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.timeUp();
        }

        private void timeButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.timeDown();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (mediaSeeking != null)
            {
                mediaSeeking.SetRate((double)speedTrackBar.Value * 2 / speedTrackBar.Maximum);
                double rate;
                mediaSeeking.GetRate(out rate);
                rateText.Text = rate.ToString();
            }
            else
            {
                this.messageNoVideo();
            }
        }

        private void messageNoVideo()
        {
            MessageBox.Show("Aucune vidéo n'a été sélectionéé", "Erreur");
        }


        private void videoTrackBar_Scroll(object sender, EventArgs e)
        {
            if (mediaSeeking != null)
            {

                Guid previousTimeFormat;
                long stopPosition;
                double newPosition;

                mediaSeeking.GetTimeFormat(out previousTimeFormat);
                mediaSeeking.SetTimeFormat(TimeFormat.MediaTime);

                mediaSeeking.GetStopPosition(out stopPosition);
                newPosition = (double)videoTrackBar.Value / videoTrackBar.Maximum;
                mediaSeeking.SetPositions((long)(newPosition * stopPosition), AMSeekingSeekingFlags.AbsolutePositioning, 0, AMSeekingSeekingFlags.NoPositioning);

                mediaSeeking.SetTimeFormat(previousTimeFormat);
            }
            else
            {
                messageNoVideo();
            }
        }



        private void observor_Tick(object sender, EventArgs e)
        {
            long curPos, stopPos;
            

            if (mediaSeeking != null)
            {
                mediaSeeking.GetPositions(out curPos, out stopPos);
                frameTextbox.Text = curPos + " / " + stopPos;
                if (curPos == stopPos)
                {
                    this.running = false;
                }
                if (curPos > 0 && stopPos > 0)
                {
                    Int32Converter conv = new Int32Converter();

                    videoTrackBar.Value = (int) (curPos * videoTrackBar.Maximum / stopPos);
                }
            }
            //paroleManager.displayVisualFrame();

        }

        private void saveFrameButton_Click(object sender, EventArgs e)
        {
            
            paroleManager.saveManualModifs(this.frameRichTextBox);
            paroleManager.saveFramesFile();
            if (MessageBox.Show("Voulez vous appliquer les modifications sur le fichier parole ?", "Sauver fichier parole", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {   
                paroleManager.saveLyricsFile();
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            Boolean valid = true;

            //todo
            String source1 = paroleManager.getLyricsFilepath();
            String source2 = paroleManager.getFramesFilepath();
            String source3 = paroleManager.getToyundaFilepath();

            if (source1 == null)
            {
                valid = false;
                MessageBox.Show("Le fichier parole n'a pas été définit", "Erreur");
            }
            if (source2 == null)
            {
                valid = false;
                MessageBox.Show("Le fichier frame n'a pas été définit", "Erreur");
            }
            if (source3 == null)
            {
                valid = false;
                MessageBox.Show("Le fichier toyunda n'a pas été définit", "Erreur");
            }
            
            if (valid)
            {
                if (!paroleManager.isFrameSaved())
                {
                    if (MessageBox.Show("Le fichier frame n'a pas été enregistré, voulez vous le faire maintenant", "Fichier frame non enregistré", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        paroleManager.saveManualModifs(this.frameRichTextBox);
                        paroleManager.saveFramesFile();
                        if (MessageBox.Show("Voulez vous appliquer les modifications sur le fichier parole ?", "Sauver fichier parole", MessageBoxButtons.YesNo)
                            == DialogResult.Yes)
                        {
                            paroleManager.saveLyricsFile();
                        }
                    }
                }
                //source1 = GenerateurManager.formatForDos(source1);
                //source2 = GenerateurManager.formatForDos(source2);
                //source3 = GenerateurManager.formatForDos(source3);
                generateurManager.launchGeneration(generateurComboBox.SelectedIndex, source1, source2, source3);
            }
            /*string result = generateurManager.launchGeneration(generateurComboBox.SelectedIndex
                , "D:\\Docume~1\\dumestier\\MESDOC~1\\EPITANIME\\svn_epitanime\\toyunda-tools\\trunk\\test\\KazeRan.lyr"
            , "D:\\Docume~1\\dumestier\\MESDOC~1\\EPITANIME\\svn_epitanime\\toyunda-tools\\trunk\\test\\KazeRan.frm");*/
            //console.AppendText(result);
        }

        /**
         * Retourne le nom de la video
         */ 
        internal string getDisplayVideoName()
        {
            string result = "";
            if (this.videoFilepath.Length > 0)
            {
                String[] splitted = videoFilepath.Split(new Char[] { '/', '\\' });
                result = splitted[splitted.Length - 1];
            }
            return result;
        }

        private String askRenameFile(string filename, string ext)
        {
            try
            {
                //timeName = "ba";
                String onlyname = filename.Substring((filename.LastIndexOf("/") > 0 ? filename.LastIndexOf("/") : filename.LastIndexOf("\\")) + 1);
                String onlypath = filename.Substring(0, (filename.LastIndexOf("/") > 0 ? filename.LastIndexOf("/") : filename.LastIndexOf("\\")) + 1);
                if (this.infoTime.name.Length == 0
                    && MessageBox.Show(
                        "Aucun nom définit, voulez vous utiliser celui du fichier", "Renommage", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    setTimeName(new selectName.Info(onlyname));
                }
                if (this.infoTime.name.Length > 0)
                {
                    String newName = infoTime.name + "." + ext;
                    if (onlyname.CompareTo(newName) != 0 && MessageBox.Show(
                        "Renomer le fichier en " + newName + " ? ", "Renommage", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        File.Move(filename, onlypath + newName);
                        //File.Delete(filename);
                        while (File.Exists(filename))
                            ;
                        filename = onlypath + newName;
                        while (!File.Exists(filename))
                            ;
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return filename;
        }

        private void openVideo()
        {
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = "Ouvrir la vidéo";
            this.videoPanel.Enabled = false;
            openFileDialog.DefaultExt = infoTime.name + ".avi";
            openFileDialog.Filter = "*.avi|*.avi";
            openFileDialog.FileName = this.infoTime.name + ".avi";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog.FileName;
                fileName = askRenameFile(fileName, "avi");
                Thread.Sleep(500);
                loadVideo(fileName);
            }
           
            this.videoPanel.Enabled = true;
        }

        private void ouvrirVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openVideo();
        }
        private void loadVideo(String videoPath)
        {
        
                videoFilepath = videoPath;
                videoFileName.Text = getDisplayVideoName();

                if (graph != null)
                {                   
                    graph = null; 
                    
                }
                if (mediaControl != null)
                {
                    // Stop media playback
                    this.mediaControl.Stop();
                    mediaControl = null;
                }

                if (videoWindow != null)
                {
                    videoWindow.put_Owner(IntPtr.Zero);
                    videoWindow = null;
                }

                if (mediaSeeking != null)
                {
                    
                    mediaSeeking = null;  
                }
                if (basicAudio != null)
                {
                    
                    basicAudio = null;
                }
                GC.Collect();

               /* if (mediaPosition != null)
                {
                    mediaPosition = null;
                }*/

                graph = (IGraphBuilder)new FilterGraph();
                mediaControl = (IMediaControl)graph;
                //mediaPosition = (IMediaPosition)graph;
                videoWindow = (IVideoWindow)graph;
                mediaSeeking = (IMediaSeeking)graph;
                basicAudio = (IBasicAudio)graph;
                

                AviSplitter spliter = new AviSplitter();
                graph.AddFilter((IBaseFilter)spliter, null);
                graph.RenderFile(videoPath, null);
                graph.SetDefaultSyncSource();
                
                /*
                 * AMSeekingSeekingCapabilities cap = AMSeekingSeekingCapabilities.CanGetCurrentPos;
                if (mediaSeeking.CheckCapabilities(ref cap) > 0)
                {
                    this.consoleErreur.AppendText("Impossible de recuperer la position de la frame");
                }
                 * */
                

                videoWindow.put_Owner(videoPanel.Handle);

                videoWindow.put_MessageDrain(videoPanel.Handle);


                videoWindow.put_WindowStyle(WindowStyle.Child);
                videoWindow.put_WindowStyleEx(WindowStyleEx.ControlParent);
                videoWindow.put_Left(0);
                videoWindow.put_Top(0);
                videoWindow.put_Width(videoPanel.Width);
                videoWindow.put_Height(videoPanel.Height);
                

                //positionTrackbar.Enabled = true;
                speedTrackBar.Enabled = true;
                mediaSeeking.SetTimeFormat(TimeFormat.Frame);
                
                double rate;
                mediaSeeking.GetRate(out rate);
                rateText.Text = rate.ToString();
                speedTrackBar.Value = (int)(speedTrackBar.Maximum * rate / 2);
                
                trackBar1.Value = trackBar1.Maximum / 2;
                this.basicAudio.put_Volume(-5000 + 5000 * trackBar1.Value / trackBar1.Maximum);    
            //mediaPosition.put_Rate(0.5);
                running = false;
                frameChanged = false;
        }

        private void nouveauFichierFrame_Click(object sender, EventArgs e)
        {
            openFileDialog.CheckFileExists = false;
            openFileDialog.Title = "Nouveau fichier frame";
            openFileDialog.DefaultExt = infoTime.name + ".frm";
            openFileDialog.Filter = "*.frm|*.frm";
            openFileDialog.FileName = this.infoTime.name + ".frm";
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    if (MessageBox.Show("Voulez vous réellement supprimer l'ancien fichier frame (pas de récuperation possible)", "Confirmer la suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        String fileName = openFileDialog.FileName;
                        fileName = askRenameFile(fileName, "frm");
                        File.Delete(fileName);
                        paroleManager.createFrameFile(fileName);
                    }
                }
                else
                {
                    String fileName = openFileDialog.FileName;
                    fileName = askRenameFile(fileName, "frm");
                    paroleManager.createFrameFile(fileName);
                }

            }
           Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void openParoles()
        {
            openFileDialog.Title = "Ouvrir le fichier parole";
            openFileDialog.DefaultExt = infoTime.name + ".lyr";
            openFileDialog.Filter = "*.lyr|*.lyr|*.txt|*.txt";
            openFileDialog.FileName = this.infoTime.name + ".lyr";
            openFileDialog.CheckFileExists = true;
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog.FileName;
                fileName = askRenameFile(fileName, "lyr");
                paroleManager.loadLyricsFile(fileName);
            }
            Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void nouveauParoles()
        {
            openFileDialog.Title = "Nouveau fichier parole";
            openFileDialog.DefaultExt = infoTime.name + ".lyr";
            openFileDialog.Filter = "*.lyr|*.lyr|*.txt|*.txt";
            openFileDialog.FileName = this.infoTime.name + ".lyr";
            openFileDialog.CheckFileExists = false;
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    if (MessageBox.Show("Voulez vous réellement supprimer l'ancien fichier paroles (pas de récuperation possible)", "Confirmer la suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        String fileName = openFileDialog.FileName;
                        fileName = askRenameFile(fileName, "lyr");
                        File.Delete(fileName);
                        FileStream file = File.Create(fileName);
                        file.Close();
                        paroleManager.loadLyricsFile(fileName);
                    }
                }
                else
                {
                    String fileName = openFileDialog.FileName;
                    fileName = askRenameFile(fileName, "lyr");
                    File.Delete(fileName);
                    FileStream file = File.Create(fileName);
                    file.Close();
                    
                    //paroleManager.loadLyricsFile(fileName);
                }
            }
            Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void nouveauFichierParole_Click(object sender, EventArgs e)
        {
            nouveauParoles();
        }

        private void ouvrirFichierParole_Click(object sender, EventArgs e)
        {
            openParoles();
        }



        private void ouvrirFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFrames();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SaveModifbutton_Click(object sender, EventArgs e)
        {
            paroleManager.saveManualModifs(this.frameRichTextBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            
            //info.FileName = GenerateurManager.formatForDos("\"" + System.Configuration.ConfigurationSettings.AppSettings["ToyundaRepository"] + "\\MPlayer-Toyunda\\" + "mplayer-toyunda.exe" +"\"");
            //info.Arguments = GenerateurManager.formatForDos("\"" + this.videoFilepath + "\"" + " -sub " + "\"" + paroleManager.getToyundaFilepath() + "\"" );
            String toyunda_dir = Environment.GetEnvironmentVariable("TOYUNDA_DATA");
            //info.FileName = System.Configuration.ConfigurationSettings.AppSettings["ToyundaRepository"] + "\\MPlayer-Toyunda\\" + "mplayer-toyunda.exe";
            info.FileName = toyunda_dir + "\\mplayer-toyunda.exe";
            if (!File.Exists(info.FileName))
            {
                MessageBox.Show("Impossible de trouver la toyunda à l'addresse : " + info.FileName);
            }

            else if (this.videoFilepath == null || !File.Exists(this.videoFilepath))
            {
                MessageBox.Show("Impossible de trouver la vidéo à l'addresse : " + this.videoFilepath);
            }
            else if (paroleManager.getToyundaFilepath() == null || !File.Exists(paroleManager.getToyundaFilepath()))
            {
                MessageBox.Show("Impossible de trouver le ficher toyunda (avez vous enregistré ?) à l'addresse : " + paroleManager.getToyundaFilepath());
            }
            else
            {

                info.FileName = "\"" + info.FileName + "\"";
                info.Arguments = "\"" + this.videoFilepath + "\"" + " -sub " + "\"" + paroleManager.getToyundaFilepath() + "\"";
                info.UseShellExecute = true;
                info.WorkingDirectory = System.Configuration.ConfigurationSettings.AppSettings["ToyundaRepository"] + "\\MPlayer-Toyunda\\";
                //info.RedirectStandardOutput = true;
                //info.RedirectStandardError = true;
                info.CreateNoWindow = false;
                
                string output = string.Empty;
                string errorOutput = string.Empty;
                try
                {
                    Process p = Process.Start(info);
                    //p.Start();
                    //output = p.StandardOutput.ReadToEnd();
                    //errorOutput = p.StandardError.ReadToEnd();
                    //p.WaitForExit(100000);
                    //p.Close();
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void nouveauFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Nouveau fichier toyunda";
            openFileDialog.DefaultExt = infoTime.name + ".txt";
            openFileDialog.Filter = "*.txt|*.txt|*.toy|*.toy";
            openFileDialog.FileName = this.infoTime.name + ".txt";
            openFileDialog.CheckFileExists = false;
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    if (MessageBox.Show("Voulez vous réellement supprimer l'ancien fichier toyunda (pas de récuperation possible)", "Confirmer la suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        String fileName = openFileDialog.FileName;
                        fileName = askRenameFile(fileName, "txt");
                        File.Delete(fileName);
                        FileStream file = File.Create(fileName);
                        file.Close();
                        paroleManager.setToyundaFilepath(fileName);
                    }
                }
                else
                {
                    String fileName = openFileDialog.FileName;
                    fileName = askRenameFile(fileName, "txt");
                    FileStream file = File.Create(fileName);
                    file.Close();
                    paroleManager.setToyundaFilepath(fileName);
                }

            }
           Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void ouvrirFichierToyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openToyunda();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.ActiveControl = this.cancelModifButton;
            paroleManager.displayAllFrames(this.frameRichTextBox);
        }

        private void rechargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paroleManager.loadLyrics();
        }

        private void recommencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paroleManager.clearFrameList();
        }

        private void videoPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void videoPanel_Click(object sender, EventArgs e)
        {

        }

        private void videoPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.timeDown();
        }

        private void videoPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.timeUp();
        }

        private void parolesText_MouseDown(object sender, MouseEventArgs e)
        {
            this.timeDown();
        }

        private void timeDown()
        {
            if (mediaSeeking != null)
            {
                long position;
                mediaSeeking.GetCurrentPosition(out position);
                paroleManager.setCurrentBeginFrame(position);
                this.frameChanged = true;
            }
            else
                messageNoVideo();
        }

        private void timeUp()
        {
            long position;
            if (mediaSeeking != null)
            {
                mediaSeeking.GetCurrentPosition(out position);
                paroleManager.setCurrentEndFrame(position);
                paroleManager.goNextSyl();
                //paroleManager.displayAllFrames();
            }
            else
                messageNoVideo();
        }

       // private void setVideoPosition()

        private void downLineButton_Click(object sender, EventArgs e)
        {
            paroleManager.goPreviousLine(false);
        }

        private void upLineButton_Click(object sender, EventArgs e)
        {
            paroleManager.goNextLine();
        }

        private void downSylButton_Click(object sender, EventArgs e)
        {
            paroleManager.goPreviousSyl();
        }

        private void upSylButton_Click(object sender, EventArgs e)
        {
            paroleManager.goNextSyl();
        }

        private void refreshFrameText_Click(object sender, EventArgs e)
        {
            this.ActiveControl = this.reloadButton;
            paroleManager.displayAllFrames(this.frameRichTextBox);
        }

        private void rateText_TextChanged(object sender, EventArgs e)
        {
        }

        private void rateText_Leave(object sender, EventArgs e)
        {
            if (mediaSeeking != null)
            {
                double rate;
                rate = double.Parse(rateText.Text);
                mediaSeeking.SetRate(rate);
                speedTrackBar.Value = (int)(speedTrackBar.Maximum * rate / 2);
            }
            else
                messageNoVideo();
        }

        private void parolesText_TextChanged(object sender, EventArgs e)
        {

        }

        private void lineText_Leave(object sender, EventArgs e)
        {
            
        }

        private void sylText_Leave(object sender, EventArgs e)
        {
            
        }

        private void sylText_TextChanged(object sender, EventArgs e)
        {
            try
            {

                paroleManager.goSyl(int.Parse(sylText.Text) - 1);
                if (checkBox_gotoposition.Checked && !running)
                    mediaGoFrame(paroleManager.getCurrentBeginFrame());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sylText.Text = paroleManager.getCurrentSylNumDisplay();
            }
        }

        private void lineText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                paroleManager.goLine(int.Parse(lineText.Text) - 1);
                if (checkBox_gotoposition.Checked && !running)
                {
                    mediaGoFrame(paroleManager.getCurrentBeginFrame());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                lineText.Text = paroleManager.getCurrentLineNumDisplay();
                sylText.Text = paroleManager.getCurrentSylNumDisplay();
            }
        }

        private void mediaGoFrame(long frame)
        {
            if (mediaSeeking != null && frame > 0)
            {

                Guid previousTimeFormat;
                

                mediaSeeking.GetTimeFormat(out previousTimeFormat);
                mediaSeeking.SetTimeFormat(TimeFormat.Frame);

                
                mediaSeeking.SetPositions((long)frame, AMSeekingSeekingFlags.AbsolutePositioning, 0, AMSeekingSeekingFlags.NoPositioning);

                mediaSeeking.SetTimeFormat(previousTimeFormat);
            }
        }

        private void frameTextbox_Leave(object sender, EventArgs e)
        {
            if (mediaSeeking != null)
            {

                Guid previousTimeFormat;
                long stopPosition;
                long newPosition;
                
                mediaSeeking.SetPositions(long.Parse(frameTextbox.Text), AMSeekingSeekingFlags.AbsolutePositioning, 0, AMSeekingSeekingFlags.NoPositioning);

                mediaSeeking.GetTimeFormat(out previousTimeFormat);
                mediaSeeking.SetTimeFormat(TimeFormat.MediaTime);

                mediaSeeking.GetPositions(out newPosition ,out stopPosition);
                videoTrackBar.Value = (int)(videoTrackBar.Maximum * newPosition);
                mediaSeeking.SetTimeFormat(previousTimeFormat);
            }
            else
            {
                messageNoVideo();
            }
        }

        private void nouveauTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectNameForm = new selectName();
            selectNameForm.setNouveauTimeMode();
            selectNameForm.Show(this);
            this.Enabled = false;
        }

        internal void nouveauTime(selectName.Info info)
        {
            setTimeName(info);
            
            ouvrirVideoToolStripMenuItem_Click(null, null);
            ouvrirFichierParole_Click(null, null);
            nouveauFichierFrame_Click(null, null);
            nouveauFichierToolStripMenuItem_Click(null, null);
        }

        internal void cancelNouveauTime()
        {
            selectNameForm.Dispose();
            //this.Enabled = true;
        }

        private void ouvrirTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ouvrirVideoToolStripMenuItem_Click(null, null);
            ouvrirFichierParole_Click(null, null);
            ouvrirFichierToolStripMenuItem_Click(null, null);
            ouvrirFichierToyToolStripMenuItem1_Click(null, null);
        }

        private void toyundaFileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void frameFileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void paroleFileTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void videoFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void console_TextChanged(object sender, EventArgs e)
        {

        }

        private void consoleErreur_TextChanged(object sender, EventArgs e)
        {

        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lachieusetouch_MouseHover(object sender, EventArgs e)
        {
            lachieuseLabel.Visible = true;
        }

        private void lachieusetouch_MouseLeave(object sender, EventArgs e)
        {
            lachieuseLabel.Visible = false;
        }

        private void lachieusetouch_MouseEnter(object sender, EventArgs e)
        {
            lachieuseLabel.Visible = true;
        }

        private void lachieusetouch_Click(object sender, EventArgs e)
        {

        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void generateurComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frameTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void reencoderLaVidéoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.console.Clear();
            this.console.AppendText(Encodeur.encode(this.videoFilepath));
            if (File.Exists(this.videoFilepath.Replace(".avi", "") + "_transcoded.avi"))
            {
                if (MessageBox.Show("Voulez vous utiliser la vidéo réencodée ?", "Changement de vidéo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   loadVideo( this.videoFilepath.Replace(".avi", "") + "_transcoded.avi");
                } 
            }

        }

        private void videoPanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void rechargerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            paroleManager.loadFrameFile();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            paroleManager.loadLyrics();
            paroleManager.loadFrameFile();
        }

        private void frameRichTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (this.saveModifCheckBox.Checked)
            {
                this.ActiveControl = this.saveFrameButton;
                paroleManager.saveManualModifs(this.frameRichTextBox);
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void prevColorPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.prevColorPanel.BackColor = colorDialog1.Color;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void afterColorPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.afterColorPanel.BackColor = colorDialog1.Color;
            }
        }

        private void duringColorPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.duringColorPanel.BackColor = colorDialog1.Color;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            openVideo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openParoles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFrames();
        }

        private void openFrames()
        {
            openFileDialog.Title = "Ouvrir le fichier frame";
            openFileDialog.DefaultExt = infoTime.name + ".frm";
            openFileDialog.Filter = "*.frm|*.frm";
            openFileDialog.FileName = this.infoTime.name + ".frm";
            openFileDialog.CheckFileExists = true;
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog.FileName;
                fileName = askRenameFile(fileName, "frm");
                paroleManager.loadFrameFile(fileName);
            }
            Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openToyunda();
        }

        private void openToyunda()
        {
            openFileDialog.Title = "Ouvrir le fichier toyunda";
            openFileDialog.DefaultExt = infoTime.name + ".txt";
            openFileDialog.Filter = "*.txt|*.txt|*.toy|*.toy";
            openFileDialog.FileName = this.infoTime.name + ".txt";
            openFileDialog.CheckFileExists = false;
            this.videoPanel.Enabled = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog.FileName;
                fileName = askRenameFile(fileName, "txt");
                Boolean loadParole = false;
                Boolean loadFrame = false;
                if (MessageBox.Show("Voulez vous extraire les données paroles ?", "Extraction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.nouveauFichierParole_Click(null, null);
                    loadParole = true;
                }
                if (MessageBox.Show("Voulez vous extraire les données frames ?", "Extraction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.nouveauFichierFrame_Click(null, null);
                    loadFrame = true;
                }
                //paroleManager.setToyundaFilepath(fileName);
                paroleManager.loadToyundaFile(fileName, loadParole, loadFrame);
            }
            Thread.Sleep(500); this.videoPanel.Enabled = true;
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            if (this.basicAudio != null)
            {
                this.basicAudio.put_Volume(-5000 + 5000 * trackBar1.Value / trackBar1.Maximum);
            }
        }

        private void pasDeNomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectNameForm = new selectName();
            selectNameForm.setChangeTimeName();
            selectNameForm.setInfoTime(this.infoTime);
            selectNameForm.Show(this);
            this.Enabled = false;
        }




        internal void setTimeName(selectName.Info info)
        {
            if (selectNameForm != null)
                selectNameForm.Close();
            //this.Enabled = true;
            this.infoTime = info;
            this.timeNameLabel.Text = info.name;
        }

        private void genererFichierIniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Generer fichier ini";
            openFileDialog.CheckFileExists = false;
            openFileDialog.DefaultExt = infoTime.fullname + ".ini";
            openFileDialog.Filter = "*.ini|*.ini";
            openFileDialog.FileName = this.infoTime.fullname + ".ini";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = null;
                FileStream file;
                if (File.Exists(openFileDialog.FileName))
                {
                    if (MessageBox.Show("Voulez vous réellement supprimer l'ancien fichier ini (pas de récuperation possible)", "Confirmer la suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        fileName = openFileDialog.FileName;
                        File.Delete(fileName);
                        file = File.Create(fileName);
                        file.Close();
                        
                        
                    }
                }
                else
                {
                    fileName = openFileDialog.FileName;
                    file = File.Create(fileName);
                    file.Close();
                }
                StreamWriter writer = new StreamWriter(fileName);

                writer.WriteLine("[Micro DVD Ini File]");
                writer.WriteLine("");
                writer.WriteLine("[MAIN]");
                writer.WriteLine("title=" + this.infoTime.fullname + (infoTime.artiste.Length > 0 ? " by " + infoTime.artiste : ""));
                writer.WriteLine("[MOVIE]");
                writer.WriteLine("directory=Videos");
                writer.WriteLine("aviname=" + getDisplayVideoName());
                writer.WriteLine("[SUBTITLES]");
                writer.WriteLine("directory=Lyrics");
                writer.WriteLine("format=0");
                writer.WriteLine("1=" + infoTime.language + " Karaoke");
                writer.WriteLine("file=" + paroleManager.getDisplayToyundaFilename());
                writer.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.operationForm = new operation();
            operationForm.Show(this);
        }




        internal void decallageFrames(int dec)
        {
            this.operationForm.Close();
            //this.Enabled = true;

            String text = this.frameRichTextBox.SelectedText;
            String[] lines = text.Split(new String[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (String line in lines)
            {
                String[] parts = line.Split(new char[] { ' ' });
                int lineNum = 0;
                int sylNum = 0;
                int beginFrame = 0;
                int endFrame = 0;

                if (int.TryParse(parts[0], out lineNum)
                    && int.TryParse(parts[1], out sylNum)
                    && int.TryParse(parts[2], out beginFrame)
                    && int.TryParse(parts[3], out endFrame))
                {
                    paroleManager.setBeginFrame(lineNum - 1, sylNum - 1, beginFrame + dec);
                    paroleManager.setEndFrame(lineNum - 1, sylNum - 1, endFrame + dec);
                }
            }
            paroleManager.displayAllFrames(this.frameRichTextBox);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (frameChanged)
            {
                int oldPos = frameRichTextBox.SelectionStart;
                this.ActiveControl = this.videoPanel;
                paroleManager.displayAllFrames(this.frameRichTextBox);
                frameRichTextBox.SelectionStart = oldPos;
                frameChanged = false;
            }
        }

        private void frameRichTextBox_MouseEnter(object sender, EventArgs e)
        {
            panel1_MouseLeave(null, null);
        }

        private void saveModifCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void displayConsoleInfocheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.generateurManager.setDisplayInfo(displayConsoleInfocheckBox1.Checked);
        }

        private void reverseTimingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ouvrirVideoToolStripMenuItem_Click(null, null);
            ouvrirFichierToyToolStripMenuItem1_Click(null, null);
        }

        private void toyundaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void displayCurrentLine(object sender, EventArgs e)
        {
            this.paroleManager.displayCurrentLine(false, this.parolesText, this.lineText, this.sylText);
        }

        private void displayCurrentLineActive(object sender, EventArgs e)
        {
            this.paroleManager.displayCurrentLine(true, this.parolesText, this.lineText, this.sylText);
        }

        private void displayAllFrames(object sender, EventArgs e)
        {
            this.paroleManager.displayAllFrames(this.frameRichTextBox);
        }

        private void displayFrameFile(object sender, EventArgs e)
        {
            this.frameFileText.Text = paroleManager.getDisplayFrameName();
        }

        private void displayLyricFile(object sender, EventArgs e)
        {
            this.paroleFileTextBox.Text = paroleManager.getDisplayLyricName();
        }

        private void displayToyundFile(object sender, EventArgs e)
        {
            this.toyundaFileText.Text = paroleManager.getDisplayToyundaFilename();
        }
        
        private void displayColor(object sender, EventArgs e)
        {
            Color[] colorArray = paroleManager.getColorArray();
            this.prevColorPanel.BackColor = colorArray[0];
            this.duringColorPanel.BackColor = colorArray[1];
            this.afterColorPanel.BackColor = colorArray[2];
            //this.toyundaFileText.Text = paroleManager.getDisplayToyundaFilename();
        }

        private void prevColorPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void prevColorPanel_BackColorChanged(object sender, EventArgs e)
        {
            this.paroleManager.setColors(new Color[] { this.prevColorPanel.BackColor, this.duringColorPanel.BackColor, this.afterColorPanel.BackColor });
        }

        private void envoyerParFtpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo voir si on garde
            if (false && this.timeNameLabel.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Impossible d'envoyer un time sans nom");
            }
            else
            {
                FtpForm ftpForm = new FtpForm();
                ftpForm.setMode(FtpForm.EMode.UPLOAD);
                
                //this.Enabled = false;
                ftpForm.setInfo(this.paroleManager, this.videoFilepath, null);
                ftpForm.Show();
            }
        }

        private void récupererParFtpToolStripMenuItem_Click(object sender, EventArgs e)
        {


            browserDialog.Tag = "Dossier Cible";
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                        FtpForm ftpForm = new FtpForm();
                        ftpForm.setMode(FtpForm.EMode.DOWNLOAD);
                //todo : ajouter le dossier cible
                        ftpForm.setInfo(this.paroleManager, this.videoFilepath, browserDialog.SelectedPath);
                        ftpForm.Show();
            }
        }
        
    }
}
