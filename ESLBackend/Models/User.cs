using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ESLBackend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? Enable { get; set; }

        // Add the OrganizationId property for the foreign key
        public string? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }

    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool? Enable { get; set; }

        public Country? Country { get; set;}
        // Add other properties as needed
    }

    public enum Country
    {
        DK,
        US
    }

}
