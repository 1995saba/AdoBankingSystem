using AdoBankingSystem.DAL.Interfaces;
using AdoBankingSystem.Shared.DTOs;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.DAOs
{
    public class OfflineDao<T> : IDAO<T> where T : EntityDto
    {
        public string Create(T record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                string tableName = typeof(T).ToString() + "s";
                tableName = tableName.Replace(".", "_");
                var col = db.GetCollection<T>(tableName);
                col.EnsureIndex(x => x.Id, true);
                col.Insert(record);
            }
            return record.Id;
        }

        public T Read(string id)
        {
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                var col = db.GetCollection<T>(typeof(T).ToString() + "s");
                return col.FindById(id);
            }
        }

        public ICollection<T> Read()
        {
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                string tableName = typeof(T).ToString() + "s";
                tableName = tableName.Replace(".", "_");
                var col = db.GetCollection<T>(tableName);
                return col.FindAll().ToList();
            }
        }

        public void Remove(string id)
        {
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                var col = db.GetCollection<T>(typeof(T).ToString() + "s");
                col.Delete(id);
            }
        }

        public void RemoveAll<T>()
        {
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                string tableName = typeof(T).ToString() + "s";
                tableName = tableName.Replace(".", "_");
                db.DropCollection(tableName);
            }
        }
        public string Update(T record)
        {
            using (var db = new LiteDatabase(@"C:\Users\Iskander\Desktop\MyData.db"))
            {
                var col = db.GetCollection<T>(typeof(T).ToString() + "s");
                var itemToUpdate = col.FindById(record.Id);
                itemToUpdate = record;
                col.Update(itemToUpdate);
            }
            return record.Id;
        }
    }
}