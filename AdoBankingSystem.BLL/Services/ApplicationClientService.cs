using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using AdoBankingSystem.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public class ApplicationClientService
    {
        private readonly BankClientDao _bankClientDao;
        private readonly CurrentSessionDao _currentSessionsDao;
        public string RegisterNewBankClient(BankClientDto bankClientDto)
        {
            bankClientDto.Id = ApplicationUserIdGenerator.GenerateUniqueId(
                bankClientDto.FirstName, 
                bankClientDto.LastName, 
                ApplicationClientType.BankClient);

            bankClientDto.PasswordHash = 
                PasswordHashGenerator.GetMd5Hash(bankClientDto.PasswordHash);

            return _bankClientDao.Create(bankClientDto);
        }

        public bool SignInToApplication(string email, string password)
        {
            string userId = "";
            CurrentSessionDto sessionToWorkWith = null;
            List<BankClientDto> allUsers = _bankClientDao.Read()
                .ToList();
            List<CurrentSessionDto> allSessions = _currentSessionsDao.Read()
                .ToList();

            foreach (var item in allUsers)
            {
                if(item.Email == email && 
                    PasswordHashGenerator.GetMd5Hash(password) == item.PasswordHash)
                {
                    userId = item.Id;
                    break;
                }
            }

            foreach (var item in allSessions)
            {
                if(item.UserId == userId)
                {
                    sessionToWorkWith = item;
                    break;
                }
            }

            if(sessionToWorkWith == null)
            {
                _currentSessionsDao.Create(new CurrentSessionDto()
                {
                    LastOperationTime = DateTime.Now,
                    UserId = userId,
                    Id = Guid.NewGuid().ToString()
                });
                return true;
            }

            if((DateTime.Now - sessionToWorkWith.LastOperationTime).TotalMinutes > 60D)
            {
                _currentSessionsDao.Remove(sessionToWorkWith.Id);
                _currentSessionsDao.Create(new CurrentSessionDto()
                {
                    LastOperationTime = DateTime.Now,
                    UserId = userId,
                    Id = Guid.NewGuid().ToString()
                });
                return true;
            }
            return false;
        }

        public bool LogOutFromApplication(string userIdToLogout)
        {
            string sessionId = _currentSessionsDao.GetCurrentSessionIdByUserId(userIdToLogout);
            if (sessionId != null)
            {
                _currentSessionsDao.Remove(sessionId);
                return true;
            }
            else return false; 
        }
        private bool IsValidEntity(string password, string passwordConfirmation)
        {
            if (password == passwordConfirmation) return true;
            return false;
        }

        public ApplicationClientService()
        {
            _bankClientDao = new BankClientDao();
            _currentSessionsDao = new CurrentSessionDao();
        }
    }
}
