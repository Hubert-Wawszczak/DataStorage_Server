using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStorage.ResponseModel
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Authors { get; set; }
        public int Year { get; set; }



        public BookResponse(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public BookResponse(string title,string isbn,string authors)
        {
            Title = title;
            ISBN = isbn;
            Authors = authors;

        }

        public BookResponse(int id, string title, string iSBN, string authors, int year) : this(id, title)
        {
            ISBN = iSBN;
            Authors = authors;
            Year = year;
        }
    }
}
