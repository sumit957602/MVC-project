using System;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

    }
}
