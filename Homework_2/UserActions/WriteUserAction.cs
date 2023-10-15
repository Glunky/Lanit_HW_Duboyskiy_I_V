using System.Net;

namespace Homework_2.UserActions;

public class WriteUserAction : IUserAction
{
    private string directoryName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

    public async Task MakeAction()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Введите URL веб-страницы: ");
            
            string webURL = Console.ReadLine();
            string htmlContent = string.Empty;
            
            using HttpClient client = new HttpClient();
            try
            {
                using HttpResponseMessage response = await client.GetAsync(webURL);
                using HttpContent content = response.Content;
                htmlContent = await content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Произошла ошибка: {e.Message}");
                
                while (true)
                {
                    Console.WriteLine("Попробовать ещё раз? y/n");
                    
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
            
            if (string.IsNullOrEmpty(htmlContent))
            {
                continue;
            }
            
            Console.Write("Контент получен, введите название .txt файла (без указания расширения) для сохранения: ");
            
            string outputFilePath = Path.Combine(directoryName, $"{Console.ReadLine()}.txt");
            
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                await writer.WriteLineAsync(htmlContent);
            }
            
            while (true)
            {
                Console.WriteLine("Контент записан, желаете повторить операцию? y/n");
                
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
