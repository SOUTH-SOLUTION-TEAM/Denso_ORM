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
    /// Interaction logic for Dekidaka.xaml
    /// </summary>
    public partial class Dekidaka : Page
    {
        public Dekidaka()
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

                Transaction("GetDekidakaTime");
                Transaction("Dekidaka");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA", CommonClasses.CommonVariable.UserID);
                //CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                ShowDateTime();
                GSecondName.Visibility = Visibility.Hidden;
                GFisrtName.Visibility = Visibility.Hidden;
                GThidName.Visibility = Visibility.Hidden;
                Transaction("GetShiftDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEKIDAKA", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {

            if (CommonClasses.CommonVariable.ShiftName == "First Shift")
            {
                GFisrtName.Visibility = Visibility.Visible;
                GSecondName.Visibility = Visibility.Hidden;
                GThidName.Visibility = Visibility.Hidden;
            }
            if (CommonClasses.CommonVariable.ShiftName == "Second Shift")
            {
                GSecondName.Visibility = Visibility.Visible;
                GFisrtName.Visibility = Visibility.Hidden;
                GThidName.Visibility = Visibility.Hidden;

            }
            if (CommonClasses.CommonVariable.ShiftName == "Third Shift")
            {
                GSecondName.Visibility = Visibility.Hidden;
                GFisrtName.Visibility = Visibility.Hidden;
                GThidName.Visibility = Visibility.Visible;

            }
            if (Type == "GetDekidakaTime")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[i]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[i]["Time"].ToString();
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["ShiftName"].ToString() == "First Shift")
                    {
                        if (i == 0)
                            txtTimeShift1Col1.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 1)
                            txtTimeShift1Col2.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 2)
                            txtTimeShift1Col3.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 3)
                            txtTimeShift1Col4.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 4)
                            txtTimeShift1Col5.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 5)
                            txtTimeShift1Col6.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 6)
                            txtTimeShift1Col7.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 7)
                            txtTimeShift1Col8.Text = dt.Rows[i]["TimeWorking"].ToString();
                    }
                    if (dt.Rows[i]["ShiftName"].ToString() == "Second Shift")
                    {
                        if (i == 0)
                            txtTimeShift2Col1.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 1)
                            txtTimeShift2Col2.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 2)
                            txtTimeShift2Col3.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 3)
                            txtTimeShift2Col4.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 4)
                            txtTimeShift2Col5.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 5)
                            txtTimeShift2Col6.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 6)
                            txtTimeShift2Col7.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 7)
                            txtTimeShift2Col8.Text = dt.Rows[i]["TimeWorking"].ToString();
                    }
                    if (dt.Rows[i]["ShiftName"].ToString() == "Third Shift")
                    {
                        if (i == 0)
                            txtTimeShift3Col1.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 1)
                            txtTimeShift3Col2.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 2)
                            txtTimeShift3Col3.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 3)
                            txtTimeShift3Col4.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 4)
                            txtTimeShift3Col5.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 5)
                            txtTimeShift3Col6.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 6)
                            txtTimeShift3Col7.Text = dt.Rows[i]["TimeWorking"].ToString();
                        if (i == 7)
                            txtTimeShift3Col8.Text = "00:00 00:00";

                    }
                }
            }
            if (Type == "Dekidaka")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                txtShift1Col1.Text = "0.0%";
                txtShift1ACol1.Text = "0000";
                txtShift1PCol1.Text = "0000";
                txtShift1Col2.Text = "0.0%";
                txtShift1ACol2.Text = "0000";
                txtShift1PCol2.Text = "0000";
                txtShift1Col3.Text = "0.0%";
                txtShift1ACol3.Text = "0000";
                txtShift1PCol3.Text = "0000";
                txtShift1Col4.Text = "0.0%";
                txtShift1ACol4.Text = "0000";
                txtShift1PCol4.Text = "0000";
                txtShift1Col5.Text = "0.0%";
                txtShift1ACol5.Text = "0000";
                txtShift1PCol5.Text = "0000";
                txtShift1Col6.Text = "0.0%";
                txtShift1ACol6.Text = "0000";
                txtShift1PCol6.Text = "0000";
                txtShift1Col7.Text = "0.0%";
                txtShift1ACol7.Text = "0000";
                txtShift1PCol7.Text = "0000";
                txtShift1Col8.Text = "0.0%";
                txtShift1ACol8.Text = "0000";
                txtShift1PCol8.Text = "0000";
                brdShift1col1.Background = Brushes.White;
                brdShift1col2.Background = Brushes.White;
                brdShift1col3.Background = Brushes.White;
                brdShift1col4.Background = Brushes.White;
                brdShift1col5.Background = Brushes.White;
                brdShift1col6.Background = Brushes.White;
                brdShift1col7.Background = Brushes.White;
                brdShift1col8.Background = Brushes.White;

                txtShift2Col1.Text = "0.0%";
                txtShift2ACol1.Text = "0000";
                txtShift2PCol1.Text = "0000";
                txtShift2Col2.Text = "0.0%";
                txtShift2ACol2.Text = "0000";
                txtShift2PCol2.Text = "0000";
                txtShift2Col8.Text = "0.0%";
                txtShift2ACol8.Text = "0000";
                txtShift2PCol8.Text = "0000";
                txtShift2Col3.Text = "0.0%";
                txtShift2ACol3.Text = "0000";
                txtShift2PCol3.Text = "0000";
                txtShift2Col4.Text = "0.0%";
                txtShift2ACol4.Text = "0000";
                txtShift2PCol4.Text = "0000";
                txtShift2Col5.Text = "0.0%";
                txtShift2ACol5.Text = "0000";
                txtShift2PCol5.Text = "0000";
                txtShift2Col6.Text = "0.0%";
                txtShift2ACol6.Text = "0000";
                txtShift2PCol6.Text = "0000";
                txtShift2Col7.Text = "0.0%";
                txtShift2ACol7.Text = "0000";
                txtShift2PCol7.Text = "0000";
                brdShift2col1.Background = Brushes.White;
                brdShift2col2.Background = Brushes.White;
                brdShift2col3.Background = Brushes.White;
                brdShift2col4.Background = Brushes.White;
                brdShift2col5.Background = Brushes.White;
                brdShift2col6.Background = Brushes.White;
                brdShift2col7.Background = Brushes.White;
                brdShift2col8.Background = Brushes.White;

                txtShift3Col1.Text = "0.0%";
                txtShift3ACol1.Text = "0000";
                txtShift3PCol1.Text = "0000";
                txtShift3Col2.Text = "0.0%";
                txtShift3ACol2.Text = "0000";
                txtShift3PCol2.Text = "0000";
                txtShift3Col3.Text = "0.0%";
                txtShift3ACol3.Text = "0000";
                txtShift3PCol3.Text = "0000";
                txtShift3Col4.Text = "0.0%";
                txtShift3ACol4.Text = "0000";
                txtShift3PCol4.Text = "0000";
                txtShift3Col5.Text = "0.0%";
                txtShift3ACol5.Text = "0000";
                txtShift3PCol5.Text = "0000";
                txtShift3Col6.Text = "0.0%";
                txtShift3ACol6.Text = "0000";
                txtShift3PCol6.Text = "0000";
                txtShift3Col7.Text = "0.0%";
                txtShift3ACol7.Text = "0000";
                txtShift3PCol7.Text = "0000";
                txtShift3Col8.Text = "0.0%";
                txtShift3ACol8.Text = "0000";
                txtShift3PCol8.Text = "0000";
                brdShift3col1.Background = Brushes.White;
                brdShift3col2.Background = Brushes.White;
                brdShift3col3.Background = Brushes.White;
                brdShift3col4.Background = Brushes.White;
                brdShift3col5.Background = Brushes.White;
                brdShift3col6.Background = Brushes.White;
                brdShift3col7.Background = Brushes.White;
                brdShift3col8.Background = Brushes.White;

                txtTimeShift3Col8.Foreground = Brushes.Maroon;
                txtTimeShift3Col7.Foreground = Brushes.Maroon;
                txtTimeShift3Col6.Foreground = Brushes.Maroon;
                txtTimeShift3Col5.Foreground = Brushes.Maroon;
                txtTimeShift3Col4.Foreground = Brushes.Maroon;
                txtTimeShift3Col3.Foreground = Brushes.Maroon;
                txtTimeShift3Col2.Foreground = Brushes.Maroon;
                txtTimeShift3Col1.Foreground = Brushes.Maroon;

                txtTimeShift2Col8.Foreground = Brushes.Maroon;
                txtTimeShift2Col7.Foreground = Brushes.Maroon;
                txtTimeShift2Col6.Foreground = Brushes.Maroon;
                txtTimeShift2Col5.Foreground = Brushes.Maroon;
                txtTimeShift2Col4.Foreground = Brushes.Maroon;
                txtTimeShift2Col3.Foreground = Brushes.Maroon;
                txtTimeShift2Col2.Foreground = Brushes.Maroon;
                txtTimeShift2Col1.Foreground = Brushes.Maroon;

                txtTimeShift1Col8.Foreground = Brushes.Maroon;
                txtTimeShift1Col7.Foreground = Brushes.Maroon;
                txtTimeShift1Col6.Foreground = Brushes.Maroon;
                txtTimeShift1Col5.Foreground = Brushes.Maroon;
                txtTimeShift1Col4.Foreground = Brushes.Maroon;
                txtTimeShift1Col3.Foreground = Brushes.Maroon;
                txtTimeShift1Col2.Foreground = Brushes.Maroon;
                txtTimeShift1Col1.Foreground = Brushes.Maroon;

                txtShift3PCol8.Foreground = Brushes.Black;
                txtShift3ACol8.Foreground = Brushes.Black;
                txtShift3Col8.Foreground = Brushes.Black;
                txtShift3PCol7.Foreground = Brushes.Black;
                txtShift3ACol7.Foreground = Brushes.Black;
                txtShift3Col7.Foreground = Brushes.Black;
                txtShift3PCol6.Foreground = Brushes.Black;
                txtShift3ACol6.Foreground = Brushes.Black;
                txtShift3Col6.Foreground = Brushes.Black;
                txtShift3PCol5.Foreground = Brushes.Black;
                txtShift3ACol5.Foreground = Brushes.Black;
                txtShift3Col5.Foreground = Brushes.Black;
                txtShift3PCol4.Foreground = Brushes.Black;
                txtShift3ACol4.Foreground = Brushes.Black;
                txtShift3Col4.Foreground = Brushes.Black;
                txtShift3PCol3.Foreground = Brushes.Black;
                txtShift3ACol3.Foreground = Brushes.Black;
                txtShift3Col3.Foreground = Brushes.Black;
                txtShift3PCol2.Foreground = Brushes.Black;
                txtShift3ACol2.Foreground = Brushes.Black;
                txtShift3Col2.Foreground = Brushes.Black;
                txtShift3PCol1.Foreground = Brushes.Black;
                txtShift3ACol1.Foreground = Brushes.Black;
                txtShift3Col1.Foreground = Brushes.Black;

                txtShift2PCol8.Foreground = Brushes.Black;
                txtShift2ACol8.Foreground = Brushes.Black;
                txtShift2Col8.Foreground = Brushes.Black;
                txtShift2PCol7.Foreground = Brushes.Black;
                txtShift2ACol7.Foreground = Brushes.Black;
                txtShift2Col7.Foreground = Brushes.Black;
                txtShift2PCol6.Foreground = Brushes.Black;
                txtShift2ACol6.Foreground = Brushes.Black;
                txtShift2Col6.Foreground = Brushes.Black;
                txtShift2PCol5.Foreground = Brushes.Black;
                txtShift2ACol5.Foreground = Brushes.Black;
                txtShift2Col5.Foreground = Brushes.Black;
                txtShift2PCol4.Foreground = Brushes.Black;
                txtShift2ACol4.Foreground = Brushes.Black;
                txtShift2Col4.Foreground = Brushes.Black;
                txtShift2PCol3.Foreground = Brushes.Black;
                txtShift2ACol3.Foreground = Brushes.Black;
                txtShift2Col3.Foreground = Brushes.Black;
                txtShift2PCol2.Foreground = Brushes.Black;
                txtShift2ACol2.Foreground = Brushes.Black;
                txtShift2Col2.Foreground = Brushes.Black;
                txtShift2PCol1.Foreground = Brushes.Black;
                txtShift2ACol1.Foreground = Brushes.Black;
                txtShift2Col1.Foreground = Brushes.Black;

                txtShift1PCol8.Foreground = Brushes.Black;
                txtShift1ACol8.Foreground = Brushes.Black;
                txtShift1Col8.Foreground = Brushes.Black;
                txtShift1PCol7.Foreground = Brushes.Black;
                txtShift1ACol7.Foreground = Brushes.Black;
                txtShift1Col7.Foreground = Brushes.Black;
                txtShift1PCol6.Foreground = Brushes.Black;
                txtShift1ACol6.Foreground = Brushes.Black;
                txtShift1Col6.Foreground = Brushes.Black;
                txtShift1PCol5.Foreground = Brushes.Black;
                txtShift1ACol5.Foreground = Brushes.Black;
                txtShift1Col5.Foreground = Brushes.Black;
                txtShift1PCol4.Foreground = Brushes.Black;
                txtShift1ACol4.Foreground = Brushes.Black;
                txtShift1Col4.Foreground = Brushes.Black;
                txtShift1PCol3.Foreground = Brushes.Black;
                txtShift1ACol3.Foreground = Brushes.Black;
                txtShift1Col3.Foreground = Brushes.Black;
                txtShift1PCol2.Foreground = Brushes.Black;
                txtShift1ACol2.Foreground = Brushes.Black;
                txtShift1Col2.Foreground = Brushes.Black;
                txtShift1PCol1.Foreground = Brushes.Black;
                txtShift1ACol1.Foreground = Brushes.Black;
                txtShift1Col1.Foreground = Brushes.Black;

                if (txtTimeShift3Col8.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col8.Foreground = Brushes.DarkGreen;
                    brdShift3col8.Background = Brushes.Red;
                    txtShift3PCol8.Foreground = Brushes.White;
                    txtShift3ACol8.Foreground = Brushes.White;
                    txtShift3Col8.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col7.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col7.Foreground = Brushes.DarkGreen;
                    brdShift3col7.Background = Brushes.Red;
                    txtShift3PCol7.Foreground = Brushes.White;
                    txtShift3ACol7.Foreground = Brushes.White;
                    txtShift3Col7.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col6.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col6.Foreground = Brushes.DarkGreen;
                    brdShift3col6.Background = Brushes.Red;
                    txtShift3PCol6.Foreground = Brushes.White;
                    txtShift3ACol6.Foreground = Brushes.White;
                    txtShift3Col6.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col5.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col5.Foreground = Brushes.DarkGreen;
                    brdShift3col5.Background = Brushes.Red;
                    txtShift3PCol5.Foreground = Brushes.White;
                    txtShift3ACol5.Foreground = Brushes.White;
                    txtShift3Col5.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col4.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col4.Foreground = Brushes.DarkGreen;
                    brdShift3col4.Background = Brushes.Red;
                    txtShift3PCol4.Foreground = Brushes.White;
                    txtShift3ACol4.Foreground = Brushes.White;
                    txtShift3Col4.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col3.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col3.Foreground = Brushes.DarkGreen;
                    brdShift3col3.Background = Brushes.Red;
                    txtShift3PCol3.Foreground = Brushes.White;
                    txtShift3ACol3.Foreground = Brushes.White;
                    txtShift3Col3.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col2.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col2.Foreground = Brushes.DarkGreen;
                    brdShift3col2.Background = Brushes.Red;
                    txtShift3PCol2.Foreground = Brushes.White;
                    txtShift3ACol2.Foreground = Brushes.White;
                    txtShift3Col2.Foreground = Brushes.White;
                }
                if (txtTimeShift3Col1.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift3Col1.Foreground = Brushes.DarkGreen;
                    brdShift3col1.Background = Brushes.Red;
                    txtShift3PCol1.Foreground = Brushes.White;
                    txtShift3ACol1.Foreground = Brushes.White;
                    txtShift3Col1.Foreground = Brushes.White;
                }

                if (txtTimeShift2Col8.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col8.Foreground = Brushes.DarkGreen;
                    brdShift2col8.Background = Brushes.Red;
                    txtShift2PCol8.Foreground = Brushes.White;
                    txtShift2ACol8.Foreground = Brushes.White;
                    txtShift2Col8.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col7.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col7.Foreground = Brushes.DarkGreen;
                    brdShift2col7.Background = Brushes.Red;
                    txtShift2PCol7.Foreground = Brushes.White;
                    txtShift2ACol7.Foreground = Brushes.White;
                    txtShift2Col7.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col6.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col6.Foreground = Brushes.DarkGreen;
                    brdShift2col6.Background = Brushes.Red;
                    txtShift2PCol6.Foreground = Brushes.White;
                    txtShift2ACol6.Foreground = Brushes.White;
                    txtShift2Col6.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col5.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col5.Foreground = Brushes.DarkGreen;
                    brdShift2col5.Background = Brushes.Red;
                    txtShift2PCol5.Foreground = Brushes.White;
                    txtShift2ACol5.Foreground = Brushes.White;
                    txtShift2Col5.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col4.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col4.Foreground = Brushes.DarkGreen;
                    brdShift2col4.Background = Brushes.Red;
                    txtShift2PCol4.Foreground = Brushes.White;
                    txtShift2ACol4.Foreground = Brushes.White;
                    txtShift2Col4.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col3.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col3.Foreground = Brushes.DarkGreen;
                    brdShift2col3.Background = Brushes.Red;
                    txtShift2PCol3.Foreground = Brushes.White;
                    txtShift2ACol3.Foreground = Brushes.White;
                    txtShift2Col3.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col2.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col2.Foreground = Brushes.DarkGreen;
                    brdShift2col2.Background = Brushes.Red;
                    txtShift2PCol2.Foreground = Brushes.White;
                    txtShift2ACol2.Foreground = Brushes.White;
                    txtShift2Col2.Foreground = Brushes.White;
                }
                if (txtTimeShift2Col1.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift2Col1.Foreground = Brushes.DarkGreen;
                    brdShift2col1.Background = Brushes.Red;
                    txtShift2PCol1.Foreground = Brushes.White;
                    txtShift2ACol1.Foreground = Brushes.White;
                    txtShift2Col1.Foreground = Brushes.White;
                }

                if (txtTimeShift1Col8.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col8.Foreground = Brushes.DarkGreen;
                    brdShift1col8.Background = Brushes.Red;
                    txtShift1PCol8.Foreground = Brushes.White;
                    txtShift1ACol8.Foreground = Brushes.White;
                    txtShift1Col8.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col7.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col7.Foreground = Brushes.DarkGreen;
                    brdShift1col7.Background = Brushes.Red;
                    txtShift1PCol7.Foreground = Brushes.White;
                    txtShift1ACol7.Foreground = Brushes.White;
                    txtShift1Col7.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col6.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col6.Foreground = Brushes.DarkGreen;
                    brdShift1col6.Background = Brushes.Red;
                    txtShift1PCol6.Foreground = Brushes.White;
                    txtShift1ACol6.Foreground = Brushes.White;
                    txtShift1Col6.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col5.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col5.Foreground = Brushes.DarkGreen;
                    brdShift1col5.Background = Brushes.Red;
                    txtShift1PCol5.Foreground = Brushes.White;
                    txtShift1ACol5.Foreground = Brushes.White;
                    txtShift1Col5.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col4.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col4.Foreground = Brushes.DarkGreen;
                    brdShift1col4.Background = Brushes.Red;
                    txtShift1PCol4.Foreground = Brushes.White;
                    txtShift1ACol4.Foreground = Brushes.White;
                    txtShift1Col4.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col3.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col3.Foreground = Brushes.DarkGreen;
                    brdShift1col3.Background = Brushes.Red;
                    txtShift1PCol3.Foreground = Brushes.White;
                    txtShift1ACol3.Foreground = Brushes.White;
                    txtShift1Col3.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col2.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col2.Foreground = Brushes.DarkGreen;
                    brdShift1col2.Background = Brushes.Red;
                    txtShift1PCol2.Foreground = Brushes.White;
                    txtShift1ACol2.Foreground = Brushes.White;
                    txtShift1Col2.Foreground = Brushes.White;
                }
                if (txtTimeShift1Col1.Text.Split(' ')[0].ToString() == CommonClasses.CommonVariable.Time)
                {
                    txtTimeShift1Col1.Foreground = Brushes.DarkGreen;
                    brdShift1col1.Background = Brushes.Red;
                    txtShift1PCol1.Foreground = Brushes.White;
                    txtShift1ACol1.Foreground = Brushes.White;
                    txtShift1Col1.Foreground = Brushes.White;
                }



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ShiftName"].ToString() == "First Shift")
                    {

                        if (txtTimeShift1Col1.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col1.Foreground = Brushes.White;
                            txtShift1ACol1.Foreground = Brushes.White;
                            txtShift1PCol1.Foreground = Brushes.White;

                            txtShift1Col1.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol1.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol1.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col1.Text.Replace("%", "")) < 90)
                                brdShift1col1.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col1.Text.Replace("%", "")) >= 90)
                                brdShift1col1.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col1.Text.Replace("%", "")) >= 100)
                                brdShift1col1.Background = Brushes.Green;

                        }
                        if (txtTimeShift1Col2.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col2.Foreground = Brushes.White;
                            txtShift1ACol2.Foreground = Brushes.White;
                            txtShift1PCol2.Foreground = Brushes.White;
                            txtShift1Col2.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol2.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol2.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col2.Text.Replace("%", "")) < 90)
                                brdShift1col2.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col2.Text.Replace("%", "")) >= 90)
                                brdShift1col2.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col2.Text.Replace("%", "")) >= 100)
                                brdShift1col2.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col3.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col3.Foreground = Brushes.White;
                            txtShift1ACol3.Foreground = Brushes.White;
                            txtShift1PCol3.Foreground = Brushes.White;

                            txtShift1Col3.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol3.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol3.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col3.Text.Replace("%", "")) < 90)
                                brdShift1col3.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col3.Text.Replace("%", "")) >= 90)
                                brdShift1col3.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col3.Text.Replace("%", "")) >= 100)
                                brdShift1col3.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col4.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col4.Foreground = Brushes.White;
                            txtShift1ACol4.Foreground = Brushes.White;
                            txtShift1PCol4.Foreground = Brushes.White;

                            txtShift1Col4.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol4.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol4.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col4.Text.Replace("%", "")) < 90)
                                brdShift1col4.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col4.Text.Replace("%", "")) >= 90)
                                brdShift1col4.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col4.Text.Replace("%", "")) >= 100)
                                brdShift1col4.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col5.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col5.Foreground = Brushes.White;
                            txtShift1ACol5.Foreground = Brushes.White;
                            txtShift1PCol5.Foreground = Brushes.White;

                            txtShift1Col5.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol5.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol5.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col5.Text.Replace("%", "")) < 90)
                                brdShift1col5.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col5.Text.Replace("%", "")) >= 90)
                                brdShift1col5.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col5.Text.Replace("%", "")) >= 100)
                                brdShift1col5.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col6.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col6.Foreground = Brushes.White;
                            txtShift1ACol6.Foreground = Brushes.White;
                            txtShift1PCol6.Foreground = Brushes.White;
                            txtShift1Col6.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol6.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol6.Text = dt.Rows[i]["PCount"].ToString();

                            if (Convert.ToDouble(txtShift1Col6.Text.Replace("%", "")) < 90)
                                brdShift1col6.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col6.Text.Replace("%", "")) >= 90)
                                brdShift1col6.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col6.Text.Replace("%", "")) >= 100)
                                brdShift1col6.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col7.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col7.Foreground = Brushes.White;
                            txtShift1ACol7.Foreground = Brushes.White;
                            txtShift1PCol7.Foreground = Brushes.White;

                            txtShift1Col7.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol7.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol7.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col7.Text.Replace("%", "")) < 90)
                                brdShift1col7.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col7.Text.Replace("%", "")) >= 90)
                                brdShift1col7.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col7.Text.Replace("%", "")) >= 100)
                                brdShift1col7.Background = Brushes.Green;
                        }
                        if (txtTimeShift1Col8.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift1Col8.Foreground = Brushes.White;
                            txtShift1ACol8.Foreground = Brushes.White;
                            txtShift1PCol8.Foreground = Brushes.White;
                            txtShift1Col8.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift1ACol8.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift1PCol8.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift1Col8.Text.Replace("%", "")) < 90)
                                brdShift1col8.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift1Col8.Text.Replace("%", "")) >= 90)
                                brdShift1col8.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift1Col8.Text.Replace("%", "")) >= 100)
                                brdShift1col8.Background = Brushes.Green;
                        }
                    }
                    if (dt.Rows[i]["ShiftName"].ToString() == "Second Shift")
                    {

                        if (txtTimeShift2Col1.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col1.Foreground = Brushes.White;
                            txtShift2ACol1.Foreground = Brushes.White;
                            txtShift2PCol1.Foreground = Brushes.White;
                            txtShift2Col1.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol1.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol1.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col1.Text.Replace("%", "")) < 90)
                                brdShift2col1.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col1.Text.Replace("%", "")) >= 90)
                                brdShift2col1.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col1.Text.Replace("%", "")) >= 100)
                                brdShift2col1.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col2.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col2.Foreground = Brushes.White;
                            txtShift2ACol2.Foreground = Brushes.White;
                            txtShift2PCol2.Foreground = Brushes.White;
                            txtShift2Col2.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol2.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol2.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col2.Text.Replace("%", "")) < 90)
                                brdShift2col2.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col2.Text.Replace("%", "")) >= 90)
                                brdShift2col2.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col2.Text.Replace("%", "")) >= 100)
                                brdShift2col2.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col3.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col3.Foreground = Brushes.White;
                            txtShift2ACol3.Foreground = Brushes.White;
                            txtShift2PCol3.Foreground = Brushes.White;
                            txtShift2Col3.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol3.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol3.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col3.Text.Replace("%", "")) < 90)
                                brdShift2col3.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col3.Text.Replace("%", "")) >= 90)
                                brdShift2col3.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col3.Text.Replace("%", "")) >= 100)
                                brdShift2col3.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col4.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col4.Foreground = Brushes.White;
                            txtShift2ACol4.Foreground = Brushes.White;
                            txtShift2PCol4.Foreground = Brushes.White;
                            txtShift2Col4.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol4.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol4.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col4.Text.Replace("%", "")) < 90)
                                brdShift2col4.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col4.Text.Replace("%", "")) >= 90)
                                brdShift2col4.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col4.Text.Replace("%", "")) >= 100)
                                brdShift2col4.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col5.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col5.Foreground = Brushes.White;
                            txtShift2ACol5.Foreground = Brushes.White;
                            txtShift2PCol5.Foreground = Brushes.White;
                            txtShift2Col5.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol5.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol5.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col5.Text.Replace("%", "")) < 90)
                                brdShift2col5.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col5.Text.Replace("%", "")) >= 90)
                                brdShift2col5.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col5.Text.Replace("%", "")) >= 100)
                                brdShift2col5.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col6.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col6.Foreground = Brushes.White;
                            txtShift2ACol6.Foreground = Brushes.White;
                            txtShift2PCol6.Foreground = Brushes.White;
                            txtShift2Col6.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol6.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol6.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col6.Text.Replace("%", "")) < 90)
                                brdShift2col6.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col6.Text.Replace("%", "")) >= 90)
                                brdShift2col6.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col6.Text.Replace("%", "")) >= 100)
                                brdShift2col6.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col7.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col7.Foreground = Brushes.White;
                            txtShift2ACol7.Foreground = Brushes.White;
                            txtShift2PCol7.Foreground = Brushes.White;

                            txtShift2Col7.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol7.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol7.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col7.Text.Replace("%", "")) < 90)
                                brdShift2col7.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col7.Text.Replace("%", "")) >= 90)
                                brdShift2col7.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col7.Text.Replace("%", "")) >= 100)
                                brdShift2col7.Background = Brushes.Green;
                        }
                        if (txtTimeShift2Col8.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift2Col8.Foreground = Brushes.White;
                            txtShift2ACol8.Foreground = Brushes.White;
                            txtShift2PCol8.Foreground = Brushes.White;

                            txtShift2Col8.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift2ACol8.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift2PCol8.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift2Col8.Text.Replace("%", "")) < 90)
                                brdShift2col8.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift2Col8.Text.Replace("%", "")) >= 90)
                                brdShift2col8.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift2Col8.Text.Replace("%", "")) >= 100)
                                brdShift2col8.Background = Brushes.Green;
                        }
                    }
                    if (dt.Rows[i]["ShiftName"].ToString() == "Third Shift")
                    {

                        if (txtTimeShift3Col1.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col1.Foreground = Brushes.White;
                            txtShift3ACol1.Foreground = Brushes.White;
                            txtShift3PCol1.Foreground = Brushes.White;
                            txtShift3Col1.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol1.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol1.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col1.Text.Replace("%", "")) < 90)
                                brdShift3col1.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col1.Text.Replace("%", "")) >= 90)
                                brdShift3col1.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col1.Text.Replace("%", "")) >= 100)
                                brdShift3col1.Background = Brushes.Green;
                        }
                        if (txtTimeShift3Col2.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col2.Foreground = Brushes.White;
                            txtShift3ACol2.Foreground = Brushes.White;
                            txtShift3PCol2.Foreground = Brushes.White;
                            txtShift3Col2.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol2.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol2.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col2.Text.Replace("%", "")) < 90)
                                brdShift3col2.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col2.Text.Replace("%", "")) >= 90)
                                brdShift3col2.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col2.Text.Replace("%", "")) >= 100)
                                brdShift3col2.Background = Brushes.Green;

                        }
                        if (txtTimeShift3Col3.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col3.Foreground = Brushes.White;
                            txtShift3ACol3.Foreground = Brushes.White;
                            txtShift3PCol3.Foreground = Brushes.White;
                            txtShift3Col3.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol3.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol3.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col3.Text.Replace("%", "")) < 90)
                                brdShift3col3.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col3.Text.Replace("%", "")) >= 90)
                                brdShift3col3.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col3.Text.Replace("%", "")) >= 100)
                                brdShift3col3.Background = Brushes.Green;

                        }
                        if (txtTimeShift3Col4.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col4.Foreground = Brushes.White;
                            txtShift3ACol4.Foreground = Brushes.White;
                            txtShift3PCol4.Foreground = Brushes.White;
                            txtShift3Col4.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol4.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol4.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col4.Text.Replace("%", "")) < 90)
                                brdShift3col4.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col4.Text.Replace("%", "")) >= 90)
                                brdShift3col4.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col4.Text.Replace("%", "")) >= 100)
                                brdShift3col4.Background = Brushes.Green;
                        }
                        if (txtTimeShift3Col5.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col5.Foreground = Brushes.White;
                            txtShift3ACol5.Foreground = Brushes.White;
                            txtShift3PCol5.Foreground = Brushes.White;
                            txtShift3Col5.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol5.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol5.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col5.Text.Replace("%", "")) < 90)
                                brdShift3col5.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col5.Text.Replace("%", "")) >= 90)
                                brdShift3col5.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col5.Text.Replace("%", "")) >= 100)
                                brdShift3col5.Background = Brushes.Green;
                        }
                        if (txtTimeShift3Col6.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col6.Foreground = Brushes.White;
                            txtShift3ACol6.Foreground = Brushes.White;
                            txtShift3PCol6.Foreground = Brushes.White;
                            txtShift3Col6.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol6.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol6.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col6.Text.Replace("%", "")) < 90)
                                brdShift3col6.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col6.Text.Replace("%", "")) >= 90)
                                brdShift3col6.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col6.Text.Replace("%", "")) >= 100)
                                brdShift3col6.Background = Brushes.Green;
                        }
                        if (txtTimeShift3Col7.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col7.Foreground = Brushes.White;
                            txtShift3ACol7.Foreground = Brushes.White;
                            txtShift3PCol7.Foreground = Brushes.White;
                            txtShift3Col7.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol7.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol7.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col7.Text.Replace("%", "")) < 90)
                                brdShift3col7.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col7.Text.Replace("%", "")) >= 90)
                                brdShift3col7.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col7.Text.Replace("%", "")) >= 100)
                                brdShift3col7.Background = Brushes.Green;
                        }
                        if (txtTimeShift3Col8.Text.Split(' ')[0].ToString() == dt.Rows[i]["TimeWorking"].ToString())
                        {
                            txtShift3Col8.Foreground = Brushes.White;
                            txtShift3ACol8.Foreground = Brushes.White;
                            txtShift3PCol8.Foreground = Brushes.White;
                            txtShift3Col8.Text = dt.Rows[i]["HourPers"].ToString() + "%";
                            txtShift3ACol8.Text = dt.Rows[i]["ACount"].ToString();
                            txtShift3PCol8.Text = dt.Rows[i]["PCount"].ToString();
                            if (Convert.ToDouble(txtShift3Col8.Text.Replace("%", "")) < 90)
                                brdShift3col8.Background = Brushes.Red;
                            if (Convert.ToDouble(txtShift3Col8.Text.Replace("%", "")) >= 90)
                                brdShift3col8.Background = Brushes.MediumBlue;
                            if (Convert.ToDouble(txtShift3Col8.Text.Replace("%", "")) >= 100)
                                brdShift3col8.Background = Brushes.Green;
                        }

                    }
                }
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "OR_MONITORING", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}