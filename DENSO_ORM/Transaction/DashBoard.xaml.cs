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
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Page
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        #region Variable and Objects

        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();

        #endregion

        private void Transaction(string Type)
        {
            if (Type == "GetShiftDetails")
            {

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[i]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[i]["Time"].ToString();
                    CommonClasses.CommonVariable.Break = dt.Rows[i]["Break"].ToString();
                }
            }

            if (Type == "MachineONandOFF")
            {
                bool Flag = false;
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (TimeSpan.Parse(dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[0].ToString()) <= TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm"))
                                              && TimeSpan.Parse(dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[1].ToString()) > TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm")))
                    {
                        Flag = true;

                    }
                }
                if (Flag == true)
                {
                    CommonClasses.CommonVariable.MachinePlane = dt.Rows[0]["Status"].ToString().ToUpper();
                    CommonClasses.CommonVariable.MachineStatus = "LINE UNDER " + dt.Rows[0]["Status"].ToString().ToUpper() + " FROM " + dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[0] + " TO " + dt.Rows[0]["ShiftTiming"].ToString().Split(' ')[1];

                    Ggrid2.Visibility = Visibility.Visible;
                    Ggrid1.Visibility = Visibility.Hidden;
                }
                else
                {
                    ENTITY_LAYER.Transaction.Transaction.Type = "MachineONandOFFUpdate";
                    DataTable dt1 = obj_Tran.BL_DashBoard().Tables[0];
                    Ggrid2.Visibility = Visibility.Hidden;
                    Ggrid1.Visibility = Visibility.Visible;
                }
            }
            if (Type == "ManPower")
            {
                bool Flag = false;
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
              //  ENTITY_LAYER.Transaction.Transaction.Manpower ="";

                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                if (dt.Columns.Count > 1)
                {
                    Ggrid3.Visibility = Visibility.Hidden;
                }
             else
                {
                    Ggrid3.Visibility = Visibility.Visible;
                }
            }
        }
        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer1_Tick);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer1.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CommonClasses.CommonVariable.MachineName != "")
                    txtHeader.Text = CommonClasses.CommonVariable.MachineName;
                else
                    txtHeader.Text = "DASH BOARD";

                if (Ggrid3.IsVisible == false)
                {

                    if (CommonClasses.CommonVariable.Break != "")
                        Ggrid4.Visibility = Visibility.Visible;
                    else
                        Ggrid4.Visibility = Visibility.Hidden;
                }
                else
                    Ggrid4.Visibility = Visibility.Hidden;

                Transaction("MachineONandOFF");
                Transaction("ManPower");
               // Transaction("ProblemCode");
                Transaction("GetShiftDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
             //   CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (frm5M.IsVisible == true)
                {
                    frm5M.Visibility = Visibility.Hidden;
                    frmStock.Visibility = Visibility.Visible;
                }
                else if (frm5M.IsVisible == false)
                {
                    frm5M.Visibility = Visibility.Visible;
                    frmStock.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dispatcherTimer.Stop();
                dispatcherTimer1.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DASH_BOARD", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
