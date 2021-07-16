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
    /// Interaction logic for ShiftMaster.xaml
    /// </summary>
    public partial class ShiftMaster : Page
    {
        public ShiftMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        int RefNo = 0;
        string shifttime = "", Break1 = "", Break2 = "", Break3 = "", Break4 = "", Break5 = "", Timepoints = "";
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
                cmbShiftName.Focus();
                //cmbGroupID.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                Merging();
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.Shifttiming = shifttime;
                ENTITY_LAYER.Masters.Masters.Break1 = Break1;
                ENTITY_LAYER.Masters.Masters.Break2 = Break2;
                ENTITY_LAYER.Masters.Masters.Break3 = Break3;
                ENTITY_LAYER.Masters.Masters.Break4 = Break4;
                ENTITY_LAYER.Masters.Masters.Break5 = Break5;
                ENTITY_LAYER.Masters.Masters.Timepoints = Timepoints;
                ENTITY_LAYER.Masters.Masters.TotalWorkingHrs = txtttlworkinghrs.Text;

                ENTITY_LAYER.Masters.Masters.ShiftName = cmbShiftName.Text;
                if (chkStatus.IsChecked == true)
                {
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.Active;
                }
                else
                {
                    ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.InActive;
                }
                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Mast.BL_ShiftMasterTransaction();
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
                DataTable dt = obj_Mast.BL_ShiftMasterDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetMachineGroupname")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //DataTable dt = obj_Mast.BL_ShiftMasterDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            }
            if (Type == "GetMachinename")
            {
                //ENTITY_LAYER.Masters.Masters.Type = Type;
                //ENTITY_LAYER.Masters.Masters.MachineGroup = cmbmachinegrp.SelectedValue.ToString();
                //DataTable dt = obj_Mast.BL_ShiftMasterDetails().Tables[0];
                //CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
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
            if (txtbreak1.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK1 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak1.Focus();
                return false;
            }
            if (txtbreak12.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK1 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak12.Focus();
                return false;
            }
            if (txtbreak13.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK1 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak13.Focus();
                return false;
            }
            if (txtbreak14.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK1 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak14.Focus();
                return false;
            }
            if (txtbreak2.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK2 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak2.Focus();
                return false;
            }
            if (txtbreak21.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK2 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak21.Focus();
                return false;
            }
            if (txtbreak23.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK2 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak23.Focus();
                return false;
            }
            if (txtbreak24.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK2 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak24.Focus();
                return false;
            }
            if (txtbreak3.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK3 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak3.Focus();
                return false;
            }
            if (txtbreak31.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK3 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak31.Focus();
                return false;
            }
            if (txtbreak32.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK3 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak32.Focus();
                return false;
            }
            if (txtbreak33.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK3 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak33.Focus();
                return false;
            }
            if (txtbreak4.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK4 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak4.Focus();
                return false;
            }
            if (txtbreak41.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK4 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak41.Focus();
                return false;
            }
            if (txtbreak42.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK4 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak42.Focus();
                return false;
            }
            if (txtbreak43.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK4 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtbreak43.Focus();
                return false;
            }
            if (cmbShiftName.Text != "Third Shift")
            {
                if (txtbreak5.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK5 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtbreak5.Focus();
                    return false;
                }
                if (txtbreak51.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK5 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtbreak51.Focus();
                    return false;
                }
                if (txtbreak52.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK5 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtbreak52.Focus();
                    return false;
                }
                if (txtbreak53.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER BREAK5 TIMING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtbreak53.Focus();
                    return false;
                }
            }
            if(txtTimePintHH1.Text=="")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 1", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH1.Focus();
                return false;
            }
            if (txtTimePintMM1.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 1", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM1.Focus();
                return false;
            }
            if (txtTimePintHH2.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 2", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH2.Focus();
                return false;
            }
            if (txtTimePintMM2.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 2", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM2.Focus();
                return false;
            }
            if (txtTimePintHH3.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 3", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH3.Focus();
                return false;
            }
            if (txtTimePintMM3.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 3", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM3.Focus();
                return false;
            }
            if (txtTimePintHH4.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 4", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH4.Focus();
                return false;
            }
            if (txtTimePintMM4.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 4", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM4.Focus();
                return false;
            }
            if (txtTimePintHH5.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 5", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH5.Focus();
                return false;
            }
            if (txtTimePintMM5.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 5", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM5.Focus();
                return false;
            }
            if (txtTimePintHH6.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 6", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintHH6.Focus();
                return false;
            }
            if (txtTimePintMM6.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 6", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtTimePintMM6.Focus();
                return false;
            }
            if (cmbShiftName.Text != "Third Shift")
            {
                if (txtTimePintHH7.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 7", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtTimePintHH7.Focus();
                    return false;
                }
                if (txtTimePintMM7.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TIMI POINT 7", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtTimePintMM7.Focus();
                    return false;
                }
               
            }
            return true;
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
        private void Clear()
        {

            Transaction("LoadDetails");
            cmbShiftName.Text = "";
            txtbreak1.Text = "";
            txtbreak12.Text = ""; txtbreak13.Text = ""; txtbreak14.Text = "";
            txtbreak2.Text = ""; txtbreak21.Text = ""; txtbreak23.Text = ""; txtbreak24.Text = "";
            txtbreak3.Text = ""; txtbreak31.Text = ""; txtbreak32.Text = ""; txtbreak33.Text = "";
            txtbreak4.Text = ""; txtbreak41.Text = ""; txtbreak42.Text = ""; txtbreak43.Text = "";
            txtbreak5.Text = ""; txtbreak51.Text = ""; txtbreak52.Text = ""; txtbreak53.Text = "";
            txtshift1.Text = ""; txtshift2.Text = ""; txtshift3.Text = ""; txtshift4.Text = "";
            txtTimePintHH1.Text = ""; txtTimePintMM1.Text = ""; txtTimePintHH2.Text = "";
            txtTimePintMM2.Text = ""; txtTimePintHH3.Text = ""; txtTimePintMM3.Text = ""; txtTimePintHH4.Text = "";
            txtTimePintMM4.Text = ""; txtTimePintHH5.Text = ""; txtTimePintMM6.Text = ""; txtTimePintHH6.Text = "";
            txtTimePintMM7.Text = ""; txtTimePintHH7.Text = ""; txtTimePintMM8.Text = ""; txtTimePintHH8.Text = "";
            txtTimePintMM5.Text = "";
            txtttlworkinghrs.Text = "";
            cmbShiftName.Focus();
            RefNo = 0;
        }
        private void ClearAll()
        {

            Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            cmbShiftName.Text = "";
            txtbreak1.Text = "";
            txtbreak12.Text = ""; txtbreak13.Text = ""; txtbreak14.Text = "";
            txtbreak2.Text = ""; txtbreak21.Text = ""; txtbreak23.Text = ""; txtbreak24.Text = "";
            txtbreak3.Text = ""; txtbreak31.Text = ""; txtbreak32.Text = ""; txtbreak33.Text = "";
            txtbreak4.Text = ""; txtbreak41.Text = ""; txtbreak42.Text = ""; txtbreak43.Text = "";
            txtbreak5.Text = ""; txtbreak51.Text = ""; txtbreak52.Text = ""; txtbreak53.Text = "";
            txtshift1.Text = ""; txtshift2.Text = ""; txtshift3.Text = ""; txtshift4.Text = "";
            txtTimePintHH1.Text = ""; txtTimePintMM1.Text = ""; txtTimePintHH2.Text = "";
            txtTimePintMM2.Text = ""; txtTimePintHH3.Text = ""; txtTimePintMM3.Text = ""; txtTimePintHH4.Text = "";
            txtTimePintMM4.Text = ""; txtTimePintHH5.Text = ""; txtTimePintMM6.Text = ""; txtTimePintHH6.Text = "";
            txtTimePintMM5.Text = "";
            txtTimePintMM7.Text = ""; txtTimePintHH7.Text = ""; txtTimePintMM8.Text = ""; txtTimePintHH8.Text = "";
            txtttlworkinghrs.Text = "";
            dvgMasterDeatils.ItemsSource = null;
            txtlinegrp.Focus();
            RefNo = 0;
        }
        //TO COMBINE TEXTBOXES
        private void Merging()
        {
            shifttime = ""; Break1 = ""; Break2 = ""; Break3 = ""; Break4 = ""; Break5 = ""; Timepoints = "";
            shifttime = txtshift1.Text.PadLeft(2, '0') + ":" + txtshift2.Text.PadLeft(2, '0') + " " + txtshift3.Text.PadLeft(2, '0') + ":" + txtshift4.Text.PadLeft(2, '0');

            if (txtbreak1.Text != "" && txtbreak12.Text != "" && txtbreak13.Text != "" && txtbreak14.Text != "")
                Break1 = txtbreak1.Text.PadLeft(2,'0') + ":" + txtbreak12.Text.PadLeft(2, '0') + " " + txtbreak13.Text.PadLeft(2, '0') + ":" + txtbreak14.Text.PadLeft(2, '0');
            if (txtbreak2.Text != "" && txtbreak21.Text != "" && txtbreak23.Text != "" && txtbreak24.Text != "")
                Break2 = txtbreak2.Text.PadLeft(2, '0') + ":" + txtbreak21.Text.PadLeft(2, '0') + " " + txtbreak23.Text.PadLeft(2, '0') + ":" + txtbreak24.Text.PadLeft(2, '0');
            if (txtbreak3.Text != "" && txtbreak31.Text != "" && txtbreak32.Text != "" && txtbreak33.Text != "")
                Break3 = txtbreak3.Text.PadLeft(2, '0') + ":" + txtbreak31.Text.PadLeft(2, '0') + " " + txtbreak32.Text.PadLeft(2, '0') + ":" + txtbreak33.Text.PadLeft(2, '0');
            if (txtbreak4.Text != "" && txtbreak41.Text != "" && txtbreak42.Text != "" && txtbreak43.Text != "")
                Break4 = txtbreak4.Text.PadLeft(2, '0') + ":" + txtbreak41.Text.PadLeft(2, '0') + " " + txtbreak42.Text.PadLeft(2, '0') + ":" + txtbreak43.Text.PadLeft(2, '0');
            if (txtbreak5.Text != "" && txtbreak51.Text != "" && txtbreak52.Text != "" && txtbreak53.Text != "")
                Break5 = txtbreak5.Text.PadLeft(2, '0') + ":" + txtbreak51.Text.PadLeft(2, '0') + " " + txtbreak52.Text.PadLeft(2, '0') + ":" + txtbreak53.Text.PadLeft(2, '0');


            if (txtTimePintHH1.Text != "" && txtTimePintMM1.Text != "")
                Timepoints = txtTimePintHH1.Text.PadLeft(2, '0') + ":" + txtTimePintMM1.Text.PadLeft(2, '0');
            if (txtTimePintHH2.Text != "" && txtTimePintMM2.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH2.Text.PadLeft(2, '0') + ":" + txtTimePintMM2.Text.PadLeft(2, '0');
            if (txtTimePintHH3.Text != "" && txtTimePintMM3.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH3.Text.PadLeft(2, '0') + ":" + txtTimePintMM3.Text.PadLeft(2, '0');
            if (txtTimePintHH4.Text != "" && txtTimePintMM4.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH4.Text.PadLeft(2, '0') + ":" + txtTimePintMM4.Text.PadLeft(2, '0');
            if (txtTimePintHH5.Text != "" && txtTimePintMM5.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH5.Text.PadLeft(2, '0') + ":" + txtTimePintMM5.Text.PadLeft(2, '0');
            if (txtTimePintHH6.Text != "" && txtTimePintMM6.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH6.Text.PadLeft(2, '0') + ":" + txtTimePintMM6.Text.PadLeft(2, '0');
            if (txtTimePintHH7.Text != "" && txtTimePintMM7.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH7.Text.PadLeft(2, '0') + ":" + txtTimePintMM7.Text.PadLeft(2, '0');
            if (txtTimePintHH8.Text != "" && txtTimePintMM8.Text != "")
                Timepoints = Timepoints + "," + txtTimePintHH8.Text.PadLeft(2, '0') + ":" + txtTimePintMM8.Text.PadLeft(2, '0');

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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        //private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if(cmbmachinename.SelectedIndex > -1)
        //        {
        //            Transaction("LoadDetails");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void Txtshift1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
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
                            RefNo = Convert.ToInt32(dr["REFNO"]);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    txtshift1.Text = "";
                    txtshift2.Text = "";
                    txtshift3.Text = "";
                    txtshift4.Text = "";
                    cmbShiftName.Text = "";
                    txtbreak1.Text = "";
                    txtbreak12.Text = ""; txtbreak13.Text = ""; txtbreak14.Text = "";
                    txtbreak2.Text = ""; txtbreak21.Text = ""; txtbreak23.Text = ""; txtbreak24.Text = "";
                    txtbreak3.Text = ""; txtbreak31.Text = ""; txtbreak32.Text = ""; txtbreak33.Text = "";
                    txtbreak4.Text = ""; txtbreak41.Text = ""; txtbreak42.Text = ""; txtbreak43.Text = "";
                    txtbreak5.Text = ""; txtbreak51.Text = ""; txtbreak52.Text = ""; txtbreak53.Text = "";
                    txtshift1.Text = ""; txtshift2.Text = ""; txtshift3.Text = ""; txtshift4.Text = "";
                    txtTimePintHH1.Text = ""; txtTimePintMM1.Text = ""; txtTimePintHH2.Text = "";
                    txtTimePintMM2.Text = ""; txtTimePintHH3.Text = ""; txtTimePintMM3.Text = ""; txtTimePintHH4.Text = "";
                    txtTimePintMM4.Text = ""; txtTimePintHH5.Text = ""; txtTimePintMM6.Text = ""; txtTimePintHH6.Text = "";
                    txtTimePintMM7.Text = ""; txtTimePintHH7.Text = ""; txtTimePintMM8.Text = ""; txtTimePintHH8.Text = "";
                    txtTimePintMM5.Text = "";
                    txtttlworkinghrs.Text = "";
                    DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[0];
                    txtlinegrp.Text = dr["MachineGrName"].ToString();
                    txtlinename.Text = dr["MachineName"].ToString();
                    cmbShiftName.Text = dr["ShiftName"].ToString();
                    RefNo = Convert.ToInt32(dr["REFNO"].ToString());
                    txtttlworkinghrs.Text = dr["TotalWorkingHour"].ToString();
                    if (dr["ShiftTiming"].ToString() != "")
                    {
                        txtshift1.Text = dr["ShiftTiming"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtshift2.Text = dr["ShiftTiming"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtshift3.Text = dr["ShiftTiming"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtshift4.Text = dr["ShiftTiming"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["Break1"].ToString() != "")
                    {
                        txtbreak1.Text = dr["Break1"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtbreak12.Text = dr["Break1"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtbreak13.Text = dr["Break1"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtbreak14.Text = dr["Break1"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["Break2"].ToString() != "")
                    {
                        txtbreak2.Text = dr["Break2"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtbreak21.Text = dr["Break2"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtbreak23.Text = dr["Break2"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtbreak24.Text = dr["Break2"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["Break3"].ToString() != "")
                    {
                        txtbreak3.Text = dr["Break3"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtbreak31.Text = dr["Break3"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtbreak32.Text = dr["Break3"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtbreak33.Text = dr["Break3"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["Break4"].ToString() != "")
                    {
                        txtbreak4.Text = dr["Break4"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtbreak41.Text = dr["Break4"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtbreak42.Text = dr["Break4"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtbreak43.Text = dr["Break4"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["Break5"].ToString() != "")
                    {
                        txtbreak5.Text = dr["Break5"].ToString().Split(' ')[0].Split(':')[0].ToString();
                        txtbreak51.Text = dr["Break5"].ToString().Split(' ')[0].Split(':')[1].ToString();
                        txtbreak52.Text = dr["Break5"].ToString().Split(' ')[1].Split(':')[0].ToString();
                        txtbreak53.Text = dr["Break5"].ToString().Split(' ')[1].Split(':')[1].ToString();
                    }
                    if (dr["TimeWorking"].ToString() != "")
                    {
                        if(dr["TimeWorking"].ToString().Split(',').Length>=1)
                        {
                            txtTimePintHH1.Text = dr["TimeWorking"].ToString().Split(',')[0].Split(':')[0].ToString();
                            txtTimePintMM1.Text = dr["TimeWorking"].ToString().Split(',')[0].Split(':')[1].ToString();
                        }

                        if (dr["TimeWorking"].ToString().Split(',').Length >= 2)
                        {
                            txtTimePintHH2.Text = dr["TimeWorking"].ToString().Split(',')[1].Split(':')[0].ToString();
                            txtTimePintMM2.Text = dr["TimeWorking"].ToString().Split(',')[1].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 3)
                        {
                            txtTimePintHH3.Text = dr["TimeWorking"].ToString().Split(',')[2].Split(':')[0].ToString();
                            txtTimePintMM3.Text = dr["TimeWorking"].ToString().Split(',')[2].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 4)
                        {
                            txtTimePintHH4.Text = dr["TimeWorking"].ToString().Split(',')[3].Split(':')[0].ToString();
                            txtTimePintMM4.Text = dr["TimeWorking"].ToString().Split(',')[3].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 5)
                        {
                            txtTimePintHH5.Text = dr["TimeWorking"].ToString().Split(',')[4].Split(':')[0].ToString();
                            txtTimePintMM5.Text = dr["TimeWorking"].ToString().Split(',')[4].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 6)
                        {
                            txtTimePintHH6.Text = dr["TimeWorking"].ToString().Split(',')[5].Split(':')[0].ToString();
                            txtTimePintMM6.Text = dr["TimeWorking"].ToString().Split(',')[5].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 7)
                        {
                            txtTimePintHH7.Text = dr["TimeWorking"].ToString().Split(',')[6].Split(':')[0].ToString();
                            txtTimePintMM7.Text = dr["TimeWorking"].ToString().Split(',')[6].Split(':')[1].ToString();
                        }
                        if (dr["TimeWorking"].ToString().Split(',').Length >= 8)
                        {
                            txtTimePintHH8.Text = dr["TimeWorking"].ToString().Split(',')[7].Split(':')[0].ToString();
                            txtTimePintMM8.Text = dr["TimeWorking"].ToString().Split(',')[7].Split(':')[1].ToString();
                        }
                    }

                  //  Timepoints = dr["TimeWorking"].ToString();
               
                    //SplitTimePoints();
                    if (CommonClasses.CommonVariable.Active == dr["Status"].ToString())
                        chkStatus.IsChecked = true;
                    else
                        chkStatus.IsChecked = false;

                }
                else
                {

                    txtshift1.Text = "";
                    txtshift2.Text = "";
                    txtshift3.Text = "";
                    txtshift4.Text = "";
                    cmbShiftName.Text = "";
                    txtbreak1.Text = "";
                    txtbreak12.Text = ""; txtbreak13.Text = ""; txtbreak14.Text = "";
                    txtbreak2.Text = ""; txtbreak21.Text = ""; txtbreak23.Text = ""; txtbreak24.Text = "";
                    txtbreak3.Text = ""; txtbreak31.Text = ""; txtbreak32.Text = ""; txtbreak33.Text = "";
                    txtbreak4.Text = ""; txtbreak41.Text = ""; txtbreak42.Text = ""; txtbreak43.Text = "";
                    txtbreak5.Text = ""; txtbreak51.Text = ""; txtbreak52.Text = ""; txtbreak53.Text = "";
                    txtshift1.Text = ""; txtshift2.Text = ""; txtshift3.Text = ""; txtshift4.Text = "";
                    txtTimePintHH1.Text = ""; txtTimePintMM1.Text = ""; txtTimePintHH2.Text = "";
                    txtTimePintMM2.Text = ""; txtTimePintHH3.Text = ""; txtTimePintMM3.Text = ""; txtTimePintHH4.Text = "";
                    txtTimePintMM4.Text = ""; txtTimePintHH5.Text = ""; txtTimePintMM6.Text = ""; txtTimePintHH6.Text = "";
                    txtTimePintMM7.Text = ""; txtTimePintHH7.Text = ""; txtTimePintMM8.Text = ""; txtTimePintHH8.Text = "";
                    txtTimePintMM5.Text = "";
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        //private void Cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cmbmachinegrp.SelectedValue != null)
        //        {
        //            Transaction("GetMachinename");
        //            dvgMasterDeatils.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SHIFT_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
    }
}

