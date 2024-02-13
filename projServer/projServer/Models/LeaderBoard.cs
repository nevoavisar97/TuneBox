namespace projServer.Models
{
    public class LeaderBoard
    {
        private int score;
        private string nickName;
        private string avatar;

        public int Score { get => score; set => score = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Avatar { get => avatar; set => avatar = value; }

        static public List<LeaderBoard> Get()
        {
            DBservices dbs = new DBservices();
            List<LeaderBoard> l = dbs.GetLeaderBoard();
            return l;
        }
        public bool Insert()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertToLeaderBoard(this);
            if (rowsAff == 1) return true;
            return false;
        }
    }
}
