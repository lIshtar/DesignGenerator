using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.DBEntities
{
    public class Illustration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string IllustrationPath { get; set; }
        public DateTime GenerationDate { get; set; } = DateTime.Now;
        public bool IsReviewed { get; set; }

        //[ForeignKey("item")]
        //public int? Item { get; set; }
    }
}
