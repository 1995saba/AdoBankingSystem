using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.Utils
{
    public static class ApplicationUserIdGenerator
    {
        public static string GenerateUniqueId(string firstName, string lastName, ApplicationClientType applicationClientType)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 3; i++)
            {
                sb.Append(firstName[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                sb.Append(lastName[i]);
            }

            switch (applicationClientType)
            {
                case ApplicationClientType.BankClient:
                    sb.Append("C");
                    break;
                case ApplicationClientType.BankManager:
                    sb.Append("M");
                    break;
            }

            sb.Append(RandomGenerator.GetRandomInteger());
            return sb.ToString().ToUpper();
        }
    }
}
