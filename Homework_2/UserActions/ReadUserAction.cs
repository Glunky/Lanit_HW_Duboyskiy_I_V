namespace Homework_2.UserActions;

public class ReadUserAction : IUserAction
{
    private string directoryName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    
    public async Task MakeAction()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Введите название .txt файла из папки проекта: ");
            
            string filePath = Path.Combine(directoryName, $"{Console.ReadLine()}.txt");
            
            if (File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Введите количестро строк для прочтения: ");
                
                if (int.TryParse(Console.ReadLine(), out int linesToRead) && linesToRead > 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    await WriteFileContentToConsole(filePath, linesToRead);
                    
                    Console.ForegroundColor = ConsoleColor.Blue;
                    
                    while (true)
                    {
                        Console.WriteLine("Данные выведены, желаете повторить операцию? y/n");
                        
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
                    Console.WriteLine("Введены некорректные данные, попробуйте ещё раз");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                    
                while (true)
                {
                    Console.WriteLine("Такого файла нет в проекте, попробовать ещё раз? y/n");
                    
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

    private async Task WriteFileContentToConsole(string filePath, int linesToRead)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            for (int i = 0; i < linesToRead; i++)
            {
                string line = await reader.ReadLineAsync();
                
                if (line != null)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
