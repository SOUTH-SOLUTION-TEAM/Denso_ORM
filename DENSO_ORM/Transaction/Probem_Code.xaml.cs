using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Interaction logic for Probem_Code.xaml
    /// </summary>
    public partial class Probem_Code : Page
    {
        public Probem_Code()
        {
            InitializeComponent();
            
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        // BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        #endregion
        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Transaction("ProblemCode");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
              //  CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "ProblemCodeUpdate")
            {
                ENTITY_LAYER.Transaction.Transaction.ProblemCode = txtPrdCode.Text;
                ENTITY_LAYER.Transaction.Transaction.RefNo = txtRefNo.Text;
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                txtResult.Visibility = Visibility.Visible;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns[0].ColumnName == "Error")
                        txtResult.Text = dt.Rows[0]["Error"].ToString();
                    else
                    {
                        txtResult.Visibility = Visibility.Hidden;
                        txtPrdCode.Text = "";
                        CommonClasses.CommonVariable.Break = "";
                        txtPrdCode.Focus();
                    }
                }
            }
            if (Type == "ProblemCode")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtReason.Text = "Machine Stop - " + dt.Rows[i]["FromTime"].ToString() + " To " + dt.Rows[i]["ToTime"].ToString() + ". ";
                    txtRefNo.Text = dt.Rows[i]["RefNo"].ToString();
                    CommonClasses.CommonVariable.Break = "ProblemCode";
                    txtPrdCode.Focus();
                }
                if (dt.Rows.Count == 0)
                {
                    txtReason.Text = "";
                    txtRefNo.Text = "";
                    CommonClasses.CommonVariable.Break = "";
                }
            }
        }
        private void TxtPrdCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    Transaction("ProblemCodeUpdate");
                }
            }
            catch(Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), System.Reflection.MethodBase.GetCurrentMethod().ToString(), "PROBLEMCODE", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                //obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_GROUP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}
