using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Windows.Forms;
using TXTRecognizer.Properties;
using TXTRecognizer.Utils;

namespace TXTRecognizer
{
    public partial class frmMain : Form
    {
        #region frmConstrols
        private MenuStrip menuStrip1;
        private ToolStripMenuItem OpenTextFile;
        private ToolStripMenuItem RecognizeText;
        private ToolStripMenuItem ExportMain;
        private ToolStripMenuItem ExpTxt;
        private ToolStripMenuItem ExpWord;
        private ToolStripMenuItem ExpPdf;
        private Panel panelPic;
        private PictureBox pic;
        private TextBox tbTextRec;
        private SplitContainer splitContainer1;
        private Label lblDrop;
        private ToolStripMenuItem LangMenu;
        private Panel panTool;
        private ToolStripMenuItem LangProgramMenu;
        private ToolStripMenuItem RuLang;
        private ToolStripMenuItem UsLang;
        private ToolStripMenuItem LangRecognizeMenu;
        private ToolStripMenuItem RusRec;
        private ToolStripMenuItem EngRec;
        private ToolStripMenuItem KazRec;
        private ToolStripMenuItem DownloadMoreLang;
        private ToolStripMenuItem about;
        private OpenFileDialog ofd;
        #endregion

        #region Main Thread
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
        #endregion

        #region Variable

        private string _filePath = string.Empty;
        private string _lang = "rus";
        private bool _droped = false;
        private List<ToolStripMenuItem> _allItemsMenu = new List<ToolStripMenuItem>();
        private int indexFile = 0;

        #endregion

