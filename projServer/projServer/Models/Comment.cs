namespace projServer.Models
{
    public class Comment
    {
        private int id;
        private string nickName;
        private int songID;
        private string commentText;
        private DateTime commentDate;
        private string avatar;

        public int Id { get => id; set => id = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public int SongID { get => songID; set => songID = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public DateTime CommentDate { get => commentDate; set => commentDate = value; }
        public string Avatar { get => avatar; set => avatar = value; }

        public List<Comment> GetCommentsBySong(int id)
        {
            DBservices dbs = new DBservices();
            List<Comment> c = dbs.GetCommentsBySongId(id);
            return c;
        }
        public bool Insert()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertComment(this);
            if (rowsAff == 1) return true;
            return false;
        }
    }
}
