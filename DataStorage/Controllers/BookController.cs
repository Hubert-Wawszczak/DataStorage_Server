using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataStorage.Data;
using DataStorage.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SQLite;
using DataStorage.ResponseModel;
using System.Text.Json;

namespace DataStorage.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Books?Author=
        [HttpGet("{Author}")]
        public ActionResult<string> Index([FromQuery] string Author)
        {
            //Varchar to Guid
            string connectionString = "Data Source=DataStorage.db";
            BookViewModel bookViewModel = new BookViewModel();
            using var con = new SQLiteConnection(connectionString);
            con.Open();
            string query = "SELECT Books.Id,Books.Title, Books.ISBN,Books.Year,Authors.FullName FROM Books INNER JOIN Authors ON Authors.Id = Books.Author_Id WHERE Authors.FullName = '" + Author + "'";
            using var cmd = new SQLiteCommand(query, con);


            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<BookResponse> parts = new List<BookResponse>();

            while (rdr.Read())
            {

                parts.Add(new BookResponse(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(4), rdr.GetInt32(3)));

            }


            string jsonString = JsonSerializer.Serialize(parts);
            //odpowiedz parameter
            return jsonString;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<string> IndexId()
        {
            //Varchar to Guid
            string connectionString = "Data Source=DataStorage.db";
            BookViewModel bookViewModel = new BookViewModel();
            using var con = new SQLiteConnection(connectionString);
            con.Open();
            string query = "SELECT Books.Id,Books.Title, Books.ISBN,Books.Year,Authors.FullName FROM Books INNER JOIN Authors ON Authors.Id = Books.Author_Id";
            using var cmd = new SQLiteCommand(query, con);


            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<GetMetaInfoBook> parts = new List<GetMetaInfoBook>();

            while (rdr.Read())
            {
                parts.Add(new GetMetaInfoBook(rdr.GetString(1), rdr.GetString(4),  rdr.GetInt32(3),rdr.GetString(2)));
            }


            string jsonString = JsonSerializer.Serialize(parts);
            //odpowiedz parameter
            return jsonString;
        }


    }
}
