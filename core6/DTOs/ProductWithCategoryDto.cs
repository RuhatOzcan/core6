using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core6.DTOs
{
    public class ProductWithCategoryDto:ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
