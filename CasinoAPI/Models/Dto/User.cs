using System.ComponentModel.DataAnnotations;

namespace CasinoAPI.Models.Dto
{
    public class User
    {
        public int id { get; set; }

        [StringLength(250)]
        public string name { get; set; }
    }
}
