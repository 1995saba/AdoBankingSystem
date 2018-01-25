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
            BankClientDto dto = new BankClientDto("qwert", "qwert", "qwert", "qwert", "qwert");
            dto.Id = "1";
            BankClientDao dao = new BankClientDao();

            string result = dao.Create(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void BankManagerDao_Create()
        {
            BankManagerDto dto = new BankManagerDto("asd", "asd", "asd", "asd", "asd");
            dto.Id = "1";
            BankManagerDao dao = new BankManagerDao();

            string result = dao.Create(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void CurrentSessionDao_Create()
        {
            CurrentSessionDto dto = new CurrentSessionDto()
            {
                UserId = "1001",
                LastOperationTime = DateTime.Now,
                SignInTime = DateTime.Now,
                EntityStatus = EntityStatusType.IsActive,
                CreatedTime = DateTime.Now
            };

            CurrentSessionDao dao = new CurrentSessionDao();
            dao.Create(dto);
        }

        [TestMethod]
        public void TransactionDao_Create()
        {
            TransactionDao transactionDao = new TransactionDao();
            string idToSet = "2001";
            string result = transactionDao.Create(new TransactionDto()
            {
                SenderId = "ericcart",
                ReceiverId = "stanmarsh",
                TransactionAmount = 666,
                TransactionType = TransactionType.ClientToClientTransaction,
                TransactionTime = DateTime.Now,
                EntityStatus = EntityStatusType.IsActive,
                CreatedTime = DateTime.Now,
                Id = idToSet
            });

            Assert.IsTrue(result == idToSet);
        }

        [TestMethod]
        public void BankClientDao_Read()
        {
            BankClientDao dao = new BankClientDao();
            var result = dao.Read();

            foreach (var item in result)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        public void BankManagerDao_Read()
        {
            BankManagerDao dao = new BankManagerDao();
            var result = dao.Read();

            foreach (var item in result)
            {
                Debug.WriteLine(item.ToString());
            }
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
        public void TransactionDao_Read()
        {
            TransactionDao dao = new TransactionDao();
            var result = dao.Read();

            foreach (var item in result)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        [TestMethod]
        public void BankClientDao_Update()
        {
            BankClientDto dto = new BankClientDto("uc", "uc", "uc", "uc", "uc");
            dto.Id = "1";
            BankClientDao dao = new BankClientDao();

            string result = dao.Update(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void BankManagerDao_Update()
        {
            BankManagerDto dto = new BankManagerDto("um", "um", "um", "um", "um");
            dto.Id = "1";
            BankManagerDao dao = new BankManagerDao();

            string result = dao.Update(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void CurrentSessionDao_Update()
        {
            CurrentSessionDto dto = new CurrentSessionDto();
            dto.UserId = "002";
            dto.LastOperationTime = DateTime.Now;
            dto.SignInTime = DateTime.Now;
            dto.EntityStatus = EntityStatusType.IsActive;
            dto.CreatedTime = DateTime.Now;
            CurrentSessionDao dao = new CurrentSessionDao();

            string result = dao.Update(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void TransactionDao_Update()
        {
            TransactionDto dto = new TransactionDto();
            dto.SenderId = "ericcart";
            dto.ReceiverId = "stanmarsh";
            dto.TransactionAmount = 10000;
            dto.TransactionType = TransactionType.ClientToClientTransaction;
            dto.TransactionTime = DateTime.Now;
            dto.EntityStatus = EntityStatusType.IsActive;
            dto.CreatedTime = DateTime.Now;
            dto.Id = "123";
            TransactionDao dao = new TransactionDao();

            string result = dao.Update(dto);

            Assert.IsTrue(dto.Id == result);
        }

        [TestMethod]
        public void BankClientsDao_Delete()
        {
            BankClientDao dao = new BankClientDao();
            dao.Remove("1");
        }

        [TestMethod]
        public void BankManagerDao_Delete()
        {
            BankManagerDao dao = new BankManagerDao();
            dao.Remove("1");
        }

        [TestMethod]
        public void CurrentSessionDao_Delete()
        {
            CurrentSessionDao dao = new CurrentSessionDao();
            dao.Remove("1");
        }

        [TestMethod]
        public void TransactionDao_Delete()
        {
            TransactionDao dao = new TransactionDao();
            dao.Remove("1");
        }
    }
}
