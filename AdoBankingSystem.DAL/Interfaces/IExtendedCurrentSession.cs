using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.Interfaces
{
    public interface IExtendedCurrentSession
    {
        string GetCurrentSessionIdByUserId(string userId);
    }
}
