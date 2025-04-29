using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return $"{Timestamp}: {Sender} - {Text}";
        }
    }
}
