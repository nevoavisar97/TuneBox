namespace projServer.Models
{
    public class Quiz
    {
        private int id;
        private string albumName;
        private string artistName;
        private string fullName;
        private string songName;
        private string text;
        private string imgUrl;
        private string releaseYear;
        private string artistImg;

        public int Id { get => id; set => id = value; }
        public string AlbumName { get => albumName; set => albumName = value; }
        public string ArtistName { get => artistName; set => artistName = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string SongName { get => songName; set => songName = value; }
        public string Text { get => text; set => text = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string ReleaseYear { get => releaseYear; set => releaseYear = value; }
        public string ArtistImg { get => artistImg; set => artistImg = value; }

        static public List<Quiz> Get(int n)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadQuizData(n);
        }
    }
}
