using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Models
{
    public class BookInformation:IComparable<BookInformation>
    {
      

        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public int CompareTo([AllowNull] BookInformation other)
        {
            throw new NotImplementedException();
        }
    }


}
