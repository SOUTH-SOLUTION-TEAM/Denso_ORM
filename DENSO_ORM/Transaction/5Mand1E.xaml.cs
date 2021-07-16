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
    /// Interaction logic for _5Mand1E.xaml
    /// </summary>
    public partial class _5Mand1E : Page
    {
        public _5Mand1E()
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
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        #endregion

        private void ShowDateTime()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Transaction("5Enad1MChanges");

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_AND_1E_CHANGES", CommonClasses.CommonVariable.UserID);
             //   CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_AND_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_AND_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_AND_1E_CHANGES", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {

            if (Type == "5Enad1MChanges")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];

                brdMan.BorderBrush = Brushes.Black;
                brdMan.Visibility = Visibility.Hidden;
                txtManchange.Foreground = Brushes.Blue;
                txtManchange1.Foreground = Brushes.Green;
                txtManProcess.Foreground = Brushes.Black;
                txtManShift.Foreground = Brushes.Black;
                txtManStation.Foreground = Brushes.Black;
                txtManchange1.Text = "No Change";
                BitmapImage ImgTest = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgManSmily1.Source = ImgTest;
                txtManProcess.Text = "Process:-";
                txtManShift.Text = "Date and Shift:-";
                txtManStation.Text = "Change Station No:-";



                brdMachchange.BorderBrush = Brushes.Black;
                brdMachchange.Visibility = Visibility.Hidden;
                txtMachchange.Foreground = Brushes.Blue;
                txtMachchange1.Foreground = Brushes.Green;
                txtMachProcess.Foreground = Brushes.Black;
                txtMachShift.Foreground = Brushes.Black;
                txtMachStation.Foreground = Brushes.Black;
                txtMachchange1.Text = "No Change";
                BitmapImage ImgTest1 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgMachSmily1.Source = ImgTest1;
                txtMachProcess.Text = "Process:-";
                txtMachShift.Text = "Date and Shift:-";
                txtMachStation.Text = "Change Station No:-";

                brdMatchange.BorderBrush = Brushes.Black;
                brdMatchange.Visibility = Visibility.Hidden;
                txtMatchange.Foreground = Brushes.Blue;
                txtMatchange1.Foreground = Brushes.Green;
                txtMatProcess.Foreground = Brushes.Black;
                txtMatShift.Foreground = Brushes.Black;
                txtMatStation.Foreground = Brushes.Black;
                txtMatchange1.Text = "No Change";
                BitmapImage ImgTest2 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgMatSmily1.Source = ImgTest2;
                txtMatProcess.Text = "Process:-";
                txtMatShift.Text = "Date and Shift:-";
                txtMatStation.Text = "Change Station No:-";

                brdMeaschange.BorderBrush = Brushes.Black;
                brdMeaschange.Visibility = Visibility.Hidden;
                txtMeaschange.Foreground = Brushes.Blue;
                txtMeaschange1.Foreground = Brushes.Green;
                txtMeasProcess.Foreground = Brushes.Black;
                txtMeasShift.Foreground = Brushes.Black;
                txtMeasStation.Foreground = Brushes.Black;
                txtMeaschange1.Text = "No Change";
                BitmapImage ImgTest3 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgMeasSmily1.Source = ImgTest3;
                txtMeasProcess.Text = "Process:-";
                txtMeasShift.Text = "Date and Shift:-";
                txtMeasStation.Text = "Change Station No:-";

                brdMethchange.BorderBrush = Brushes.Black;
                brdMethchange.Visibility = Visibility.Hidden;
                txtMethchange.Foreground = Brushes.Blue;
                txtMethchange1.Foreground = Brushes.Green;
                txtMethProcess.Foreground = Brushes.Black;
                txtMethShift.Foreground = Brushes.Black;
                txtMethStation.Foreground = Brushes.Black;
                txtMethchange1.Text = "No Change";
                BitmapImage ImgTest4 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgMethSmily1.Source = ImgTest4;
                txtMethProcess.Text = "Process:-";
                txtMethShift.Text = "Date and Shift:-";
                txtMethStation.Text = "Change Station No:-";

                brdEnvchange.BorderBrush = Brushes.Black;
                brdEnvchange.Visibility = Visibility.Hidden;
                txtEnvchange.Foreground = Brushes.Blue;
                txtEnvchange1.Foreground = Brushes.Green;
                txtEnvProcess.Foreground = Brushes.Black;
                txtEnvShift.Foreground = Brushes.Black;
                txtEnvStation.Foreground = Brushes.Black;
                txtEnvchange1.Text = "No Change";
                BitmapImage ImgTest5 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/Happy.png"));
                imgEnvSmily1.Source = ImgTest5;
                txtEnvProcess.Text = "Process:-";
                txtEnvShift.Text = "Date and Shift:-";
                txtEnvStation.Text = "Change Station No:-";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ChangeType"].ToString() == "Man")
                    {
                        brdMan.BorderBrush = Brushes.Red;
                        brdMan.Visibility = Visibility.Visible;
                        txtManchange.Foreground = Brushes.Red;
                        txtManchange1.Foreground = Brushes.Red;
                        txtManProcess.Foreground = Brushes.Red;
                        txtManShift.Foreground = Brushes.Red;
                        txtManStation.Foreground = Brushes.Red;
                        txtManchange1.Text = "Change";
                        txtManProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtManShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtManStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest7 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgManSmily1.Source = ImgTest7;
                    }
                    if (dt.Rows[i]["ChangeType"].ToString() == "Machine")
                    {
                        brdMachchange.BorderBrush = Brushes.Red;
                        brdMachchange.Visibility = Visibility.Visible;
                        txtMachchange1.Foreground = Brushes.Red;
                        txtMachchange.Foreground = Brushes.Red;
                        txtMachProcess.Foreground = Brushes.Red;
                        txtMachShift.Foreground = Brushes.Red;
                        txtMachStation.Foreground = Brushes.Red;
                        txtMachchange1.Text = "Change";
                        txtMachProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtMachShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtMachStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest8 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgMachSmily1.Source = ImgTest8;
                    }
                    if (dt.Rows[i]["ChangeType"].ToString() == "Method")
                    {
                        brdMethchange.BorderBrush = Brushes.Red;
                        brdMethchange.Visibility = Visibility.Visible;
                        txtMethchange.Foreground = Brushes.Red;
                        txtMethchange1.Foreground = Brushes.Red;
                        txtMethProcess.Foreground = Brushes.Red;
                        txtMethShift.Foreground = Brushes.Red;
                        txtMethStation.Foreground = Brushes.Red;
                        txtMethchange1.Text = "Change";
                        txtMethProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtMethShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtMethStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest9 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgMethSmily1.Source = ImgTest9;

                    }
                    if (dt.Rows[i]["ChangeType"].ToString() == "Material")
                    {
                        brdMatchange.BorderBrush = Brushes.Red;
                        brdMatchange.Visibility = Visibility.Visible;
                        txtMatchange.Foreground = Brushes.Red;
                        txtMatchange1.Foreground = Brushes.Red;
                        txtMatProcess.Foreground = Brushes.Red;
                        txtMatShift.Foreground = Brushes.Red;
                        txtMatStation.Foreground = Brushes.Red;
                        txtMatchange1.Text = "Change";
                        txtMatProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtMatShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtMatStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest10 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgMatSmily1.Source = ImgTest10;
                    }
                    if (dt.Rows[i]["ChangeType"].ToString() == "Measurement")
                    {
                        brdMeaschange.BorderBrush = Brushes.Red;
                        brdMeaschange.Visibility = Visibility.Visible;
                        txtMeaschange.Foreground = Brushes.Red;
                        txtMeaschange1.Foreground = Brushes.Red;
                        txtMeasProcess.Foreground = Brushes.Red;
                        txtMeasShift.Foreground = Brushes.Red;
                        txtMeasStation.Foreground = Brushes.Red;
                        txtMeaschange1.Text = "Change";
                        txtMeasProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtMeasShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtMeasStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest11 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgMeasSmily1.Source = ImgTest11;

                    }
                    if (dt.Rows[i]["ChangeType"].ToString() == "Environment")
                    {
                        brdEnvchange.BorderBrush = Brushes.Red;
                        brdEnvchange.Visibility = Visibility.Visible;
                        txtEnvchange.Foreground = Brushes.Red;
                        txtEnvchange1.Foreground = Brushes.Red;
                        txtEnvProcess.Foreground = Brushes.Red;
                        txtEnvShift.Foreground = Brushes.Red;
                        txtEnvStation.Foreground = Brushes.Red;
                        txtEnvchange1.Text = "Change";
                        txtEnvProcess.Text = "Process:-" + dt.Rows[i]["Process"].ToString();
                        txtEnvShift.Text = "Date and Shift:-" + dt.Rows[i]["ShiftName"].ToString();
                        txtEnvStation.Text = "Change Station No:-" + dt.Rows[i]["Station"].ToString().Replace("STATION", "");
                        BitmapImage ImgTest12 = new BitmapImage(new Uri(System.Windows.Navigation.BaseUriHelper.GetBaseUri(this), @"/DENSO_ORM;component/Images/SadSmily.png"));
                        imgEnvSmily1.Source = ImgTest12;

                    }
                }
            }
        }
    }
}

