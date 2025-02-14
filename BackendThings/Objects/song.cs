using System;
using System.Data.Common;

namespace BackendThings.Objects
{
    /*public class SongDetailed
    {
        public string Artist { get; set; }
        public string? Album { get; set; }
        public int? NumberInAlbum { get; set; }
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public int Bitrate { get; set; }
        public string FileType { get; set; }
        public bool IsDeleted {  get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private SongDetailed() { }          //doesn't allow the programmer to do an empty constructor :)
#pragma warning restore CS8618

        public SongDetailed(string artist, string album, int noInAlbum, string title, TimeSpan length, int bitrate, string fileType, bool isDeleted)
        {
            Artist = artist;
            Album = album;
            NumberInAlbum = noInAlbum;
            Title = title;
            Length = length;
            Bitrate = bitrate;
            FileType = fileType;
            IsDeleted = isDeleted;
        }
        public SongDetailed(string artist, string title, TimeSpan length, int bitrate, string fileType, bool isDeleted)
        {
            Artist = artist;
            Album = null;
            NumberInAlbum = 0;
            Title = title;
            Length = length;
            Bitrate = bitrate;
            FileType = fileType;
            IsDeleted = isDeleted;
        }
        ~SongDetailed() { }
    }*/
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string FilePath { get; set; }
        public TimeSpan Length { get; set; }

        
        public string? Album { get; set; }
        public int? NumberInAlbum { get; set; }
        public int Bitrate { get; set; }
        public string FileType { get; set; }
        public bool IsDeleted { get; private set; }

       public Song() { }
        /*public Song(string title, string artist, string filePath, TimeSpan length)
        {
            Title = title;
            Artist = artist;
            FilePath = filePath;
            Length = length;
        }*/

        public Song(string title, string artist, string filePath, TimeSpan length, string album, int numberInAlbum, int bitrate, string fileType, bool isDeleted)
        {
            Title = title;
            Artist = artist;
            FilePath = filePath;
            Length = length;
            Album = album;
            NumberInAlbum = numberInAlbum;
            Bitrate = bitrate;
            FileType = fileType;
            IsDeleted = isDeleted;
        }

        public void SetDeleted(bool value){IsDeleted = value;}
    }
}

