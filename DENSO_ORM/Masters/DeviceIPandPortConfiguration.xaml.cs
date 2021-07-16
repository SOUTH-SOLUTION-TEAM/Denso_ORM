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

namespace DENSO_ORM.Masters
{
    /// <summary>
    /// Interaction logic for DeviceIPandPortConfiguration.xaml
    /// </summary>
    public partial class DeviceIPandPortConfiguration : Page
    {
        public DeviceIPandPortConfiguration()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

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
            txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IPANDPORT_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                //Transaction("LoadDetails");
                Transaction("GetMachineGroupname");
                txtIP.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IPANDPORT_CONFIGURATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                btnSave_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.U) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.U))
            {
                btnUpdate_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                btnExit_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.D))
            {
                btnDelete_Click(sender, e);
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {

                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.MachineOperation = cmboperation.Text;
                ENTITY_LAYER.Masters.Masters.ProcessType = cmbProcesstype.Text;
                ENTITY_LAYER.Masters.Masters.MachineType = cmbMachineType.Text;
                ENTITY_LAYER.Masters.Masters.IP = txtIP.Text;
                ENTITY_LAYER.Masters.Masters.Port = txtPort.Text;
                ENTITY_LAYER.Masters.Masters.Plcaddress = txtPlcaddress.Text;
                ENTITY_LAYER.Masters.Masters.PLCProcess = cmbAndonType.Text;
                if (chkStatus.IsChecked == true)
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.Active;
                else
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.InActive;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;

                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_IPPortConfigurationTransaction();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result == "Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result == "Duplicate")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Duplicate, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                else if (CommonClasses.CommonVariable.Result != "Deleted")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.MachineOperation = cmboperation.SelectedValue.ToString();
                DataTable dt = obj_Mast.BL_IPPortConfigurationDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetMachineGroupname")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //DataTable dt = obj_Mast.BL_IPPortConfigurationDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            }
            if (Type == "GetMachinename")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //ENTITY_LAYER.Masters.Masters.MachineGroup = cmbmachinegrp.SelectedValue.ToString();
                //DataTable dt = obj_Mast.BL_IPPortConfigurationDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
            }
            if (Type == "GetOperation")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Mast.BL_IPPortConfigurationDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmboperation, dt, "Operation", "Operation");
            }

        }
        private void ClearAll()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            cmboperation.Text = "";
            cmbProcesstype.Text = "";
            cmbMachineType.Text = "";
            txtIP.Text = "";
            txtPort.Text = "";
            txtPlcaddress.Text = "";
            cmbAndonType.Text = "";
            txtlinegrp.Focus();
            RefNo = 0;
        }
        private void Clear()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            cmbProcesstype.Text = "";
            cmbMachineType.Text = "";
            cmbAndonType.Text = "";
            txtIP.Text = "";
            txtPort.Text = "";
            txtPlcaddress.Text = "";
            txtIP.Focus();
            RefNo = 0;
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
            if (cmboperation.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT OPERATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmboperation.Focus();
                return false;
            }
          
            if (txtIP.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER IP ADDRESS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtIP.Focus();
                return false;
            }
            if (txtPort.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PORT", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPort.Focus();
                return false;
            }
            if (txtPlcaddress.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PLC ADDRESS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPlcaddress.Focus();
                return false;
            }
            if (cmbProcesstype.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT PROCESS TYPE ", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbProcesstype.Focus();
                return false;
            }

            if (cmbMachineType.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT PLC TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbMachineType.Focus();
                return false;
            }
            if (cmbAndonType.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT PLC process", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbAndonType.Focus();
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 0)
                {
                    if (ControlValidation())
                    {
                        Transaction("Save");
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
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
                            Transaction("Update");
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                    if (CommonClasses.CommonVariable.Result == "YES")
                    {
                        for (int i = 0; i < dvgMasterDeatils.SelectedItems.Count; i++)
                        {
                            DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Delete");

                        }

                        if (CommonClasses.CommonVariable.Result == "Deleted")
                        {
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                            Clear();
                        }
                        else
                        {
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        //private void cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinegrp.SelectedValue != null)
        //        {
        //            Transaction("GetMachinename");
        //            dvgMasterDeatils.ItemsSource = null;
        //            cmboperation.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[0];
                    txtlinegrp.Text = dr["MachineGroupName"].ToString();
                    txtlinename.Text = dr["MachineName"].ToString();
                    cmboperation.Text = dr["Operation"].ToString();
                    cmbProcesstype.Text = dr["ProcessType"].ToString();
                    cmbMachineType.Text = dr["MachineType"].ToString();
                    txtIP.Text = dr["IPAddress"].ToString();
                    txtPort.Text= dr["Port"].ToString();
                    txtPlcaddress.Text = dr["PLCAddress"].ToString();
                    cmbAndonType.Text= dr["andonType"].ToString();
                    if (CommonClasses.CommonVariable.Active == dr["Status"].ToString())
                        chkStatus.IsChecked = true;
                    else
                        chkStatus.IsChecked = false;
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                }
                else
                {
                    cmbProcesstype.Text = "";
                    cmbMachineType.Text = "";
                    txtIP.Text = "";
                    txtPort.Text = "";
                    txtPlcaddress.Text = "";
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (txtlinename.Text!= "")
                {
                    Transaction("GetOperation");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Cmboperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmboperation.SelectedValue != null)
                {
                    Transaction("LoadDetails");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPlcaddress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbMachineType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMachineType.SelectedIndex == 1)
                {
                    cmbAndonType.Text = "MACHINE";
                    cmbAndonType.IsEnabled = false;
                }
                else
                {
                    cmbAndonType.Text = "";
                    cmbAndonType.IsEnabled = true;

                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtlinename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtlinename.Text != "")
                {
                    Transaction("GetOperation");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DEVICEIPANDPORT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }
    }
}

