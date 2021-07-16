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
    /// Interaction logic for Kanban_Progress.xaml
    /// </summary>
    public partial class Kanban_Progress : Page
    {
        public Kanban_Progress()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public int RefNo = 0;
        string TotalPrd = "";
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
        private void TxtTotalQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtNoofKanban_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void TxtNoofAdvance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtNoofNormal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void TxtQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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
                txtNoofAdvance.Focus();
                Transaction("GetModuleName");
                Transaction("LoadDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                Transaction("GetTotalQty");

                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.NoOfadvancekanban = txtNoofAdvance.Text;
                ENTITY_LAYER.Masters.Masters.NoofNormalkanban = txtNoofNormal.Text;
                ENTITY_LAYER.Masters.Masters.FreqHrs = txtFrequency.Text;
                ENTITY_LAYER.Masters.Masters.ModelName = cmbModelNo.Text;
                ENTITY_LAYER.Masters.Masters.PouchNo = cmbPouchNo.Text;
                ENTITY_LAYER.Masters.Masters.Qty = txtQty.Text;
                ENTITY_LAYER.Masters.Masters.NoofKanban = txtNoofKanban.Text;
                ENTITY_LAYER.Masters.Masters.TotalQty = txtTotalQty.Text;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;

                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_KanbanProgressMasterTransaction();
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
                DataTable dt = obj_Mast.BL_KanbanProgressMasterDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
                if (dt.Rows.Count > 0)
                {
                    txtNoofAdvance.Text = dt.Rows[0]["NoofAdvance"].ToString();
                    txtNoofNormal.Text = dt.Rows[0]["NoofNormal"].ToString();
                    txtFrequency.Text = dt.Rows[0]["FrequencyHour"].ToString();
                }
            }
            if (Type == "GetTotalQty")
            {
                txtTotalQty.Text = (Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtNoofKanban.Text)).ToString();
            }
            if (Type == "GetModuleName")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Mast.BL_KanbanProgressMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbModelNo, dt, "ModelName", "ModelName");
            }

        }
        private void Clear()
        {
            //chkStatus.IsChecked = true;
            txtNoofAdvance.Text = "";
            txtNoofKanban.Text = "";
            txtNoofNormal.Text = "";
            txtQty.Text = "";
            txtTotalQty.Text = "";
            txtFrequency.Text = "";
            cmbModelNo.Text="";
            cmbPouchNo.Text = "";
            Transaction("LoadDetails");

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
            if (txtNoofAdvance.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF ADVANCE POUCH", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtNoofAdvance.Focus();
                return false;
            }

            if (txtNoofNormal.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF NORMAL POUCH", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtNoofNormal.Focus();
                return false;
            }
            if (txtFrequency.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER FREQUENCY HRS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtFrequency.Focus();
                return false;
            }
            if (cmbModelNo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MODEL NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbModelNo.Focus();
                return false;
            }
            if (cmbPouchNo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT POUCH NO. ", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbPouchNo.Focus();
                return false;
            }

            if (txtQty.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER QTY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtQty.Focus();
                return false;
            }
            if (txtNoofKanban.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF KANBAN", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtNoofKanban.Focus();
                return false;
            }
            if (txtTotalQty.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TOTAL QTY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTotalQty.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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
                    txtlinegrp.Text = dr["MachineGroup"].ToString();
                    txtlinename.Text = dr["MachineName"].ToString();
                    txtNoofAdvance.Text = dr["NoofAdvance"].ToString();
                    txtNoofNormal.Text = dr["NoofNormal"].ToString();
                    txtFrequency.Text = dr["FrequencyHour"].ToString();
                    cmbModelNo.Text = dr["ModelNo"].ToString();
                    cmbPouchNo.Text = dr["PouchNo"].ToString();
                    txtQty.Text = dr["Qty"].ToString();
                    txtNoofKanban.Text = dr["NoofKanban"].ToString();
                    txtTotalQty.Text = dr["TotalQty"].ToString();
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                }
                else
                {
                    txtNoofAdvance.Text = "";
                    txtNoofNormal.Text = "";
                    txtFrequency.Text = "";
                    cmbModelNo.Text = "";
                    cmbPouchNo.Text = "";
                    txtQty.Text = "";
                    txtNoofKanban.Text = "";
                    txtTotalQty.Text = "";
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }


        private void TxtNoofKanban_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
            {

            try
            {
                if (txtQty.Text != "")
                {
                    Transaction("GetTotalQty");

                    
                }
                else
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER QTY!!", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtQty.Focus();
                    return ;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtNoofAdvance_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {

                if (txtNoofAdvance.Text != "")
                {
                    if (Convert.ToInt32(txtNoofAdvance.Text) > 4)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER VALID ADVANCE POUCH!!", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        txtNoofAdvance.Text = "";
                        txtNoofNormal.Text = "";
                        txtNoofAdvance.Focus();
                        return;


                    }
                    else
                    {
                        if (Convert.ToInt32(txtNoofAdvance.Text) == 0)
                        {
                            txtNoofNormal.Text = "4";
                        }
                        if (Convert.ToInt32(txtNoofAdvance.Text) == 1)
                        {
                            txtNoofNormal.Text = "3";
                        }
                        if (Convert.ToInt32(txtNoofAdvance.Text) == 2)
                        {
                            txtNoofNormal.Text = "2";
                        }
                        if (Convert.ToInt32(txtNoofAdvance.Text) == 3)
                        {
                            txtNoofNormal.Text = "1";
                        }
                        if (Convert.ToInt32(txtNoofAdvance.Text) == 4)
                        {
                            txtNoofNormal.Text = "0";
                        }
                    }
                    txtFrequency.Focus();
                }
                txtFrequency.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtFrequency_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.DigitsOnly(e);
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROGRESS_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
