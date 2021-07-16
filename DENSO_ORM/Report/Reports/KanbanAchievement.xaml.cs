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

namespace DENSO_ORM.Report.Reports
{
    /// <summary>
    /// Interaction logic for KanbanAchievement.xaml
    /// </summary>
    public partial class KanbanAchievement : Page
    {
        public KanbanAchievement()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Reports.Reports obj_Report = new BUSINESS_LAYER.Reports.Reports();
        string FromTime = "", ToTime = "";
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DataTable dt = new DataTable();
        #endregion
        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                cmbShiftName.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "Report")
            {
                ENTITY_LAYER.Reports.Reports.Type = "kanbanAchievement";
                ENTITY_LAYER.Reports.Reports.MachineGroupName = txtlinegrp.Text;
                ENTITY_LAYER.Reports.Reports.Machinename = txtlinename.Text;
                ENTITY_LAYER.Reports.Reports.ShiftName = cmbShiftName.Text;
                ENTITY_LAYER.Reports.Reports.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                dt = obj_Report.BL_Reports().Tables[0];
                Report.Reports.ReportViewer.dtReport = dt;
                Report.Reports.ReportViewer.ReportName = ENTITY_LAYER.Reports.Reports.Type;
                NavigationService.Navigate(new Report.Reports.ReportViewer());
            }

        }

        private void Clear()
        {
            cmbShiftName.Text = "";
            dtpFrom.Text = "";
            dtpTo.Text = "";
            cmbShiftName.Focus();
        }
        private bool ControlValidation()
        {
            if (txtlinegrp.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtlinegrp.Focus();
                return false;
            }
            if (txtlinename.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtlinename.Focus();
                return false;
            }

            if (dtpFrom.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT FROM DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                dtpFrom.Focus();
                return false;
            }
            if (dtpTo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT TO DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                dtpTo.Focus();
                return false;
            }
            if (cmbShiftName.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbShiftName.Focus();
                return false;
            }
          
           

            return true;

        }
        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlValidation())
                    Transaction("Report");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                BtnShow_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                BtnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                BtnExit_Click(sender, e);
            }
        }

        private void CmbShiftName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbShiftName.SelectedIndex > -1)
                {
                    if (cmbShiftName.SelectedIndex == 0)//first shift
                    {
                        FromTime = " 06:30:00";
                        ToTime = " 15:00:00";
                        dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                        dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                    }
                    if (cmbShiftName.SelectedIndex == 1)//Second shift
                    {
                        FromTime = " 15:00:00";
                        ToTime = " 23:30:00";
                        dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                        dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                    }
                    if (cmbShiftName.SelectedIndex == 2)//Third shift
                    {
                        FromTime = " 23:30:00";
                        ToTime = " 06:30:00";
                        dtpFrom.Text = System.DateTime.Now.AddDays(-1).ToString("dd MMM yyyy") + FromTime;
                        dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }
        private void DtpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpTo.Text != "")
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + ToTime;
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DtpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpFrom.Text != "")
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + FromTime;
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_ACIEVEMENT_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
