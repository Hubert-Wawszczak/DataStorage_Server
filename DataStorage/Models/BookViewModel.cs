using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataStorage.Models
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        public int Author_Id { get; set; }

    }
}
