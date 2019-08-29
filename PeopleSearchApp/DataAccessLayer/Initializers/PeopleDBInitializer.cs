using PeopleSearchApp.DataAccessLayer.DBContexts;
using PeopleSearchApp.DataAccessLayer.Models;
using System;
using System.IO;

namespace PeopleSearchApp.DataAccessLayer.Initializers
{
    public class PeopleDBInitializer
    {
        private readonly PeopleDBContext peopleDbContext;

        public enum PeopleIds
        {
            hanId = int.MaxValue - 1,
            lukeId = int.MaxValue - 2,
            chewyId = int.MaxValue - 3
        }

        public PeopleDBInitializer(PeopleDBContext peopleDbContext)
        {
            this.peopleDbContext = peopleDbContext;
        }

        public void SeedDBContext()
        {
            //Seed the in memory database. This could be done in OnModelCreating,
            //but that seems to only get called during migrations
            var hanPhoto = "";
            var lukePhoto = "";
            var chewyPhoto = "";

            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                hanPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\han-solo.jpg"));
                lukePhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\luke-skywalker.jpeg"));
                chewyPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\chewy.jpg"));
            } catch (Exception e)
            {
            }

            var people = new Person[]
                {
                    new Person()
                    {
                        Id = (int)PeopleIds.hanId,
                        FirstName = "Han",
                        LastName = "Solo",
                        Age = 25,
                        StreetAddress = "111 Space Smuggler Road",
                        City = "Chicago",
                        State = "Illinois",
                        ZipCode = 555556,
                        Photograph = hanPhoto
                    },
                    new Person()
                    {
                        Id = (int)PeopleIds.lukeId,
                        FirstName = "Luke",
                        LastName = "Skywalker",
                        Age = 20,
                        StreetAddress = "12 Sandhut lane",
                        City = "Pomeroy",
                        State = "OH",
                        ZipCode = 555555,
                        Photograph = lukePhoto
                    },
                    new Person()
                    {
                        Id = (int)PeopleIds.chewyId,
                        FirstName = "Chew",
                        LastName = "Bacca",
                        Age = 20,
                        StreetAddress = "1 Wookie BLVD",
                        City = "Columbus",
                        State = "OH",
                        ZipCode = 555557,
                        Photograph = chewyPhoto
                    }
                };

            this.peopleDbContext.AddRange(people);
            this.peopleDbContext.SaveChanges();
        }
    }
}
