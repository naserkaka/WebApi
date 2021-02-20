using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class BookContext : DbContext
        {
            public BookContext(DbContextOptions<BookContext> options)
                : base(options)
            {
            }

            public DbSet<BookInformation> BookItems { get; set; }
        }
    }
