using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    public enum ApplicationClientType
    {
        BankClient,
        BankManager
    }
    public abstract class ApplicationClientDto : EntityDto
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ApplicationClientType ApplicationClientType { get; set; }

        #endregion
        public ApplicationClientDto(string firstName, string lastName, 
            string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public ApplicationClientDto()
        {

        }
    }
}
