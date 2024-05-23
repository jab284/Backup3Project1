class User
{
    //Properties
    public int Id { get; set;}
    public string Name { get; set;}
    public string UserName { get; set;}  
    public string Password { get; set;}
    

   //Constructor
   public User()
   {
        Name = "";
        UserName = "";
        Password = "";
   }

    //Constructor
    public User(string firstName, string userName, string password)
    {
        Name = firstName;
        UserName = userName;
        Password = password;
    }

    //ToString
    public override string ToString()
    {
        string str = "";
        str += "Thank You " + Name;
        str += ".";
        str += "It is time to start creating your list.";
        
        return str;
    }    
}