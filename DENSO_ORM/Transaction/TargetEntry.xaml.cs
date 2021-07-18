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
    /// Interaction logic for TargetEntry.xaml
    /// </summary>
    public partial class TargetEntry : Page
    {
        public TargetEntry()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Trns = new BUSINESS_LAYER.Transaction.Transaction();
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                //Date.DisplayDateStart = System.DateTime.Today;
                Transaction("LoadDetails");
                cmbMonth.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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

                
                ENTITY_LAYER.Transaction.Transaction.Month = cmbMonth.Text;
                ENTITY_LAYER.Transaction.Transaction.Year = DateTime.Today.Year.ToString();
                ENTITY_LAYER.Transaction.Transaction.TargetType = cmbTargettype.Text;
                ENTITY_LAYER.Transaction.Transaction.Category = cmbCategory.Text;
                ENTITY_LAYER.Transaction.Transaction.Target = txttarget.Text;
                ENTITY_LAYER.Transaction.Transaction.RefNo = RefNo.ToString();
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Trns.BL_TargetEntryTransaction();
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
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                
                //ENTITY_LAYER.Transaction.Transaction.Station = cmbstation.SelectedValue.ToString();
                DataTable dt = obj_Trns.BL_TargetEntryDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetLineGroup")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
               /// ENTITY_LAYER.Transaction.Transaction.MachineGroup = txtlinegrp.Text;
                //ENTITY_LAYER.Transaction.Transaction.MachineName = txtlinename.Text;
                DataTable dt = obj_Trns.BL_TargetEntryDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbCategory, dt, "MachineGrName", "MachineGrName");
            }

        }
        private void ClearAll()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            Transaction("GetStations");
            cmbMonth.Text = "";
            cmbTargettype.Text = "";
            cmbCategory.Text = "";
            txttarget.Text = "";
            //Date.SelectedDate = null;
            cmbMonth.Focus();
            RefNo = 0;
        }
        private void Clear()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            cmbMonth.Text = "";
            cmbTargettype.Text = "";
            cmbCategory.Text = "";
            txttarget.Text = "";
            //Date.SelectedDate = null;
            cmbMonth.Focus();
            RefNo = 0;
        }
        private bool ControlValidation()
        {
           
            if (cmbMonth.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MONTH", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbMonth.Focus();
                return false;
            }

            if (cmbTargettype.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT TARGET TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbTargettype.Focus();
                return false;
            }
            if (cmbCategory.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CATEGORY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbCategory.Focus();
                return false;
            }
            if (txttarget.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER TARGET", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txttarget.Focus();
                return false;
            }
            //if (Date.SelectedDate = null)
            //{
            //    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT DATE ", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            //    return false;
            //}


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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
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
                    cmbMonth.Text = dr["Month"].ToString();
                    cmbTargettype.Text = dr["TargetType"].ToString();
                    cmbCategory.Text = dr["Category"].ToString();
                    txttarget.Text = dr["Target"].ToString();
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                }
                else
                {
                    cmbMonth.Text = "";
                    cmbTargettype.Text = "";
                    cmbCategory.Text = "";
                    txttarget.Text = "";
                    cmbMonth.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbTargettype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTargettype.SelectedIndex != -1)
                {
                    if (cmbTargettype.SelectedIndex==0)
                    {
                        Transaction("GetLineGroup");
                    }
                    else if (cmbTargettype.SelectedIndex == 1)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Category");
                        dt.Rows.Add("Machine Breakdown");
                        dt.Rows.Add("Defect and Rework");
                        dt.Rows.Add("Changeover Loss");
                        dt.Rows.Add("Schedule Loss");
                        dt.Rows.Add("Speed Loss");
                        dt.Rows.Add("Other Loss");
                        dt.Rows.Add("Good Hour Ratio");
                        dt.Rows.Add("Loss Hour Ratio");
                        CommonClasses.CommonMethods.FillComboBox(cmbCategory, dt, "Category", "");
                    }

                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TARGETENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}