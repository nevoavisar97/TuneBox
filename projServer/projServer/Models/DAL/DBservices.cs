using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using projServer.Models;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //////////////////////////////////////////////  USER  //////////////////////////////////////////////
    //--------------------------------------------------------------------------------------------------
    // This method log in a user
    //--------------------------------------------------------------------------------------------------
    public User LogIn(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@email", email);
        paramDic.Add("@password", password);


        cmd = CreateCommandWithStoredProcedure("LoginUser", con, paramDic);             // create the command
        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

        returnParameter.Direction = ParameterDirection.ReturnValue;


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (!dataReader.HasRows)
            {
                return null;
            }
            User u = new User();
            while (dataReader.Read())
            {
                u.Id = Convert.ToInt32(dataReader["UserID"]);
                u.UserName = dataReader["UserName"].ToString();
                u.Email = dataReader["Email"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.Avatar = dataReader["Avatar"].ToString();
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            // note that the return value appears only after closing the connection
            var result = returnParameter.Value;
        }

    }

    // This method get user by id
    //--------------------------------------------------------------------------------------------------
    public User GetUserById(int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", id);


        cmd = CreateCommandWithStoredProcedure("SPGetUserById", con, paramDic);             // create the command
        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

        returnParameter.Direction = ParameterDirection.ReturnValue;


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (!dataReader.HasRows)
            {
                return null;
            }
            User u = new User();
            while (dataReader.Read())
            {
                u.Id = Convert.ToInt32(dataReader["UserID"]);
                u.UserName = dataReader["UserName"].ToString();
                u.Email = dataReader["Email"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.Avatar = dataReader["Avatar"].ToString();

            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            // note that the return value appears only after closing the connection
            var result = returnParameter.Value;
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method update a user to the user table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", user.Id);
        paramDic.Add("@UserName", user.UserName);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);


        cmd = CreateCommandWithStoredProcedure("UpdateUser", con, paramDic); // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // This method Inserts a user to the user table 
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        
        paramDic.Add("@UserName", user.UserName);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        paramDic.Add("@avatar", user.Avatar);

        cmd = CreateCommandWithStoredProcedure("InsertIntoUsersTable", con, paramDic);  // create the command

        try
        {
            //int numEffected = cmd.ExecuteNonQuery(); // execute the command
            int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public int Fav(int user, int song)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        
        paramDic.Add("@UserID", user);
        paramDic.Add("@SongID", song);

        cmd = CreateCommandWithStoredProcedure("SP_AddRemoveFavoriteSong", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
             //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method Reads all users
    //--------------------------------------------------------------------------------------------------
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("GetAllUsers", con, null);             // create the command


        List<User> userList = new List<User>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new User();
                u.Id = Convert.ToInt32(dataReader["UserID"]);
                u.UserName = dataReader["UserName"].ToString();
                u.Email = dataReader["Email"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.Avatar = dataReader["Avatar"].ToString();

                userList.Add(u);
            }
            return userList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    public List<string> ReadArtistsNames()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("SP_RetrieveArtistNames", con, null);             // create the command


        List<string> namesList = new List<string>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                namesList.Add(dataReader["Artist"].ToString());
            }
            return namesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }public List<string> GetUserInfo()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("SP_GetUsersWithFavoriteSongs", con, null);             // create the command


        List<string> data = new List<string>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                data.Add(dataReader["UserInfo"].ToString());
            }
            return data;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }public List<string> GetArtistInfo()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("SP_GetArtistFavCount", con, null);             // create the command


        List<string> data = new List<string>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                data.Add(dataReader["artist"].ToString());
            }
            return data;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }public List<string> GetSongInfo()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("SP_GetSongAppearanceCount", con, null);             // create the command


        List<string> data = new List<string>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                data.Add(dataReader["SongInfo"].ToString());
            }
            return data;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public List<string> ReadSongsNames()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = CreateCommandWithStoredProcedure("SP_RetrieveSongsNames", con, null);             // create the command


        List<string> namesList = new List<string>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                namesList.Add(dataReader["SongName"].ToString());
            }
            return namesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
     public List<Song> ReadNsongs(int n)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@n", n);

        cmd = CreateCommandWithStoredProcedure("SP_RetrieveNsongs", con, paramDic);             // create the command


        List<Song> songsList = new List<Song>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Song s = new Song();
                s.Id = Convert.ToInt32(dataReader["ID"]);
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]);
                s.SongName = dataReader["Song"].ToString();
                songsList.Add(s);
            }
            return songsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public List<Quiz> ReadQuizData(int n)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@n", n);

        cmd = CreateCommandWithStoredProcedure("SP_RetrieveNsongsForQuiz", con, paramDic);             // create the command


        List<Quiz> quizData = new List<Quiz>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Quiz q = new Quiz();
                q.Id = Convert.ToInt32(dataReader["ID"]);
                q.ArtistName = dataReader["Artist"].ToString();
                q.Text = dataReader["Text"].ToString();
                q.AlbumName = dataReader["albumName"].ToString();
                q.ImgUrl = dataReader["imgUrl"].ToString();
                q.ReleaseYear = (dataReader["releaseYear"]).ToString();
                q.SongName = dataReader["Song"].ToString();
                q.FullName = dataReader["fullName"].ToString();
                q.ArtistImg = dataReader["artistImg"].ToString();
                quizData.Add(q);
            }
            return quizData;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
      public List<Song> GetSongByArtist(string name)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", name);

        cmd = CreateCommandWithStoredProcedure("SP_GetArtistSongs", con, paramDic);             // create the command


        List<Song> songsList = new List<Song>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Song s = new Song();
                s.Id = Convert.ToInt32(dataReader["ID"]);
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]);
                s.SongName = dataReader["Song"].ToString();
                songsList.Add(s);
            }
            return songsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public List<Song>  Search(string key)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SearchKeyword", key);

        cmd = CreateCommandWithStoredProcedure("SP_SearchSongs", con, paramDic);             // create the command


        List<Song> songsList = new List<Song>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Song s = new Song();
                s.Id = Convert.ToInt32(dataReader["ID"]);
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                 s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]).Date;
                s.SongName = dataReader["Song"].ToString();
                songsList.Add(s);
            }
            return songsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
     public List<Song> SearchBartist(string key)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@searchKeyword", key);

        cmd = CreateCommandWithStoredProcedure("SP_SearchSongsByArtist", con, paramDic);             // create the command


        List<Song> songsList = new List<Song>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Song s = new Song();
                s.Id = Convert.ToInt32(dataReader["ID"]);
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]).Date;
                s.SongName = dataReader["Song"].ToString();
                songsList.Add(s);
            }
            return songsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
     public List<Song> GetUserFavoriteSongs(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);

        cmd = CreateCommandWithStoredProcedure("GetUserFavoriteSongs", con, paramDic);             // create the command


        List<Song> songsList = new List<Song>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Song s = new Song();
                s.Id = Convert.ToInt32(dataReader["ID"]);
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]).Date;
                s.SongName = dataReader["Song"].ToString();
                songsList.Add(s);
            }
            return songsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    public Song GetSongById(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@id", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetSongByID", con, paramDic);             // create the command
        
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Song s = new Song();

            if (dataReader.Read())
            {
                s.Id = id;
                s.Artist_id = Convert.ToInt32(dataReader["artist_id"]);
                s.Album_id = Convert.ToInt32(dataReader["album_id"]);
                s.Api_id = Convert.ToInt32(dataReader["api_id"]);
                s.ArtistName = dataReader["Artist"].ToString();
                s.Text = dataReader["Text"].ToString();
                s.AppleM = dataReader["appleM"].ToString();
                s.UTube = dataReader["uTube"].ToString();
                s.AlbumName = dataReader["albumName"].ToString();
                s.ImgUrl = dataReader["imgUrl"].ToString();
                s.RealeaseDate = Convert.ToDateTime(dataReader["realeaseDate"]).Date;
                s.SongName = dataReader["Song"].ToString();

                return s;
            }
            return s;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    public Artist GetArtistByName(string name)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", name);

        cmd = CreateCommandWithStoredProcedure("SP_GetArtistByName", con, paramDic);             // create the command
        
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Artist a = new Artist();

            if (dataReader.Read())
            {
                a.StageName = dataReader["stageName"].ToString();
                a.FullName = dataReader["fullName"].ToString();
                a.ImgUrl = dataReader["imgUrl"].ToString();
                a.Ig_id = dataReader["ig_id"].ToString();
                a.Fb_id = dataReader["fb_id"].ToString();
                a.Tw_id = dataReader["tw_id"].ToString();
                a.Description = dataReader["description"].ToString();
                return a;
            }
            return a;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
     public List<Artist> ReadNartists(int n)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@n", n);

        cmd = CreateCommandWithStoredProcedure("SP_RetrieveNartists", con, paramDic);             // create the command


        List<Artist> artistsList = new List<Artist>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Artist a = new Artist();
                a.StageName = dataReader["stageName"].ToString();
                a.FullName = dataReader["fullName"].ToString();
                a.ImgUrl = dataReader["imgUrl"].ToString();
                a.Ig_id = dataReader["ig_id"].ToString();
                a.Fb_id = dataReader["fb_id"].ToString();
                a.Tw_id = dataReader["tw_id"].ToString();
                a.Description = dataReader["description"].ToString();
                artistsList.Add(a);
            }
            return artistsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
     public List<Artist> GetArtists()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedure("SP_GetAllArtists", con,null);             // create the command


        List<Artist> artistsList = new List<Artist>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Artist a = new Artist();
                a.StageName = dataReader["stageName"].ToString();
                a.FullName = dataReader["fullName"].ToString();
                a.ImgUrl = dataReader["imgUrl"].ToString();
                a.Ig_id = dataReader["ig_id"].ToString();
                a.Fb_id = dataReader["fb_id"].ToString();
                a.Tw_id = dataReader["tw_id"].ToString();
                a.Description = dataReader["description"].ToString();
                artistsList.Add(a);
            }
            return artistsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public List<Comment> GetCommentsBySongId(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SongID", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetSongComments", con, paramDic);             // create the command


        List<Comment> commentsList = new List<Comment>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Comment c = new Comment();
                c.Id = Convert.ToInt32(dataReader["CommentID"]);
                c.NickName = dataReader["nick"].ToString();
                c.SongID = id;
                c.CommentText = dataReader["CommentText"].ToString();
                c.CommentDate = Convert.ToDateTime(dataReader["commentDate"]);
                c.Avatar = dataReader["avatar"].ToString();
                commentsList.Add(c);
            }
            return commentsList;
        }
        catch (Exception ex)
        {
            return new List<Comment>();
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    public List<LeaderBoard> GetLeaderBoard()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

       

        cmd = CreateCommandWithStoredProcedure("GetLeaderboard", con, null);             // create the command


        List<LeaderBoard> leaderBoard = new List<LeaderBoard>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                LeaderBoard l = new LeaderBoard();
                l.Score = Convert.ToInt32(dataReader["score"]);
                l.NickName = dataReader["nickname"].ToString();
                l.Avatar = dataReader["avatar"].ToString();
                 leaderBoard.Add(l);
            }
            return leaderBoard;
        }
        catch (Exception ex)
        {
            return leaderBoard;
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    public List<ArtistComment> GetCommentsByArtist(string name)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", name);

        cmd = CreateCommandWithStoredProcedure("SP_GetArtistComments", con, paramDic);             // create the command


        List<ArtistComment> commentsList = new List<ArtistComment>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                ArtistComment c = new ArtistComment();
                c.Id = Convert.ToInt32(dataReader["id"]);
                c.NickName = dataReader["nick"].ToString();
                c.Artist = name;
                c.CommentText = dataReader["CommentText"].ToString();
                c.CommentDate = Convert.ToDateTime(dataReader["commentDate"]);
                c.Avatar = dataReader["avatar"].ToString();
                commentsList.Add(c);
            }
            return commentsList;
        }
        catch (Exception ex)
        {
            return new List<ArtistComment>();
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    //    ////////////////////////*******************  FLAT *********************//////////////////////////////
    //    ///
    //    //--------------------------------------------------------------------------------------------------
    //    // This method Inserts a flat to the flats table 
    //    //--------------------------------------------------------------------------------------------------
    public int InsertArtist(Artist a)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@stageName", a.StageName);
        paramDic.Add("@fullName", a.FullName);
        paramDic.Add("@description", a.Description);
        paramDic.Add("@imgUrl", a.ImgUrl);
        paramDic.Add("@ig_id", a.Ig_id);
        paramDic.Add("@fb_id", a.Fb_id);
        paramDic.Add("@tw_id", a.Tw_id);

        cmd = CreateCommandWithStoredProcedure("SP_InsertNewArtist", con, paramDic);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    


        public int InsertSongInfo(Song s)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@imgUrl", s.ImgUrl);
        paramDic.Add("@Song", s.SongName);
        paramDic.Add("@realeaseDate", s.RealeaseDate);
        paramDic.Add("@api_id", s.Api_id);
        paramDic.Add("@album_id", s.Album_id);
        paramDic.Add("@artist_id", s.Artist_id);
        paramDic.Add("@albumName", s.AlbumName);
        paramDic.Add("@uTube", s.UTube);
        paramDic.Add("@appleM", s.AppleM);

        cmd = CreateCommandWithStoredProcedure("SP_UpdateSongInfo", con, paramDic);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public int InsertComment(Comment c)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SongID", c.SongID);
        paramDic.Add("@avatar", c.Avatar);
        paramDic.Add("@CommentText", c.CommentText);
        paramDic.Add("@nick", c.NickName);
     

        cmd = CreateCommandWithStoredProcedure("SP_AddComment", con, paramDic);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
     
    public int InsertToLeaderBoard(LeaderBoard l)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@avatar", l.Avatar);
        paramDic.Add("@score", l.Score);
        paramDic.Add("@nickname", l.NickName) ;
     

        cmd = CreateCommandWithStoredProcedure("InsertOrUpdateLeaderboard", con, paramDic);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    public int InsertArtistComment(ArtistComment c)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@name", c.Artist);
        paramDic.Add("@avatar", c.Avatar);
        paramDic.Add("@CommentText", c.CommentText);
        paramDic.Add("@nick", c.NickName);
     

        cmd = CreateCommandWithStoredProcedure("AddArtistComment", con, paramDic);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int CheckFav(int user, int song)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", user);
        paramDic.Add("@SongID", song);


        cmd = CreateCommandWithStoredProcedure("CheckUserFavorite", con, paramDic);             // create the command
        var returnParameter = cmd.Parameters.Add("@IsFavorite", SqlDbType.Int);

        returnParameter.Direction = ParameterDirection.Output;

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
            int isFavoriteValue = Convert.ToInt32(returnParameter.Value);
            return isFavoriteValue;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            // note that the return value appears only after closing the connection
            var result = returnParameter.Value;
        }

    }


    //    //--------------------------------------------------------------------------------------------------
    //    // This method Reads flats above a certain rating and from a specific city
    //    // This method uses the return value mechanism
    //    //--------------------------------------------------------------------------------------------------
    //    public List <Flat> GetByRatingAndCity(float rating, string city, int uId)
    //    {
    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }


    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@reviewScore", rating);
    //        paramDic.Add("@city", city);
    //        paramDic.Add("@userId", uId);


    //        cmd = CreateCommandWithStoredProcedure("GetApartmentsByCityAndRating", con, paramDic);             // create the command



    //        List<Flat> listFlats = new List<Flat>();

    //        try
    //        {
    //            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //            while (dataReader.Read())
    //            {
    //                Flat flat = new Flat();
    //                flat.Id = dataReader["Id"].ToString();
    //                flat.UserId = Convert.ToInt32(dataReader["userId"]);
    //                flat.Address = dataReader["address"].ToString();
    //                flat.Price = Convert.ToDouble(dataReader["price"]);
    //                flat.NumOfRooms = Convert.ToInt32(dataReader["noOfRooms"]);
    //                flat.ImgUrl = dataReader["imgUrl"].ToString();
    //                flat.ApartmentName = dataReader["apartmentName"].ToString();
    //                flat.Description = dataReader["description"].ToString();
    //                flat.ReviewScore = Convert.ToSingle(dataReader["reviewScore"]);
    //                listFlats.Add(flat);
    //            }



    //            return listFlats;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //            // note that the return value appears only after closing the connection
    //        }


    //}

    //    ////--------------------------------------------------------------------------------------------------
    //    //// This method reads all flats from flats table above a certain price
    //    ////--------------------------------------------------------------------------------------------------
    //    public List<Flat> ReadAbovePrice(double p,int uId)
    //    {

    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }


    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@price", p);
    //        paramDic.Add("@userId", uId);


    //        cmd = CreateCommandWithStoredProcedure("GetApartmentsBelowPrice", con, paramDic);             // create the command
    //        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

    //        returnParameter.Direction = ParameterDirection.ReturnValue;


    //        List<Flat> flatList = new List<Flat>();

    //        try
    //        {
    //            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //            while (dataReader.Read())
    //            {
    //                Flat flat = new Flat();
    //                flat.Id = dataReader["Id"].ToString();
    //                flat.UserId = Convert.ToInt32(dataReader["userId"]);
    //                flat.Address = dataReader["address"].ToString();
    //                flat.Price = Convert.ToDouble(dataReader["price"]);
    //                flat.NumOfRooms = Convert.ToInt32(dataReader["noOfRooms"]);
    //                flat.ImgUrl = dataReader["imgUrl"].ToString();
    //                flat.ApartmentName = dataReader["apartmentName"].ToString();
    //                flat.Description = dataReader["description"].ToString();
    //                flat.ReviewScore = Convert.ToSingle(dataReader["reviewScore"]);
    //                flatList.Add(flat);
    //            }

    //            return flatList;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //            // note that the return value appears only after closing the connection
    //            var result = returnParameter.Value;
    //        }

    //    }

    //    ////--------------------------------------------------------------------------------------------------
    //    //// This method delete a flat from flats table
    //    ////--------------------------------------------------------------------------------------------------
    //    public int DeleteFlat(string id,int uId)
    //    {

    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@id",id);
    //        paramDic.Add("@userId", uId);

    //        cmd = CreateCommandWithStoredProcedure("DeleteFlatByID", con, paramDic);             // create the command

    //        try
    //        {
    //            int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
    //            return numEffected;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //        }

    //    }

    //    ////--------------------------------------------------------------------------------------------------
    //    //// This method reads all flats from flats table
    //    ////--------------------------------------------------------------------------------------------------
    //    public List<Flat> ReadFlats(int u_id)
    //    {

    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@userId",u_id );


    //        cmd = CreateCommandWithStoredProcedure("GetAllFlats", con, paramDic);             // create the command
    //        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

    //        returnParameter.Direction = ParameterDirection.ReturnValue;


    //        List<Flat> flatList = new List<Flat>();


    //        try
    //        {
    //            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //            while (dataReader.Read())
    //            {
    //                Flat flat = new Flat();
    //                flat.Id = dataReader["Id"].ToString();
    //                flat.Address = dataReader["address"].ToString();
    //                flat.Price = Convert.ToDouble(dataReader["price"]);
    //                flat.NumOfRooms = Convert.ToInt32(dataReader["noOfRooms"]);
    //                flat.ImgUrl = dataReader["imgUrl"].ToString();
    //                flat.ApartmentName = dataReader["apartmentName"].ToString();
    //                flat.Description = dataReader["description"].ToString();
    //                flat.ReviewScore = Convert.ToSingle(dataReader["reviewScore"]);
    //                flat.UserId = Convert.ToInt32(dataReader["userId"]);
    //                flatList.Add(flat);
    //            }
    //            return flatList;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //        }

    //    }




    //    //////////////////////////////////////////////  ORDER  //////////////////////////////////////////////
    //    ////--------------------------------------------------------------------------------------------------
    //    //// This method gets a user's future orders
    //    ////--------------------------------------------------------------------------------------------------
    //    public List<Order> GetFutureOrders(int u_id)
    //    {
    //        SqlConnection con;
    //        SqlCommand cmd;
    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }


    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@userId", u_id);


    //        cmd = CreateCommandWithStoredProcedure("SPGetFutureOrdersByUserId", con, paramDic);             // create the command
    //        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

    //        returnParameter.Direction = ParameterDirection.ReturnValue;


    //        List<Order> orderList = new List<Order>();

    //        try
    //        {
    //            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //            while (dataReader.Read())
    //            {
    //                Order o = new Order();
    //                o.Id = Convert.ToInt32(dataReader["id"]);
    //                o.StartDate = Convert.ToDateTime(dataReader["startDate"]);
    //                o.EndDate = Convert.ToDateTime(dataReader["endDate"]);
    //                o.UserId = Convert.ToInt32(dataReader["userId"]);
    //                o.FlatId = dataReader["flatId"].ToString();
    //                orderList.Add(o);
    //            }

    //            return orderList;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //            // note that the return value appears only after closing the connection
    //            var result = returnParameter.Value;
    //        }

    //    }

    //    //--------------------------------------------------------------------------------------------------
    //    // This method Inserts an order to the order table 
    //    //--------------------------------------------------------------------------------------------------
    //    public int InsertOrder(Order o)
    //    {

    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //        paramDic.Add("@startDate", o.StartDate);
    //        paramDic.Add("@endDate", o.EndDate);
    //        paramDic.Add("@flatId", o.FlatId);
    //        paramDic.Add("@userId", o.UserId);


    //        cmd = CreateCommandWithStoredProcedure("SPInsertIntoOrderTable", con, paramDic);  // create the command

    //        try
    //        {
    //            int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
    //            return numEffected;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //        }

    //    }
    //    //--------------------------------------------------------------------------------------------------
    //    // This method Reads all orders
    //    //--------------------------------------------------------------------------------------------------
    //    public List<Order> ReadOrders()
    //    {

    //        SqlConnection con;
    //        SqlCommand cmd;

    //        try
    //        {
    //            con = connect("myProjDB"); // create the connection
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }


    //        cmd = CreateCommandWithStoredProcedure("SPGetAllOrders", con, null);             // create the command


    //        List<Order> orderList = new List<Order>();

    //        try
    //        {
    //            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //            while (dataReader.Read())
    //            {
    //                Order o = new Order();
    //                o.Id = Convert.ToInt32(dataReader["id"]);
    //                o.StartDate = Convert.ToDateTime(dataReader["startDate"]);
    //                o.EndDate = Convert.ToDateTime(dataReader["endDate"]);
    //                o.UserId = Convert.ToInt32(dataReader["userId"]);
    //                o.FlatId = dataReader["flatId"].ToString();
    //                orderList.Add(o);
    //            }
    //            return orderList;
    //        }
    //        catch (Exception ex)
    //        {
    //            // write to log
    //            throw (ex);
    //        }

    //        finally
    //        {
    //            if (con != null)
    //            {
    //                // close the db connection
    //                con.Close();
    //            }
    //        }

    //    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if(paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic) {
                cmd.Parameters.AddWithValue(param.Key,param.Value);

            }


        return cmd;
    }
}
