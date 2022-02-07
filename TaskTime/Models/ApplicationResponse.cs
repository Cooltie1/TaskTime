using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTime.Models
{
    public class ApplicationResponse
	{
		[Key]
        [Required]
        public int AppResponseId { get; set; }
        [Required]
        public string Task { get; set; }
        public string DueDate { get; set; }
        [Required]
        public string Quadrant { get; set; }
        public bool Completed { get; set; }

        // Build Foreign Key Relationship
        [Required]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
}
}
