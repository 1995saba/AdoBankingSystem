using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Interfaces
{
    public interface IDataPublisher
    {
        void PublishMessageToStorage<T>(T data) where T : EntityDto;
    }
}
