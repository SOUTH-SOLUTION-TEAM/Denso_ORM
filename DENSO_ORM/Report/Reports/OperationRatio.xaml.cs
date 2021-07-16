using System;
using System.Collections.Generic;
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
using System.Data;
namespace DENSO_ORM.Report.Reports
{
    /// <summary>
    /// Interaction logic for OperationRatio.xaml
    /// </summary>
    public partial class OperationRatio : Page
    {
        public OperationRatio()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Reports.Reports obj_Report = new BUSINESS_LAYER.Reports.Reports();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string FromTime = "", ToTime = "";
        DataTable dt = new DataTable();
        DataTable ModuleList = new DataTable();
        DataTable ShiftName = new DataTable();

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
                Transaction("LoadShitName");
                dtpFrom.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "ModuleName")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ModuleList = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                listModel.ItemsSource = ModuleList.DefaultView;

            }
            if (Type == "LoadShitName")
            {
                ShiftName = new DataTable();
                ShiftName.Columns.Add("ShiftName");
                ShiftName.Columns.Add("Flag");
                ShiftName.Rows.Add("First Shift", "False");
                ShiftName.Rows.Add("Second Shift", "False");
                ShiftName.Rows.Add("Third Shift", "False");
                listShift.ItemsSource = ShiftName.DefaultView;
            }
            if (Type == "Report")
            {
                ENTITY_LAYER.Reports.Reports.Type = "OperationRatio";
                ENTITY_LAYER.Reports.Reports.MachineGroupName = txtlinegrp.Text;
                ENTITY_LAYER.Reports.Reports.Machinename = txtlinename.Text;
                ENTITY_LAYER.Reports.Reports.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.ShiftName = "";
                ENTITY_LAYER.Reports.Reports.ModelNo = "";

                for (int i = 0; i < ShiftName.Rows.Count; i++)
                {
                    if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                    {
                        if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                            ENTITY_LAYER.Reports.Reports.ShiftName = "'" + ShiftName.Rows[i][0].ToString() + "'";
                        else
                            ENTITY_LAYER.Reports.Reports.ShiftName = ENTITY_LAYER.Reports.Reports.ShiftName + ",'" + ShiftName.Rows[i]["ShiftName"].ToString() + "'";
                    }
                }
                if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                {
                    chkShift.IsChecked = true;
                    for (int i = 0; i < ShiftName.Rows.Count; i++)
                    {
                        if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                        {
                            if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                                ENTITY_LAYER.Reports.Reports.ShiftName = "'" + ShiftName.Rows[i][0].ToString() + "'";
                            else
                                ENTITY_LAYER.Reports.Reports.ShiftName = ENTITY_LAYER.Reports.Reports.ShiftName + ",'" + ShiftName.Rows[i]["ShiftName"].ToString() + "'";
                        }
                    }
                }

                for (int i = 0; i < ModuleList.Rows.Count; i++)
                {
                    if (ModuleList.Rows[i]["Flag"].ToString() == "True")
                    {
                        if (ENTITY_LAYER.Reports.Reports.ModelNo == "")
                            ENTITY_LAYER.Reports.Reports.ModelNo = "'" + ModuleList.Rows[i][0].ToString() + "'";
                        else
                            ENTITY_LAYER.Reports.Reports.ModelNo = ENTITY_LAYER.Reports.Reports.ModelNo + ",'" + ModuleList.Rows[i]["ModelName"].ToString() + "'";

                    }
                }

                if (ENTITY_LAYER.Reports.Reports.ModelNo == "")
                {
                    chkModel.IsChecked = true;
                    for (int i = 0; i < ModuleList.Rows.Count; i++)
                    {
                        if (ModuleList.Rows[i]["Flag"].ToString() == "True")
                        {
                            if (ENTITY_LAYER.Reports.Reports.ModelNo == "")
                                ENTITY_LAYER.Reports.Reports.ModelNo = "'" + ModuleList.Rows[i][0].ToString() + "'";
                            else
                                ENTITY_LAYER.Reports.Reports.ModelNo = ENTITY_LAYER.Reports.Reports.ModelNo + ",'" + ModuleList.Rows[i]["ModelName"].ToString() + "'";

                        }
                    }
                }
         
                dt = obj_Report.BL_Reports().Tables[0];
                Report.Reports.ReportViewer.dtReport = dt;
                Report.Reports.ReportViewer.ReportName = ENTITY_LAYER.Reports.Reports.Type;
                NavigationService.Navigate(new Report.Reports.ReportViewer());
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
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

            return true;

        }
        //private void Cmbmachinegrp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Cmbmachinegrp.SelectedItem != null)
        //        {
        //            Transaction("GetMachinename");
                   
        //        }
        //        else
        //        {
        //            //chkShift.IsChecked = false;
        //            //chkModel.IsChecked = false;
        //            cmbmachinename.ItemsSource = null;
        //            //listModel.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void Clear()
        {
            txtlinename.Text = "";
            txtlinegrp.Text = "";
            chkModel.IsChecked = false;
            chkShift.IsChecked = false;
            dtpFrom.Text = "";
            dtpTo.Text = "";
            //txtlinename.ItemsSource = null;
            dtpFrom.Focus();
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ControlValidation())
                Transaction("Report");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
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
        private void Txtlinename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtlinename.Text != "")
                {
                    Transaction("ModuleName");
                }
                else
                {
                    chkShift.IsChecked = false;
                    chkModel.IsChecked = false;
                    //cmbmachinename.ItemsSource = null;
                    listModel.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        //private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinename.SelectedItem != null)
        //        {
        //            Transaction("ModuleName");
        //        }
        //        else
        //        {
        //            chkShift.IsChecked = false;
        //            chkModel.IsChecked = false;
        //            cmbmachinename.ItemsSource = null;
        //            listModel.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void ChkShift_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkShift.IsChecked ==true)
                {
                    for (int i = 0; i < ShiftName.Rows.Count; i++)
                    {
                        ShiftName.Rows[i]["Flag"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkModel_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkModel.IsChecked == true)
                {
                    for (int i = 0; i <ModuleList.Rows.Count; i++)
                    {
                        ModuleList.Rows[i]["Flag"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkModel_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkModel.IsChecked == false)
                {
                    for (int i = 0; i < ModuleList.Rows.Count; i++)
                    {
                        ModuleList.Rows[i]["Flag"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkShift_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkShift.IsChecked == false)
                {
                    for (int i = 0; i < ShiftName.Rows.Count; i++)
                    {
                        ShiftName.Rows[i]["Flag"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtpFrom.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT FROM DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpFrom.Focus();
                    return;
                }
                if (dtpTo.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT TO DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpTo.Focus();
                    return;
                }
                if (e.Source.ToString().Contains("Third Shift"))
                {
                    dtpTo.Text = dtpTo.SelectedDate.Value.AddDays(1).ToString("dd MMM yyyy");
                }
                DateSet();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DateSet()
        {
            bool FirstShift = false, SecondSHift = false, ThirdShift = false;

            for (int i = 0; i < ShiftName.Rows.Count; i++)
            {
                if(ShiftName.Rows[i]["ShiftName"].ToString()=="First Shift")
                {
                    if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                    {
                        FirstShift = true;
                        FromTime = " 06:30";
                        ToTime = " 15:00";
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00";
                      
                    }
                }
                if (ShiftName.Rows[i]["ShiftName"].ToString() == "Second Shift")
                {
                    if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                    {

                        if (FirstShift == true)
                        {
                            FromTime = " 06:30";
                            ToTime = " 23:30";
                            dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                            dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30";
                            SecondSHift = false;
                           
                        }
                        else
                        {
                            FromTime = " 15:00";
                            ToTime = " 23:30";
                            dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00";
                            dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30";
                            SecondSHift = true;
                         
                        }
                    }
                }
                if (ShiftName.Rows[i]["ShiftName"].ToString() == "Third Shift")
                {
                    if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                    {
                        ThirdShift = true;

                        if (FirstShift == true)
                        {
                            FromTime = " 06:30";
                            ToTime = " 06:30";
                            dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                            dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                         
                        }
                        else if (SecondSHift == true)
                        {
                            FromTime = " 15:00";
                            ToTime = " 06:30";
                            dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00";
                            dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                            
                        }
                        else
                        {
                            FromTime = " 23:30";
                            ToTime = " 06:30";
                            dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30";
                            dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30";
                            
                        }
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtpFrom.Text != "" && dtpTo.Text != "")
                    DateSet();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OPERATION_RATIO_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
