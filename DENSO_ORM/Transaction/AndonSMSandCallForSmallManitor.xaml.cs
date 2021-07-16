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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for AndonSMSandCallForSmallManitor.xaml
    /// </summary>
    public partial class AndonSMSandCallForSmallManitor : Page
    {
        public AndonSMSandCallForSmallManitor()
        {
            InitializeComponent();
            if (CommonClasses.CommonVariable.TransactioType != "DashBoard")
            {
                this.Style = (Style)FindResource("PageStyle");
            }
            else
                imgSmily3.Visibility = Visibility.Hidden;
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        // Dash_Board.Business_Layer.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        int RefNo = 0;
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

                Transaction("AndonCall");
                Transaction("GetShiftDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS_VIEW", CommonClasses.CommonVariable.UserID);
               // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
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
                    //    if (TimeSpan.Parse(dt.Rows[i]["ShiftTiming"].ToString().Split(' ')[0].ToString()) <= TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm"))
                    //        && TimeSpan.Parse(dt.Rows[i]["ShiftTiming"].ToString().ToString().Split(' ')[1].ToString()) > TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm")))
                    //    {
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[i]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[i]["Time"].ToString();
                      //  Flag = true;
                //    }
                }

                //if (Flag == false)
                //{
                //    if (dt.Rows.Count > 0)
                //    {

                //        CommonClasses.CommonVariable.ShiftName = dt.Rows[2]["ShiftName"].ToString();
                //    }
                //}
            }
            if (Type == "AndonCall")
            {

               ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                brd1.Background = Brushes.White;
                brd2.Background = Brushes.White;
                brd3.Background = Brushes.White;
                brd4.Background = Brushes.White;
                brd5.Background = Brushes.White;
                brd6.Background = Brushes.White;
                brd7.Background = Brushes.White;
                brd8.Background = Brushes.White;
                brd9.Background = Brushes.White;
                Station1.Text = "";
                Station2.Text = "";
                Station3.Text = "";
                Line1.Text = "";
                Line2.Text = "";
                Quality.Text = "";
                DataTable dt2 = new DataTable();
                DataView dv = new DataView(dt);
                DataTable dt1 = dv.ToTable(true, "Station");
                DataView dv1 = new DataView(dt);
                if (dt1.Rows.Count >= 1)
                {
                    dv1.RowFilter = "station='" + dt1.Rows[0][0] + "'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd7.Background == Brushes.White)
                                brd7.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd1.Background == Brushes.White)
                                brd1.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station1.Text = dt2.Rows[i]["Station"].ToString();
                            Line1.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd4.Background == Brushes.White)
                                brd4.Background = Brushes.Yellow;

                        }
                    }
                }
                if (dt1.Rows.Count >= 2)
                {
                    dv1.RowFilter = "station='" + dt1.Rows[1][0] + "'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd8.Background == Brushes.White)
                                brd8.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd2.Background == Brushes.White)
                                brd2.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station2.Text = dt2.Rows[i]["Station"].ToString();
                            Line2.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd5.Background == Brushes.White)
                                brd5.Background = Brushes.Yellow;

                        }
                    }
                }
                if (dt1.Rows.Count >= 3)
                {
                    dv1.RowFilter = "station='" + dt1.Rows[2][0] + "'";
                    dt2 = dv1.ToTable();

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i]["andonType"].ToString() == "QUALITY")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;

                            if (brd9.Background == Brushes.White)
                                brd9.Background = Brushes.DeepPink;
                        }

                        if (dt2.Rows[i]["andonType"].ToString() == "MAINTENANCE")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd3.Background == Brushes.White)
                                brd3.Background = Brushes.Red;
                        }
                        if (dt2.Rows[i]["andonType"].ToString() == "PART FEADER")
                        {
                            Station3.Text = dt2.Rows[i]["Station"].ToString();
                            Quality.Text = CommonClasses.CommonVariable.MachineName;
                            if (brd6.Background == Brushes.White)
                                brd6.Background = Brushes.Yellow;

                        }
                    }
                }
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS_VIEW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Screen[] se = Screen.AllScreens;
           
                //if (se.Length > 1)
                //{
                //    this.Width = se[1].WorkingArea.Width + 50;
                //    this.Height = se[1].WorkingArea.Height + 50;
                //}

                //var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                //this.Left = desktopWorkingArea.Right - this.Width + Convert.ToInt32(1360);

                //this.Top = desktopWorkingArea.Top;
                ShowDateTime();

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS_VIEW", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "ANDON_CALL_SMS_VIEW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}