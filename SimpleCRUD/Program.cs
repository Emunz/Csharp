using System;
using System.Collections.Generic;
using DBConnection;

namespace SimpleCRUD
{
    class Program
    {
        
        public static void ReadUsers(){
            List<Dictionary<string, object>> allUsers = DbConnector.Query("SELECT * from consoleDB.users");
            foreach(var info in allUsers){
                Console.WriteLine("{0}  {1} {2}    Favorite Number: {3}", info["id"], info["FirstName"], info["LastName"], info["FavoriteNumber"]);
            }
        }

        public static void CreateUser(){
            Console.WriteLine("First Name?");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last Name?");
            string lastname = Console.ReadLine();
            Console.WriteLine("Favorite Number?");
            string favnum = Console.ReadLine();
            DbConnector.Execute($"INSERT INTO consoleDB.users(users.FirstName, users.LastName, users.FavoriteNumber) VALUES ('{firstname}', '{lastname}', '{favnum}')");
        }

        public static void UpdateUser(){
            Console.WriteLine("For Which User ID?");
            string userID = Console.ReadLine();
            Console.WriteLine("First Name?");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last Name?");
            string lastname = Console.ReadLine();
            Console.WriteLine("Favorite Number?");
            string favnum = Console.ReadLine();
            DbConnector.Execute($"UPDATE consoleDB.users SET FirstName = '{firstname}', LastName = '{lastname}', FavoriteNumber = '{favnum}' WHERE id = {userID}");
        }

        public static void DeleteUser(){
            Console.WriteLine("For Which User ID?");
            string userID = Console.ReadLine();
            DbConnector.Execute($"DELETE FROM consoleDB.users WHERE id = '{userID}'");
        }

        static void Main(string[] args)
        {
            string EndCommand = "no";
            while(EndCommand == "no"){
                Console.WriteLine("Command?");
                string InputLine = Console.ReadLine();
                if(InputLine == "print users"){
                    ReadUsers();
                } else if (InputLine == "add"){
                    CreateUser();
                } else if(InputLine == "update"){
                    UpdateUser();
                } else if(InputLine == "delete"){
                    DeleteUser();
                } else if(InputLine == "kill") {
                    EndCommand = "yes";
                }
                    
                
                    
            }
                
        }
    }
}
