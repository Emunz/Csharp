using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
        
            
            
            Artist singleArtist = Artists.Where(Artist => Artist.Hometown == "Mount Vernon").Single();
            Console.WriteLine($"{singleArtist.ArtistName} is from Mount Vernon and is {singleArtist.Age} years old.");

            //Who is the youngest artist in our collection of artists?

            Artist youngArtist = Artists.OrderBy(artist => artist.Age).First();
            Console.WriteLine($"{youngArtist.ArtistName} is the youngest artist at {youngArtist.Age} years old. Their real name is {youngArtist.RealName}");

            //Display all artists with 'William' somewhere in their real name

            List<Artist> wilhelm = Artists.Where(artist => artist.RealName.Contains("William")).ToList();
            foreach(var artist in wilhelm){
                Console.WriteLine($"{artist.ArtistName}'s real name is {artist.RealName}");
            }

            //Display the 3 oldest artist from Atlanta

            List<Artist> oldAtliens = Artists.Where(artist=> artist.Hometown == "Atlanta").OrderByDescending(artist => artist.Age).Take(3).ToList();
            foreach(var artist in oldAtliens){
                Console.WriteLine($"{artist.ArtistName} is one of the three oldest artists from {artist.Hometown}. They are {artist.Age} years old");
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City

            List<string> nonNYgroups = Artists
                .Join(Groups, artist => artist.GroupId, group => group.Id, (artist, group) => {artist.Group = group; return artist;}).Where(artist => (artist.Hometown != "New York City" && artist.Group != null))
                .Select(artist => artist.Group.GroupName)
                .Distinct()
                .ToList();
            foreach(var group in nonNYgroups){
                Console.WriteLine(group);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'

            Group WuTang = Groups
                .Where(group => group.GroupName == "Wu-Tang Clan")
                .GroupJoin(Artists, group => group.Id, artist => artist.GroupId, (group, artists) => {group.Members = artists.ToList(); return group;})
                .Single();
            foreach(var artist in WuTang.Members){
                Console.WriteLine(artist.ArtistName);
            }
        }
    }
}
