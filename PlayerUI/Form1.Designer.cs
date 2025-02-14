namespace PlayerUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			btnPrev = new Button();
			btnPlay = new Button();
			btnNext = new Button();
			listPlay = new ListBox();
			listSongs = new ListBox();
			trackBar1 = new TrackBar();
			textMultiPurpose = new TextBox();
			btnImportAlbum = new Button();
			labelSong = new Label();
			labelArtist = new Label();
			btnImportSong = new Button();
			listQueue = new ListBox();
			btnSelectedToPlaylist = new Button();
			((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
			SuspendLayout();
			// 
			// btnPrev
			// 
			btnPrev.Anchor = AnchorStyles.Bottom;
			btnPrev.Location = new Point(254, 314);
			btnPrev.Name = "btnPrev";
			btnPrev.Size = new Size(50, 40);
			btnPrev.TabIndex = 0;
			btnPrev.Text = "Prev";
			btnPrev.UseVisualStyleBackColor = true;
			btnPrev.Click += btnPrev_Click;
			// 
			// btnPlay
			// 
			btnPlay.Anchor = AnchorStyles.Bottom;
			btnPlay.Location = new Point(310, 314);
			btnPlay.Name = "btnPlay";
			btnPlay.Size = new Size(50, 40);
			btnPlay.TabIndex = 1;
			btnPlay.Text = "Play";
			btnPlay.UseVisualStyleBackColor = true;
			btnPlay.Click += btnPlay_Click;
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Bottom;
			btnNext.Location = new Point(366, 314);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(50, 40);
			btnNext.TabIndex = 2;
			btnNext.Text = "Next";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += btnNext_Click;
			// 
			// listPlay
			// 
			listPlay.FormattingEnabled = true;
			listPlay.ItemHeight = 20;
			listPlay.Location = new Point(12, 12);
			listPlay.Name = "listPlay";
			listPlay.Size = new Size(180, 104);
			listPlay.TabIndex = 3;
			listPlay.SelectedIndexChanged += listPlay_SelectedIndexChanged;
			listPlay.MouseDoubleClick += listPlay_MouseDoubleClick;
			// 
			// listSongs
			// 
			listSongs.AllowDrop = true;
			listSongs.FormattingEnabled = true;
			listSongs.ItemHeight = 20;
			listSongs.Location = new Point(198, 12);
			listSongs.Name = "listSongs";
			listSongs.SelectionMode = SelectionMode.MultiExtended;
			listSongs.Size = new Size(220, 104);
			listSongs.TabIndex = 5;
			listSongs.SelectedIndexChanged += listSongs_SelectedIndexChanged;
			listSongs.DragDrop += listSongs_DragDrop;
			listSongs.DragOver += listSongs_DragOver;
			listSongs.MouseDown += listSongs_MouseDown;
			// 
			// trackBar1
			// 
			trackBar1.Anchor = AnchorStyles.Bottom;
			trackBar1.LargeChange = 1;
			trackBar1.Location = new Point(9, 412);
			trackBar1.Maximum = 500;
			trackBar1.Name = "trackBar1";
			trackBar1.Size = new Size(645, 56);
			trackBar1.TabIndex = 6;
			trackBar1.TickFrequency = 0;
			// 
			// textMultiPurpose
			// 
			textMultiPurpose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			textMultiPurpose.Location = new Point(12, 122);
			textMultiPurpose.Name = "textMultiPurpose";
			textMultiPurpose.Size = new Size(226, 27);
			textMultiPurpose.TabIndex = 7;
			// 
			// btnImportAlbum
			// 
			btnImportAlbum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImportAlbum.Location = new Point(12, 155);
			btnImportAlbum.Name = "btnImportAlbum";
			btnImportAlbum.Size = new Size(110, 40);
			btnImportAlbum.TabIndex = 8;
			btnImportAlbum.Text = "Import album";
			btnImportAlbum.UseVisualStyleBackColor = true;
			btnImportAlbum.Click += btnImportAlbum_Click;
			// 
			// labelSong
			// 
			labelSong.Anchor = AnchorStyles.Bottom;
			labelSong.AutoSize = true;
			labelSong.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelSong.Location = new Point(150, 356);
			labelSong.MinimumSize = new Size(370, 0);
			labelSong.Name = "labelSong";
			labelSong.Size = new Size(370, 28);
			labelSong.TabIndex = 9;
			labelSong.Text = "Nothing is playing";
			labelSong.TextAlign = ContentAlignment.BottomCenter;
			labelSong.UseMnemonic = false;
			// 
			// labelArtist
			// 
			labelArtist.Anchor = AnchorStyles.Bottom;
			labelArtist.AutoSize = true;
			labelArtist.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			labelArtist.Location = new Point(236, 384);
			labelArtist.MaximumSize = new Size(200, 0);
			labelArtist.MinimumSize = new Size(200, 0);
			labelArtist.Name = "labelArtist";
			labelArtist.Size = new Size(200, 23);
			labelArtist.TabIndex = 10;
			labelArtist.Text = "-";
			labelArtist.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnImportSong
			// 
			btnImportSong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnImportSong.Location = new Point(128, 155);
			btnImportSong.Name = "btnImportSong";
			btnImportSong.Size = new Size(110, 40);
			btnImportSong.TabIndex = 11;
			btnImportSong.Text = "Import song";
			btnImportSong.UseVisualStyleBackColor = true;
			btnImportSong.Click += btnImportSong_Click;
			// 
			// listQueue
			// 
			listQueue.AllowDrop = true;
			listQueue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			listQueue.FormattingEnabled = true;
			listQueue.ItemHeight = 20;
			listQueue.Location = new Point(424, 12);
			listQueue.Name = "listQueue";
			listQueue.SelectionMode = SelectionMode.MultiExtended;
			listQueue.Size = new Size(220, 304);
			listQueue.TabIndex = 12;
			listQueue.SelectedIndexChanged += listQueue_SelectedIndexChanged;
			listQueue.DragDrop += listQueue_DragDrop;
			listQueue.DragOver += listQueue_DragOver;
			listQueue.MouseDown += listQueue_MouseDown;
			// 
			// btnSelectedToPlaylist
			// 
			btnSelectedToPlaylist.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnSelectedToPlaylist.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
			btnSelectedToPlaylist.Location = new Point(12, 201);
			btnSelectedToPlaylist.Name = "btnSelectedToPlaylist";
			btnSelectedToPlaylist.Size = new Size(226, 40);
			btnSelectedToPlaylist.TabIndex = 15;
			btnSelectedToPlaylist.Text = "Selected to playlist";
			btnSelectedToPlaylist.UseVisualStyleBackColor = true;
			btnSelectedToPlaylist.Click += btnSelectedToPlaylist_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(662, 453);
			Controls.Add(btnSelectedToPlaylist);
			Controls.Add(listQueue);
			Controls.Add(btnImportSong);
			Controls.Add(labelArtist);
			Controls.Add(labelSong);
			Controls.Add(btnImportAlbum);
			Controls.Add(textMultiPurpose);
			Controls.Add(trackBar1);
			Controls.Add(listSongs);
			Controls.Add(listPlay);
			Controls.Add(btnNext);
			Controls.Add(btnPlay);
			Controls.Add(btnPrev);
			MinimumSize = new Size(680, 500);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnPrev;
        private Button btnPlay;
        private Button btnNext;
        private ListBox listPlay;
        private ListBox listSongs;
        private TrackBar trackBar1;
        private TextBox textMultiPurpose;
        private Button btnImportAlbum;
        private Label labelSong;
        private Label labelArtist;
        private Button btnImportSong;
		private ListBox listQueue;
		private Button btnSelectedToPlaylist;
	}
}
