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

namespace DENSO_ORM.Report.Reports
{
    /// <summary>
    /// Interaction logic for CycleTimeFluctuationforGraph.xaml
    /// </summary>
    public partial class CycleTimeFluctuationforGraph : Page
    {
        public CycleTimeFluctuationforGraph()
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
        DataTable DtTime = new DataTable();
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
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                Transaction("LoadShitName");
                Transaction("ModuleName");
                dt = new DataTable();
                dt.Columns.Add("ModelNo");
                dt.Columns.Add("CycleTime");
                dt.Columns["CycleTime"].DataType = typeof(Decimal);

                dt.Columns.Add("TotalTime");
                dt.Columns["TotalTime"].DataType = typeof(Decimal);

                dt.Columns.Add("SLNO");
                dt.Columns.Add("ShiftName");
                dt.Columns.Add("LineGroup");
                dt.Columns.Add("LineName");
                dt.Columns.Add("Date");

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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
               DataTable ModuleList = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbModelName, ModuleList, "ModelName");

                //  listModel.ItemsSource = ModuleList.DefaultView;

            }
            if (Type == "LoadShitName")
            {
                DataTable ShiftName = new DataTable();
                ShiftName.Columns.Add("ShiftName");
                ShiftName.Columns.Add("Flag");
                ShiftName.Rows.Add("First Shift", "False");
                ShiftName.Rows.Add("Second Shift", "False");
                ShiftName.Rows.Add("Third Shift", "False");
                CommonClasses.CommonMethods.FillComboBox(cmbSHiftName, ShiftName, "ShiftName");
               // listShift.ItemsSource = ShiftName.DefaultView;
            }
            if (Type == "Report")
            {
                ENTITY_LAYER.Reports.Reports.Type = "CycleTimeFlactuationForGraph";
                ENTITY_LAYER.Reports.Reports.MachineGroupName = txtlinegrp.Text;
                ENTITY_LAYER.Reports.Reports.Machinename = txtlinename.Text;
                ENTITY_LAYER.Reports.Reports.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Time = "";
                ENTITY_LAYER.Reports.Reports.ShiftName = cmbSHiftName.Text;
                ENTITY_LAYER.Reports.Reports.ModelNo = cmbModelName.Text;
                for (int i = 0; i < DtTime.Rows.Count; i++)
                {
                    if (DtTime.Rows[i]["Flag"].ToString() == "True")
                    {
                        if (ENTITY_LAYER.Reports.Reports.Time == "")
                            ENTITY_LAYER.Reports.Reports.Time = "'" + DtTime.Rows[i][0].ToString() + "'";
                        else
                            ENTITY_LAYER.Reports.Reports.Time = ENTITY_LAYER.Reports.Reports.Time + ",'" + DtTime.Rows[i][0].ToString() + "'";
                    }
                }
                if (ENTITY_LAYER.Reports.Reports.Time == "")
                {
                    chkTime.IsChecked = true;
                    for (int i = 0; i < DtTime.Rows.Count; i++)
                    {
                        if (DtTime.Rows[i]["Flag"].ToString() == "True")
                        {
                            if (ENTITY_LAYER.Reports.Reports.Time == "")
                                ENTITY_LAYER.Reports.Reports.Time = "'" + DtTime.Rows[i][0].ToString() + "'";
                            else
                                ENTITY_LAYER.Reports.Reports.Time = ENTITY_LAYER.Reports.Reports.Time + ",'" + DtTime.Rows[i][0].ToString() + "'";
                        }
                    }
                }
                
                DataTable dt1 = obj_Report.BL_Reports().Tables[0];
                dt.Rows.Clear();
                for(int i=0;i<dt1.Rows.Count;i++)
                {
                    dt.Rows.Add(dt1.Rows[i]["ModelNo"].ToString(), dt1.Rows[i]["CycleTime"].ToString(), dt1.Rows[i]["TotalTime"].ToString(), dt1.Rows[i]["SLNO"].ToString(), dt1.Rows[i]["ShiftName"].ToString(), dt1.Rows[i]["LineGroup"].ToString(), dt1.Rows[i]["LineName"].ToString(), dt1.Rows[i]["Date"].ToString());
                }


                Report.Reports.ReportViewer.dtReport = dt;
                Report.Reports.ReportViewer.ReportName = ENTITY_LAYER.Reports.Reports.Type;
                NavigationService.Navigate(new Report.Reports.ReportViewer());
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Mast.BL_ShiftMasterDetails().Tables[0];
                DataView dv = new DataView(dt);
                dv.RowFilter = "ShiftName='" + cmbSHiftName.Text + "'";
                DtTime = new DataTable();
                DtTime.Columns.Add("TimeWorking");
                DtTime.Columns.Add("Flag");
                string[] Time = dv.ToTable(true, "TimeWorking").Rows[0][0].ToString().Split(',');
                for (int i = 0; i < Time.Length; i++)
                {
                    DtTime.Rows.Add(Time[i], "false");
                }
                listTime.ItemsSource = DtTime.DefaultView;
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
            if (cmbSHiftName.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbSHiftName.Focus();
                return false;
            }
            if (cmbModelName.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MODEL NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbModelName.Focus();
                return false;
            }
            

            return true;

        }
      
        private void Clear()
        {
            dtpFrom.Text = "";
            dtpTo.Text = "";
            cmbModelName.Text = "";
            cmbSHiftName.Text = "";
            chkTime.IsChecked = false;
            DtTime.Rows.Clear();
            //chkShift.IsChecked = false;

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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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

        private void ChkTime_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkTime.IsChecked == false)
                {
                    for (int i = 0; i < DtTime.Rows.Count; i++)
                    {
                        DtTime.Rows[i]["Flag"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkTime_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkTime.IsChecked == true)
                {
                    for (int i = 0; i < DtTime.Rows.Count; i++)
                    {
                        DtTime.Rows[i]["Flag"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbSHiftName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbSHiftName.SelectedIndex > -1)
                {
                    DataRowView dv = (DataRowView)cmbSHiftName.SelectedValue;
                    cmbSHiftName.Text = dv[0].ToString();
                    Transaction("LoadDetails");
                        if (cmbSHiftName.SelectedIndex == 0)//first shift
                        {
                            FromTime = " 06:30:00";
                            ToTime = " 15:00:00";
                            dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                            dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                        }
                        if (cmbSHiftName.SelectedIndex == 1)//Second shift
                        {
                            FromTime = " 15:00:00";
                            ToTime = " 23:30:00";
                            dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                            dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                        }
                        if (cmbSHiftName.SelectedIndex == 2)//Third shift
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLETIMEFLACTUATIONGRAPH_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
