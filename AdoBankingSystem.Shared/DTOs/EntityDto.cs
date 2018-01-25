using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    #region Helper Types
    public enum EntityStatusType
    {
        IsDeleted,
        IsBlocked,
        IsActive
    }
    #endregion

    public abstract class EntityDto
    {
        #region Properties
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public EntityStatusType EntityStatus { get; set; }
        #endregion

        #region Methods
        protected string GetDetailedTypeInfo()
        {
            StringBuilder sb = new StringBuilder();
            Type currentType = GetType();
            PropertyInfo[] properties = currentType.GetProperties();

            sb.AppendLine(currentType.Name);
            foreach (var item in properties)
            {
                sb.AppendLine($"{item.Name} - {item.GetValue(this, null)}");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return $"{GetDetailedTypeInfo()}";
        }
        #endregion

        #region Constructors
        public EntityDto(string id) : this()
        {
            Id = id;
        }
        public EntityDto()
        {
            CreatedTime = DateTime.Now;
            EntityStatus = EntityStatusType.IsActive;
        }
        #endregion

    }
}
