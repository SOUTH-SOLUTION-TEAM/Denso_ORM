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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for AndonSMSandCall.xaml
    /// </summary>
    public partial class AndonSMSandCall : Page
    {
        public AndonSMSandCall()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string AndonType = "";
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
                txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                Transaction("GetShiftDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "GetShiftDetails")
            {

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                bool Flag = false;
                bool Flag1 = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (TimeSpan.Parse(dt.Rows[i]["ShiftTiming"].ToString().Split(' ')[0].ToString()) <= TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm"))
                    //    && TimeSpan.Parse(dt.Rows[i]["ShiftTiming"].ToString().ToString().Split(' ')[1].ToString()) > TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm")))
                    //{
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[i]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[i]["Time"].ToString();
                    // Flag = true;
                    // }

                }
                
                //if (Flag == false)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        CommonClasses.CommonVariable.ShiftName = dt.Rows[2]["ShiftName"].ToString();
                //    }
                //}
            }
            if (Type == "GetOperation")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Masters.Masters.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbOperation, dt, "Operation", "Operation");
            }
            if (Type == "Save")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                ENTITY_LAYER.Transaction.Transaction.Fortype = AndonType;
                ENTITY_LAYER.Transaction.Transaction.Station = cmbOperation.Text;
                CommonClasses.CommonVariable.Result = obj_Tran.BL_AndonCall();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                }
                else if (CommonClasses.CommonVariable.Result == "Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
            Transaction("GetOperation");

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_SMS_AND_CALL", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private bool ControlValidation()
        {
            if(cmbOperation.SelectedIndex==-1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLESAE SELECT STATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbOperation.Focus();
                return false;
            }
            return true;
        }
        private void BtnCalltoAGV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ControlValidation())
                {
                    string Result = CommonClasses.CommonMethods.objWS.APKDetails("AGV", CommonClasses.CommonVariable.MachineGroup, CommonClasses.CommonVariable.MachineName, CommonClasses.CommonVariable.ModelName, CommonClasses.CommonVariable.Time, CommonClasses.CommonVariable.ShiftName, cmbOperation.Text, "Save");
                    CommonClasses.CommonMethods.MessageBoxShow(Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

                }

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnPARTS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlValidation())
                {
                    AndonType = "PART FEADER";
                    Transaction("Save");

                   // string Result = CommonClasses.CommonMethods.objWS.APKDetails("PART FEADER", CommonClasses.CommonVariable.MachineGroup, CommonClasses.CommonVariable.MachineName,CommonClasses.CommonVariable.ModelName,CommonClasses.CommonVariable.Time,CommonClasses.CommonVariable.ShiftName,cmbOperation.Text,"Save");
                    //CommonClasses.CommonMethods.MessageBoxShow(Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnCalltoQA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlValidation())
                {
                    AndonType = "QUALITY";
                    Transaction("Save");
                    //string Result = CommonClasses.CommonMethods.objWS.APKDetails("QUALITY", CommonClasses.CommonVariable.MachineGroup, CommonClasses.CommonVariable.MachineName, CommonClasses.CommonVariable.ModelName, CommonClasses.CommonVariable.Time, CommonClasses.CommonVariable.ShiftName, cmbOperation.Text, "Save");
                    //CommonClasses.CommonMethods.MessageBoxShow(Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnCalltoMaintenance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlValidation())
                {
                    AndonType = "MAINTENANCE";
                    Transaction("Save");
                  //  string Result = CommonClasses.CommonMethods.objWS.APKDetails("MAINTENANCE", CommonClasses.CommonVariable.MachineGroup, CommonClasses.CommonVariable.MachineName, CommonClasses.CommonVariable.ModelName, CommonClasses.CommonVariable.Time, CommonClasses.CommonVariable.ShiftName, cmbOperation.Text, "Save");
                   // CommonClasses.CommonMethods.MessageBoxShow(Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ImgSmily3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}