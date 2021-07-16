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

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for _5M1EChanges.xaml
    /// </summary>
    public partial class _5M1EChanges : Page
    {
        public _5M1EChanges()
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
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                Date.DisplayDateStart = System.DateTime.Today;
                Transaction("GetStations");
                cmbstation.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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

                ENTITY_LAYER.Transaction.Transaction.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Transaction.Transaction.MachineName = txtlinename.Text;
                ENTITY_LAYER.Transaction.Transaction.Fortype = cmbchangetype.Text;
                ENTITY_LAYER.Transaction.Transaction.Station = cmbstation.Text;
                ENTITY_LAYER.Transaction.Transaction.Process = txtprocess.Text;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = cmbshift.Text;
                ENTITY_LAYER.Transaction.Transaction.Date = Date.SelectedDate.ToString();
                ENTITY_LAYER.Transaction.Transaction.RefNo = RefNo.ToString();
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Trns.BL_5M1ETransaction();
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
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Transaction.Transaction.MachineName = txtlinename.Text;
                ENTITY_LAYER.Transaction.Transaction.Station = cmbstation.SelectedValue.ToString();
                DataTable dt = obj_Trns.BL_5M1EDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "GetStations")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Transaction.Transaction.MachineName = txtlinename.Text;
                DataTable dt = obj_Trns.BL_5M1EDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbstation, dt, "Operation", "Operation");
            }

        }
        private void ClearAll()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            Transaction("GetStations");
            cmbchangetype.Text = "";
            cmbstation.Text = "";
            txtprocess.Text = "";
            cmbshift.Text = "";
            Date.SelectedDate = null;
            cmbstation.Focus();
            RefNo = 0;
        }
        private void Clear()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            cmbchangetype.Text = "";
            //cmbstation.Text = "";
            txtprocess.Text = "";
            cmbshift.Text = "";
            Date.SelectedDate = null;
            cmbstation.Focus();
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
            if (cmbchangetype.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CHANGE TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbchangetype.Focus();
                return false;
            }

            if (cmbstation.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT STATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbstation.Focus();
                return false;
            }
            if (txtprocess.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PROCESS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtprocess.Focus();
                return false;
            }
            if (cmbshift.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbshift.Focus();
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbchangetype.Text != "" || cmbstation.Text!="")
                {
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
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
                    cmbchangetype.Text = dr["ChangeType"].ToString();
                    cmbstation.Text = dr["Station"].ToString();
                    txtprocess.Text = dr["Process"].ToString();
                    cmbshift.Text = dr["ShiftName"].ToString();
                    Date.Text = dr["ForDate"].ToString();
                   
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                }
                else
                {
                    cmbchangetype.Text = "";
                    cmbstation.Text = "";
                    txtprocess.Text = "";
                    cmbshift.Text = "";
                    Date.SelectedDate = null;
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Cmbstation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbstation.SelectedValue != null)
                {
                    Transaction("LoadDetails");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "5M_1E_CHANGES", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
