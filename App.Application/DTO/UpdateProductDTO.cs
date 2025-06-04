using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTO{
    public record UpdateProductDTO(int Id, string Name, decimal Price);
}
