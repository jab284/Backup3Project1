class UserController   
{
     public UserService userService;


     //Constructor
    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    //Constructor
    public User RegisterUser(string firstName, string userName, string password)
    {
        User user = userService.RegisterUser(firstName, userName, password);
        return user!;
    }

    //Constructor
    public User Login(string userName, string inputPassword)
    {
        User user = userService.Login(userName, inputPassword);
        return user;
    }
}