using BackendThings.Objects;
using BackendThings.Processing;
using System;
using System.Runtime.InteropServices;
using DoublyLL = C5.HashedLinkedList<BackendThings.Objects.Song>;
namespace PlayerUI
{
	public partial class Form1 : Form
	{
		JSONProcessor processor;

		int oldIndex, oldIndexQ;
		string selectedPlaylist, selectedSonginPlaylist, selectedSongInQueue;
		List<string> playlistNames;
		public Form1() {
			InitializeComponent();
			playlistNames = new List<string>();
			processor = new JSONProcessor();
			processor.LoadPlaylists(playlistNames);
			selectedPlaylist = "";
			selectedSonginPlaylist = "";
			selectedSongInQueue = "";
			playlistsIntoLB();
		}
		void playlistsIntoLB() {
			listPlay.Items.Clear();
			foreach (string playlist in playlistNames)
			{
				listPlay.Items.Add(playlist);
			}
		}
		void songsIntoLBofPlaylist() {
			DoublyLL songs = new DoublyLL();
			processor.GetPlaylist(selectedPlaylist, songs);
			listSongs.Items.Clear();
			if (songs.Count == 0) { return; }
			foreach (Song song in songs)
			{
				listSongs.Items.Add($"{song.Artist} - {song.Title}");
			}
		}
		void UpdateQueue() {
			int currentlyPlaying = listQueue.SelectedIndex;
			DoublyLL songs = processor.GetQueue();
			listQueue.Items.Clear();
			foreach (Song song in songs)
			{
				listQueue.Items.Add($"{song.Artist} - {song.Title}");
			}
			listQueue.SelectedIndex = currentlyPlaying;
			listSongs.SelectedIndex = -1;
		}
		private void listSongs_SelectedIndexChanged(object sender, EventArgs e) {
			if (listSongs.SelectedItem == null && selectedSonginPlaylist != null)
			{
				listSongs.SelectedItem = selectedSonginPlaylist;
				//listQueue.SelectedIndex = -1;
				return;
			}
			if (listSongs.SelectedItem != null)
			{
				selectedSonginPlaylist = listSongs.SelectedItem.ToString();
				listQueue.SelectedIndex = -1;
				return;
			}
		}

		private void listSongs_DragDrop(object sender, DragEventArgs e) {
			int newIndex = this.listSongs.IndexFromPoint(listSongs.PointToClient(new Point(e.X, e.Y)));
			if (newIndex < 0) newIndex = listSongs.Items.Count - 1;
			string listSongsItem = listSongs.Items[oldIndex].ToString();
			//swap the songs around
			this.listSongs.Items.Remove(listSongsItem);
			this.listSongs.Items.Insert(newIndex, listSongsItem);
			processor.ChangePositionInPlaylist(oldIndex, newIndex, listPlay.SelectedItem.ToString());
		}

		private void listSongs_DragOver(object sender, DragEventArgs e) {
			e.Effect = DragDropEffects.Move;
		}

