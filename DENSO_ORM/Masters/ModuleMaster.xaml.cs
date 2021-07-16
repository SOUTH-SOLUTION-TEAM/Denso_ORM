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
    /// Interaction logic for ModuleMaster.xaml
    /// </summary>
    public partial class ModuleMaster : Page
    {
        public ModuleMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public int RefNo = 0;
        string TotalPrd="";
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
        private bool ControlValidation()
        {
            if (txtlinegrp.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtlinegrp.Focus();
                return false;
            }
            if (txtlinename.Text=="")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtlinename.Focus();
                return false;
            }
            if (txtModulename.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER MODEL NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtModulename.Focus();
                return false;
            }

            if (txtcycletime.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CYCLE TIME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtcycletime.Focus();
                return false;
            }
            
            if (txtPulse.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF PULSE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPulse.Focus();
                return false;
            }
            if (txtPiece.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PIECES", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPiece.Focus();
                return false;
            }
            if (txtPartNo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PART-NO", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPiece.Focus();
                return false;
            }
            if (txtPresentTime.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PRESENT TIME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPresentTime.Focus();
                return false;
            }
            if (txtBaseTime.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BASE TIME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtBaseTime.Focus();
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                Transaction("GetMachineGroupname");
                txtModulename.Focus();
                Transaction("LoadDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.ModelName = txtModulename.Text;
                ENTITY_LAYER.Masters.Masters.Cycletime = Convert.ToDouble(txtcycletime.Text);
                ENTITY_LAYER.Masters.Masters.Pulse = Convert.ToInt32(txtPulse.Text);
                ENTITY_LAYER.Masters.Masters.Piece = Convert.ToDouble(txtPiece.Text);
                ENTITY_LAYER.Masters.Masters.TotPices = TotalPrd;
                ENTITY_LAYER.Masters.Masters.PartNo = txtPartNo.Text;
                ENTITY_LAYER.Masters.Masters.PresentStandardTime = txtPresentTime.Text; ;
                ENTITY_LAYER.Masters.Masters.BaseStandardTime = txtBaseTime.Text;
                if (chkStatus.IsChecked == true)
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.Active;
                else
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.InActive;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;

                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_ModulemasterTransaction();
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
                DataTable dt = obj_Mast.BL_ModuleMasterDetails().Tables[0];
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

        private void Clear()
        {
            chkStatus.IsChecked =true;
            Transaction("LoadDetails");
            //  Transaction("GetMachineGroupname");
            //txtlinegrp.Text = "";
           // txtlinename.Text = "";
            txtcycletime.Text = "";
            txtModulename.Text = "";
            txtPiece.Text = "";
            txtPulse.Text = "";
            txtPartNo.Text = "";
            txtPresentTime.Text = "";
            txtBaseTime.Text = "";
            txtlinegrp.Focus();
            RefNo = 0;
        }
        private void ClearAll()
        {
            chkStatus.IsChecked = true;
            dvgMasterDeatils.ItemsSource = null;
            //Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            //txtlinegrp.Text = "";
            //txtlinename.Text = "";
            txtcycletime.Text = "";
            txtModulename.Text = "";
            txtPiece.Text = "";
            txtPulse.Text = "";
            txtPartNo.Text = "";
            txtPresentTime.Text = "";
            txtBaseTime.Text = "";
            txtlinegrp.Focus();
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                    txtlinegrp.Text = dr["MachineGrName"].ToString();
                    txtlinename.Text = dr["MachineName"].ToString();
                    txtModulename.Text= dr["ModelName"].ToString();
                    txtcycletime.Text = dr["CycleTime"].ToString();
                    txtPulse.Text = dr["puls"].ToString();
                    txtPiece.Text = dr["Piece"].ToString();
                    txtPartNo.Text = dr["PartNo"].ToString();
                    txtPresentTime.Text = dr["PresentStandardtTime"].ToString();
                    txtBaseTime.Text = dr["BaseStandardTime"].ToString();
                    RefNo = Convert.ToInt32( dr["RefNo"].ToString());
                    if (CommonClasses.CommonVariable.Active == dr["Status"].ToString())
                        chkStatus.IsChecked = true;
                    else
                        chkStatus.IsChecked = false;
                }
                else
                {
                   // txtlinegrp.Text = "";
                   // txtlinename.Text = "";
                    txtcycletime.Text = "";
                    txtModulename.Text = "";
                    txtPiece.Text = "";
                    txtPulse.Text = "";
                    txtPartNo.Text = "";
                    txtPresentTime.Text = "";
                    txtBaseTime.Text = "";
                    chkStatus.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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


        //private void Cmbmachinegrp_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Cmbmachinegrp.SelectedIndex > -1)
        //        {
        //            Transaction("GetMachinename");
        //            dvgMasterDeatils.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

       
        private void Txtcycletime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.DigitsOnly(e);
                
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txttime2stop_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPiece_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.DigitsOnly(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtcycletime_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if (txtcycletime.Text != "")
                {
                    //txtPiece.Text = Math.Round((Convert.ToDouble(3600) / Convert.ToDouble(txtcycletime.Text))).ToString();
                    TotalPrd = Math.Round((Convert.ToDouble(86400) / Convert.ToDouble(txtcycletime.Text))).ToString();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void TxtPresentTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.DigitsOnly(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtBaseTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.DigitsOnly(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MODULE_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
