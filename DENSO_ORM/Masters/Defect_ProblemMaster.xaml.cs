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
using System.Windows.Shapes;

namespace DENSO_ORM.Masters
{
    /// <summary>
    /// Interaction logic for Defect_ProblemMaster.xaml
    /// </summary>
    public partial class Defect_ProblemMaster : Page
    {
        public Defect_ProblemMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
         System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            
        public int RefNo = 0;
        public string RadioType = null;
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
            if (txtlinegrp.Text=="")
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
            if (RadioType == "Problem")
            {
                if (txtprobcode.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PROBLEM CODE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtprobcode.Focus();
                    return false;
                }

                if (txtprobname.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PROBLEM NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtprobname.Focus();
                    return false;
                }
            }
            else
            {
                if (txtdefcode.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER DEFECT CODE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtdefcode.Focus();
                    return false;
                }

                if (txtdefname.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER DEFECT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtdefname.Focus();
                    return false;
                }
            }
            return true;
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                rdoProblem.IsChecked = true;
                txtprobcode.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.OperationType = RadioType;
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;
                ENTITY_LAYER.Masters.Masters.Fortype = cmbtype1.Text;
                ENTITY_LAYER.Masters.Masters.Type = Type;
                if (RadioType == "Problem")
                {
                    ENTITY_LAYER.Masters.Masters.OperationCode = txtprobcode.Text;
                    ENTITY_LAYER.Masters.Masters.OperationName = txtprobname.Text;
                    if (chkprdStatus.IsChecked == true)
                        ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.Active;
                    else
                        ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.InActive;

                }
                else
                {
                    ENTITY_LAYER.Masters.Masters.OperationCode = txtdefcode.Text;
                    ENTITY_LAYER.Masters.Masters.OperationName = txtdefname.Text;
                    if (chkDefStatus.IsChecked == true)
                        ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.Active;
                    else
                        ENTITY_LAYER.Masters.Masters.Status = CommonClasses.CommonVariable.InActive;

                }
              
                CommonClasses.CommonVariable.Result = obj_Mast.BL_ProblemDefectMasterTransaction();
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
                DataTable dt = obj_Mast.BL_ProblemDefectMasterDetails().Tables[0];
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
            chkDefStatus.IsChecked = true;
            chkprdStatus.IsChecked = true;
            rdoProblem.IsChecked = true;
            Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            txtdefcode.Text = "";
            txtdefname.Text = "";
            txtprobcode.Text = "";
            txtprobname.Text = "";
            txtlinegrp.Focus();
            dvgMasterDeatils.ItemsSource = null;
            RefNo = 0;
        }
        private void Clear()
        {
            chkDefStatus.IsChecked = true;
            chkprdStatus.IsChecked = true;
            //rdoProblem.IsChecked = true;
            //txtlinegrp.Text = "";
            //txtlinename.Text = "";
            Transaction("LoadDetails");
            txtdefcode.Text = "";
            txtdefname.Text = "";
            txtprobcode.Text = "";
            txtprobname.Text = "";
            txtprobcode.Focus();
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
                    RadioType = dr["OperationType"].ToString();
                    cmbtype1.Text = dr["Problem_type"].ToString();
                    if (RadioType == "Problem")
                    {
                        txtprobcode.Text = dr["OperationCode"].ToString();
                        txtprobname.Text = dr["OperationName"].ToString();
                        if (CommonClasses.CommonVariable.Active == dr["Status"].ToString())
                            chkprdStatus.IsChecked = true;
                        else
                            chkprdStatus.IsChecked = false;

                        rdoProblem.IsChecked = true;
                    }
                    else
                    {
                        txtdefcode.Text = dr["OperationCode"].ToString();
                        txtdefname.Text = dr["OperationName"].ToString();
                        if (CommonClasses.CommonVariable.Active == dr["Status"].ToString())
                            chkDefStatus.IsChecked = true;
                        else
                            chkDefStatus.IsChecked = false;
                        rdoDefect.IsChecked = true;
                    }
                    
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                }
                else
                {
                    txtprobcode.Text = "";
                    txtprobname.Text = "";
                    txtdefcode.Text = "";
                    txtdefname.Text = "";
                    cmbtype1.Text = "";
                    chkDefStatus.IsChecked = true;
                    chkprdStatus.IsChecked = true;
                    rdoDefect.IsChecked = false;
                    rdoProblem.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
        //        if (Cmbmachinegrp.SelectedValue != null)
        //        {
        //            Transaction("GetMachinename");
        //            dvgMasterDeatils.ItemsSource = null;
        //         //   Transaction("LoadDetails");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}
       

        private void RdoProblem_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdoProblem.IsChecked == true)
                {
                    RadioType = "Problem";
                    brdDefect.Visibility = Visibility.Hidden;
                    brdProblem.Visibility = Visibility.Visible;
                    lbl1.Visibility = Visibility.Visible;
                    cmbtype1.Visibility = Visibility.Visible;
                    txtprobcode.Focus();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
            
        }

        private void RdoDefect_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdoDefect.IsChecked == true)
                {
                    RadioType = "Defect";
                    brdDefect.Visibility = Visibility.Visible;
                    brdProblem.Visibility = Visibility.Hidden;
                    lbl1.Visibility = Visibility.Visible;
                    cmbtype1.Visibility = Visibility.Visible;
                    txtdefcode.Focus();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
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
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void Txtprobcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtdefcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROBLEMDEFECT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
