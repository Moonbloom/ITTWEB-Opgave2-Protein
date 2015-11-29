using System.ComponentModel.DataAnnotations;

namespace ITTWEB_Opgave2_Protein.Models
{
    public class TodoItemViewModel
    {
        [Required(ErrorMessage = "The Task Field is Required.")]
        public string Task { get; set; }
        public bool Completed { get; set; }
    }
}