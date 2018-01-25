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
    public class BankManagerOfflineDao : IDAO<BankManagerDto>
    {
        private string dbToStorePath;
        public string Create(BankManagerDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankManagerDto>("bank_manager_dtos");
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
            return record.Id;
        }

        public BankManagerDto Read(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankManagerDto>("bank_manager_dtos");
                return col.FindById(id);
            }
        }

        public ICollection<BankManagerDto> Read()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankManagerDto>("bank_manager_dtos");
                return col.FindAll().ToList();
            }
        }

        public void Remove(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankManagerDto>("bank_manager_dtos");
                col.Delete(id);
            }
        }

        public string Update(BankManagerDto record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<BankManagerDto>("bank_manager_dtos");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
            return record.Id;
        }
    }
}