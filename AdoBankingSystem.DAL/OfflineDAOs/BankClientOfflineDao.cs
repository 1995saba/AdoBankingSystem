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
    public class BankClientOfflineDao : IDAO<BankClientDto>
    {
        private string dbToStorePath;
        public string Create(BankClientDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankClientDto>("bank_client_dtos");
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
            return record.Id;
        }

        public BankClientDto Read(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankClientDto>("bank_client_dtos");
                return col.FindById(id);
            }
        }

        public ICollection<BankClientDto> Read()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankClientDto>("bank_client_dtos");
                return col.FindAll().ToList();
            }
        }

        public void Remove(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankClientDto>("bank_client_dtos");
                col.Delete(id);
            }
        }

        public string Update(BankClientDto record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankClientDto>("bank_client_dtos");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
            return record.Id;
        }
    }
}
