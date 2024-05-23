using Microsoft.Data.SqlClient;

class UserRepo
{
    private readonly string _connectionString; 


    // Dependency/Constructor Injection / Happening in constructor
    public UserRepo(string connString)
    {
        _connectionString = connString;
    }

   
    //Add User
    public User? AddUser(User user)
    {
        using SqlConnection connection = new(_connectionString);  
        connection.Open();   

        string sql = "INSERT INTO [User] VALUES (@Name, @UserName, @Password)";  
        using SqlCommand cmd = new(sql, connection);  
        cmd.Parameters.AddWithValue("@Name", user.Name);  
        cmd.Parameters.AddWithValue("@UserName", user.UserName);  
        cmd.Parameters.AddWithValue("@Password", user.Password);  

        //Execute the Query
        // cmd.ExecuteNonQuery(); //This executes a non-select SQL statement (inserts, updates, deletes)
        using SqlDataReader reader = cmd.ExecuteReader();

        //Extract the Results
        if (reader.Read())
        {
            //If Read() found data -> then extract it / Helper Method for repetitive task
            User newUser = BuildUser(reader); 
            return newUser;
        }
        else
        {
            //Read() found nothing 
            return null;
        }
    }


    //Get User by Username
    public User? GetUserByUsername(string username)
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.[User] WHERE UserName = @UserName";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@UserName", username);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);
                return newUser;
            }
            //Read() found nothing
            return null; 
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }


    //Build User
    private User BuildUser(SqlDataReader reader)
    {
        User newUser = new();
        newUser.Id = (int)reader["Id"];
        newUser.Name = (string)reader["Name"];
        newUser.UserName = (string)reader["userName"];
        newUser.Password = (string)reader["Password"];
    
        return newUser;
    }
}
