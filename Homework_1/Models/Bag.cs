namespace Homework_1.Models;

class Bag
{
    private int _pizzaPieces;
    private int _beerBottles;
    private float _currentCapacity;
    internal float Capacity { get; }

    internal Bag(float capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Недопустимая ёмкость");
        }
        
        Capacity = capacity;
    }
    
    internal int PizzaPieces
    {
        get => _pizzaPieces;
        set
        {
            _pizzaPieces += value;
            CurrentCapacity += value * Constants.PIECE_OF_PIZZA_CAPACITY;
        }
    }

    internal int BeerBottles
    {
        get => _beerBottles;
        set
        {
            _beerBottles += value;
            CurrentCapacity += value * Constants.BOTTLE_CAPACITY;
        }
    }
    
    internal float CurrentCapacity
    {
        get => _currentCapacity;
        private set
        {
            if (value > Capacity)
                throw new Exception("Максимальная ёмкость переполнена");

            _currentCapacity = value;
        }
    }
}