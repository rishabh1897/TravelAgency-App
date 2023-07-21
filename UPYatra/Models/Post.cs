using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UPYatra.Models
{
    public class Post
    {
        public long Id { get; set; }
        private string _key;
        public string Key {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");//Guid.NewGuid().ToString();
                }
                return _key;
            }
            set { _key = value; }
        }

        [Display(Name = "Post Name")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Title must be b/w 5 and 100 in lenght")]
        public string Title { get; set; }
        public string? Author { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(10,ErrorMessage = "Body must be greater then 10 characters")]
        public string Body { get; set; }
        public DateTime Posted { get; set; }
    }
}
