using System.ComponentModel.DataAnnotations;

namespace Kiosk.Business.ViewModels
{
    public partial class  PersonModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }        
    }
}