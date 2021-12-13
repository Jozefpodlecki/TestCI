using System.ComponentModel.DataAnnotations;

namespace TestCI.Web.Models
{
    public class NewTodo
    {
        [Required]
        public string Name { get; set; }
    }
}
