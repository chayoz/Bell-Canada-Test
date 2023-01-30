using System.ComponentModel.DataAnnotations;

namespace Bell_Canada_Test.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? DepartmentName { get; set; }
        public string? RequestedBy { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }

    }
}
