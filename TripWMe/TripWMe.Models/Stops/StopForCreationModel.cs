using System.ComponentModel.DataAnnotations;

namespace TripWMe.Models.Stops
{
    public class StopForCreationModel
    {
        [Required]
        public string StopName { get; set; }
        public string StopDescription { get; set; }
        public int Order { get; set; }
    }
}
