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
    /// Interaction logic for Scrap.xaml
    /// </summary>
    public partial class Scrap : Page
    {
        public Scrap()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        int RefNo = 0;
        string Mergereworktime = "", SplitReworktime = "";
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
                
                
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP", CommonClasses.CommonVariable.UserID);
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
            if (cmbModelNo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MODEL NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbModelNo.Focus();
                return false;
            }

            if (cmbDefectCode.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SEELCT DEFECT CODE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbDefectCode.Focus();
                return false;
            }

            if (txtscrapcount.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF SCRAP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtscrapcount.Focus();
                return false;
            }
            if (txtreworkcount.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF REWORK", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtreworkcount.Focus();
                return false;
            }
            if (txtRetime1.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER HOUR", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtRetime1.Focus();
                return false;
            }
            if (txtRetime2.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER MIN", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtRetime2.Focus();
                return false;
            }
            return true;
        }
        private void Merging()
        {
            Mergereworktime = txtRetime1.Text.PadLeft(2,'0') + ":" + txtRetime2.Text.PadLeft(2, '0');

        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                Merging();
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                ENTITY_LAYER.Masters.Masters.ModelName = cmbModelNo.Text;
                ENTITY_LAYER.Masters.Masters.DefectCode = cmbDefectCode.Text;
                ENTITY_LAYER.Masters.Masters.ScrapCount = Convert.ToInt32(txtscrapcount.Text);
                ENTITY_LAYER.Masters.Masters.ReworkCount = Convert.ToInt32(txtreworkcount.Text);
                ENTITY_LAYER.Masters.Masters.ReworkTime = Mergereworktime.ToString();
                ENTITY_LAYER.Masters.Masters.RefNo = RefNo;

                ENTITY_LAYER.Masters.Masters.Type = Type;
                CommonClasses.CommonVariable.Result = obj_Tran.BL_ScarpTransaction();
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
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text ;
                DataTable dt = obj_Tran.BL_ScarpDetails().Tables[0];
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
            if (Type == "GetModuleName")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup =txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Tran.BL_ScarpDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbModelNo, dt, "ModelName", "ModelName");
            }
            if (Type == "GetDefectcode")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Masters.Masters.MachineName = txtlinename.Text;
                DataTable dt = obj_Tran.BL_ScarpDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbDefectCode, dt, "OperationCode", "OperationCode");
            }

        }

        private void Clear()
        {
            Transaction("LoadDetails");
            cmbModelNo.Text = "";
            cmbDefectCode.Text = "";
            txtRetime1.Text = "";
            txtRetime2.Text = "";
            txtreworkcount.Text = "";
            txtreworkcount.Text = "";
            txtscrapcount.Text = "";
            cmbModelNo.Focus();
            RefNo = 0;
        }
        private void ClearAll()
        {
            dvgMasterDeatils.ItemsSource = null;
            Transaction("GetMachineGroupname");
            txtlinegrp.Text = "";
            txtlinename.Text = "";
            cmbModelNo.Text = "";
            cmbDefectCode.Text = "";
            txtRetime1.Text = "";
            txtRetime2.Text = "";
            txtreworkcount.Text = "";
            txtreworkcount.Text = "";
            txtscrapcount.Text = "";
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbModelNo.Text = dr["ModelName"].ToString();
                    cmbDefectCode.Text = dr["DefectCode"].ToString();
                    txtscrapcount.Text = dr["ScrapCount"].ToString();
                    txtreworkcount.Text = dr["ReworkCount"].ToString();
                    if(dr["ReworkTime"].ToString()!="")
                    {
                        txtRetime1.Text = dr["ReworkTime"].ToString().Split(':')[0].ToString();
                        txtRetime2.Text = dr["ReworkTime"].ToString().Split(':')[1].ToString();
                    }
                    RefNo = Convert.ToInt32(dr["RefNo"].ToString());

                }
                else
                {
                //    cmbmachinename.SelectedIndex = -1;
                    txtreworkcount.Text = "";
                    cmbModelNo.SelectedIndex = -1;
                    txtscrapcount.Text = "";
                    txtRetime1.Text = "";
                    txtRetime2.Text = "";
                    cmbDefectCode.Text = "";
                    cmbModelNo.Text = "";
                  //  Cmbmachinegrp.Focus();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
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
        //            cmbModelNo.ItemsSource = null;
        //            cmbDefectCode.ItemsSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
        //        CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}


        private void Txtscrapcount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void txtRetime1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void txtRetime2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void txtreworkcount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonClasses.CommonMethods.NumericValue(e);

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtlinename_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtlinename.Text != "")
                {
                    Transaction("LoadDetails");
                    Transaction("GetModuleName");
                    Transaction("GetDefectcode");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "SCRAP_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
