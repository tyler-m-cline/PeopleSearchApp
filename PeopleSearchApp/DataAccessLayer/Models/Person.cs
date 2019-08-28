using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleSearchApp.DataAccessLayer.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Column("LastName")]
        public string LastName { get; set; }

        [Required]
        [Column("Age")]
        public int Age { get; set; }

        [Required]
        [Column("StreetAddress")]
        public string StreetAddress { get; set; }

        [Required]
        [Column("City")]
        public string City { get; set; }

        [Required]
        [Column("State")]
        public string State { get; set; }

        [Required]
        [Column("ZipCode")]
        public int ZipCode { get; set; }

        [Column("Photograph")]
        public string Photograph { get; set; }
    }
}
