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
    /// Interaction logic for LossEntry.xaml
    /// </summary>
    public partial class LossEntry : Page
    {
        public LossEntry()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Trns = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DataTable dt_Operation = null;
        DataSet ds_Operation = new DataSet();
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbSHift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CmbSHift.SelectedIndex == 0)//first shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                }
                if (CmbSHift.SelectedIndex == 1)//Second shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                }
                if (CmbSHift.SelectedIndex == 2)//Third shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Transaction("Save");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpFrom.Text = "";
                dtpTo.Text = "";
                CmbSHift.Text = "";
                dvgModelDeatils.ItemsSource = null;
                dvgModelDeatils1.ItemsSource = null;
                dvgModelDeatils2.ItemsSource = null;
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
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
                Transaction("GetOperationDetails");

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                btnSave_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                btnExit_Click(sender, e);
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "GetOperationDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                dt_Operation = obj_Trns.BL_LossHourDetails().Tables[0];
                // DataView dv = new DataView(dt_Operation);
                //DataTable dt= dt.DefaultView;
                cmb.ItemsSource = dt_Operation.DefaultView;
                cmb.DisplayMemberPath = "LossCode";
                cmb.SelectedValuePath = "LossCode";

                cmb1.ItemsSource = dt_Operation.DefaultView;
                cmb1.DisplayMemberPath = "LossCode";
                cmb1.SelectedValuePath = "LossCode";

                cmb2.ItemsSource = dt_Operation.DefaultView;
                cmb2.DisplayMemberPath = "LossCode";
                cmb2.SelectedValuePath = "LossCode";
            }
            if (Type == "GetLossHourDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss"); ;
                ENTITY_LAYER.Transaction.Transaction.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CmbSHift.Text;
                ds_Operation = obj_Trns.BL_LossHourDetails();
                dvgModelDeatils.ItemsSource = ds_Operation.Tables[0].DefaultView;
                dvgModelDeatils1.ItemsSource = ds_Operation.Tables[1].DefaultView;
                dvgModelDeatils2.ItemsSource = ds_Operation.Tables[2].DefaultView;


                dvgModelDeatils.ScrollIntoView(dvgModelDeatils.Items[0], dvgModelDeatils.Columns[2]);
                //Transaction("GetOperationDetails");
            }
            if (Type == "Save")
            {
               
                if(ds_Operation.Tables.Count>0)
                {
                    for(int i=0;i< ds_Operation.Tables[0].Rows.Count;i++)
                    {
                        if(ds_Operation.Tables[0].Rows[i]["LossCode"].ToString()!="")
                        {
                            ENTITY_LAYER.Transaction.Transaction.Type = Type;
                            ENTITY_LAYER.Transaction.Transaction.RefNo = ds_Operation.Tables[0].Rows[i]["RefNo"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.HOUR = ds_Operation.Tables[0].Rows[i]["Hour"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.MAN = ds_Operation.Tables[0].Rows[i]["ManPower"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.OPERATIONNAME = ds_Operation.Tables[0].Rows[i]["LossName"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.ProblemCode = ds_Operation.Tables[0].Rows[i]["LossCode"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS = ds_Operation.Tables[0].Rows[i]["Total"].ToString();
                            CommonClasses.CommonVariable.Result = obj_Trns.BL_LossHourTransaction();
                        }
                    }
                    for (int i = 0; i < ds_Operation.Tables[1].Rows.Count; i++)
                    {
                        if (ds_Operation.Tables[1].Rows[i]["LossCode"].ToString() != "")
                        {
                            ENTITY_LAYER.Transaction.Transaction.Type = Type;
                            ENTITY_LAYER.Transaction.Transaction.RefNo = ds_Operation.Tables[1].Rows[i]["RefNo"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.HOUR = ds_Operation.Tables[1].Rows[i]["Hour"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.MAN = ds_Operation.Tables[1].Rows[i]["ManPower"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.OPERATIONNAME = ds_Operation.Tables[1].Rows[i]["LossName"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.ProblemCode = ds_Operation.Tables[1].Rows[i]["LossCode"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS = ds_Operation.Tables[1].Rows[i]["Total"].ToString();
                            CommonClasses.CommonVariable.Result = obj_Trns.BL_LossHourTransaction();
                        }
                    }
                    for (int i = 0; i < ds_Operation.Tables[2].Rows.Count; i++)
                    {
                        if (ds_Operation.Tables[2].Rows[i]["LossCode"].ToString() != "")
                        {
                            ENTITY_LAYER.Transaction.Transaction.Type = Type;
                            ENTITY_LAYER.Transaction.Transaction.RefNo = ds_Operation.Tables[2].Rows[i]["RefNo"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.HOUR = ds_Operation.Tables[2].Rows[i]["Hour"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.MAN = ds_Operation.Tables[2].Rows[i]["ManPower"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.OPERATIONNAME = ds_Operation.Tables[2].Rows[i]["LossName"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.ProblemCode = ds_Operation.Tables[2].Rows[i]["LossCode"].ToString();
                            ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS = ds_Operation.Tables[2].Rows[i]["Total"].ToString();
                            CommonClasses.CommonVariable.Result = obj_Trns.BL_LossHourTransaction();
                        }
                    }

                }
              
                if(CommonClasses.CommonVariable.Result=="Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("NO DATA TO SAVE ", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

                //Transaction("GetOperationDetails");
            }
        }

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtlinegrp.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtlinegrp.Focus();
                    return;
                }
                if (txtlinename.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtlinename.Focus();
                    return;
                }

                if (dtpFrom.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT FROM DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpFrom.Focus();
                    return;
                }
                if (dtpTo.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT TO DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpTo.Focus();
                    return;
                }
                if (CmbSHift.SelectedIndex == -1)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    CmbSHift.Focus();
                    return;
                }

                if (CmbSHift.SelectedIndex == 0)//first shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                }
                if (CmbSHift.SelectedIndex == 1)//Second shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                }
                if (CmbSHift.SelectedIndex == 2)//Third shift
                {
                    dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                    dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                }

                Transaction("GetLossHourDetails");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = ds_Operation.Tables[0].Rows.Count;
                //string sd = cmb.TextBinding.ToString();

                var item = dvgModelDeatils.SelectedItem as DataRowView;

                if (null == item)
                    return;
                int l = dvgModelDeatils.SelectedIndex;
                string sEntityName = item.Row["LossCode"].ToString();
                if (sEntityName != "")
                {
                    for (int j = 0; j < dt_Operation.Rows.Count; j++)
                    {
                        if (sEntityName == dt_Operation.Rows[j]["LossCode"].ToString())
                        {
                            ds_Operation.Tables[0].Rows[l]["LossName"] = dt_Operation.Rows[j]["LossName"].ToString();
                        }
                    }
                }
                else
                    ds_Operation.Tables[0].Rows[l]["LossName"] = "";
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = ds_Operation.Tables[1].Rows.Count;
                //string sd = cmb.TextBinding.ToString();

                var item = dvgModelDeatils1.SelectedItem as DataRowView;

                if (null == item)
                    return;
                int l = dvgModelDeatils1.SelectedIndex;
                string sEntityName = item.Row["LossCode"].ToString();
                if (sEntityName != "")
                {
                    for (int j = 0; j < dt_Operation.Rows.Count; j++)
                    {
                        if (sEntityName == dt_Operation.Rows[j]["LossCode"].ToString())
                        {
                            ds_Operation.Tables[1].Rows[l]["LossName"] = dt_Operation.Rows[j]["LossName"].ToString();
                        }
                    }
                }
                else
                {
                    ds_Operation.Tables[1].Rows[l]["LossName"] = "";

                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = ds_Operation.Tables[2].Rows.Count;
                //string sd = cmb.TextBinding.ToString();

                var item = dvgModelDeatils2.SelectedItem as DataRowView;

                if (null == item)
                    return;
                int l = dvgModelDeatils2.SelectedIndex;
                string sEntityName = item.Row["LossCode"].ToString();
                if (sEntityName != "")
                {
                    for (int j = 0; j < dt_Operation.Rows.Count; j++)
                    {
                        if (sEntityName == dt_Operation.Rows[j]["LossCode"].ToString())
                        {
                            ds_Operation.Tables[2].Rows[l]["LossName"] = dt_Operation.Rows[j]["LossName"].ToString();
                        }
                    }
                }
                else
                    ds_Operation.Tables[2].Rows[l]["LossName"] = "";
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void DataGridCell_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType() == typeof(DataGridCell))
                {
                    // Starts the Edit on the row;
                    DataGrid grd = (DataGrid)sender;
                    grd.BeginEdit(e);
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOSS_HOUR", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}

