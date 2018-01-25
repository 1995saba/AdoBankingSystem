using AdoBankingSystem.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoBankingSystem.ClientUI.Base
{
    public class BaseForm : Form
    {
        protected ApplicationClientService _applicationClientService;
        public BaseForm()
        {
            _applicationClientService = new ApplicationClientService();
        }
    }
}
