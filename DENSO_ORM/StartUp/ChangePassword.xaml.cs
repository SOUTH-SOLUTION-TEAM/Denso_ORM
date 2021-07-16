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

namespace DENSO_ORM.StartUp
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        #region Variables and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Login.Login obj_Login = new BUSINESS_LAYER.Login.Login();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        #region methods
        private void ShowCapslock()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Clear()
        {
            txtUserID.Text = "";
            txtOldPassowrd.Password = "";
            txtNewPassword.Password = "";
            txtConfirmedPassword.Password = "";
            txtUserID.Focus();
        }
        private void Transaction()
        {
            ENTITY_LAYER.Login.Login.UserID = txtUserID.Text;
            ENTITY_LAYER.Login.Login.Password = txtOldPassowrd.Password;
            ENTITY_LAYER.Login.Login.ConfirmPassword = txtConfirmedPassword.Password;
            ENTITY_LAYER.Login.Login.Type = "ChangePassword";
            CommonClasses.CommonVariable.Result = obj_Login.BL_Login();
            if (CommonClasses.CommonVariable.Result == "Updated")
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                Clear();
            }
            else if (CommonClasses.CommonVariable.Result == "INVALID USER ID")
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtUserID.Focus();
            }

            else if (CommonClasses.CommonVariable.Result == "INVALID PASSWORD")
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtOldPassowrd.Focus();
            }
            else
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtUserID.Focus();
            }

        }

        #endregion

        #region Events
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Boolean Capslock = Console.CapsLock;
            if (txtConfirmedPassword.IsFocused == true || txtOldPassowrd.IsFocused == true || txtNewPassword.IsFocused == true)
            {
                if (Capslock == true)
                    txtPasswordPopup.IsOpen = true;
                else
                    txtPasswordPopup.IsOpen = false;
            }
            else
            {
                txtPasswordPopup.IsOpen = false;
            }
        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }


        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserID.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER USER ID", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtUserID.Focus();
            }
            else if (txtOldPassowrd.Password == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER OLD PASSWORD", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtOldPassowrd.Focus();
            }
            else if (txtNewPassword.Password == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NEW PASSWORD", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtNewPassword.Focus();
            }
            else if (txtConfirmedPassword.Password == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CONFIRMED PASSWORD", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtConfirmedPassword.Focus();
            }
            else if (txtNewPassword.Password != txtConfirmedPassword.Password)
            {
                CommonClasses.CommonMethods.MessageBoxShow("NEW AND CONFIRMED PASWWORD IS NOT MATCHING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtNewPassword.Focus();
            }
            else
                Transaction();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                btnsave_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B))
            {
                btnExit_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtUserID.Focus();
                ShowCapslock();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHANGE_PASSWORD", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        #endregion
    }
}
