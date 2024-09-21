
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{

    public class BlogAuthor
    {
        public int AuthorID { get; set; }
        public int BlogID { get; set; }
    }
}