namespace Homework_2.UserActions;

public class ExitUserAction : IUserAction
{
    public async Task MakeAction()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Завершение программы");
        
        Environment.Exit(1);
    }
}