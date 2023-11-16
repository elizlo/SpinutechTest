using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models
{
    public class GameViewModel
    {
        [Required]
        [RegularExpression(@"([01]\s*)+", ErrorMessage = "The input must contain only 0 and 1 separated with space and new line characters")]
        public string Input { get; set; }
        
        public string? Result { get; set; }
    }
}
