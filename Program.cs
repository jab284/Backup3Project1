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


        User? user = null;  

        //Display Entry Menu ******
        bool exitRequested = false;
        while (!exitRequested) { 
        
            bool loggedIn = false;
            bool logoutRequested = false;  

            //Display Entry Menu
        while (!(exitRequested || loggedIn))
        {
            int selection = DisplayEntryMainMenu();
            switch (selection)
            {
                case 1:
                    try  
                    {
                        DisplayRegisterUserMenu();    
                        
                    }  
                    catch (Exception)  
                    {  
                        System.Console.WriteLine("Unable to Register.  Username already exists."); 
                        System.Console.WriteLine("Please try again.");  
                    }  
                    break; 
                case 2:
                    try
                    {
                        user = DisplayLoginMenu(userController);   
                        loggedIn = true;
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Unable to Login.  Either the username or password is incorrect.");
                        System.Console.WriteLine("Please try again!");
                        System.Console.WriteLine();
                        System.Console.WriteLine("If you have not already Registered, please do so now.");
                    }
                    break;
                case 3:
                    System.Console.WriteLine("Have a great day.  See you later!");
                    System.Console.WriteLine();
                    exitRequested = true;
                    logoutRequested = true;  
                    break;
                default:
                    System.Console.WriteLine("Invalid selection.  Please select, 1, 2, or 3.");
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
        }


        while (!logoutRequested) 
        {
            int selection = DisplayMainMenuAndGetSelection();

            switch (selection)
            {
                case 1:
                    DisplayAddTodoMenu(todoService, user!);
                    break;
                case 2:
                    DisplayAllTodosMenu(todoController, user!);
                    break;
                case 3:
                    System.Console.WriteLine("Have a great day.  See you later!");
                   
                    logoutRequested = true; 
                    loggedIn = false;  
                    user=null;  
                    System.Console.WriteLine(); 
                    break;
                default:
                    System.Console.WriteLine("Invalid selection.  Please select, 1, 2, or 3.");
                    break;
            }
            Console.WriteLine("--------------------------------");
        }
    }
} 




    }
}
