using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Link
    {
        [Key]
        public long ID { get; set; }
        public string Url { get; set; }
        public string ShortUrL { get; set; }
        public string LinkHash { get; set; }
    }
}
