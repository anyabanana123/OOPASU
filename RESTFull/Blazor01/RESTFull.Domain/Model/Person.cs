using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace RESTFull.Domain.Model
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string Familia { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Otchestvo { get; set; } = string.Empty;
        public string DateBirth { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public List<CodeIS> CodeIS_s { get; set; } = new List<CodeIS>();


        public void AddCodeIS(CodeIS codeIS)
        {
            CodeIS_s.Add(codeIS);
        }

        public void RemoveAt(int index)
        {
            CodeIS_s.RemoveAt(index);
        }

        public int CodeISCount { get { return CodeIS_s.Count; } }


    }
}

