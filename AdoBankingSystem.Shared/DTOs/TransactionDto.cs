using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{

    public enum TransactionType
    {
        ClientToClientTransaction
    }
    public class TransactionDto : EntityDto
    {
        #region Properties
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionTime { get; set; }
        #endregion

        #region Constructors
        public TransactionDto()
        {

        }
        #endregion
    }
}
