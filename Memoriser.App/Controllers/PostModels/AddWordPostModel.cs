using System.ComponentModel.DataAnnotations;

namespace Memoriser.App.Controllers.PostModels
{
    public class AddWordPostModel
    {
        [Required]
        [RegularExpression(@"^[\p{L} | \s]*$")]
        public string Word { get; set; }

        [Required]
        [MinLength(1)]
        public string[] Answers { get; set; }
    }
}
