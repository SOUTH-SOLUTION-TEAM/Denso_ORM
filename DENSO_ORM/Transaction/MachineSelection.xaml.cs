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
    /// Interaction logic for MachineSelection.xaml
    /// </summary>
    public partial class MachineSelection : Page
    {
        public MachineSelection()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

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
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbmachinename.SelectedIndex = -1;
                Cmbmachinegrp.SelectedIndex = -1;
                ShowDateTime();
                Transaction("GetMachineGroupname");
                Cmbmachinegrp.Focus();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }

        private void Cmbmachinegrp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Cmbmachinegrp.SelectedValue != null)
                {
                    CommonClasses.CommonVariable.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
                    Transaction("GetMachinename");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            
            if (Type == "GetMachineGroupname")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(Cmbmachinegrp, dt, "MachineGrName", "MachineGrName");
            }
            if (Type == "GetMachinename")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
                DataTable dt = obj_Mast.BL_MachineGroupDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbmachinename, dt, "MachineName", "MachineName");
            }
       
            if (Type == "GetModuleName")
            {
                ENTITY_LAYER.Masters.Masters.Type = Type;
                ENTITY_LAYER.Masters.Masters.MachineGroup = Cmbmachinegrp.SelectedValue.ToString();
                ENTITY_LAYER.Masters.Masters.MachineName = cmbmachinename.SelectedValue.ToString();
                DataTable dt = obj_Mast.BL_ModuleMasterDetails().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    CommonClasses.CommonVariable.CycleTime = dt.Rows[0]["CycleTime"].ToString();
                    CommonClasses.CommonVariable.ModelName = dt.Rows[0]["ModelName"].ToString();
                    CommonClasses.CommonVariable.Puls = dt.Rows[0]["Puls"].ToString();
                    CommonClasses.CommonVariable.NoofItems = dt.Rows[0]["NoofItems"].ToString();

                }

            }
        }

        private void Cmbmachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cmbmachinename.SelectedIndex != -1)
                {
                    CommonClasses.CommonVariable.MachineName = cmbmachinename.SelectedValue.ToString();
                    Transaction("GetOperation");
                    Transaction("GetModuleName");
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }

        private bool ControlValidation()
        {
            if(Cmbmachinegrp.SelectedIndex==-1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                Cmbmachinegrp.Focus();
                return false;
            }
            if(cmbmachinename.SelectedIndex==-1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbmachinename.Focus();
                return false;
            }
            return true;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ControlValidation())
                {
                    NavigationService.Navigate(new StartUp.MainWindow());
              
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Cmbmachinename_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    if (ControlValidation())
                    {
                        NavigationService.Navigate(new StartUp.MainWindow());

                    }
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
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
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_SELECTION", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
