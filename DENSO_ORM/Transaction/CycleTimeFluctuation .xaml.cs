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
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for CycleTimeFluctuation.xaml
    /// </summary>
    public partial class CycleTimeFluctuation : Page
    {
        public CycleTimeFluctuation()
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
        ObservableCollection<KeyValuePair<double, double>> Power1 = new ObservableCollection<KeyValuePair<double, double>>();
        ObservableCollection<KeyValuePair<double, double>> Power2 = new ObservableCollection<KeyValuePair<double, double>>();

        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DataTable Dt_Graph = new DataTable();
        DataTable Dt_Graph1 = new DataTable();
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

                txtPartName.Text = CommonClasses.CommonVariable.ModelName + " - " + CommonClasses.CommonVariable.CycleTime + " S";
                Transaction("GetShiftDetails");
                Transaction("CycleTime");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
               // CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void LoadBarChartData()
        {
           // ((LineSeries)mcChart.Series[0]).ItemsSource = null;
           ((LineSeries)mcChart.Series[0]).DataContext =Power1;
     
            //((LineSeries)mcChart.Series[1]).ItemsSource = null;
            ((LineSeries)mcChart.Series[1]).DataContext = Power2;

            var style = new Style();
            style.TargetType = typeof(Polyline);
            style.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 3d));
            Actual.PolylineStyle = style;

            style = new Style();
            style.TargetType = typeof(Polyline);
            style.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, 3d));
            Target.PolylineStyle = style;

        }
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
                }
            }
            if (Type == "CycleTime")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                DataSet dt = obj_Tran.BL_DashBoard();

               // Dt_Graph.Rows.Clear();
                bool Flag = true;
                //Dt_Graph1.Rows.Clear();
                for (int i = Power1.Count; i < dt.Tables[0].Rows.Count; i++)
                {
                   // Dt_Graph.Rows.Add(dt.Tables[0].Rows[i]["SLNO"].ToString(), dt.Tables[0].Rows[i]["Vaues"].ToString());
                    Power1.Add(new KeyValuePair<double, double>(Convert.ToDouble( dt.Tables[0].Rows[i]["SLNO"]),Convert.ToDouble( dt.Tables[0].Rows[i]["Vaues"])));
                    if (Convert.ToInt32(dt.Tables[0].Rows[i]["Vaues"].ToString()) > 100)
                    {
                        Flag = false;
                    }
                }
                for (int i = Power2.Count; i < dt.Tables[1].Rows.Count; i++)
                {
                   // Dt_Graph1.Rows.Add(dt.Tables[1].Rows[i]["SLNO"].ToString(), dt.Tables[1].Rows[i]["Vaues"].ToString());
                    Power2.Add(new KeyValuePair<double, double>(Convert.ToDouble(dt.Tables[1].Rows[i]["SLNO"]), Convert.ToDouble(dt.Tables[1].Rows[i]["Vaues"])));

                }
                if (Flag == false)
                {
                    mcChart.Height = 600;
                    mcChart.Margin = new Thickness(-17, -320, 0, 0);
                }
                else
                {
                    mcChart.Height = 290;
                    mcChart.Margin = new Thickness(-9, -8, 0, 0);
                }
                LoadBarChartData();

                if (dt.Tables[2].Rows.Count > 0)
                {
                    txtAvg.Text = "Avg = " + dt.Tables[2].Rows[0]["Avg"].ToString();
                    txtMax.Text = "Max = " + dt.Tables[2].Rows[0]["max"].ToString();
                    txtMin.Text = "Min = " + dt.Tables[2].Rows[0]["Min"].ToString();
                }
                else
                {

                    txtAvg.Text = "Avg = 0";
                    txtMax.Text = "Max = 0";
                    txtMin.Text = "Min = 0";
                }
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadBarChartData();
                ShowDateTime();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CYCLE_TIME_FLUCTUATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

      
    }
}