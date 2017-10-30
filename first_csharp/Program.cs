using System;

namespace first_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print numbers 1-255
            for(int i = 1; i <= 255; i++){
                Console.WriteLine(i);
            }

            // Print 1-100, numbers divisible by 3 or 5, but not both
            for(int i = 1; i <=100; i++){
                if(i % 3 == 0 && i % 5 == 0){
                    continue;
                }
                if(i % 3 == 0){
                    Console.WriteLine(i);
                }
                if(i % 5 == 0){
                    Console.WriteLine(i);
                }
            }

            // Print Fizz and Buzz and FizzBuzz
            for(int i = 1; i <=100; i++){
                if(i % 3 == 0 && i % 5 == 0){
                    Console.WriteLine("FizzBuzz");
                }
                else if(i % 3 == 0){
                    Console.WriteLine("Fizz");
                }
                else if(i % 5 == 0){
                    Console.WriteLine("Buzz");
                }
            }

            // Generates random number between 2-4 and prints a corresponding phrase based on random number
            Random rand = new Random();
            for(int val = 0; val <= 10; val++)
            {
                
                int num = rand.Next(2,5);
                if(num == 2){
                    Console.WriteLine("Fizz");
                } else if(num == 3){
                    Console.WriteLine("Buzz");
                } else if(num == 4){
                    Console.WriteLine("FizzBuzz");
                } 
            }
        }
    }
}
