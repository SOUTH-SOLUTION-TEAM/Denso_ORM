using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    /// Interaction logic for OR_Monitoring_System.xaml
    /// </summary>
    public partial class OR_Monitoring_System : Page
    {
        public OR_Monitoring_System()
        {
            InitializeComponent();
            //OR_Monitoring_System obj_or = new OR_Monitoring_System();
            if (CommonClasses.CommonVariable.TransactioType != "DashBoard")
            {
                this.Style = (Style)FindResource("PageStyle");
            }
            else
                imgSmily3.Visibility = Visibility.Hidden;

        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
       // BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string ShiftName ="",Time="";
        decimal HourQty = 0;
        decimal TotalQty = 0;
        string IPAddress;  double CycleTime; int PblCOunt = 0;
        Probem_Code obj_prd = new Probem_Code();
        #endregion

        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void Transaction(string Type)
        {
            if (Type == "GetShiftDetails")
            {

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup =CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
              
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[0]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[0]["Time"].ToString().ToString();
                    CommonClasses.CommonVariable.Break = dt.Rows[0]["Break"].ToString().ToString();
                    txtAShift.Text = "Cumulative for Shift " + dt.Rows[0]["TimeWorking"].ToString().Split(',')[0].ToString() + " - " + dt.Rows[0]["CurrentTime"].ToString();
                    txtATimeInterval.Text = "Current Hour " + CommonClasses.CommonVariable.Time + " - " + dt.Rows[0]["CurrentTime"].ToString();
                    if (CommonClasses.CommonVariable.Break == "Break")
                        txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF0E67E8");
                    else
                        txtTime.Background = (Brush)new BrushConverter().ConvertFrom("#FF20F30B");
                }
            }
            if (Type == "OrMonitoring")
            {

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup =CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                ENTITY_LAYER.Transaction.Transaction.NoofItem = CommonClasses.CommonVariable.NoofItems;
                ENTITY_LAYER.Transaction.Transaction.Puls = CommonClasses.CommonVariable.Puls;
                ENTITY_LAYER.Transaction.Transaction.Cycletime = CommonClasses.CommonVariable.CycleTime;
                DataSet dt = obj_Tran.BL_DashBoard();

                if (dt.Tables[1].Rows.Count > 0)
                {
                    txtActToatl.Text = "A " + dt.Tables[1].Rows[0]["TotalQty"].ToString();
                    txtTotaPrd.Text = dt.Tables[1].Rows[0]["TotalPers"].ToString() + "%";
                    txtPrdToatl.Text = "P " + (dt.Tables[1].Rows[0]["TotalPlan"].ToString());
                    if (txtPrdToatl.Text == "P 0")
                        txtPPlan.Text = "P 0";
                    if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 50)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        txtTotaPrd.Foreground = Brushes.Red;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 83)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/HallfHappy.jpg"));
                        txtTotaPrd.Foreground = Brushes.DarkOrange;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.jpg"));
                        txtTotaPrd.Foreground = Brushes.Green;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/FullHappy.jpg"));
                        txtTotaPrd.Foreground = Brushes.Green;
                        imgSmily1.Source = ImgTest;
                    }
                }
                else
                {
                    txtActToatl.Text = "A 0";
                    txtTotaPrd.Text = "0.0%";
                    if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) == 0.0)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        txtTotaPrd.Foreground = Brushes.Red;
                        imgSmily1.Source = ImgTest;
                    }
                }
                if (dt.Tables[0].Rows.Count > 0)
                {
                    txtAPlan.Text = "A " + (dt.Tables[0].Rows[0]["HourPlan"].ToString());
                    txtPPlan.Text = "P " + (dt.Tables[0].Rows[0]["ProductionPlan"].ToString());
                    txtPrdToatl.Text = "P " + (dt.Tables[0].Rows[0]["TotalPlan"].ToString());
                    txtPrdQty.Text = (dt.Tables[0].Rows[0]["HourPers"].ToString() + "%");
                    txtActToatl.Text = "A " + dt.Tables[0].Rows[0]["TotalQty"].ToString();
                    txtTotaPrd.Text = dt.Tables[0].Rows[0]["TotalPers"].ToString() + "%";
                    HourQty = Convert.ToDecimal(dt.Tables[0].Rows[0]["HourPlan"].ToString());
                    slColorG.Value = Convert.ToDouble(txtPrdQty.Text.Replace("%", ""));

                    if (slColorG.Value <= 50)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        txtPrdQty.Foreground = Brushes.Red;
                        imgSmily2.Source = ImgTest;
                    }
                    else if (slColorG.Value > 50 && slColorG.Value <= 83)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/HallfHappy.jpg"));
                        txtPrdQty.Foreground = Brushes.DarkOrange;
                        imgSmily2.Source = ImgTest;
                    }
                    else if (slColorG.Value > 83 && slColorG.Value <= 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.jpg"));
                        txtPrdQty.Foreground = Brushes.Green;
                        imgSmily2.Source = ImgTest;
                    }
                    else if (slColorG.Value > 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/FullHappy.jpg"));
                        txtPrdQty.Foreground = Brushes.Green;
                        imgSmily2.Source = ImgTest;
                    }

                    if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 50)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        txtTotaPrd.Foreground = Brushes.Red;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 50 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 83)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/HallfHappy.jpg"));
                        txtTotaPrd.Foreground = Brushes.DarkOrange;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 83 && Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) <= 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.jpg"));
                        txtTotaPrd.Foreground = Brushes.Green;
                        imgSmily1.Source = ImgTest;
                    }
                    else if (Convert.ToDouble(txtTotaPrd.Text.Split('%')[0]) > 100)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/FullHappy.jpg"));
                        txtTotaPrd.Foreground = Brushes.Green;
                        imgSmily1.Source = ImgTest;
                    }
                    else
                    {
                        txtPrdQty.Text = "0.0%";
                        txtPPlan.Text = "P 0";
                        slColorG.Value = Convert.ToDouble(0);
                        if (slColorG.Value == 0)
                        {
                            BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                            txtPrdQty.Foreground = Brushes.Red;
                            imgSmily2.Source = ImgTest;
                        }

                    }
                }
                else
                {
                    txtAPlan.Text = "A 0";
                    txtPrdQty.Text = "0.0%";
                    slColorG.Value = Convert.ToDouble(0);
                    if (slColorG.Value == 0)
                    {
                        BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        txtPrdQty.Foreground = Brushes.Red;
                        imgSmily2.Source = ImgTest;
                    }

                }

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                txtTime.Text = System.DateTime.Now.ToString("HH:mm");
                if (CommonClasses.CommonVariable.ModelName != "")
                {
                    txtModelName.Text = CommonClasses.CommonVariable.ModelName + " - " + CommonClasses.CommonVariable.CycleTime + " S";
                    CycleTime = Convert.ToDouble(CommonClasses.CommonVariable.CycleTime);

                    Transaction("GetShiftDetails");
                    Transaction("OrMonitoring");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
               // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
     
    }
}