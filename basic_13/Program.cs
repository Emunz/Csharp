using System;
using System.Collections.Generic;

namespace basic_13
{
    class Program
    {
        public static void Count1To255(){
            for(int i = 1; i <=255; i++){
                Console.WriteLine(i);
            }
        }

        public static void Odd1To255(){
            for(int i = 1; i <=255; i++){
                if(i % 2 != 0){
                    Console.WriteLine(i);
                }
            }
        }

        public static void printSum(){
            int sum = 0;
            for(int i = 0; i <=255; i++){
                sum += i;
                Console.WriteLine("New Number: " + i + " Sum: " + sum);
            }
        }

        public static void iterateArray(int[] arrOfInts){
            for(int i = 0; i < arrOfInts.Length; i++){
                Console.WriteLine(arrOfInts[i]);
            }
        }

        public static void findMax(int[] arrOfInts){
            int max = arrOfInts[0];
            for(int i = 1; i < arrOfInts.Length; i++){
                if(arrOfInts[i] > max){
                    max = arrOfInts[i];
                }
            }
            Console.WriteLine(max);
        }

        public static void findAvg(int[] arrOfInts){
            int sum = 0;
            for(int i = 0; i < arrOfInts.Length; i++){
                sum += arrOfInts[i];
                
            }
            float avg = sum/arrOfInts.Length;
            Console.WriteLine(avg);
        }

        public static void MaxMinAvg(int[] arrOfInts){
            int sum = arrOfInts[0];
            int max = arrOfInts[0];
            int min = arrOfInts[0];
            for(int i = 1; i < arrOfInts.Length; i++){
                sum += arrOfInts[i];
                if(arrOfInts[i] > max){
                    max = arrOfInts[i];
                }
                if(arrOfInts[i] < min){
                    min = arrOfInts[i];
                }
                
            }
            float avg = sum/arrOfInts.Length;
            Console.WriteLine(max);
            Console.WriteLine(min);
            Console.WriteLine(avg);
        }

        public static void arrayOdd1To255(){
            List<int> yList = new List<int>();
            for(int i = 1; i <=255; i++){
                if(i % 2 != 0){
                    yList.Add(i);
                }
            }
            int[] y = yList.ToArray();
            foreach(var x in y){
                Console.WriteLine(x);
            }
            
        }

        public static void greaterThanY(int[] arrOfInts, int Y){
            int count = 0;
            for(int i = 0; i < arrOfInts.Length; i++){
                if(arrOfInts[i] > Y){
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        public static void squareArray(int[] arrOfInts){
            for(int i = 0; i < arrOfInts.Length; i++){
                arrOfInts[i] *= arrOfInts[i];
            }

            foreach(var x in arrOfInts){
                Console.WriteLine(x);
            }
            Console.WriteLine(arrOfInts);
        }

        public static void noNegs(int[] arrOfInts){
            for(int i = 0; i < arrOfInts.Length; i++){
                if(arrOfInts[i] < 0){
                    arrOfInts[i] = 0;
                }
            }

            foreach(var x in arrOfInts){
                Console.WriteLine(x);
            }
            
        }

        public static void shiftArrayLeft(int[] arrOfInts){
            for(int i = 0; i < arrOfInts.Length; i++){
                if(i == arrOfInts.Length-1){
                    arrOfInts[i] = 0;
                } else {
                    arrOfInts[i] = arrOfInts[i + 1];
                }
            }
            foreach(var x in arrOfInts){
                Console.WriteLine(x);
            }
        }

        public static void negToString(int[] arrOfInts){
            List<object> newArr = new List<object>();
            for(int i = 0; i < arrOfInts.Length; i++){
                if(arrOfInts[i] < 0){
                    newArr.Add("Dojo");
                } else {
                    newArr.Add(arrOfInts[i]);
                }
            }

            object[] y = newArr.ToArray();
            foreach(var x in y){
                Console.WriteLine(x);
            }
        }

        static void Main(string[] args)
        {
            // Count1To255();

            // Odd1To255();

            // printSum();

            int[] numarr = {7,3,-5,6,6,-8,-13,10,};
            // iterateArray(numarr);

            // findMax(numarr);

            // findAvg(numarr);

            // arrayOdd1To255();

            // greaterThanY(numarr, 5);

            // squareArray(numarr);

            // noNegs(numarr);

            // MaxMinAvg(numarr);

            // shiftArrayLeft(numarr);

            negToString(numarr);
        }
    }
}
