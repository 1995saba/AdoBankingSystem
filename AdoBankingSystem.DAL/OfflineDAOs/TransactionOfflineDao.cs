using AdoBankingSystem.DAL.Interfaces;
using AdoBankingSystem.Shared.DTOs;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.OfflineDAOs
{
    public class TransactionOfflineDao : IDAO<TransactionDto>
    {
        private string dbToStorePath;
        public string Create(TransactionDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<TransactionDto>("transaction_dtos");
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
            return record.Id;
        }

        public TransactionDto Read(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<TransactionDto>("transaction_dtos");
                return col.FindById(id);
            }
        }

        public ICollection<TransactionDto> Read()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<TransactionDto>("transaction_dtos");
                return col.FindAll().ToList();
            }
        }

        public void Remove(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<TransactionDto>("transaction_dtos");
                col.Delete(id);
            }
        }

        public string Update(TransactionDto record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<TransactionDto>("transaction_dtos");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
            return record.Id;
        }
    }
}