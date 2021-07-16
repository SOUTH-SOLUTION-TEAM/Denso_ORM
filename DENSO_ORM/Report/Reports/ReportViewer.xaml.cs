using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Reflection;
using SAPBusinessObjects.WPF.Viewer;

namespace DENSO_ORM.Report.Reports
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Page
    {
        #region Variables and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public static DataTable dtReport = new DataTable();
        public static string ReportName = "";
        public static string HeaderType = "";
        //[DllImport("Winspool.drv")]
        //private static extern bool SetDefaultPrinter(string printerName);
        //ReportDocument Doc = new ReportDocument();
        #endregion

        public ReportViewer()
        {
            InitializeComponent();
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
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPORT_VIEWER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {               
                switch (ReportName)
                {

                    case "OperationRatio":
                        Report.CrystallReport.OperationRatio ObjOperationRatio = new CrystallReport.OperationRatio();
                        ObjOperationRatio.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjOperationRatio;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "Dekidaka":
                        Report.CrystallReport.Dekidaka ObjDekidaka = new CrystallReport.Dekidaka();
                        ObjDekidaka.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjDekidaka;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "CycleTimeFlactuation":
                        Report.CrystallReport.CycleTimeFlactuation ObjCycleTimeFlactuation = new CrystallReport.CycleTimeFlactuation();
                        ObjCycleTimeFlactuation.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjCycleTimeFlactuation;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "CallandResponse":
                        Report.CrystallReport.CallandResponse ObjCallandResponse = new CrystallReport.CallandResponse();
                        ObjCallandResponse.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjCallandResponse;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "CycleTimeFlactuationForGraph":
                        Report.CrystallReport.CycleTimeFluctuationGraph ObjCycleTimeFlactuationGraph = new CrystallReport.CycleTimeFluctuationGraph();
                        ObjCycleTimeFlactuationGraph.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjCycleTimeFlactuationGraph;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "kanbanAchievement":
                        Report.CrystallReport.KanbanProgress ObjKanbanProgress = new CrystallReport.KanbanProgress();
                        ObjKanbanProgress.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjKanbanProgress;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "LossHour":
                        Report.CrystallReport.LossHour ObjLossHour = new CrystallReport.LossHour();
                        ObjLossHour.SetDataSource(dtReport);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjLossHour;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "Man-Hour":
                        Report.CrystallReport.ManHour ObjManHour = new CrystallReport.ManHour();
                        ObjManHour.SetDataSource(dtReport);
                        ObjManHour.SetParameterValue("Header", HeaderType);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjManHour;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                    case "Ratio":
                        Report.CrystallReport.Ratio ObjRatio = new CrystallReport.Ratio();
                        ObjRatio.SetDataSource(dtReport);
                        ObjRatio.SetParameterValue("Header", HeaderType);
                        crystalReportsViewer1.ViewerCore.ReportSource = ObjRatio;
                        crystalReportsViewer1.ToggleSidePanel = Constants.SidePanelKind.None;
                        break;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPORT_VIEWER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPORT_VIEWER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "REPORT_VIEWER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
