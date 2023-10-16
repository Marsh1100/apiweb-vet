using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Laboratory : BaseEntity
    {
        public string Name { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }

        public ICollection<Medicine> Medicines { get; set; }
    }
}