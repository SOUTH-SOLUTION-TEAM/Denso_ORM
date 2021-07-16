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
    /// Interaction logic for SMSandVoiceCallConfiguration.xaml
    /// </summary>
    public partial class SMSandVoiceCallConfiguration : Page
    {
        public SMSandVoiceCallConfiguration()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        int RefNo = 0;
        #endregion

        private void ShowDateTime()
        {
            //System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
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
                cmbrectype.Focus();
                //cmbGroupID.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
            if (cmbrectype.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT  RECIPIENT TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbrectype.Focus();
                return false;
            }

            if (txtrecno.Text.Length < 10)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER VALID RECIPIENT NO", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtrecno.Focus();
                return false;
            }

            if (txtrecname.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER RECIPIENT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtrecname.Focus();
                return false;
            }
            if (txtrecmsg.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER MESSAGE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtrecmsg.Focus();
                return false;
            }
            
            return true;
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {

                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.RecipientType = cmbrectype.Text;
                ENTITY_LAYER.Masters.Masters.RecipientName = txtrecname.Text;
                ENTITY_LAYER.Masters.Masters.Recipientno = txtrecno.Text;
                ENTITY_LAYER.Masters.Masters.Recipientmsg = txtrecmsg.Text;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;
                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_SMSCALLTransaction();
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
                DataTable dt = obj_Mast.BL_SMSCallDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetMachineGroupname")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(Cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            }
            if (Type == "GetMachinename")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //ENTITY_LAYER.Masters.Masters.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
                //DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
            }

        }
        private void ClearAll()
        {
            Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            cmbrectype.Text = "";
            txtrecname.Text = "";
            txtrecno.Text = "";
            txtrecmsg.Text = "";
            dvgMasterDeatils.ItemsSource = null;
            txtlinegrp.Focus();
            RefNo = 0;
        }
        private void Clear()
        {
            Transaction("LoadDetails");
            cmbrectype.Text = "";
            txtrecname.Text = "";
            txtrecno.Text = "";
            txtrecmsg.Text = "";
            cmbrectype.Focus();
            RefNo = 0;
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                            RefNo = Convert.ToInt32(dr["RefNo"]);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
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
                    cmbrectype.Text = dr["RecipientType"].ToString();
                    txtrecno.Text = dr["RecipientNo"].ToString();
                    txtrecname.Text = dr["RecipientName"].ToString();
                    txtrecmsg.Text = dr["RecipientMsg"].ToString();
                    RefNo = Convert.ToInt32(dr["RefNo"].ToString());
                    
                }
                else
                {
                   // Cmbmachinegrp.SelectedIndex = -1;
                    //cmbmachinename.SelectedIndex = -1;
                    cmbrectype.SelectedIndex = -1;
                    txtrecname.Text = "";
                    txtrecno.Text = "";
                    txtrecmsg.Text = "";
                    //Cmbmachinegrp.Focus();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        //private void Cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Cmbmachinegrp.SelectedValue != null)
        //        {
        //            Transaction("GetMachinename");
        //            dvgMasterDeatils.ItemsSource = null;
        //         //   Transaction("LoadDetails");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void Txtrecno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

      
        //private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinename.SelectedValue != null)
        //        {
        //            Transaction("LoadDetails");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SMSANDCALL_CONFIGURATION", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
    }
}

