namespace projServer.Models
{
    public class Artist
    {
        private string stageName;
        private string fullName;
        //private int id;
        private string description;
        private string imgUrl;
        private string ig_id; //instagram id
        private string fb_id; //facebook name
        private string tw_id; //twitter id

        public string StageName { get => stageName; set => stageName = value; }
        public string FullName { get => fullName; set => fullName = value; }
        //public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string Ig_id { get => ig_id; set => ig_id = value; }
        public string Fb_id { get => fb_id; set => fb_id = value; }
        public string Tw_id { get => tw_id; set => tw_id = value; }


        static public List<string> GetArtitsNames()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadArtistsNames();
        }
          static public Artist GetArtistByName(string name)
        {
            DBservices dbs = new DBservices();
            return dbs.GetArtistByName(name);
        }
         static public List<Artist> GetArtists()
        {
            DBservices dbs = new DBservices();
            return dbs.GetArtists();
        }

        //quiz -- return 3 artists for question
        static public List<Artist> GetRandomArtists()
        {
            DBservices dbs = new DBservices();
            return dbs.GetArtists();
        }

        static public List<Artist> GetN(int n)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadNartists(n);
        }

        public bool InsertArtist()
        {
            DBservices dbs = new DBservices();
            int rowsAff = dbs.InsertArtist(this);
            if (rowsAff == 1) return true;
            return false;
        }

    }
}
