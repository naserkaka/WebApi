using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebApi.Models
{
    public class Json
    {
        List<BookInformation> books = new List<BookInformation>();

        public List<BookInformation> GetBookList()
        {

            var jsonFilePath = File.ReadAllText("Books.json");
            dynamic bookdynamic = JArray.Parse(jsonFilePath);

            foreach (dynamic book in bookdynamic)
            {

                
                string id = book.id;
                string author = book.author;
                string title = book.title;
                string genre = book.genre;
                double price = book.price;

               DateTime publishdate = book.publish_date;
                string description = book.description;

                books.AddRange(new[] {
           new BookInformation{
            Id = id,
            Author = author,
            Title = title,
            Genre=genre,
            Price=price,
            PublishDate=publishdate,
            Description=description
        } });


            }



            return books;
        }
    }
}