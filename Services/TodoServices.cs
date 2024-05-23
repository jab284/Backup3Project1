class TodoService
{
    TodoRepo _todoRepo;
    
    
    public TodoService(TodoRepo todoRepo)
    {
        this._todoRepo = todoRepo;
    }

    //Add Task
    public Todo AddTodo(string description, User user)
    {
        Todo todo = new Todo(description, user.Id);
        _todoRepo.AddTodo(todo);
        
        return todo;
    }
    
    //Get All Tasks
    public List<Todo> GetAllTodos(User user)
    {
        return _todoRepo.GetAllTodosByUserId(user.Id);
    }
}
