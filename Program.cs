using System;

class Program
{
    // Application setup
        static UserService userService;
        static TodoService todoService;     
        static UserController userController; 
        static ToDoController todoController;

        //Main Method----------
    static void Main(string[] args)
    {
       userController = new(userService);

           
        //string with path to database
        string path = @"C:\Revature\DBConnections\JenAToDoListApp.txt";  
        //command that reads path 
        string connectionString = File.ReadAllText(path);      
        //Command to verify reading path
        //System.Console.WriteLine(connectionString);  

        UserRepo userRepo = new(connectionString);    
        userService = new(userRepo);   
        userController = new(userService);
        TodoRepo todoRepo = new(connectionString);
        todoService = new(todoRepo);
        todoController = new(todoService);


        //Console
        System.Console.WriteLine();
        System.Console.WriteLine("       Welcome to: ");
        System.Console.WriteLine();
        System.Console.WriteLine("**************************");
        System.Console.WriteLine("* CREATE YOUR TO DO LIST * ");
        System.Console.WriteLine("**************************");
        System.Console.WriteLine();
        System.Console.WriteLine("Here, you can easily maintain and track your list of need to do chores!");
        System.Console.WriteLine();
        System.Console.WriteLine("--------------------------");






    }
}
