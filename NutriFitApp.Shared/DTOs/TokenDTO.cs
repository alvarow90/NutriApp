using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFitApp.Shared.DTOs;

public class TokenDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiraton { get; set; }
}
