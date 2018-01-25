using System;
using System.Diagnostics;
using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdoBankingSystem.UnitTests
{
    [TestClass]
    public class DALTests
    {
        [TestMethod]
        public void BankClientDao_Create()
        {
            // Arrange
            BankClientDto bankClientDto = 
                new BankClientDto("Test", "Test", "Test", "Test", "Test");

            bankClientDto.Id = "2222";

            BankClientDao bankClientDao =
                new BankClientDao();

            // Act
            string result = bankClientDao.Create(bankClientDto);

            // Assert
            Assert.IsTrue(bankClientDto.Id == result);
        }

        [TestMethod]
        public void CurrentSessionDao_Create()
        {
            CurrentSessionDto dto = new CurrentSessionDto()
            {
                UserId = "1111",
                LastOperationTime = DateTime.Now,
                SignInTime = DateTime.Now,
                EntityStatus = EntityStatusType.IsActive,
                CreatedTime = DateTime.Now
            };

            CurrentSessionDao dao = new CurrentSessionDao();
            dao.Create(dto);
        }

        [TestMethod]
        public void CurrentSessionDao_Read()
        {
            CurrentSessionDao dao = new CurrentSessionDao();
            var result = dao.Read();

            foreach (var item in result)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        [TestMethod]
        public void TransactionDao_Create()
        {
            TransactionDao transactionDao = new TransactionDao();
            string idToSet = Guid.NewGuid().ToString();
            string result = transactionDao.Create(new TransactionDto()
            {
                SenderId = "Zeus",
                ReceiverId = "Drow Ranger",
                TransactionAmount = 6200,
                TransactionType = TransactionType.ClientToClientTransaction,
                TransactionTime = DateTime.Now,
                EntityStatus = EntityStatusType.IsActive,
                CreatedTime = DateTime.Now,
                Id = idToSet
            });

            Assert.IsTrue(result == idToSet);
        }
    }
}
