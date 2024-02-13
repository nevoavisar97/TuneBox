namespace projServer.Models
{
        public class User
        {
            private int id;
            private string email;
            private string password;
            private string userName;
            private string avatar;
            public string Email { get => email; set => email = value; }
            public string Password { get => password; set => password = value; }
            public string UserName { get => userName; set => userName = value; }
            public int Id { get => id; set => id = value; }
            public string Avatar { get => avatar; set => avatar = value; }

        public bool Update(int id)
            {
                DBservices dbs = new DBservices();
                this.id = id;
                int rowsAff = dbs.UpdateUser(this);
                if (rowsAff == 1) return true;
                return false;
            }

        public List<string> GetSongInfo()
        {
            DBservices dbs = new DBservices();
            return dbs.GetSongInfo();
        }
         public List<string> GetArtistInfo()
        {
            DBservices dbs = new DBservices();
            return dbs.GetArtistInfo();
        }
         public List<string> GetUserInfo()
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserInfo();
        }

            public List<User> Read()
            {
                DBservices dbs = new DBservices();
                return dbs.ReadUsers();
            }

            public int Registration()
            {
                DBservices dbs = new DBservices();
                int u_id = dbs.InsertUser(this);
                if (u_id > 0) return u_id;
                return -1;
            }
        
        public List<Song> GetUserFavoriteSongs(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserFavoriteSongs(id);
        }
            public bool CheckFav(int user,int song)
            {
                DBservices dbs = new DBservices();
                int retcode = dbs.CheckFav(user,song);
                if (retcode == 1) return true;
                return false;
            }
        
            public bool Fav(int user, int song)
            {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.Fav(user, song);
            if (rowsAff == 1) return true;
            return false;
        }

            public User LogIn(string email, string password)
            {
                DBservices dbs = new DBservices();
                User u = dbs.LogIn(email, password);
                if (u == null) throw new Exception("We couldn't find an account with the email and password you provided. Please check your details and try again.");
                else return u;
            }

            public User GetUserById(int id)
            {
                DBservices dbs = new DBservices();
                User u = dbs.GetUserById(id);
                return u;
            }
        }

}
