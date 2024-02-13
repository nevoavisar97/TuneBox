namespace projServer.Models
{
    public class Song
    {
        private int id;
        private int api_id;
        private string artistName;
        private string songName;
        private string text;
        private string imgUrl;
        private DateTime realeaseDate;
        private int album_id;
        private int artist_id;
        private string albumName;
        private string uTube;
        private string appleM;

        public int Api_id { get => api_id; set => api_id = value; }
        public string ArtistName { get => artistName; set => artistName = value; }
        public string SongName { get => songName; set => songName = value; }
        public string Text { get => text; set => text = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public DateTime RealeaseDate { get => realeaseDate; set => realeaseDate = value; }
        public int Album_id { get => album_id; set => album_id = value; }
        public int Artist_id { get => artist_id; set => artist_id = value; }
        public string AlbumName { get => albumName; set => albumName = value; }
        public string UTube { get => uTube; set => uTube = value; }
        public string AppleM { get => appleM; set => appleM = value; }
        public int Id { get => id; set => id = value; }

        static public List<string> GetSongsNames()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadSongsNames();
        }   
        
        static public List<Song> GetN(int n)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadNsongs(n);
        }
        
        static public Song GetSong(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongById(id);
        }
         static public List<Song> GetSongsByArtist(string name)
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongByArtist(name);
        }

        static public List<Song>Search(string key)
        {
            DBservices dbs = new DBservices();
            return dbs.Search(key);
        }
            static public List<Song> SearchByArtist(string key)
        {
            DBservices dbs = new DBservices();
            return dbs.SearchBartist(key);
        }

        public bool UpdateSongInfo()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertSongInfo(this);
            if (rowsAff == 1) return true;
            return false;
        }
    }
}