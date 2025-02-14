using System;
using BackendThings.Objects;
using DoublyLL = C5.HashedLinkedList<BackendThings.Objects.Song>;
using System.Text.Json;
using scg = System.Collections.Generic;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Text;
using TagLib;
using System.IO;
using System.Linq;
/*
File structure:
{"Name": "<playlist name>",
"Songs":[
{"Title":"Song title","Artist":"artist","FilePath":"filePath", "Length":"xMxS"},
{"Title":"Song title","Artist":"artist","FilePath":"filePath", "Length":"xMxS"},
{"Title":"Song title","Artist":"artist","FilePath":"filePath", "Length":"xMxS"}
]}
*/
namespace BackendThings.Processing
{
	public class JSONProcessor
	{
		string appDirectory;
		DoublyLL currentPlay;  //couldn't find a better doubly linked list implementation
		Dictionary<string, DoublyLL> allPlaylists;          //string is playlist name, DoublyLL contains the playlist
		LinkedList<string> fileList;
		JsonSerializerOptions op = new JsonSerializerOptions();
		public JSONProcessor() {
			op.Converters.Add(new SongJSONConverter());
			appDirectory = Directory.GetCurrentDirectory().ToString() + "\\Playlists\\"; //not really app directory is it?
			currentPlay = new DoublyLL();
			allPlaylists = new Dictionary<string, DoublyLL>();
			fileList = new LinkedList<string>();
		}
		public void UnpackPlaylist(string playlistFile, List<string> names) {
			string jsonString = System.IO.File.ReadAllText(Path.Combine(appDirectory, playlistFile)), name;
			JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
			names.Add(jsonDocument.RootElement.GetProperty("Name").ToString());
			name = names[names.Count - 1];
			var songs = JsonSerializer.Deserialize<DoublyLL>(jsonDocument.RootElement.GetProperty("Songs").GetRawText(), op);
			if (songs != null && name != null)
			{
				allPlaylists[name] = songs;
			}
		}
		public void LoadPlaylists(List<string> names) {
			string[] files = Directory.GetFiles(appDirectory, "*.json");
			foreach (string file in files)
			{
				UnpackPlaylist(file, names);
			}
		}

		public void SavePlaylist(string playlistName) {
			string songCustom;
			StringBuilder jsonString = new StringBuilder();
			jsonString.Append($"{{\"Name\": \"{playlistName}\",\n\"Songs\":[\n");
			foreach (Song song in allPlaylists[playlistName])
			{
				//jsonString.AppendLine(JsonSerializer.Serialize(song) + ",\n");
				songCustom = $"{{\"Title\":\"{song.Title}\"," +
					$"\"Artist\":\"{song.Artist}\"," +
					$"\"FilePath\":\"{song.FilePath}\"," +
					$"\"Length\":\"{song.Length.ToString()}\"," +
					$"\"Album\":\"{song.Album}\"," +
					$"\"NumberInAlbum\":\"{song.NumberInAlbum}\"," +
					$"\"Bitrate\":\"{song.Bitrate}\"," +
					$"\"FileType\":\"{song.FileType}\"," +
					$"\"IsDeleted\":\"{song.IsDeleted}\"}},\n";
				jsonString.Append(songCustom);
			}
			jsonString.Remove(jsonString.Length - 2, 1);
			jsonString.Append("\n]}");
			string destination = Path.Combine(appDirectory, $"{playlistName}.json");
			System.IO.File.WriteAllText(destination, jsonString.ToString());
		}

