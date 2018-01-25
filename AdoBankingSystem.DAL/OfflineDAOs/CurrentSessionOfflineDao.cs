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
    public class CurrentSessionOfflineDao : IDAO<CurrentSessionDto>
    {
        private string dbToStorePath;
        public string Create(CurrentSessionDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<CurrentSessionDto>("current_session_dtos");
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
            return record.Id;
        }

        public CurrentSessionDto Read(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<CurrentSessionDto>("current_session_dtos");
                return col.FindById(id);
            }
        }

        public ICollection<CurrentSessionDto> Read()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<CurrentSessionDto>("current_session_dtos");
                return col.FindAll().ToList();
            }
        }

        public void Remove(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<CurrentSessionDto>("current_session_dtos");
                col.Delete(id);
            }
        }

        public string Update(CurrentSessionDto record)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<CurrentSessionDto>("current_session_dtos");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
            return record.Id;
        }
    }
}