using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    public class CurrentSessionDto : EntityDto
    {
        #region Properties
        public string UserId { get; set; }
        public DateTime SignInTime { get; set; }
        public DateTime LastOperationTime { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return GetDetailedTypeInfo();
        }
        #endregion
        public CurrentSessionDto() : base()
        {
            SignInTime = DateTime.Now;
            LastOperationTime = DateTime.Now;
        }
    }
}