		public void ImportFolder(string location) {
			/*string[] files;
            TagLib.File tFile;
            files = Directory.EnumerateFiles(location)
                .Where(file => file.ToLower().EndsWith("flac") ||
                file.ToLower().EndsWith("m4a") || file.ToLower().EndsWith("mp3") ||
                file.ToLower().EndsWith("wav")).ToArray();
            foreach (string file in files)
            {
                tFile = TagLib.File.Create(file);
                if (tFile.Tag.FirstAlbumArtist != null)
                {
                    currentPlay.Add(new Song(tFile.Tag.Title, tFile.Tag.FirstAlbumArtist, file, tFile.Properties.Duration));
                }

                else
                {
                    currentPlay.Add(new Song(Path.GetFileName(file), "No artist", file, tFile.Properties.Duration));
                }
            }*/

			string[] files, fileData;
			TagLib.File tFile;
			files = Directory.EnumerateFiles(location)
				.Where(file => file.ToLower().EndsWith("flac") ||
				file.ToLower().EndsWith("m4a") || file.ToLower().EndsWith("mp3") ||
				file.ToLower().EndsWith("wav")).ToArray();
			foreach (string file in files)
			{
				tFile = TagLib.File.Create(file);
				fileList.AddLast(file);
				fileData = Path.GetFileName(file).Split(" - ");           //Artist - Album - Year - NN - Name.type
				AddToQueue(new Song(fileData[4].Split(".")[0], fileData[0], file, tFile.Properties.Duration, fileData[1],
					Int32.Parse(fileData[3]), tFile.Properties.AudioBitrate, fileData[4].Split(".")[1], false));    //it would've been better to pull metadata using tFile but the task was to read from file names :(
			}
		}
		public void ImportSong(string file) {
			string[] fileData;
			TagLib.File tFile = TagLib.File.Create(file);
			fileList.AddLast(file);
			fileData = Path.GetFileName(file).Split(" - ");           //Artist - Album - Year - NN - Name.type
			if (fileData.Length == 5)
				AddToQueue(new Song(fileData[4].Split(".")[0], fileData[0], file, tFile.Properties.Duration, fileData[1],
				Int32.Parse(fileData[3]), tFile.Properties.AudioBitrate, fileData[4].Split(".")[1], false));
		}
		public void CreatePlaylist(string name) {
			if (!allPlaylists.ContainsKey(name))
			{
				allPlaylists[name] = new DoublyLL();
			}
			else { return; }
		}
		public void AddToPlaylist(DoublyLL songs, string playlist) {
			CreatePlaylist(playlist);
			foreach (Song song in songs)
			{
				allPlaylists[playlist].Add(song);
			}
		}
		public void GetPlaylist(string playlist, DoublyLL songs) {
			if (playlist != "")
			{
				currentPlay.Clear();
				foreach (Song song in allPlaylists[playlist])
				{
					currentPlay.Add(song);
					songs.Add(song);
				}
			}
		}
		public Song GetPlaylistSong(string playlist, int index) {
			return allPlaylists[playlist][index];
		}
		public void ChangePositionInPlaylist(int oldIndex, int newIndex, string playlist) {
			allPlaylists[playlist].Insert(newIndex, allPlaylists[playlist].RemoveAt(oldIndex));
		}
		public void RemoveFromPlaylist(Song song, string playlist) {
			allPlaylists[playlist].Remove(song);
		}
		public void AddToQueue(Song song) {
			currentPlay.Add(song);
		}
		public void ChangePositionInQueue(int oldIndex, int newIndex) {
			currentPlay.Insert(newIndex, currentPlay.RemoveAt(oldIndex));
		}
		public void RemoveFromQueue(Song song) {
			currentPlay.Remove(song);
		}
		public DoublyLL GetQueue() {
			return currentPlay;
		}
		public Song GetSongFromQueue(int index) {
			return currentPlay[index];
		}
		///<summary>
		///Returns SongDetailed either with or without album details.
		///Use with Song.FilePath as argument
		///</summary>
		/*public SongDetailed GetSongDetails(string location)     
        {
            var tFile = TagLib.File.Create(location);
            if (tFile.Tag.Album == null)
                return new SongDetailed(tFile.Tag.FirstAlbumArtist, tFile.Tag.Title, tFile.Properties.Duration,tFile.Properties.AudioBitrate, Path.GetExtension(location), false);
            else
                return new SongDetailed(tFile.Tag.FirstAlbumArtist, tFile.Tag.Album, ((int)tFile.Tag.Track), tFile.Tag.Title, tFile.Properties.Duration, tFile.Properties.AudioBitrate, Path.GetExtension(location), false);
        }*/
	}










	public class SongJSONConverter : JsonConverter<Song>
	{
#pragma warning disable CS8600, CS8601
		public override Song Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
			Song song = new Song();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{

					string propertyName = reader.GetString();

					reader.Read();
					switch (propertyName)
					{
						case "Title":
							song.Title = reader.GetString();
							break;
						case "Artist":
							song.Artist = reader.GetString();
							break;
						case "FilePath":
							song.FilePath = reader.GetString();
							break;
						case "Length":
							song.Length = TimeSpan.Parse(reader.GetString());
							break;
						case "Album":
							song.Album = reader.GetString();
							break;
						case "NumberInAlbum":
							string a = reader.GetString();
							if (a != "")
								song.NumberInAlbum = Convert.ToInt32(reader.GetString());
							else song.NumberInAlbum = 0;
							break;
						case "Bitrate":
							song.Bitrate = Convert.ToInt32(reader.GetString());
							break;
						case "FileType":
							song.FileType = reader.GetString();
							break;
						case "IsDeleted":
							song.SetDeleted(Convert.ToBoolean(reader.GetString()));
							break;
					}
				}
			}
			return song;
		}
		public override void Write(Utf8JsonWriter writer, Song value, JsonSerializerOptions options) {
			throw new NotImplementedException();
		}
	}
}
#pragma warning restore CS8600