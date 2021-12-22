using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStorage.ResponseModel
{
    public class GetMetaInfoBook
    {
        public GetMetaInfoBook(string name, string author,  int year,string iSBN)
        {
            Name = name;
            Author = author;
            Year = year;
            ISBN = iSBN;
            
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        
    }
}
