using System;

namespace human_oop
{
    class Program
    {
        static void Main(string[] args)
        {
            Human firstPlayer = new Human("Karl", 3, 5, 3, 120);
            Console.WriteLine(firstPlayer.name);
            Console.WriteLine(firstPlayer.intelligence);
            Console.WriteLine(firstPlayer.health);

            Human secondPlayer = new Human("Ted", 5, 2, 3, 110);
            Console.WriteLine(secondPlayer.name);
            Console.WriteLine(secondPlayer.strength);
            Console.WriteLine(secondPlayer.health);

            secondPlayer.Attack(firstPlayer);
            Console.WriteLine(firstPlayer.name);
            Console.WriteLine(firstPlayer.health);

            firstPlayer.Attack(secondPlayer);
            firstPlayer.Attack(secondPlayer);
            firstPlayer.Attack(secondPlayer);
            Console.WriteLine(secondPlayer.name);
            Console.WriteLine(secondPlayer.health);

        }
    }
}
