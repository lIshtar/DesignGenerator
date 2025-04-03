using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.DBEntities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Request {  get; set; }
        public string Response { get; set; }

        [ForeignKey("model")]
        public int ModelId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
