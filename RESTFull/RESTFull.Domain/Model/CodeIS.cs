using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Model
{
    public class CodeIS
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Code { get; set; }

        // Свойства навигации
        public int PersonID { get; set; }

        public Person Person { get; set; }
    }
}
