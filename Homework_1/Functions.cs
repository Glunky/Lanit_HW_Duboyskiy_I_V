using Homework_1.Models;

namespace Homework_1;

partial class Programm
{
        static bool AddPizza(int pizzaPieces, Bag studentBag)
        {
                float capacityToAdd = pizzaPieces * Constants.PIECE_OF_PIZZA_CAPACITY;

                if (capacityToAdd + studentBag.CurrentCapacity <= studentBag.Capacity)
                {
                        studentBag.PizzaPieces += pizzaPieces;
                        return true;
                }

                return false;
        }

       static bool AddBeer(int beerBottles, Bag studentBag)
        {
                float capacityToAdd = beerBottles * Constants.BOTTLE_CAPACITY;

                if (capacityToAdd + studentBag.CurrentCapacity <= studentBag.Capacity)
                {
                        studentBag.BeerBottles += beerBottles;
                        return true;
                }

                return false;
        }

        static string CountSatiety(Student student)
        {
                if (student.Bag.PizzaPieces == 0)
                {
                        return "Студент голоден";
                }

                float satietyCoeff = student.Age / (2 * student.Bag.PizzaPieces);
                
                if (satietyCoeff < Constants.EAT_ENOUGH_MIN_RANGE_VALUE)
                        return "Студент переел";
                
                if (satietyCoeff > Constants.EAT_ENOUGH_MAX_RANGE_VALUE)
                        return "Студент голоден";
                
                return "Студент наелся";
        }

        static string CountDrunk(Student student)
        {
                if (student.Bag.BeerBottles == 0)
                {
                        return "Студент трезв";
                }

                float satietyCoeff = student.Age / (2 * student.Bag.BeerBottles);
                
                if (satietyCoeff < Constants.DRUNK_ENOUGH_MIN_RANGE_VALUE)
                        return "Студент пьян";
                
                if (satietyCoeff > Constants.DRUNK_ENOUGH_MAX_RANGE_VALUE)
                        return "Студент трезв";
                
                return "Студент выпил достаточно";
        }
}