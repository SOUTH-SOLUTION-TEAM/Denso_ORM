using DENSO_ORM.CommonClasses;
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
    /// Interaction logic for MaintainceConfirmation.xaml
    /// </summary>
    public partial class MaintainceConfirmation : Page
    {
        public MaintainceConfirmation()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Trns = new BUSINESS_LAYER.Transaction.Transaction();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public int RefNo = 0;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                Transaction("LoadDetails");
                Transaction("GetMachineGroupname");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
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
            if (txtStation.Text == "")    
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER STATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtStation.Focus();
                return false;
            }
            if (txtReason.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER REMARKS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtReason.Focus();
                return false;
            }
            return true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.F) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.F))
            {
                btnSave_Click(sender, e);
            }
          
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                btnExit_Click(sender, e);
            }
           
        }

               
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count == 1)
                    {
                        if (ControlValidation())
                        {
                            Transaction("Save");
                        }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR MAINTENANCE CALL", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count == 1)
                    {
                        if (ControlValidation())
                            Transaction("Confirmation");
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR CONFIRMATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            
            if (Type == "Save" || Type == "Confirmation" || Type == "Close")
            {

                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineOperation = txtStation.Text;
                ENTITY_LAYER.Masters.Masters.Remarks = txtReason.Text;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;
                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_MaintananceTconformationransaction();
                if (CommonClasses.CommonVariable.Result.StartsWith("Saved"))
                {
                    Clear();
                    if (Type == "Save")
                    {
                        string[] Data = CommonClasses.CommonVariable.Result.Split('|');
                        if(Data.Length>0)
                        {
                            PLC_Connectivity obj_Client = new PLC_Connectivity();
                            obj_Client.IP = Data[1].ToString();
                            obj_Client.port = Convert.ToInt32(Data[2]);
                            obj_Client.Connect();
                            Thread.Sleep(100);
                            obj_Client.Write(true, Convert.ToInt32(Data[3]));
                          //  obj_Client.Write(true, Convert.ToInt32(Data[3]));
                           // obj_Client.Write(true, Convert.ToInt32(Data[3]));

                            obj_Client.Dispose();
                        }
                       // string Result = CommonClasses.CommonMethods.objWS.APKDetails("MAINTENANCE", CommonClasses.CommonVariable.MachineGroup, CommonClasses.CommonVariable.MachineName, "", "", "", txtStation.Text, "MaintenanceConfimatiom");
                        CommonClasses.CommonMethods.MessageBoxShow("CALL GENERATED SUCCESSFULLY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                }
                else if (CommonClasses.CommonVariable.Result == "Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result == "Duplicate")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Duplicate, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
               
              
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Mast.BL_MaintananceTconformationDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            //if (Type == "GetOperation")
            //{
            //    ENTITY_LAYER.Masters.Masters.Type = Type;
            //    ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
            //    ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
            //    DataTable dt = obj_Mast.BL_MaintananceTconformationDetails().Tables[0];
            //    CommonClasses.CommonMethods.FillComboBox(cmbStation, dt, "Operation", "Operation");
            //}
          
        }

        private void Clear()
        {
            Transaction("LoadDetails");
            txtStation.Text = "";
            txtReason.Text = "";
            txtReason.Focus();
            RefNo = 0;
        }
        private void ClearAll()
        {
            txtStation.Text = "";
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            txtReason.Text = "";
            dvgMasterDeatils.ItemsSource = null;
            RefNo = 0;
        }
        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[0];
                    txtlinegrp.Text = dr["MachineGroupName"].ToString();
                    txtlinename.Text = dr["MachineName"].ToString();
                    txtStation.Text = dr["Operation"].ToString();
                    ///txtReason.Text= dr["Remarks"].ToString();
                    RefNo = Convert.ToInt32(dr["RefNo"].ToString());
                    txtReason.Focus();
                }
                else
                {
                    txtStation.Text = "";
                    txtReason.Text = "";
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnJobClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count == 1)
                    {
                        if (ControlValidation())
                            Transaction("Close");
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR CLOSE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTAINANCE_CONFIRMATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
