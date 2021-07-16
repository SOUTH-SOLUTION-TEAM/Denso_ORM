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
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data;

namespace DENSO_ORM.StartUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Varialble and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


        //  BUSINESS_LAYER.Transaction.Transaction obj_Transaction = new BUSINESS_LAYER.Transaction.Transaction();
        #endregion

        #region Methods
        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        #endregion

        #region Events
        private void ImgSmily3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //System.Windows.Forms.SendKeys.SendWait("{F11}");

                txtuserID.Text ="Application is using by "+  CommonClasses.CommonVariable.UserName;
                ShowDateTime();
                //if (CommonClasses.CommonVariable.obj_prd != null)
                //    CommonClasses.CommonVariable.obj_prd.Close();

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnMasters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridSubMenu.Children.RemoveRange(0, 20);

                if (CommonClasses.CommonVariable.Rights == "" || CommonClasses.CommonVariable.Rights == null)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    return;
                }
                for (int J = 0; J < GridSubMenu.RowDefinitions.Count; J++)
                {
                    int ControlsCount = 0;

                    if (ControlsCount != 5)   //how many Controls in Grid
                    {
                        for (int i = 0; i < GridSubMenu.ColumnDefinitions.Count; i++)
                        {
                            if (i == 0 && J == 0)
                            {
                               // Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "USER MASTER";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = null;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("USER MASTER"))
                                {
                                    obj.Click += UserMaster_Click;
                                }
                                else
                                {
                                    obj.Click -= UserMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 1 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "GROUP MASTER";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                               // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("GROUP MASTER"))
                                    obj.Click += GroupMaster_Click;
                                else
                                {
                                    obj.Click -= GroupMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "MACHINE GROUP";
                              //  obj.Height = 80;
                               // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("MACHINE GROUP"))
                                    obj.Click += MachineGroup_Click;
                                else
                                {
                                    obj.Click -= MachineGroup_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            //Module Master
                            if (i == 3 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "MODULE MASTER";
                              //  obj.Height = 80;
                               // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("MODULE MASTER"))
                                    obj.Click += ModuleMaster_Click;
                                else
                                {
                                    obj.Click -= ModuleMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            //ProblemDefeact Master
                            if (i == 4 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "PROBLEM AND DEFECT MASTER";
                               // obj.Height = 80;
                               // obj.Width = 270;
                                //obj.FontSize = 15;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("PROBLEM AND DEFECT MASTER"))
                                    obj.Click += ProblemDefectMaster_Click;
                                else
                                {
                                    obj.Click -= ProblemDefectMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            //if (i == 0 && J == 1)
                            //{
                            //    Grid g = new Grid();
                            //    Button obj = new Button();
                            //    obj.Content = "COLOR CONGURATION";
                            //    //obj.Height = 80;
                            //    //obj.Width = 190;
                            //    //obj.FontSize = 15;
                            //    obj.Style = (Style)FindResource("SubMenuButton");
                            //    Grid.SetColumn(obj, i);
                            //    Grid.SetRow(obj, J);
                            //    GridSubMenu.Children.Add(obj);
                            //    ControlsCount = ControlsCount + 1;
                            //    // obj.Click += GroupMaster_Click;
                            //    if (CommonClasses.CommonVariable.Rights.Contains("COLOR CONGURATION"))
                            //        obj.Click += ColorConfig_Click;
                            //    else
                            //    {
                            //        obj.Click -= ColorConfig_Click;
                            //        obj.ToolTip = "Access Denied";
                            //    }
                            //}
                            if (i == 0 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "SHIFT MASTER";
                               // obj.Height = 80;
                                //obj.Width = 190;
                                //obj.FontSize = 15;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("SHIFT MASTER"))
                                    obj.Click += ShiftMaster_Click;
                                else
                                {
                                    obj.Click -= ShiftMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 1 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "SMS AND CALL CONFIGURATION";
                               // obj.Height = 80;
                                //obj.Width = 190;
                                //obj.FontSize = 15;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("SMS AND CALL CONFIGURATION"))
                                    obj.Click += SMSandCallMaster_Click;
                                else
                                {
                                    obj.Click -= SMSandCallMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "DEVICE IP AND PORT CONFIGURATION";
                             //   obj.Height = 80;
                              //  obj.Width = 190;
                                //obj.FontSize = 15;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("DEVICE IP AND PORT CONFIG"))
                                    obj.Click += IPandPortMaster_Click;
                                else
                                {
                                    obj.Click -= IPandPortMaster_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 3 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "KANBAN PROGREE MASTER";
                                //   obj.Height = 80;
                                //  obj.Width = 190;
                                //obj.FontSize = 15;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;
                                // obj.Click += GroupMaster_Click;
                                if (CommonClasses.CommonVariable.Rights.Contains("KANBAN PROGREE MASTER"))
                                    obj.Click += KANBANPROGREEMASTER_Click;
                                else
                                {
                                    obj.Click -= KANBANPROGREEMASTER_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void KANBANPROGREEMASTER_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.Kanban_Progress());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void IPandPortMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.DeviceIPandPortConfiguration());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void SMSandCallMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.SMSandVoiceCallConfiguration());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ShiftMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.ShiftMaster());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ColorConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.ColorMaster());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void GroupMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.GroupMaster());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }

        private void UserMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.UserMaster());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }
        private void MachineGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.MachineGroup());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }
        private void ModuleMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.ModuleMaster());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }

        private void ProblemDefectMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Masters.Defect_ProblemMaster());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }

        private void BtnTransport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridSubMenu.Children.RemoveRange(0, 20);

                if (CommonClasses.CommonVariable.Rights == "" || CommonClasses.CommonVariable.Rights == null)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    return;
                }
                for (int J = 0; J < GridSubMenu.RowDefinitions.Count; J++)
                {
                    int ControlsCount = 0;

                    if (ControlsCount != 5)   //how many Controls in Grid
                    {
                        for (int i = 0; i < GridSubMenu.ColumnDefinitions.Count; i++)
                        {
                            if (i == 0 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "DASH BOARD";
                                // obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("DASHBOARD"))
                                {
                                    obj.Click += DashBoard_Click;
                                }
                                else
                                {
                                    obj.Click -= DashBoard_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 1 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "OR MONITORING";
                               // obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("OR MONITORING"))
                                {
                                    obj.Click += ORMonitoring_Click;
                                }
                                else
                                {
                                    obj.Click -= ORMonitoring_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "DEKIDAKA";
                              //  obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("DEKIDAKA"))
                                {
                                    obj.Click += Dekidaka_Click;
                                }  
                                else
                                {
                                     obj.Click -= Dekidaka_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }

                            if (i == 3 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "CYCLE TIME FLUCTUATION";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("CYCLE TIME FLUCTUATION"))
                                {
                                    obj.Click += CycleTime_Click;
                                }  
                                else
                                {
                                      obj.Click -= CycleTime_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 4 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "ANDON SMS AND CALL";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("ANDON SMS AND CALL"))
                                {
                                    obj.Click += AndonCallandSMS_Click;
                                }
                                else
                                {
                                    obj.Click -= AndonCallandSMS_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 0 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "ANDON CALL STATUS";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("ANDON CALL STATUS"))
                                {
                                    obj.Click += AndonCallStatus_Click;
                                }
                                else
                                {
                                    obj.Click -= AndonCallStatus_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i ==1  && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "STOCK CONTROL";
                               // obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("STOCK CONTROL"))
                                {
                                    obj.Click += StockControl_Click;
                                }
                                else
                                {
                                    obj.Click -= StockControl_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "KANBAN PROCESS";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("KANBAN PROCESS"))
                                {
                                    obj.Click += KanbanProcess_Click;
                                }
                                else
                                {
                                    obj.Click -= KanbanProcess_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 3 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "SCRAP";
                                //obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("SCRAP"))
                                {
                                    obj.Click += Scrap_Click;
                                }
                                else
                                {
                                    obj.Click -= Scrap_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 4 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "MACHINE ON AND OFF";
                               // obj.Height = 80;
                                //obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("MACHINE ON AND OFF"))
                                {
                                    obj.Click += MachineONandOff_Click;
                                }
                                 
                                else
                                {
                                    obj.Click += MachineONandOff_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            //if (i == 4 && J == 1)
                            //{
                            //    Grid g = new Grid();
                            //    Button obj = new Button();
                            //    obj.Content = "MAN HOUR CONTROL";
                            //  //  obj.Height = 80;
                            //   // obj.Width = 190;
                            //    obj.Style = (Style)FindResource("SubMenuButton");
                            //    Grid.SetColumn(obj, i);
                            //    Grid.SetRow(obj, J);
                            //    GridSubMenu.Children.Add(obj);
                            //    ControlsCount = ControlsCount + 1;

                            //    if (CommonClasses.CommonVariable.Rights.Contains("MAN HOUR CONTROL"))
                            //    {
                            //        obj.Click += ManHourControl_Click;
                            //    }
                            //    //  
                            //    else
                            //    {
                            //         obj.Click -= ManHourControl_Click;
                            //        obj.ToolTip = "Access Denied";
                            //    }
                            //}
                            if (i == 0 && J == 2)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "MAINTAINANCE CONFIRMATION";
                                //  obj.Height = 80;
                                // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("MAINTAINANCE CONFIRMATION"))
                                {
                                    obj.Click += MaintainanceConfirm_Click;
                                }
                                //  
                                else
                                {
                                    obj.Click -= MaintainanceConfirm_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 1 && J == 2)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "5M AND 1E CHANGES";
                                //  obj.Height = 80;
                                // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("5M AND 1E CHANGES"))
                                {
                                    obj.Click += _5M1EChanges_Click;
                                }
                                //  
                                else
                                {
                                    obj.Click -= _5M1EChanges_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 2)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "5M AND 1E CHANGES VIEW";
                                //  obj.Height = 80;
                                // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("5M AND 1E CHANGES VIEW"))
                                {
                                    obj.Click += _5M1EChangesView_Click;
                                }
                                //  
                                else
                                {
                                    obj.Click -= _5M1EChangesView_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 3 && J == 2)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "MAN HOUR CONTROL(KPI)";
                                //  obj.Height = 80;
                                // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("MAN HOUR CONTROL (KPI)"))
                                {
                                    obj.Click += ManHourControl_Click;
                                }
                                //  
                                else
                                {
                                    obj.Click -= ManHourControl_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 4 && J == 2)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "LOSS HOUR";
                                //  obj.Height = 80;
                                // obj.Width = 190;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("LOSS HOUR"))
                                {
                                    obj.Click += LossHour_Click;
                                }
                                //  
                                else
                                {
                                    obj.Click -= LossHour_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }
        private void ManHourControl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction.KPIEntry());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void LossHour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction.LossEntry());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void _5M1EChangesView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "5mAnd1eChangeView";

                NavigationService.Navigate(new Transaction._5Mand1E());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void _5M1EChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction._5M1EChanges());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void AndonCallStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "AndonSMSandCallForSmallManitor";
                NavigationService.Navigate(new Transaction.AndonSMSandCallForSmallManitor());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DashBoard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "DashBoard";

                NavigationService.Navigate(new Transaction.DashBoard());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MaintainanceConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction.MaintainceConfirmation());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MachineONandOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction.MachineONandOFF());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Scrap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Transaction.Scrap());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void KanbanProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "KanbanProcess";

                NavigationService.Navigate(new Transaction.KanbanProcessSystem());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void StockControl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "StockVisualisation";

                NavigationService.Navigate(new Transaction.StockVisualisation());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void AndonCallandSMS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "AndonCall";
                NavigationService.Navigate(new Transaction.AndonSMSandCall());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CycleTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "CycleTime";
                NavigationService.Navigate(new Transaction.CycleTimeFluctuation());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Dekidaka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "Dekidaka";
                NavigationService.Navigate(new Transaction.Dekidaka());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ORMonitoring_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.TransactioType = "ORMonitor";
                NavigationService.Navigate(new Transaction.OR_Monitoring_System());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ControlsCount = 0;
                GridSubMenu.Children.RemoveRange(0, 20);

                if (CommonClasses.CommonVariable.Rights == "" || CommonClasses.CommonVariable.Rights == null)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    return;
                }
                for (int J = 0; J < GridSubMenu.RowDefinitions.Count; J++)
                {
                    if (ControlsCount != 3)   //how many Controls in Grid
                    {
                        for (int i = 0; i < GridSubMenu.ColumnDefinitions.Count; i++)
                        {
                            if (i == 0 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "OPERATION RATIO";
                              //  obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("OPERATION RATIO"))
                                {
                                     obj.Click += OperationRatio_Click;
                                }
                                else
                                {
                                      obj.Click -= OperationRatio_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 1 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "DEKIDAKA";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("DEKIDAKA REPORT"))
                                {
                                    obj.Click += DekidakaReport_Click;
                                }
                                else
                                {
                                     obj.Click -= DekidakaReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 2 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "CYCLE TIME FLUCTUATION";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("CYCLE TIME FLUCTUATION REPORT"))
                                {
                                    obj.Click += CycleTimeFluctuationReport_Click;
                                }
                                else
                                {
                                     obj.Click -= CycleTimeFluctuationReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 3 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "CYCLE TIME FLUCTUATION GRAPH";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("CYCLE TIME FLUCTUATION GRAPH"))
                                {
                                    obj.Click += CycleTimeFluctuationGraphReport_Click;
                                }
                                else
                                {
                                    obj.Click -= CycleTimeFluctuationGraphReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                            }
                            if (i == 4 && J == 0)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "KANBAN ACHIEVEMENT STATUS";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("KANBAN ACHIEVEMENT STATUS"))
                                {
                                    obj.Click += KanbanAchievementReport_Click;
                                }
                                else
                                {
                                    obj.Click -= KanbanAchievementReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                                
                            }
                            if (i == 0 && J == 1)
                            {
                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "CALL AND RESPONSE";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("CALL AND RESPONSE"))
                                {
                                    obj.Click += CAllandResponseReport_Click;
                                }
                                else
                                {
                                    obj.Click -= CAllandResponseReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }

                            }
                            if (i == 1 && J == 1)
                            {

                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "LOSS HOUR";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("MAJOR LOSS"))
                                {
                                    obj.Click += MajorLossReport_Click;
                                }
                                else
                                {
                                    obj.Click -= MajorLossReport_Click;
                                    obj.ToolTip = "Access Denied";
                                }
                                
                            }
                            if (i == 2 && J == 1)
                            {

                                Grid g = new Grid();
                                Button obj = new Button();
                                obj.Content = "KPI";
                                //obj.Height = 100;
                                //obj.Width = 230;
                                obj.Style = (Style)FindResource("SubMenuButton");
                                Grid.SetColumn(obj, i);
                                Grid.SetRow(obj, J);
                                GridSubMenu.Children.Add(obj);
                                ControlsCount = ControlsCount + 1;

                                if (CommonClasses.CommonVariable.Rights.Contains("KPI REPORT"))
                                {
                                    obj.Click += KPI_Reports_Click;
                                }
                                else
                                {
                                    obj.Click -= KPI_Reports_Click;
                                    obj.ToolTip = "Access Denied";
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }

        private void CycleTimeFluctuationGraphReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.CycleTimeFluctuationforGraph());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MajorLossReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.LossHour());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CAllandResponseReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.CallAndResponse());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void KanbanAchievementReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.KanbanAchievement());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CycleTimeFluctuationReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.CycleTimeFluctuation());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DekidakaReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.Dekidaka());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void OperationRatio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.OperationRatio());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void KPI_Reports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Report.Reports.KPI());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        #endregion


    }
}
