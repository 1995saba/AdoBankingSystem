using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    public class BankClientDto : ApplicationClientDto
    {
        #region Methods
        public override string ToString()
        {
            return GetDetailedTypeInfo();
        }
        #endregion
        #region Constructors
        public BankClientDto(
            string firstName, string lastName, string email,
            string password, string passwordConfirmation) :
            base(firstName, lastName, email, password)
        {
        }

        public BankClientDto()
        {

        }
        #endregion

    }
}
