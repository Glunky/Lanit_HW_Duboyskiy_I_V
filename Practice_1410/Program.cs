namespace Practice_1410 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FooBar(15));
            Console.WriteLine(FooBar(5));
            Console.WriteLine(FooBar(3));
            Console.WriteLine(FooBar(17));
        }

        static string FooBar(int num)
        {
            if (num % 3 == 0)
            {
                return num % 5 == 0 ? "foobar" : "foo";
            }
            
            if (num % 5 == 0)
            {
                return num % 3 == 0 ? "foobar" : "bar";
            }

            return "";
        }
    }
}