        public frmMain()
        {
            InitializeComponent();
            Init();
            DownloadMoreLang.Click += (s, a) => { Tools.OpenUrl("https://github.com/tesseract-ocr/tessdata"); };
            about.Click += (s, a) => { new frmAbout().ShowDialog(); };
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OpenTextFile = new System.Windows.Forms.ToolStripMenuItem();
            this.RecognizeText = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMain = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpWord = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.LangMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LangProgramMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RuLang = new System.Windows.Forms.ToolStripMenuItem();
            this.UsLang = new System.Windows.Forms.ToolStripMenuItem();
            this.LangRecognizeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RusRec = new System.Windows.Forms.ToolStripMenuItem();
            this.EngRec = new System.Windows.Forms.ToolStripMenuItem();
            this.KazRec = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadMoreLang = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTextRec = new System.Windows.Forms.TextBox();
            this.panelPic = new System.Windows.Forms.Panel();
            this.lblDrop = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panTool = new System.Windows.Forms.Panel();
            this.about = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panelPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenTextFile,
            this.RecognizeText,
            this.ExportMain,
            this.LangMenu,
            this.about});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(564, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OpenTextFile
            // 
            this.OpenTextFile.Name = "OpenTextFile";
            this.OpenTextFile.Size = new System.Drawing.Size(79, 20);
            this.OpenTextFile.Text = "Открыть...";
            this.OpenTextFile.Click += new System.EventHandler(this.OpenTextFile_Click);
            // 
            // RecognizeText
            // 
            this.RecognizeText.Image = global::TXTRecognizer.Properties.Resources.word_count1;
            this.RecognizeText.Name = "RecognizeText";
            this.RecognizeText.Size = new System.Drawing.Size(133, 20);
            this.RecognizeText.Text = "Распознать текст";
            this.RecognizeText.Click += new System.EventHandler(this.распознатьТекстToolStripMenuItem_Click);
            // 
            // ExportMain
            // 
            this.ExportMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpTxt,
            this.ExpWord,
            this.ExpPdf});
            this.ExportMain.Name = "ExportMain";
            this.ExportMain.Size = new System.Drawing.Size(65, 20);
            this.ExportMain.Text = "Экспорт";
            // 
            // ExpTxt
            // 
            this.ExpTxt.Image = global::TXTRecognizer.Properties.Resources.file_extension_txt1;
            this.ExpTxt.Name = "ExpTxt";
            this.ExpTxt.Size = new System.Drawing.Size(169, 22);
            this.ExpTxt.Text = "Текстовый файл";
            this.ExpTxt.Click += new System.EventHandler(this.ExpTxt_Click);
            // 
            // ExpWord
            // 
            this.ExpWord.Image = global::TXTRecognizer.Properties.Resources.file_extension_wps1;
            this.ExpWord.Name = "ExpWord";
            this.ExpWord.Size = new System.Drawing.Size(169, 22);
            this.ExpWord.Text = "Word";
            // 
            // ExpPdf
            // 
            this.ExpPdf.Image = global::TXTRecognizer.Properties.Resources.file_extension_pdf1;
            this.ExpPdf.Name = "ExpPdf";
            this.ExpPdf.Size = new System.Drawing.Size(169, 22);
            this.ExpPdf.Text = "PDF";
            // 
            // LangMenu
            // 
            this.LangMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LangProgramMenu,
            this.LangRecognizeMenu});
            this.LangMenu.Name = "LangMenu";
            this.LangMenu.Size = new System.Drawing.Size(52, 20);
            this.LangMenu.Text = "Языки";
            // 
            // LangProgramMenu
            // 
            this.LangProgramMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RuLang,
            this.UsLang});
            this.LangProgramMenu.Name = "LangProgramMenu";
            this.LangProgramMenu.Size = new System.Drawing.Size(180, 22);
            this.LangProgramMenu.Text = "Язык программы";
            // 
            // RuLang
            // 
            this.RuLang.Image = global::TXTRecognizer.Properties.Resources.flag_russia1;
            this.RuLang.Name = "RuLang";
            this.RuLang.Size = new System.Drawing.Size(119, 22);
            this.RuLang.Text = "Русский";
            this.RuLang.Click += new System.EventHandler(this.RuLang_Click);
            // 
            // UsLang
            // 
            this.UsLang.Image = global::TXTRecognizer.Properties.Resources.flag_usa1;
            this.UsLang.Name = "UsLang";
            this.UsLang.Size = new System.Drawing.Size(119, 22);
            this.UsLang.Text = "English";
            this.UsLang.Click += new System.EventHandler(this.UsLang_Click);
            // 
            // LangRecognizeMenu
            // 
            this.LangRecognizeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RusRec,
            this.EngRec,
            this.KazRec,
            this.DownloadMoreLang});
            this.LangRecognizeMenu.Name = "LangRecognizeMenu";
            this.LangRecognizeMenu.Size = new System.Drawing.Size(180, 22);
            this.LangRecognizeMenu.Text = "Распознование";
            // 
            // RusRec
            // 
            this.RusRec.CheckOnClick = true;
            this.RusRec.Image = global::TXTRecognizer.Properties.Resources.flag_russia1;
            this.RusRec.Name = "RusRec";
            this.RusRec.Size = new System.Drawing.Size(219, 22);
            this.RusRec.Text = "Русский";
            // 
            // EngRec
            // 
            this.EngRec.CheckOnClick = true;
            this.EngRec.Image = global::TXTRecognizer.Properties.Resources.flag_usa1;
            this.EngRec.Name = "EngRec";
            this.EngRec.Size = new System.Drawing.Size(219, 22);
            this.EngRec.Text = "English";
            // 
            // KazRec
            // 
            this.KazRec.CheckOnClick = true;
            this.KazRec.Image = global::TXTRecognizer.Properties.Resources.flag_kazakhstan;
            this.KazRec.Name = "KazRec";
            this.KazRec.Size = new System.Drawing.Size(219, 22);
            this.KazRec.Text = "қазақ";
            // 
            // DownloadMoreLang
            // 
            this.DownloadMoreLang.Name = "DownloadMoreLang";
            this.DownloadMoreLang.Size = new System.Drawing.Size(219, 22);
            this.DownloadMoreLang.Text = "Скачать больше языков...";
            // 
            // tbTextRec
            // 
            this.tbTextRec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTextRec.Location = new System.Drawing.Point(0, 0);
            this.tbTextRec.Multiline = true;
            this.tbTextRec.Name = "tbTextRec";
            this.tbTextRec.ReadOnly = true;
            this.tbTextRec.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTextRec.Size = new System.Drawing.Size(365, 561);
            this.tbTextRec.TabIndex = 1;
            // 
            // panelPic
            // 
            this.panelPic.AllowDrop = true;
            this.panelPic.Controls.Add(this.lblDrop);
            this.panelPic.Controls.Add(this.pic);
            this.panelPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPic.Location = new System.Drawing.Point(0, 0);
            this.panelPic.Name = "panelPic";
            this.panelPic.Size = new System.Drawing.Size(653, 561);
            this.panelPic.TabIndex = 3;
            this.panelPic.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel2_DragDrop);
            this.panelPic.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel2_DragEnter);
            this.panelPic.DragLeave += new System.EventHandler(this.panel2_DragLeave);
            // 
            // lblDrop
            // 
            this.lblDrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDrop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblDrop.Location = new System.Drawing.Point(211, 246);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(219, 55);
            this.lblDrop.TabIndex = 1;
            this.lblDrop.Text = "Перетащите картинку сюда";
            this.lblDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(653, 561);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            this.ofd.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelPic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbTextRec);
            this.splitContainer1.Size = new System.Drawing.Size(1022, 561);
            this.splitContainer1.SplitterDistance = 653;
            this.splitContainer1.TabIndex = 4;
            // 
            // panTool
            // 
            this.panTool.Controls.Add(this.menuStrip1);
            this.panTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTool.Location = new System.Drawing.Point(0, 0);
            this.panTool.Name = "panTool";
            this.panTool.Size = new System.Drawing.Size(1022, 26);
            this.panTool.TabIndex = 2;
            // 
            // about
            // 
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(107, 20);
            this.about.Text = "О Программе...";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 593);
            this.Controls.Add(this.panTool);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Распознователь текста с картинки";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panTool.ResumeLayout(false);
            this.panTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private void OpenTextFile_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                _filePath = ofd.FileName;
                pic.Image = Image.FromFile(_filePath);
            }
            else MessageBox.Show("Не выбрано изображение", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void распознатьТекстToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_filePath)) throw new Exception("Картнка не выбрана");

                Tesseract tesseract = new Tesseract(Path.Combine(Environment.CurrentDirectory, "Assets"), _lang, OcrEngineMode.TesseractLstmCombined);
                //tesseract.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890");
                tesseract.SetImage(new Image<Bgr, byte>(_filePath));
                tesseract.Recognize();
                tbTextRec.Text = tesseract.GetUTF8Text();

                tesseract.Dispose();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Drag&Drop
        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            _droped = true;
            repaint();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                lblDrop.Text = "Можете отпустить";
            }
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            lblDrop.Text = "";
            _droped = false;
            repaint();
            string tmpFile = Path.Combine(Environment.CurrentDirectory, $"tmp{indexFile++}.png");
            
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);

            _filePath = path[0].ToString();

            var fileExtension = _filePath.Substring(_filePath.LastIndexOf(".") + 1);
            switch (fileExtension)
            {
                case "png": break;
                case "jpg": break;
                case "jpeg": break;
                case "pdf":
                    new Tools(new Fixture()).ConvertPageToSimpleImage(_filePath, tmpFile);
                    _filePath = tmpFile;
                    break;

                default:
                    MessageBox.Show("Неподдерживаемый формат", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            pic.Image = Image.FromFile(_filePath);
            lblDrop.Visible = false;

           
        }

        private void panel2_DragLeave(object sender, EventArgs e)
        {
            lblDrop.Text = "Перетащите картинку сюда";
            _droped = false;
            repaint();
        }

        #region PaintGrap

        private void repaint()
        {
            Pen pen = new(Color.Black, 2);
            pen.DashPattern = _droped ? new float[] { 2, 2 } : new float[] { 0.1F, 0.1F };
            Graphics graph = pic.CreateGraphics();
            graph.DrawRectangle(pen, 1, 1, pic.Width - 2, pic.Height - 2);
        }

        #endregion

        #endregion

        #region Initial Update

        //TODO : Будет добавлена возможность сохранение позже
        private void Init()
        {
            ExpPdf.Enabled = ExpWord.Enabled = false;

            foreach (ToolStripMenuItem toolItem in menuStrip1.Items)
            {
                _allItemsMenu.Add(toolItem);
                _allItemsMenu.AddRange(Tools.GetItems(toolItem));
            }
        }



        #endregion

        private void ExpTxt_Click(object sender, EventArgs e)
        {
            Tools.ExportFile("txt", tbTextRec);
        }

        private void RuLang_Click(object sender, EventArgs e)
        {
            Settings.Default.lang = "ru"; TranslateConstrols();
        }

        private void UsLang_Click(object sender, EventArgs e)
        {
            Settings.Default.lang = "us"; TranslateConstrols();
        }

        void TranslateConstrols()
        {
            Tools.GetAll(this, typeof(Label)).ForEach(xcon =>
            {
                if (Settings.Default.lang == "ru")
                    xcon.Text = Translate.rus.FirstOrDefault(x => x.Key == xcon.Name).Value;
                else
                    xcon.Text = Translate.eng.FirstOrDefault(x => x.Key == xcon.Name).Value;
            });

            _allItemsMenu.ForEach(xcon =>
            {
                if (Settings.Default.lang == "ru")
                    xcon.Text = Translate.rus.FirstOrDefault(x => x.Key == xcon.Name).Value;
                else
                    xcon.Text = Translate.eng.FirstOrDefault(x => x.Key == xcon.Name).Value;
            });
            
        }

  
    }
}