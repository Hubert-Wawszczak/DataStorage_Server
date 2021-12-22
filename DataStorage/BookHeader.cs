using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStorage
{
    public class BookHeader
    {
        [FromHeader]
        public string Title { get; set; }
        [FromHeader]
        public string ISBN { get; set; }
        [FromHeader]
        public int Year { get; set; }
        [FromHeader]
        public string Author { get; set; }

    }
}