		private void listSongs_MouseDown(object sender, MouseEventArgs e) {
			if (this.listSongs.SelectedItem == null||this.listSongs.SelectedItems.Count==0) return;
			oldIndex = this.listSongs.SelectedIndex;
			this.listSongs.DoDragDrop(this.listSongs.SelectedItem, DragDropEffects.Move);
		}
		private void listPlay_SelectedIndexChanged(object sender, EventArgs e) {
			if (listPlay.SelectedItem == null) return;
#pragma warning disable CS8601 // Possible null reference assignment.
			selectedPlaylist = listPlay.SelectedItem.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
		}
		private void listPlay_MouseDoubleClick(object sender, MouseEventArgs e) {
			songsIntoLBofPlaylist();
		}
		private void btnImportSong_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.Filter = "Audio file |*.flac;*.m4a;*.mp3;*.wav";
			ofd.ShowDialog();
			textMultiPurpose.Text = ofd.FileName;
			if (ofd.FileName != "")
			{
				processor.ImportSong(ofd.FileName);
				UpdateQueue();
			}

		}
		private void btnImportAlbum_Click(object sender, EventArgs e) {
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowDialog();
			//folderWithSongs = folderBrowserDialog.SelectedPath;
			textMultiPurpose.Text = folderBrowserDialog.SelectedPath;
			if (folderBrowserDialog.SelectedPath != "")
			{
				processor.ImportFolder(folderBrowserDialog.SelectedPath);
				UpdateQueue();
			}
		}
		private void btnPlay_Click(object sender, EventArgs e) {
			if (listSongs.SelectedIndex != -1)
			{
				DoublyLL queue = new DoublyLL();
				processor.GetPlaylist(selectedPlaylist, queue);
				listQueue.Items.Clear();
				foreach (Song song in queue)
				{
					listQueue.Items.Add($"{song.Artist} - {song.Title}");
				}
				labelArtist.Text = queue[listSongs.SelectedIndex].Artist;
				labelSong.Text = queue[listSongs.SelectedIndex].Title;
				listQueue.SelectedIndex = listSongs.SelectedIndex;
				listSongs.SelectedIndex = -1;
			}
			else if (listQueue.SelectedIndex != -1)
			{
				labelArtist.Text = listQueue.SelectedItem.ToString().Split(" - ")[0];
				labelSong.Text = listQueue.SelectedItem.ToString().Split(" - ")[1];
			}
		}
		bool ValidPlaylistName() {
			return (!Directory.Exists(textMultiPurpose.Text) && textMultiPurpose.Text != "");
		}
		private void btnSelectedToPlaylist_Click(object sender, EventArgs e) {
			if (ValidPlaylistName() && listPlay.FindString(textMultiPurpose.Text) == ListBox.NoMatches&&listQueue.SelectedIndices.Count>0)
			{
				DoublyLL selection = new DoublyLL();
				foreach (int i in listQueue.SelectedIndices)		//throws exception for some reason. idk why.
				{
					selection.Add(processor.GetSongFromQueue(i));
				}
				processor.AddToPlaylist(selection, textMultiPurpose.Text);
				processor.SavePlaylist(textMultiPurpose.Text);
				playlistNames.Add(textMultiPurpose.Text);
				playlistsIntoLB();
			}
		}
		private void btnNext_Click(object sender, EventArgs e) {
			string[] currentlyPlaying;
			int index;
			if (listQueue.SelectedIndex < listQueue.Items.Count - 1)
			{
				index = listQueue.SelectedIndex;
				listQueue.SelectedIndices.Clear();
				listQueue.SelectedIndex = index + 1;
				currentlyPlaying = listQueue.SelectedItem.ToString().Split(" - ");
				labelArtist.Text = currentlyPlaying[0];
				labelSong.Text = currentlyPlaying[1];
				//listSongs.SelectedIndex = listSongs.SelectedIndex + 1;
			}
		}

		private void btnPrev_Click(object sender, EventArgs e) {
			string[] currentlyPlaying;
			int index;
			if (listQueue.SelectedIndex > 0)
			{
				index = listQueue.SelectedIndex;
				listQueue.SelectedIndices.Clear();
				listQueue.SelectedIndex = index - 1;
				currentlyPlaying = listQueue.SelectedItem.ToString().Split(" - ");
				labelArtist.Text = currentlyPlaying[0];
				labelSong.Text = currentlyPlaying[1];
				//listSongs.SelectedIndex = listSongs.SelectedIndex - 1;
			}
		}

		private void listQueue_SelectedIndexChanged(object sender, EventArgs e) {
			listSongs.SelectedIndex = -1;
		}

		private void listQueue_DragDrop(object sender, DragEventArgs e) {
			int newIndex = this.listQueue.IndexFromPoint(listQueue.PointToClient(new Point(e.X, e.Y)));
			if(newIndex<0) newIndex = listQueue.Items.Count - 1;
			string listQueueItem = listQueue.Items[oldIndexQ].ToString();
			this.listQueue.Items.Remove(listQueueItem);
			this.listQueue.Items.Insert(newIndex, listQueueItem);
			processor.ChangePositionInQueue(oldIndexQ, newIndex);
		}

		private void listQueue_DragOver(object sender, DragEventArgs e) {
			e.Effect=DragDropEffects.Move;
		}

		private void listQueue_MouseDown(object sender, MouseEventArgs e) {
			if (this.listQueue.SelectedItem == null) return;
			oldIndexQ = this.listQueue.SelectedIndex;
			this.listQueue.DoDragDrop(this.listQueue.SelectedItem, DragDropEffects.Move);
		}
	}
}
