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
    /// Interaction logic for KPI.xaml
    /// </summary>
    public partial class KPI : Page
    {
        public KPI()
        {
            InitializeComponent();
        }
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Reports.Reports obj_Report = new BUSINESS_LAYER.Reports.Reports();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string FromTime = "", ToTime = "";
        DataTable dt = new DataTable();
        DataSet LineDetails = new DataSet();
        DataTable ShiftName = new DataTable();

        private void ChkLineName_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkLineName.IsChecked == true)
                {
                    for (int i = 0; i < LineDetails.Tables[0].Rows.Count; i++)
                    {
                        LineDetails.Tables[0].Rows[i]["Flag"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ChkLineName_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkLineName.IsChecked == false)
                {
                    for (int i = 0; i < LineDetails.Tables[0].Rows.Count; i++)
                    {
                        LineDetails.Tables[0].Rows[i]["Flag"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

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
               
                Transaction("GetLineNames");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Clear()
        {
            cmbMonth.Text = "";
            cmbReport.Text = "";
            chkLineName.IsChecked = false;
            dtpFrom.Text = "";
            dtpTo.Text = "";
            cmbWeek.Text = "";
            cmbHeader.Text = "";
            //txtlinename.ItemsSource = null;
            cmbReport.Focus();
        }
        private bool ControlValidation()
        {
            if (cmbReport.SelectedIndex== -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT REPORT TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbReport.Focus();
                return false;
            }
            if (cmbReport.SelectedIndex == 0 || cmbReport.SelectedIndex == 1)
            {
                if (cmbHeader.SelectedIndex == -1)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT HEADER", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    cmbHeader.Focus();
                    return false;
                }
            }
            if (cmbMonth.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MONTH", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbMonth.Focus();
                return false;
            }
            if (cmbReport.SelectedIndex == 0 || cmbReport.SelectedIndex == 1)
            {
                if (cmbWeek.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT WEEK/DAY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpFrom.Focus();
                    return false;
                }
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
        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlValidation())
                    Transaction("Report");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }


        private void CmbReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbReport.SelectedIndex > -1)
                {
                    if (cmbReport.SelectedIndex == 0)
                    {
                        CommonClasses.CommonMethods.FillComboBox(cmbHeader, LineDetails.Tables[1], "HeaderType");

                    }
                   else if (cmbReport.SelectedIndex == 1)
                    {
                        CommonClasses.CommonMethods.FillComboBox(cmbHeader, LineDetails.Tables[2], "HeaderType");
                    }
                    else
                        CommonClasses.CommonMethods.UNFill_ComboBox(cmbHeader);
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMonth.SelectedIndex > -1)
                {
                    var startDate = new DateTime(DateTime.Now.Year, cmbMonth.SelectedIndex + 1, 1, 06, 30, 00);
                    var endDate = startDate.AddMonths(1);
                    dtpFrom.Text = startDate.ToString();
                    dtpTo.Text = endDate.ToString();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbWeek.SelectedIndex > -1)
                {
                    var startDate = new DateTime(DateTime.Now.Year, cmbMonth.SelectedIndex + 1, 1, 06, 30, 00);
                    dtpFrom.IsEnabled = false;
                    dtpTo.IsEnabled = false;

                    if (cmbWeek.SelectedIndex == 3)
                    {
                        if (DateTime.DaysInMonth(DateTime.Now.Year, cmbMonth.SelectedIndex + 1) == 30)
                        {
                            dtpFrom.Text = startDate.AddDays(23).ToString();
                            dtpTo.Text = startDate.AddDays(30).ToString();
                        }
                        else if (DateTime.DaysInMonth(DateTime.Now.Year, cmbMonth.SelectedIndex + 1) == 31)
                        {
                            dtpFrom.Text = startDate.AddDays(23).ToString();
                            dtpTo.Text = startDate.AddDays(31).ToString();
                        }
                    }
                    else if (cmbWeek.SelectedIndex == 2)
                    {
                        dtpFrom.Text = startDate.AddDays(15).ToString();
                        dtpTo.Text = startDate.AddDays(23).ToString();
                    }
                    else if (cmbWeek.SelectedIndex == 1)
                    {
                        dtpFrom.Text = startDate.AddDays(7).ToString();
                        dtpTo.Text = startDate.AddDays(15).ToString();
                    }
                    else if (cmbWeek.SelectedIndex == 0)
                    {
                        dtpFrom.Text = startDate.ToString();
                        dtpTo.Text = startDate.AddDays(7).ToString();
                    }
                    else if (cmbWeek.SelectedIndex == 4)
                    {
                        dtpFrom.Text = startDate.ToString();
                        dtpTo.Text = startDate.AddDays(1).ToString();
                        dtpFrom.IsEnabled = true;
                        dtpTo.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "GetLineNames")
            {
                ENTITY_LAYER.Reports.Reports.Type = Type;
                LineDetails = obj_Report.BL_Reports();
                listLineName.ItemsSource = LineDetails.Tables[0].DefaultView;
            }
            if (Type == "Report")
            {
                ENTITY_LAYER.Reports.Reports.Type = "KPI";
                ENTITY_LAYER.Reports.Reports.Month = cmbMonth.Text;
                ENTITY_LAYER.Reports.Reports.ReportType = cmbReport.Text;
                ENTITY_LAYER.Reports.Reports.HeaderType = cmbHeader.Text;
                ENTITY_LAYER.Reports.Reports.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Machinename = "";
                for (int i = 0; i < LineDetails.Tables[0].Rows.Count; i++)
                {
                    if (LineDetails.Tables[0].Rows[i]["Flag"].ToString() == "True")
                    {
                        if (ENTITY_LAYER.Reports.Reports.Machinename == "")
                            ENTITY_LAYER.Reports.Reports.Machinename = "'" + LineDetails.Tables[0].Rows[i][0].ToString() + "'";
                        else
                            ENTITY_LAYER.Reports.Reports.Machinename = ENTITY_LAYER.Reports.Reports.Machinename + ",'" + LineDetails.Tables[0].Rows[i]["MachineName"].ToString() + "'";

                    }
                }

                if (ENTITY_LAYER.Reports.Reports.Machinename == "")
                {
                    chkLineName.IsChecked = true;
                    for (int i = 0; i < LineDetails.Tables[0].Rows.Count; i++)
                    {
                        if (LineDetails.Tables[0].Rows[i]["Flag"].ToString() == "True")
                        {
                            if (ENTITY_LAYER.Reports.Reports.Machinename == "")
                                ENTITY_LAYER.Reports.Reports.Machinename = "'" + LineDetails.Tables[0].Rows[i][0].ToString() + "'";
                            else
                                ENTITY_LAYER.Reports.Reports.Machinename = ENTITY_LAYER.Reports.Reports.Machinename + ",'" + LineDetails.Tables[0].Rows[i]["MachineName"].ToString() + "'";

                        }
                    }
                }

                if (cmbReport.Text == "Man-Hour" || cmbReport.Text == "Ratio")
                {
                    dt = obj_Report.BL_Reports().Tables[0];

                    Report.Reports.ReportViewer.dtReport = dt;
                    Report.Reports.ReportViewer.ReportName = ENTITY_LAYER.Reports.Reports.ReportType;
                    Report.Reports.ReportViewer.HeaderType = ENTITY_LAYER.Reports.Reports.HeaderType.ToUpper();
                    NavigationService.Navigate(new Report.Reports.ReportViewer());
                }
                else if(cmbReport.Text== "MM")
                {
                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    Microsoft.Office.Interop.Excel.Range xlRange;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlApp.Visible = true;

                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\DAILY_DPR.xlsm", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("DPR");

                }
                else if (cmbReport.Text == "PIM")
                {
                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    Microsoft.Office.Interop.Excel.Range xlRange;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlApp.Visible = true;

                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\PIM.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("PIM");

                }
                else if (cmbReport.Text == "MMR")
                {
                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    Microsoft.Office.Interop.Excel.Range xlRange;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlApp.Visible = true;

                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\MMR.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("MMR");

                }
                else if (cmbReport.Text == "OR")
                {
                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    Microsoft.Office.Interop.Excel.Range xlRange;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlApp.Visible = true;

                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\OR.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("OR");

                }
            }
        }
        private void DtpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpTo.Text != "")
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") +  " 06:30:00" ;
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DtpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpFrom.Text != "")
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
