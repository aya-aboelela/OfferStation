using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user, int Id);
    }
}
