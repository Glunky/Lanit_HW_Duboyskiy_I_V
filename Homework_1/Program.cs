using Homework_1.Models;

namespace Homework_1;

partial class Programm
{
        static void Main(string[] args)
        {
                Student student = new Student("Ilya", 24, 2f);

                Console.Write("Введите количество кусков пиццы: ");

                if(!int.TryParse(Console.ReadLine(), out int pizzasCount) || pizzasCount < 0)
                {
                        Console.WriteLine("Веедены некорректные даннные");
                        return;
                }

                if (!AddPizza(pizzasCount, student.Bag))
                {
                        Console.WriteLine($"{pizzasCount} кусков пиццы не влезет в рюкзак студента");
                        return;
                }
                
                Console.Write("Введите количество бутылок пива: ");

                if(!int.TryParse(Console.ReadLine(), out int beerBottlesCount) || beerBottlesCount < 0)
                {
                        Console.WriteLine("Веедены некорректные даннные");
                        return;
                }

                if (!AddBeer(beerBottlesCount, student.Bag))
                {
                        Console.WriteLine($"{beerBottlesCount} бутылок пива не влезет в рюкзак студента");
                        return;
                }

                Console.WriteLine($@"
                        Имя студента: {student.Name};
                        Количество взятых кусков пиццы: {student.Bag.PizzaPieces};
                        Количество взятых бутылок пива: {student.Bag.BeerBottles};
                        {CountSatiety(student)};
                        {CountDrunk(student)};
                ");
        }
}




