using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WebApi.Models;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;

        }
        public async Task SaveItemsInDB() // saves 
        {
            Json json = new Json();
            List<BookInformation> booklist = json.GetBookList();

            foreach (BookInformation b in booklist)
            {
                _context.BookItems.Add(b);
                await _context.SaveChangesAsync();
            }


        }

        [HttpGet]
        public async Task<ActionResult<List<BookInformation>>> UnsortedBooks()
        {

            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.ToList();
            Random rng = new Random();


            List<BookInformation> booklist1 = booklist.OrderBy(a => rng.Next()).ToList();


            if (!booklist1.Any())
            {
                return NotFound();
            }

            return booklist1;

        }





        [HttpGet("id")]
        public async Task<ActionResult<List<BookInformation>>> BooksById()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }


            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Id).ToList();


            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;


        }


        [HttpGet("id/{id}")]

        public async Task<ActionResult<List<BookInformation>>> GetBooksById(string id)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.Where(a => a.Id.Contains(id, StringComparison.OrdinalIgnoreCase))
            .OrderBy(a => a.Id)
            .ToList();


            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;

        }

        [HttpGet("author")]
        public async Task<ActionResult<List<BookInformation>>> AuthorSort()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Author).ToList();


            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;


        }


        [HttpGet("author/{authorName}")]
        public async Task<ActionResult<List<BookInformation>>> SortByAuthorName(string authorName)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }


            List<BookInformation> booklist = _context.BookItems.
                Where(a => a.Author.Contains(authorName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.Author)
                .ToList();

            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;
        }


        [HttpGet("title")]
        public async Task<ActionResult<List<BookInformation>>> TitleSort()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Title).ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;
        }


        [HttpGet("title/{titleName}")]
        public async Task<ActionResult<List<BookInformation>>> SortByTitleName(string titlename)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }



            List<BookInformation> booklist = _context.BookItems.Where(a => a.Title.Contains(titlename, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;

        }


        [HttpGet("genre")]
        public async Task<ActionResult<List<BookInformation>>> GetSortedListByGenre()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }



            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Genre).ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;
        }


        [HttpGet("genre/{genreName}")]
        public async Task<ActionResult<List<BookInformation>>> SortByGenreName(string genreName)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }
            List<BookInformation> booklist2 = _context.BookItems.Where(a => a.Genre.Contains(genreName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!booklist2.Any())
            {
                return NotFound();
            }

            return booklist2;
        }



        [HttpGet("price")]
        public async Task<ActionResult<List<BookInformation>>> SortByPrice()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Price).ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;


        }

        [HttpGet("price/{pricevalueS}")]
        public async Task<ActionResult<List<BookInformation>>> SortByPriceValueS(string pricevalueS)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }
            List<BookInformation> booklist = new List<BookInformation>();
            List<BookInformation> answer = new List<BookInformation>();

            if (pricevalueS.Contains("&"))
            {
                booklist = _context.BookItems.OrderBy(a => a.Price).ToList(); // sorted list by price
                if (!booklist.Any())
                {
                    return NotFound();
                }
                string[] priceArray = pricevalueS.Split("&");

                double priceOne = Convert.ToDouble(priceArray[0]);
                double priceTwo = Convert.ToDouble(priceArray[1]);
                return booklist.Where(a => a.Price >= priceOne && a.Price <= priceTwo).OrderBy(a => a.Price).ToList();

            }
            else
            {
                return _context.BookItems.Where(a => a.Price >= Convert.ToDouble(pricevalueS)).OrderBy(a => a.Price).ToList();
            }


        }

        [HttpGet("published")]
        public async Task<ActionResult<List<BookInformation>>> GetBookByDateLis()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.PublishDate).ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;



        }


        [HttpGet("published/{year:int}")]
        public async Task<ActionResult<List<BookInformation>>> GetSortBYear(int year)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.Where(a => a.PublishDate.Year >= year)
                .OrderBy(a => a.PublishDate)
                .ToList();

            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;

        }



        [HttpGet("published/{year:int}/{month:int}")]
        public async Task<ActionResult<List<BookInformation>>> GetSortByYearMonth(int year, int month)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }


            List<BookInformation> booklist = _context.BookItems.Where(a => a.PublishDate.Year >= year && a.PublishDate.Month >= month)
                .OrderBy(a => a.PublishDate)
                .ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }

            return booklist;

        }


        [HttpGet("published/{year:int}/{month:int}/{day:int}")]
        public async Task<ActionResult<List<BookInformation>>> GetSortByYearMonth(int year, int month, int day)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.PublishDate)
                .OrderBy(a => a.PublishDate)
                .ToList();

            if (!booklist.Any())
            {
                return NotFound();
            }

            List<BookInformation> answer = new List<BookInformation>();



            DateTime endpointDate = new DateTime(year, month, day);
            string endpointDateInt = endpointDate.ToString("yyyyMMdd");

            int parsedDate = Convert.ToInt32(endpointDateInt);

            foreach (BookInformation c in booklist)
            {

                string YearMonthDay = c.PublishDate.ToString("yyyyMMdd");

                int dateInt = Convert.ToInt32(YearMonthDay);

                if (dateInt >= parsedDate)
                {
                    answer.Add(c);

                }
            }




            return answer;


        }



        [HttpGet("description")]
        public async Task<ActionResult<List<BookInformation>>> SortByDescription()
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }
            List<BookInformation> booklist = _context.BookItems.OrderBy(a => a.Description)
                .OrderBy(a => a.Description)
                .ToList();
            if (!booklist.Any())
            {
                return NotFound();
            }
            return booklist;
        }


        [HttpGet("description/{description}")]
        public async Task<ActionResult<List<BookInformation>>> SortByDescriptionName(string description)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }

            List<BookInformation> booklist = _context.BookItems.Where(a => a.Description.Contains(description, StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.Description)
                .ToList();

            if (!booklist.Any())
            {
                return NotFound();
            }
            return booklist;

        }



        // DELETE: api/TodoItems/5
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteTodoItem(String id)
        {
            if (!_context.BookItems.Any())
            {
                await SaveItemsInDB();
            }
            var todoItem = await _context.BookItems.FindAsync(id.ToUpper());
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.BookItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpPost]
        public async Task<ActionResult<String>> PostBook(BookInformation book)
        {
            List<BookInformation> booklist = _context.BookItems.Where(a => a.Id.Contains(book.Id, StringComparison.OrdinalIgnoreCase)).ToList();

            if (booklist.Any())
            {
                return Conflict();
            }
            _context.BookItems.Add(book);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return "Book Succesfully added";
        }
    }
}