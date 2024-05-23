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







    //METHODS
    static string GetAndValidateUserInput(string inputName, int? minLength, int? maxLength)
    {
        
        bool validateLength = minLength.HasValue && maxLength.HasValue;
        
        //While Loop
        while (true)
        {
            //format strings for registering inputName
            if (validateLength)
            {
                System.Console.WriteLine(
                    $"Please enter your {inputName}. It must be between {minLength} and {maxLength} alpha characters long.");
                    
            }
            else
            {
                System.Console.WriteLine(
                    $"Please enter your {inputName}:");  
            }
            
            string? input = Console.ReadLine(); //read input from user
            try 
            {
                if (validateLength)
                {
                    if (input.Length > maxLength || input.Length < minLength)     //Exceptions --  for wrong length
                    {
                        throw new IndexOutOfRangeException(
                            $"Input must be between {minLength} and {maxLength} alpha characters long.");
                    }
                }

                for (int i = 0; i < input.Length; i++)
                {
                    if (!char.IsLetter(input[i])) //invalid character exception
                    {
                        throw new FormatException("Input must only be alpha characters.");
                    }
                }

                // return to break out of while loop
                return input; //user input
            }
            catch (IndexOutOfRangeException)
            {
                System.Console.WriteLine();
                System.Console.WriteLine(
                    $"Your {inputName} must be between {minLength} and {maxLength} alpha characters long.");
                System.Console.WriteLine("Please try again.");
                System.Console.WriteLine();
            }
            catch (FormatException)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Your {inputName} must only be alpha characters.");
                System.Console.WriteLine("Please try again.");
                System.Console.WriteLine();
            }
        }
    }
    

    // Method to Display Entry Main Menu
    static int DisplayEntryMainMenu()
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("MAIN MENU");
            System.Console.WriteLine();
            System.Console.WriteLine("Please Register or Login:");
            System.Console.WriteLine();
            System.Console.WriteLine("[1] Register");
            System.Console.WriteLine("[2] Login");
            System.Console.WriteLine("[3] Exit");
            System.Console.WriteLine();
            System.Console.WriteLine("Please select an option:");

            string? input = Console.ReadLine().TrimEnd()?? ""; //returns user input for selection
            System.Console.WriteLine(); 
            System.Console.WriteLine("------------------------");
            System.Console.WriteLine();
            try
            {
                int selection = int.Parse(input); //turn user input into int
                if (selection < 1 || selection > 3)
                {
                    throw new IndexOutOfRangeException("Selection must be between 1 and 3.");
                }

                return selection;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Sorry!  Your selection is invalid.");
                System.Console.WriteLine("Selection must be between 1 and 3.  Please try again.");
                System.Console.WriteLine();
            }
            System.Console.WriteLine("-----------------------------------------------------");
            System.Console.WriteLine();
        }
    }


    //Method to Display main menu and make selection
    static int DisplayMainMenuAndGetSelection()
    {
        while (true)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("TASK MENU:");
            System.Console.WriteLine("What would you like to do next?");
            System.Console.WriteLine();
            System.Console.WriteLine("[1] Add a new ToDo task");
            System.Console.WriteLine("[2] View all ToDo tasks");  
            System.Console.WriteLine("[3] Logout");
            System.Console.WriteLine();
            System.Console.WriteLine("Please select an option:");

            string? input = Console.ReadLine().TrimEnd()?? ""; //returns user input for selection
            System.Console.WriteLine();
            try
            {
                int selection = int.Parse(input); //turn user input into int
                if (selection < 1 || selection > 3)
                {
                    throw new IndexOutOfRangeException("Selection must be between 1 and 3.");
                }

                return selection;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Sorry!  Your selection is invalid.");
                System.Console.WriteLine("Selection must be between 1 and 3.  Please try again.");
                System.Console.WriteLine();
            }
        }
    }

    //Method Display Add Todo Menu
    static void DisplayAddTodoMenu(TodoService todoService, User activeUser)
    {
        System.Console.WriteLine("What task would you like add?");
        string? input = Console.ReadLine().TrimEnd()?? "";
        System.Console.WriteLine();

        Todo todo = todoService.AddTodo(input, activeUser);

        System.Console.WriteLine($"ToDo Task '{input.ToUpper()}' was added to your list.");
        System.Console.WriteLine();
    }

    //Method Display All Todo's
    static void DisplayAllTodosMenu(ToDoController todoController, User activeUser)
    {
        System.Console.WriteLine("YOUR LIST OF TODO TASKS:");
        System.Console.WriteLine();
       
        List<Todo> todos = todoController.GetAllTodos(activeUser);   
        foreach (Todo todo in todos)
        {
            System.Console.WriteLine(todo);
        }

        System.Console.WriteLine();
        System.Console.WriteLine();
    }

    // Method to Display Register User Menu
    static User DisplayRegisterUserMenu()   
    {
        System.Console.WriteLine("REGISTER:");
        System.Console.WriteLine();
        //Method to get First Name
        System.Console.WriteLine("Before creating your list you must first register with us.");
        System.Console.WriteLine("AFTER registering with us, you will also need to Login.");
        System.Console.WriteLine();
        string firstName = GetAndValidateUserInput("First Name", null, null); 
        System.Console.WriteLine();
        System.Console.WriteLine($"Welcome {firstName.ToUpper()}!");
        System.Console.WriteLine();
        //Method to get User Name
        string userName = GetAndValidateUserInput("Username", 7, 10);
        System.Console.WriteLine();
        //Method to get Password
        string password = GetAndValidateUserInput("Password", 5, 10); 
        System.Console.WriteLine();
        //Method to create RegisterUser with first name, user name, and password - pulls from above
        User user = userController.RegisterUser(firstName, userName, password);  
        return user;
    }


    // Method to Display Login Menu
    static User DisplayLoginMenu(UserController userController)  
    {
        System.Console.WriteLine("Welcome Back! ");
        System.Console.WriteLine();
        System.Console.WriteLine("LOGIN MENU:");
        System.Console.WriteLine();
        string userName = GetAndValidateUserInput("Username", null, null);
        System.Console.WriteLine();
        string password = GetAndValidateUserInput("Password", null, null);
        System.Console.WriteLine();
        User user = userController.Login(userName, password);  
        
        return user;
    }
}







    

