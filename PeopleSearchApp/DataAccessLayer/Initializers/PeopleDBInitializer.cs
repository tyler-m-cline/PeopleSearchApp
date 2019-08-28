using PeopleSearchApp.DataAccessLayer.DBContexts;
using PeopleSearchApp.DataAccessLayer.Models;
using System;
using System.IO;

namespace PeopleSearchApp.DataAccessLayer.Initializers
{
    public class PeopleDBInitializer
    {
        private readonly PeopleDBContext peopleDbContext;

        public PeopleDBInitializer(PeopleDBContext peopleDbContext)
        {
            this.peopleDbContext = peopleDbContext;
        }

        public void SeedDBContext()
        {
            //Seed the in memory database. This could be done in OnModelCreating,
            //but that seems to only get called during migrations
            var currentDirectory = Directory.GetCurrentDirectory();
            var hanPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\han-solo.jpg"));
            var lukePhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\luke-skywalker.jpeg"));
            var chewyPhoto = Convert.ToBase64String(File.ReadAllBytes(currentDirectory + "\\Photos\\chewy.jpg"));

            var hanId = int.MaxValue - 1;
            var lukeId = int.MaxValue - 2;
            var chewyId = int.MaxValue - 3;

            var people = new Person[]
                {
                    new Person()
                    {
                        Id = hanId,
                        FirstName = "Han",
                        LastName = "Solo",
                        Age = 25,
                        StreetAddress = "111 Space Smuggler Road",
                        City = "Millennium Falcon",
                        State = "Space",
                        ZipCode = 555556,
                        Photograph = hanPhoto
                    },
                    new Person()
                    {
                        Id = lukeId,
                        FirstName = "Luke",
                        LastName = "Skywalker",
                        Age = 20,
                        StreetAddress = "12 Sandhut lane",
                        City = "Pomeroy",
                        State = "Tatooine",
                        ZipCode = 555555,
                        Photograph = lukePhoto
                    },
                    new Person()
                    {
                        Id = chewyId,
                        FirstName = "Chew",
                        LastName = "Bacca",
                        Age = 20,
                        StreetAddress = "1 Wookie BLVD",
                        City = "Columbus",
                        State = "Kashyyyk",
                        ZipCode = 555557,
                        Photograph = chewyPhoto
                    }
                };

            this.peopleDbContext.AddRange(people);
            this.peopleDbContext.SaveChanges();
        }
    }
}
