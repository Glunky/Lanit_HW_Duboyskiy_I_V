namespace Homework_1.Models;

class Student
{
    private const int MIN_AGE = 16;
    private const int MAX_AGE = 40;
    
    private int _age;
    private string _name;
    
    public Student(string name, int age, float bagCapacity)
    {
        Name = name;
        Age = age;
        Bag = new(bagCapacity);
    }

    internal string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Недопустимое имя");
            
            _name = value;
        }
    }

    internal int Age
    {
        get => _age;
        set
        {
            if (value is < MIN_AGE or > MAX_AGE)
                throw new ArgumentException("Недопустимый возраст");
            
            _age = value;
        }
    }
    internal Bag Bag { get; }
}