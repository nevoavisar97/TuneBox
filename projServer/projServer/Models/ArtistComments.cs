namespace projServer.Models
{
    public class ArtistComment
    {
        private int id;
        private string nickName;
        private string artist;
        private string commentText;
        private DateTime commentDate;
        private string avatar;

        public int Id { get => id; set => id = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Artist { get => artist; set => artist = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string Avatar { get => avatar; set => avatar = value; }

        public List<ArtistComment> GetCommentsByArtist(string name)
        {
            DBservices dbs = new DBservices();
            List<ArtistComment> c = dbs.GetCommentsByArtist(name);
            return c;
        }
        public bool Insert()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertArtistComment(this);
            if (rowsAff == 1) return true;
            return false;
        }
    }
}
