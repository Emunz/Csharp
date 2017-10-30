using System;
using System.Collections.Generic;

namespace boxing_unboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> BoxedData = new List<object>();
            BoxedData.Add(7);
            BoxedData.Add(28);
            BoxedData.Add(-1);
            BoxedData.Add(true);
            BoxedData.Add("chair");

            int sum = 0;
            foreach(var thing in BoxedData){
                Console.WriteLine(thing);
                if(thing is int){
                    sum += (int)thing;
                }
            }
            Console.WriteLine(sum);
        }
    }
}
