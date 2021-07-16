using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for ManPowerEntry.xaml
    /// </summary>
    public partial class ManPowerEntry : Page
    {
        public ManPowerEntry()
        {
            InitializeComponent();
            txtNoOfMan.Focus();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        #endregion
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    if (txtNoOfMan.Text == "")
                    {
                        CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF MAN POWER", CommonClasses.CommonVariable.CustomStriing.Error.ToString());
                        txtNoOfMan.Focus();
                        return;
                    }
                    Transaction("ManPower");
                   
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAN_POWER_ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "ManPower")
            {
                bool Flag = false;
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                ENTITY_LAYER.Transaction.Transaction.Manpower = txtNoOfMan.Text;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                txtNoOfMan.Text = "";
                txtNoOfMan.Focus();
            }
        }
      
    }
}
