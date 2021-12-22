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
using Newtonsoft.Json.Linq;

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
           
            
            string connectionString = "Data Source=DataStorage.db";
            using var con = new SQLiteConnection(connectionString);
            con.Open();
            string query = "SELECT Books.Id,Books.Title, Books.ISBN,Books.Year,Authors.FullName FROM Books INNER JOIN Authors ON Authors.Id = Books.Author_Id WHERE Authors.FullName = '" + Author + "'";
            using var cmd = new SQLiteCommand(query, con);

            
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<BookResponse> parts = new List<BookResponse>();

            while (rdr.Read())
            {

                parts.Add(new BookResponse(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(4), rdr.GetInt32(3)));

            }
            var Json = JsonSerializer.Serialize(parts);
            var jArr = JArray.Parse(Json);

            jArr.Descendants().OfType<JProperty>()
                              .Where(p => p.Name == "" )
                              .ToList()
                              .ForEach(att => att.Remove());

            var newJson = jArr.ToString();
            string jsonString = newJson;

     
            //odpowiedz parameter
            return Ok(jsonString);
        }

        [HttpGet]
        [Route("GetAllId")]
        public ActionResult<string> IndexId()
        {
            
            string connectionString = "Data Source=DataStorage.db";
          
            using var con = new SQLiteConnection(connectionString);
            con.Open();
            string query = "SELECT Books.Id,Books.Title, Books.ISBN,Books.Year,Authors.FullName FROM Books INNER JOIN Authors ON Authors.Id = Books.Author_Id";
            using var cmd = new SQLiteCommand(query, con);


            using SQLiteDataReader rdr = cmd.ExecuteReader();
            List<BookResponse> parts = new List<BookResponse>();

            while (rdr.Read())
            {

                parts.Add(new BookResponse(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(4), rdr.GetInt32(3)));
                

            }
            
            var Json = JsonSerializer.Serialize(parts);
            var jArr = JArray.Parse(Json);

            jArr.Descendants().OfType<JProperty>()
                              .Where(p => p.Name == "Title" || p.Name =="ISBN" || p.Name == "Year" || p.Name == "Authors")
                              .ToList()
                              .ForEach(att => att.Remove());

            var newJson = jArr.ToString();
            string jsonString = newJson;
            //odpowiedz parameter
            return Ok(jsonString);
        }

        [HttpPost]
        [Route("AddBook")]
        public ActionResult Post([FromHeader] BookHeader book)
        {
            Guid g = Guid.NewGuid();
            var byteArray = g.ToByteArray();
            string hex = BitConverter.ToString(byteArray).Replace("-", string.Empty);
            Console.WriteLine(hex);
            string connectionString = "Data Source=DataStorage.db";
            using var con = new SQLiteConnection(connectionString);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO Books(Id,Title,ISBN,Year) VALUES(@Id,@Title,@ISBN,@Year)";
            cmd.Parameters.AddWithValue("@Id",hex);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Year", book.Year);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
          
            cmd.CommandText = "INSERT INTO Authors(Id,FullName) VALUES(@Id,@FullName)";
            cmd.Parameters.AddWithValue("@Id",Guid.NewGuid() );
            cmd.Parameters.AddWithValue("@FullName", book.Author);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            return Ok();
        }
    }
}
