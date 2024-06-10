using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Dtos
{
    public class CreateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
