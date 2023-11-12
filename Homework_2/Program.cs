using Homework_2.UserActions;

namespace Homework_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IUserAction readAction = new ReadUserAction();
            IUserAction writeAction = new WriteUserAction();
            IUserAction fibonacciNumbersAction = new FibonacciNumbersUserAction();
            IUserAction exitAction = new ExitUserAction();
            IUserAction adoNetAction = new ADOnetAction();
            
            var userActionToImplementation = new Dictionary<Constants.UserActions, IUserAction>
            {
                {Constants.UserActions.Read, readAction},
                {Constants.UserActions.Write, writeAction},
                {Constants.UserActions.FibonacciNumbers, fibonacciNumbersAction},
                {Constants.UserActions.Exit, exitAction},
                {Constants.UserActions.ADOnet, adoNetAction}
            };
            
            while (true)
            { 
                Console.ForegroundColor = ConsoleColor.Yellow;
                
                Console.WriteLine($"{(int)Constants.UserActions.Read}. {Constants.UserActions.Read.ToString()}");
                Console.WriteLine($"{(int)Constants.UserActions.Write}. {Constants.UserActions.Write.ToString()}");
                Console.WriteLine($"{(int)Constants.UserActions.FibonacciNumbers}. {Constants.UserActions.FibonacciNumbers.ToString()}");
                Console.WriteLine($"{(int)Constants.UserActions.Exit}. {Constants.UserActions.Exit.ToString()}");
                Console.WriteLine($"{(int)Constants.UserActions.ADOnet}. {Constants.UserActions.ADOnet.ToString()}");
                Console.Write("Выберите действие: ");
                
                if (int.TryParse(Console.ReadLine(), out int userSelection) &&
                    userActionToImplementation.TryGetValue((Constants.UserActions) userSelection, out var selectedAction))
                {
                    await selectedAction.MakeAction();
                    
                    while (true)
                    {
                        Console.WriteLine("Желаете закрыть программу? y/n");
                        
                        ConsoleKey inputKey = Console.ReadKey().Key;
                        
                        if (inputKey == ConsoleKey.Y)
                        {
                            await userActionToImplementation[Constants.UserActions.Exit].MakeAction();
                        }

                        if (inputKey == ConsoleKey.N)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    while (true)
                    {
                        Console.WriteLine("Введены некорректные данные, попробовать ещё раз? y/n");
                        
                        ConsoleKey inputKey = Console.ReadKey().Key;
                        
                        if (inputKey == ConsoleKey.Y)
                        {
                            break;
                        }
                        
                        if (inputKey == ConsoleKey.N)
                        {
                            await userActionToImplementation[Constants.UserActions.Exit].MakeAction();
                        }
                    }
                }
            }
        }
    }
}
