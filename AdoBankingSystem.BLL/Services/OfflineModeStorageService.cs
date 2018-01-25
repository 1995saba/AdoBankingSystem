using AdoBankingSystem.BLL.Interfaces;
using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public class OfflineModeStorageService : IDataPublisher
    {
        public void PublishMessageToStorage<T>(T data) where T : EntityDto
        {
            OfflineDao<T> offlineDao = new OfflineDao<T>();
            offlineDao.Create(data);
        }

        public ICollection<T> GetAllPastData<T>() where T : EntityDto
        {
            OfflineDao<T> offlineDao = new OfflineDao<T>();
            var result = offlineDao.Read().ToList();
            offlineDao.RemoveAll<T>();
            return result;
        }

        public OfflineModeStorageService()
        {
        }
    }
}
