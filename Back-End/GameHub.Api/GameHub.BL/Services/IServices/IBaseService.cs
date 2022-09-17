using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.BL.Services.IServices
{
    public interface IBaseService
    {
        Task SaveChangesAsync();
    }
}
