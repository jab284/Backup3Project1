using Microsoft.Data.SqlClient;

class TodoRepo
{
    
    private readonly string _connectionString;


      // Dependency Injection / Constructor Injection - happening in constructor
    public TodoRepo(string connString)
    {
        _connectionString = connString;
    }




     //Add Task to User
    public Todo? AddTodo(Todo todo)   ///wrong
    {
        
        //Set up DB Connection
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        //Create the SQL String
        string sql = "INSERT INTO dbo.[ToDo] OUTPUT inserted.* VALUES (@UserId, @Description)";  //table column names

        //Set up SqlCommand Object and use its methods to modify the Parameterized Values
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@UserId", todo.UserId);
        cmd.Parameters.AddWithValue("@Description", todo.Description);
        
        //Execute the Query
        // cmd.ExecuteNonQuery(); //This executes a non-select SQL statement (inserts, updates, deletes)
        using SqlDataReader reader = cmd.ExecuteReader();

        //Extract the Results
        if (reader.Read())
        {
            //If Read() found data -> then extract it.
            Todo newtodo = BuildTodo(reader);
            return newtodo;
        }
        else
        {
            //Else Read() found nothing -> Insert Failed. 
            return null;
        }
    }



    //Get All ToDo's by UserId
    public List<Todo>? GetAllTodosByUserId(int userId)
    {
        //Get user from user table by user id ->Get tasks table by user
        List<Todo> todos = new();

        try
        {
            //Set up DB connection  
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL string to pull all data on table 
            string sql = "SELECT * FROM dbo.[Todo] WHERE UserId = @UserId";     //table name and column names

            //Set up the SqlCommand Object 
            using SqlCommand  cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@UserId", userId);
            //Read table 
            using SqlDataReader reader = cmd.ExecuteReader();

            // Extract the results from the table read 
            while (reader.Read())
            {
                //If read found data we will extract it and add to list
                Todo allTodo = BuildTodo(reader);
                todos.Add(allTodo);
            }
            return todos; 
        }
        catch (Exception e)
        {
            Console.WriteLine (e.Message);
            Console.WriteLine (e.StackTrace);
            return null; 
        } 
    }


    private Todo BuildTodo(SqlDataReader reader)
    {
        Todo newtodo = new();
        newtodo.Id = (int)reader["Id"];
        newtodo.UserId = (int)reader["UserId"];
        newtodo.Description = (string)reader["Description"];
        
        return newtodo;
    }
}