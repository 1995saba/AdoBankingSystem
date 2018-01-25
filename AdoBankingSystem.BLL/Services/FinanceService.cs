using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public class FinanceService
    {
        private readonly TransactionDao _transactionDao = null;
        //public decimal GetAvailableAmount(string userId)
        //{

        //}

        // Decimal 1 - Sent Total, Decimal 2 - Received Total
        public Tuple<decimal, decimal> GetHistoryOverview(string userId)
        {
            decimal totalSent = 0M, totalReceived = 0M;

            var allTransactionsWithUser = _transactionDao
                .ReadAllTransactionsOfUser(userId)
                .ToList();

            foreach (var item in allTransactionsWithUser)
            {
                if(userId == item.SenderId)
                {
                    totalSent += item.TransactionAmount;
                }
                else if(userId == item.ReceiverId)
                {
                    totalReceived += item.TransactionAmount;
                }
            }
            return new Tuple<decimal, decimal>(totalSent, totalReceived);
        }

        public void MakeClientToClientTransaction(string userId, string receiverId, decimal amount)
        {
            var transactionDto = new TransactionDto()
            {
                SenderId = userId,
                ReceiverId = receiverId,
                TransactionAmount = amount,
                TransactionTime = DateTime.Now,
                TransactionType = TransactionType.ClientToClientTransaction,
                Id = Guid.NewGuid().ToString()          
            };
            _transactionDao.Create(transactionDto);
        }

        public FinanceService()
        {
            _transactionDao = new TransactionDao();
        }
    }
}
