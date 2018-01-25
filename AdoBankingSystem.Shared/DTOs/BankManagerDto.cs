using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    public class BankManagerDto : ApplicationClientDto
    {
        public BankManagerDto(
            string firstName, string lastName, string email,
            string password):base(firstName,lastName,email,password)
        {
        }
    }
}
