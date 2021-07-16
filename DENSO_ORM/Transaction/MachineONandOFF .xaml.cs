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
    /// Interaction logic for MachineONandOFF.xaml
    /// </summary>
    public partial class MachineONandOFF : Page
    {
        public MachineONandOFF()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Trns = new BUSINESS_LAYER.Transaction.Transaction();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int RefNo = 0;
        string ShiftTiming = "";
        string Mergeshifttime = "";
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_ON_AND_OFF", CommonClasses.CommonVariable.UserID);
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
                Transaction("LoadDetails");
                Transaction("GetMachineGroupname");
                Transaction("GetShiftName");
                Date.DisplayDateStart = System.DateTime.Today;
                cmbshift.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_ON_AND_OFF", CommonClasses.CommonVariable.UserID);
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
            if (cmbshift.SelectedIndex == -1 && cmbshift.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbshift.Focus();
                return false;
            }
            if (txtshift1.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtshift1.Focus();
                return false;
            }

            if (txtshift2.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtshift2.Focus();
                return false;
            }


            if (txtshift3.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtshift3.Focus();
                return false;
            }

            if (txtshift4.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER SHIFT TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtshift4.Focus();
                return false;
            }
            if (cmbPrbCode.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT PROBLEM CODE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbPrbCode.Focus();
                return false;
            }
            return true;
        }
        private void Merging()
        {
         Mergeshifttime = txtshift1.Text.PadLeft(2,'0') + ":" + txtshift2.Text.PadLeft(2, '0') + " " + txtshift3.Text.PadLeft(2, '0') + ":" + txtshift4.Text.PadLeft(2, '0');
            
        }
        private void Transaction(string Type)
        {
           
            if (Type == "Planned Off" || Type == "UnPlanned Off" || Type == "Cancel Off")
            {
                if (Type == "Planned Off" || Type == "UnPlanned Off")
                {
                    Merging();
                    ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                    ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                    ENTITY_LAYER.Masters.Masters.Date = Date.SelectedDate.ToString();
                    ENTITY_LAYER.Masters.Masters.ShiftName = cmbshift.Text;
                    ENTITY_LAYER.Masters.Masters.Shifttiming = Mergeshifttime;
                    ENTITY_LAYER.Masters.Masters.DefectCode = cmbPrbCode.Text;
                    ENTITY_LAYER.Masters.Masters.Remarks = txtRemarks.Text;
                }

                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;
                ENTITY_LAYER.Masters.Masters.Type = Type;

                CommonClasses.CommonVariable.Result = obj_Trns.BL_MachineOnandOffTransaction();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result == "Unplanned off")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result != "Cancelled")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    Clear();

                }
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Trns.BL_MachineOnandOffDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetMachineGroupname")
            {
            //    ENTITY_LAYER.Masters.Masters.Type = Type;
            //    DataTable dt = obj_Trns.BL_MachineOnandOffDetails().Tables[0];
            //    CommonClasses.CommonMethods.FillComboBox(cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            }
            if (Type == "GetMachinename")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //ENTITY_LAYER.Masters.Masters.MachineGroup = cmbmachinegrp.SelectedValue.ToString();
                //DataTable dt = obj_Mast.BL_ShiftMasterDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
            }
            if (Type == "GetShiftName")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                DataTable dt = obj_Trns.BL_MachineOnandOffDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbshift, dt, "ShiftName", "ShiftName");
            }
            if (Type == "GetShiftTiming")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.ShiftName = cmbshift.SelectedValue.ToString();
                DataTable dt = obj_Trns.BL_MachineOnandOffDetails().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtshift1.Text = dt.Rows[0][0].ToString().Split(' ')[0].Split(':')[0];
                    txtshift2.Text = dt.Rows[0][0].ToString().Split(' ')[0].Split(':')[1];
                    txtshift3.Text = dt.Rows[0][0].ToString().Split(' ')[1].Split(':')[0];
                    txtshift4.Text = dt.Rows[0][0].ToString().Split(' ')[1].Split(':')[1];
                }

                
               // CommonClasses.CommonMethods.FillComboBox(cmbmachinegrp, dt, "ShiftTiming", "ShiftTiming");
            }
            if (Type == "GetProblemCode")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                DataTable dt = obj_Trns.BL_MachineOnandOffDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbPrbCode, dt, "OperationCode", "OperationCode");
            }

        }
        private void Clear()
        {
           
            //cmbshift.Text = "";
            txtshift1.Text = "";
            txtshift2.Text = "";
            txtshift3.Text = "";
            txtshift4.Text = "";
            Date.SelectedDate = null;
            txtDatetime.Text = "";
            txtRemarks.Text = "";
            cmbshift.SelectedIndex = -1;
            cmbPrbCode.SelectedIndex = -1;
            Transaction("LoadDetails");
            cmbshift.Focus();
            RefNo = 0;
        }
        private void ClearAll()
        {

            txtlinegrp.Text ="";
            txtlinename.Text = "";
            txtshift1.Text = "";
            txtshift2.Text = "";
            txtshift3.Text = "";
            txtshift4.Text = "";
            Date.SelectedDate = null;
            txtDatetime.Text = "";
            txtRemarks.Text = "";
            cmbshift.SelectedIndex = -1;
            cmbPrbCode.SelectedIndex = -1;
            dvgMasterDeatils.ItemsSource = null;
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
                        Transaction("Planned Off");
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 0)
                {
                    if (ControlValidation())
                    {
                        Transaction("UnPlanned Off");
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
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
                            Transaction("Cancel Off");

                        }

                        if (CommonClasses.CommonVariable.Result == "Cancelled")
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtlinename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtlinename.Text != "")
                {
                    Transaction("GetProblemCode");
                    dvgMasterDeatils.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        //private void Cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinegrp.SelectedItem != null)
        //        {
        //            Transaction("GetMachinename");
        //            Transaction("GetProblemCode");
        //            dvgMasterDeatils.ItemsSource = null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }

        //}

        private void Cmbshift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                
            try
            {
                if (cmbshift.SelectedItem != null)
                {
                    Transaction("GetShiftTiming");
                }

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }

        private void Txtshift1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        //private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinename.SelectedItem != null)
        //        {
        //            Transaction("LoadDetails");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINEONANDOFF", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
    }
}