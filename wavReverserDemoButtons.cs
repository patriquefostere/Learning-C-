using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using wavReversing;
using System.Media;


namespace wavReversing
{
	public class RiffParserDemo2 : Form
	{
		#region privateVariables

		private readonly string FILEFILTER = "WAV files (*.wav)|*.wav";
		private OpenFileDialog openFileDialog1;
		private TextBox txtChosenFile;
		private Label lblChooseFile;
		private Label lblChosenFile;
		private Button btnBrowse;
		private Label lblError;
		private Button btnPlay;
		private Label lblFileRev;
		private TextBox txtFileRev;
		private Button btnPlayRev;
		private Button btnReverse;

		private int leftAlign = 32;
		private int middleAlign = 130; 
		
		#endregion

		wavReverser wr;

		public RiffParserDemo2()
		{
			InitializeComponent();
			wr = new wavReverser();
		}

	
		private void InitializeComponent()
		{
			this.SuspendLayout();

			// openFileDialog1
			this.openFileDialog1 = new OpenFileDialog();
			this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);

			
			// lblChooseFile
			this.lblChooseFile = new Label();
			this.lblChooseFile.Location = new Point(leftAlign, 24);
			this.lblChooseFile.Name = "lblChooseFile";
			this.AutoSize = true;
			this.lblChooseFile.TabIndex = 0;
			this.lblChooseFile.Text = "Choose a .wav file to reverse: ";
			this.lblChooseFile.TextAlign = ContentAlignment.MiddleLeft;

			// btnBrowse
			this.btnBrowse = new Button();
			this.btnBrowse.Location = new Point(middleAlign, 24);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new Size(80, 20);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);

			//lblChosenFile
			this.lblChosenFile = new Label();
			this.lblChosenFile.Location = new Point(leftAlign,48);
			this.lblChosenFile.Name = "lblChosenFile";
			//this.lblChosenFile.Size = new Size(150,24);
			this.lblChosenFile.AutoSize = true;
			this.lblChosenFile.TabIndex = 2;
			this.lblChosenFile.Text = "File chosen:";
			this.lblChooseFile.TextAlign = ContentAlignment.MiddleLeft;


			// txtChosenFile
			this.txtChosenFile = new TextBox();
			this.txtChosenFile.Location = new Point(middleAlign, 48);
			this.txtChosenFile.Name = "txtChosenFile";
			this.txtChosenFile.TabIndex = 3;
			this.txtChosenFile.Text = "";
			this.txtChosenFile.Size = new Size(150,24);
			
			// btnPlay
			this.btnPlay = new Button();
			this.btnPlay.Location = new Point(middleAlign, 72);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new Size(60, 20);
			this.btnPlay.TabIndex = 4;
			this.btnPlay.Text = "Play";
			this.btnPlay.Click += new EventHandler(this.btnPlay_Click);
			this.lblChooseFile.TextAlign = ContentAlignment.MiddleLeft;

			//btnReverse
			this.btnReverse = new Button();
			this.btnReverse.Location = new Point(leftAlign, 100);
			this.btnReverse.Name = "btnReverse";
			this.btnReverse.Size = new Size(80,40);
			this.btnReverse.TabIndex = 5;
			this.btnReverse.Text = "Reverse";
			this.btnReverse.Click += new EventHandler(this.btnReverse_Click);

			// lblFileRev
			this.lblFileRev = new Label();
			this.lblFileRev.Location = new Point(leftAlign,150);
			this.lblFileRev.Name = "lblFileRev";
			//this.lblFileRev.Size = new Size(100,32);
			this.lblFileRev.AutoSize = true;
			this.lblFileRev.TabIndex = 6;
			this.lblFileRev.Text = "Reversed file:";
			this.lblChooseFile.TextAlign = ContentAlignment.MiddleLeft;


			// txtFileRev
			this.txtFileRev = new TextBox();
			this.txtFileRev.Location = new Point(middleAlign, 150);
			this.txtFileRev.Name = "txtFileRev";
			this.txtFileRev.TabIndex = 7;
			this.txtFileRev.Text = "";
			this.txtChosenFile.Size = new Size(150,24);


			// btnPlayRev
			this.btnPlayRev = new Button();
			this.btnPlayRev.Location = new Point(middleAlign, 174);
			this.btnPlayRev.Name = "btnPlayRev";
			this.btnPlayRev.Size = new Size(60, 20);
			this.btnPlayRev.TabIndex = 8;
			this.btnPlayRev.Text = "Play";
			this.btnPlayRev.Click += new EventHandler(this.btnPlayRev_Click);

			

			// lblError
			this.lblError = new Label();
			this.lblError.BorderStyle = BorderStyle.FixedSingle;
			this.lblError.ForeColor = Color.Red;
			this.lblError.Location = new Point(leftAlign,368-80);
			this.lblError.Name = "lblError";
			this.lblError.Size = new Size(578-64, 70);
			this.lblError.TabIndex = 9;
			this.lblError.Visible = false;

			// main window
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(400, 200);
			this.Controls.Add(this.lblError);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtChosenFile);
			this.Controls.Add(this.lblChooseFile);
			this.Controls.Add(this.lblChosenFile);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.lblFileRev);
			this.Controls.Add(this.txtFileRev);
			this.Controls.Add(this.btnPlayRev);
			this.Controls.Add(this.btnReverse);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Name = "wavReverserDemo";
			this.Text = "wav Reversing";
			this.ResumeLayout(false);
		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new RiffParserDemo2());
		}

		private void Clear()
		{
			lblError.Visible = false;
			txtChosenFile.Text = "";
			txtFileRev.Text = "";
		}

		private void resizeToText(Button formObject)
		{
			Size size = TextRenderer.MeasureText(formObject.Text, formObject.Font);
			formObject.Width = size.Width+10;
			formObject.Height = size.Height+10;
		}

		private void resizeToText(TextBox formObject)
		{
			Size size = TextRenderer.MeasureText(formObject.Text, formObject.Font);
			formObject.Width = size.Width;
			formObject.Height = size.Height;
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			Clear();
			txtChosenFile.Text = openFileDialog1.FileName;

			resizeToText(txtChosenFile);
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			Clear();
			openFileDialog1.DefaultExt = "avi";
			openFileDialog1.Filter = FILEFILTER;
			openFileDialog1.Multiselect = false;
			openFileDialog1.ReadOnlyChecked = true;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.Title = "Choose a .wav file";
			openFileDialog1.ShowDialog();
		}

		private void btnPlay_Click(object sender, EventArgs e)
		{
			myPlay(txtChosenFile.Text);
		}

		private void btnPlayRev_Click(object sender, EventArgs e)
		{
			myPlay(txtFileRev.Text);
		}

		private void btnReverse_Click(object sender, EventArgs e)
		{
			txtFileRev.Text = wr.semiMain(txtChosenFile.Text);
			resizeToText(txtFileRev);
		}

		private void myPlay(string file)
		{
			try
			{
				SoundPlayer player = new SoundPlayer(file);
				player.Play();
			}
			catch (Exception ex) 
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}
	}
}
