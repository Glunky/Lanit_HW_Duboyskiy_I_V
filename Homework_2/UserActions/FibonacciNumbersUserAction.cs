namespace Homework_2.UserActions;

public class FibonacciNumbersUserAction : IUserAction
{
    public async Task MakeAction()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Введите число Фибоначчи: ");
            Console.ForegroundColor = ConsoleColor.White;
            
            if (int.TryParse(Console.ReadLine(), out int n) && n >= 0)
            {
                // Fibonacci calculation
                int result = 0;
                int next = 1;
                int tmp;
                
                for (int i = 0; i < n; i++)
                {
                    tmp = result;
                    result = next;
                    next += tmp;
                }
                
                Console.WriteLine($"Полученный результат: {result}");
                
                while (true)
                {
                    Console.WriteLine("Желаете повторить вычисление чисел Фибоначчи? y/n");
                    
                    ConsoleKey inputKey = Console.ReadKey().Key;
                    
                    if (inputKey == ConsoleKey.Y)
                    {
                        break;
                    }
                    
                    if (inputKey == ConsoleKey.N)
                    {
                        return;
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
                        return;
                    }
                }
            }
        }
    }
}
