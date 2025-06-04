using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTO {
    public record RegistrationResponse(bool Flag, string Message = null!);
    
}
