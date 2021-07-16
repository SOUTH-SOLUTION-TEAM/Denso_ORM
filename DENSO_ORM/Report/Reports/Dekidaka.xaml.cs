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
    /// Interaction logic for Dekidaka.xaml
    /// </summary>
    public partial class Dekidaka : Page
    {
        public Dekidaka()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Reports.Reports obj_Report = new BUSINESS_LAYER.Reports.Reports();
        string FromTime = "" ,ToTime="";
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DataTable dt = new DataTable();
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
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
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
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
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
            if (CmbSHift.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                CmbSHift.Focus();
                return false;
            }

            return true;

        }

        private void Transaction(string Type)
        {
            if (Type == "LoadShitName")
            {
                ShiftName = new DataTable();
                ShiftName.Columns.Add("ShiftName");
                ShiftName.Columns.Add("Flag");
                ShiftName.Rows.Add("First Shift", "False");
                ShiftName.Rows.Add("Second Shift", "False");
                ShiftName.Rows.Add("Third Shift", "False");
                CommonClasses.CommonMethods.FillComboBox(CmbSHift, ShiftName, "ShiftName");
               // listShift.ItemsSource = ShiftName.DefaultView;
            }
            if (Type == "Report")
            {
                ENTITY_LAYER.Reports.Reports.Type = "Dekidaka";
                ENTITY_LAYER.Reports.Reports.MachineGroupName = txtlinegrp.Text;
                ENTITY_LAYER.Reports.Reports.Machinename = txtlinename.Text;
                ENTITY_LAYER.Reports.Reports.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Reports.Reports.ShiftName = CmbSHift.Text;
                //ENTITY_LAYER.Reports.Reports.ShiftName = "";
                //for (int i = 0; i < ShiftName.Rows.Count; i++)
                //{
                //    if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                //    {
                //        if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                //            ENTITY_LAYER.Reports.Reports.ShiftName = "'" + ShiftName.Rows[i][0].ToString() + "'";
                //        else
                //            ENTITY_LAYER.Reports.Reports.ShiftName = ENTITY_LAYER.Reports.Reports.ShiftName + ",'" + ShiftName.Rows[i]["ShiftName"].ToString() + "'";
                //    }
                //}
                //if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                //{
                //    chkShift.IsChecked = true;
                //    for (int i = 0; i < ShiftName.Rows.Count; i++)
                //    {
                //        if (ShiftName.Rows[i]["Flag"].ToString() == "True")
                //        {
                //            if (ENTITY_LAYER.Reports.Reports.ShiftName == "")
                //                ENTITY_LAYER.Reports.Reports.ShiftName = "'" + ShiftName.Rows[i][0].ToString() + "'";
                //            else
                //                ENTITY_LAYER.Reports.Reports.ShiftName = ENTITY_LAYER.Reports.Reports.ShiftName + ",'" + ShiftName.Rows[i]["ShiftName"].ToString() + "'";
                //        }
                //    }
                //}

                DataSet ds = obj_Report.BL_Reports();
                int Count = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ds.Tables[0].Rows[i]["PlanCumulative"] = ds.Tables[0].Rows[i]["ProductionQty"].ToString();
                        ds.Tables[0].Rows[i]["PlanCumulative1"] = ds.Tables[0].Rows[i]["ProductionQty1"].ToString();
                        ds.Tables[0].Rows[i]["ActualCumulative"] = ds.Tables[0].Rows[i]["ActualQty"].ToString();
                    }
                    else if (Count == 8)
                    {
                        ds.Tables[0].Rows[i]["PlanCumulative"] = ds.Tables[0].Rows[i]["ProductionQty"].ToString();
                        ds.Tables[0].Rows[i]["PlanCumulative1"] = ds.Tables[0].Rows[i]["ProductionQty1"].ToString();
                        ds.Tables[0].Rows[i]["ActualCumulative"] = ds.Tables[0].Rows[i]["ActualQty"].ToString();
                        Count = 0;
                    }
                    else if (Count == 7 && CmbSHift.Text=="Third Shift")
                    {
                        ds.Tables[0].Rows[i]["PlanCumulative"] = ds.Tables[0].Rows[i]["ProductionQty"].ToString();
                        ds.Tables[0].Rows[i]["PlanCumulative1"] = ds.Tables[0].Rows[i]["ProductionQty1"].ToString();
                        ds.Tables[0].Rows[i]["ActualCumulative"] = ds.Tables[0].Rows[i]["ActualQty"].ToString();
                        Count = 0;
                    }
                    else
                    {
                        ds.Tables[0].Rows[i]["PlanCumulative1"] = Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PlanCumulative1"]) + Convert.ToInt32(ds.Tables[0].Rows[i]["ProductionQty1"]); 
                        ds.Tables[0].Rows[i]["PlanCumulative"] = Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PlanCumulative"]) + Convert.ToInt32(ds.Tables[0].Rows[i]["ProductionQty"]); 
                        ds.Tables[0].Rows[i]["ActualCumulative"] = Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ActualCumulative"]) + Convert.ToInt32(ds.Tables[0].Rows[i]["ActualQty"]); 
                    }
                    Count++;

                }

                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                Microsoft.Office.Interop.Excel.Range xlRange;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.Visible = true;

                xlApp.DisplayAlerts = false;
                xlWorkBook = xlApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\DEKIDAKA.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item("DEKIDAKA");
             //   xlRange = xlWorkSheet.Range[1, 1];

                //You can loop through all cells and use i and j to get the cells
                xlWorkSheet.Cells[11, 4].Value2 = System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;
                xlWorkSheet.Cells[2, 2].Value2 = CommonClasses.CommonVariable.MachineGroup;
                xlWorkSheet.Cells[11, 7].Value2 = CommonClasses.CommonVariable.MachineName ;
                xlWorkSheet.Cells[11, 16].Value2 = CmbSHift.Text;
               // xlWorkSheet.Cells[13, 5].Value2 = CommonClasses.CommonVariable.MachineName;
               // xlWorkSheet.Cells[14, 13].Value2 = System.DateTime.Now.ToString("dd MMM yyyy");
                xlWorkSheet.Cells[12, 4].Value2 =  (System.DateTime.Today.Day / 7) + 1;
                if (ds.Tables[1].Rows.Count > 8)
                {
                    xlWorkSheet.Cells[19, 2].Value2 = ds.Tables[1].Rows[0]["Time"].ToString();
                    xlWorkSheet.Cells[21, 2].Value2 = ds.Tables[1].Rows[1]["Time"].ToString();
                    xlWorkSheet.Cells[23, 2].Value2 = ds.Tables[1].Rows[2]["Time"].ToString();
                    xlWorkSheet.Cells[25, 2].Value2 = ds.Tables[1].Rows[3]["Time"].ToString();
                    xlWorkSheet.Cells[27, 2].Value2 = ds.Tables[1].Rows[4]["Time"].ToString();
                    xlWorkSheet.Cells[29, 2].Value2 = ds.Tables[1].Rows[5]["Time"].ToString();
                    xlWorkSheet.Cells[31, 2].Value2 = ds.Tables[1].Rows[6]["Time"].ToString();
                    xlWorkSheet.Cells[33, 2].Value2 = ds.Tables[1].Rows[7]["Time"].ToString();

                    DataView dv = new DataView(ds.Tables[1]);
                    DataTable dt = dv.ToTable(true, "ModelName");
                    int Cel = 3, row = 18, Modelcel = 45;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //DataView dv1 = new DataView(ds.Tables[1]);
                        //dv1.RowFilter = "ModelName='" + dt.Rows[i]["ModelName"].ToString() + "'";
                        ////    if (dv1.ToTable().Rows.Count > 0)
                        ////    {
                        ////        row = 18;
                        ////        for (int j = 0; j < dv1.ToTable().Rows.Count; j++)
                        ////        {
                        ////            if (Cel <= 18)
                        ////            {
                        ////                xlWorkSheet.Cells[15, Cel + 2].Value2 = dv1.ToTable().Rows[0]["ModelName"].ToString();
                        ////                xlWorkSheet.Cells[17, Cel + 2].Value2 = dv1.ToTable().Rows[0]["CycleTime"].ToString() + " SECS";
                        ////                xlWorkSheet.Cells[18, Cel + 2].Value2 = dv1.ToTable().Rows[0]["Cycletime1"].ToString() + " SECS";
                        ////                row++;
                        ////                xlWorkSheet.Cells[row, Cel + 2].Value2 = dv1.ToTable().Rows[j]["PlanCount"].ToString();
                        ////                row++;
                        ////                xlWorkSheet.Cells[row, Cel + 3].Value2 = dv1.ToTable().Rows[j]["PlanCumulative"].ToString();


                        ////            }
                        ////        }
                              Modelcel++;
                              xlWorkSheet.Cells[Modelcel, 3].Value2 = dt.Rows[i]["ModelName"].ToString();
                        //        Cel = Cel + 2;
                        //    }
                    }
                    DataView dv2 = new DataView(ds.Tables[0]);
                    DataTable dt1 = dv2.ToTable(true, "createdon");
                    int Cel1 = 4, row1 = 18, Modelcel1 = 45,row3=17;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataView dv1 = new DataView(ds.Tables[0]);
                        dv1.RowFilter = "createdon='" + dt1.Rows[i]["createdon"].ToString() + "'";
                        if (i <= 6)
                        {
                            if (dv1.ToTable().Rows.Count > 0)
                            {
                                row1 = 18;
                                row3 = 17;
                                xlWorkSheet.Cells[14, Cel1 + 1].Value2 = dv1.ToTable().Rows[0]["createdon"].ToString();
                                for (int j = 0; j < dv1.ToTable().Rows.Count; j++)
                                {
                                    //if (Cel1 <= 5)
                                    //{
                                        row1 = row1 + 2;
                                        row3 = row3 + 2;
                                        xlWorkSheet.Cells[row1, Cel1 + 1].Value2 = dv1.ToTable().Rows[j]["ActualQty"].ToString();
                                        if (Convert.ToDecimal(dv1.ToTable().Rows[j]["HourPers"]) < 90)
                                        {
                                            xlWorkSheet.Cells[row1, Cel1 + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                            //xlWorkSheet.Cells[row3, Cel1 + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (Convert.ToDecimal(dv1.ToTable().Rows[j]["HourPers"]) > 90)
                                        {
                                            xlWorkSheet.Cells[row1, Cel1 + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                            //xlWorkSheet.Cells[row3, Cel1 + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                        }
                                        //row1++;
                                        xlWorkSheet.Cells[row1, Cel1 + 2].Value2 = dv1.ToTable().Rows[j]["ActualCumulative"].ToString();
                                        if (Convert.ToDecimal(dv1.ToTable().Rows[j]["HourPers"]) < 90)
                                        {
                                            xlWorkSheet.Cells[row1, Cel1 + 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                           //xlWorkSheet.Cells[row3, Cel1 + 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (Convert.ToDecimal(dv1.ToTable().Rows[j]["HourPers"]) > 90)
                                        {
                                            xlWorkSheet.Cells[row1, Cel1 + 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                           // xlWorkSheet.Cells[row3, Cel1 + 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                        }
                                    //}
                                }
                                Cel1 = Cel1 + 2;
                            }
                        }
                    }
                    DataView dv3 = new DataView(ds.Tables[2]);
                    DataTable dt2 = dv3.ToTable(true, "CreateDate");
                    int Cel2 = 4, row2 = 17;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        DataView dv1 = new DataView(ds.Tables[2]);
                        dv1.RowFilter = "CreateDate='" + dt2.Rows[i]["CreateDate"].ToString() + "'";
                        if (i <= 6)
                        {
                            if (dv1.ToTable().Rows.Count > 0)
                            {
                                row2 = 17;
                                for (int j = 0; j < dv1.ToTable().Rows.Count; j++)
                                {
                                    //if (Cel2 <= 5)
                                    //{
                                        row2 = row2 + 2;
                                        xlWorkSheet.Cells[row2, Cel2 + 1].Value2 = dv1.ToTable().Rows[j]["Alphacode"].ToString();
                                        //row1++;
                                    // }
                                }
                                Cel2 = Cel2 + 2;
                            }
                        }
                    }
                    //DataView dv = new DataView(ds.Tables[2]);
                    //DataTable dt = dv.ToTable(true, "ModuleName","Alphacode");
                    //int Cel = 3, row = 18, Modelcel = 45;
                    //string[] str = new string[] { "A", "B", "C", "D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W" };
                    
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (dt.Rows[i]["ModuleName"].ToString() != "0" && dt.Rows[i]["ModuleName"].ToString() != "")
                    //    {
                    //        for (int j = 0; j < str.Length; j++)
                    //        {
                    //            Modelcel++;
                    //            if (dt.Rows[i]["Alphacode"].ToString() == str[j])
                    //            {

                    //                xlWorkSheet.Cells[Modelcel, 3].Value2 = dt.Rows[i]["ModuleName"].ToString();
                    //            }
                    //        }
                    //    }
                    //}
                      
                    DataView dv4 = new DataView(ds.Tables[3]);
                    DataTable dt3 = dv4.ToTable(true, "CreatedDate");
                    int Cel4 = 4, row4 = 42;
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DataView dv1 = new DataView(ds.Tables[3]);
                        dv1.RowFilter = "CreatedDate='" + dt3.Rows[i]["CreatedDate"].ToString() + "'";
                        if (i <= 6)
                        {
                            if (dv1.ToTable().Rows.Count > 0)
                            {
                                row4 = 42;
                                for(int j = 0; j < dv1.ToTable().Rows.Count; j++)
                                {
                                    //if (Cel4 <= 5)
                                    //{
                                        row4 = row4 + 3;
                                        xlWorkSheet.Cells[row4, Cel4 + 1].Value2 = dv1.ToTable().Rows[j]["Problem"].ToString();
                                    //}
                                }
                                Cel4 = Cel4 + 2;
                            }
                        }
                    }
                }

                //Report.Reports.ReportViewer.dtReport = dt;
                //Report.Reports.ReportViewer.ReportName = ENTITY_LAYER.Reports.Reports.Type;
                //NavigationService.Navigate(new Report.Reports.ReportViewer());
            }

        }

        private void Clear()
        {
            dtpFrom.Text = "";
            dtpTo.Text = "";
            CmbSHift.Text = "";
           // chkShift.IsChecked = false;
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        //private void ChkShift_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (chkShift.IsChecked == true)
        //        {
        //            for (int i = 0; i < ShiftName.Rows.Count; i++)
        //            {
        //                ShiftName.Rows[i]["Flag"] = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
        //private void ChkShift_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (chkShift.IsChecked == false)
        //        {
        //            for (int i = 0; i < ShiftName.Rows.Count; i++)
        //            {
        //                ShiftName.Rows[i]["Flag"] = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
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

        private void CmbSHift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CmbSHift.SelectedIndex > -1)
                {

                    if (CmbSHift.SelectedIndex == 0)//first shift
                    {
                        FromTime = " 06:30:00";
                        ToTime = " 15:00:00";
                        dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                        dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                    }
                    if (CmbSHift.SelectedIndex == 1)//Second shift
                    {
                        FromTime = " 15:00:00";
                        ToTime = " 23:30:00";
                        dtpFrom.Text = System.DateTime.Now.ToString("dd MMM yyyy") + FromTime;
                        dtpTo.Text = System.DateTime.Now.ToString("dd MMM yyyy") + ToTime;
                    }
                    if (CmbSHift.SelectedIndex == 2)//Third shift
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DtpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpTo.Text!="")
                dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + ToTime;
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA_REPORT", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
