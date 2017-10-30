using System;
using System.Collections.Generic;

namespace collections_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int[] numbers = {1,2,3,4,5,6,7,8,9,10};
            string[] names = new string[4] {"Tim", "Martin", "Nikki", "Sara"};
            bool[] arrTrueFalse = new bool[10] {true, false, true, false, true, false, true, false, true, false};

            for(int i = 0; i < 10; i++){
                Console.Write("[");
                for(int x = 0; x < 10; x++){
                     Console.Write(numbers[i] * numbers[x] + ", ");
                }
                Console.Write("]");
                Console.WriteLine("");
            }


            List<string> flavors = new List<string>();
            flavors.Add("Chocolate Chip");
            flavors.Add("Blackberry");
            flavors.Add("Green Tea");
            flavors.Add("Strawberry");
            flavors.Add("Chocolate");

            Console.WriteLine(flavors.Count);
            Console.WriteLine(flavors[3]);
            flavors.Remove("Strawberry");
            Console.WriteLine(flavors.Count);

            Dictionary<string,string> flavor_flave = new Dictionary<string,string>();
            Random rand = new Random();
                for(int i = 0; i < 4; i++){
                    int index = rand.Next(0,4);
                    flavor_flave.Add(names[i], flavors[index]);
                }

            foreach(var entry in flavor_flave){
                Console.WriteLine(entry.Key + " - " + entry.Value);
            }

            
        }
    }
}
