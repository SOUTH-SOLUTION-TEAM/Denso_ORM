using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DENSO_ORM.StartUp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            
        }
        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Login.Login obj_Login = new BUSINESS_LAYER.Login.Login();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        #region Methods
        private void ShowCapslock()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void ValidateLogin()
        {
            ENTITY_LAYER.Login.Login.UserID = txtUserID.Text;
            ENTITY_LAYER.Login.Login.Password = txtPassword.Password;
            ENTITY_LAYER.Login.Login.Type = "Login";
            CommonClasses.CommonVariable.Result = obj_Login.BL_Login();
            if (CommonClasses.CommonVariable.Result.StartsWith("VALID CREDENTIAL"))
            {
                CommonClasses.CommonVariable.UserID = txtUserID.Text;
                CommonClasses.CommonVariable.UserName = CommonClasses.CommonVariable.Result.Split('+')[1].ToString();
                CommonClasses.CommonVariable.Rights = CommonClasses.CommonVariable.Result.Split('+')[2].ToString();
                CommonClasses.CommonMethods obj_CommonMethod = new CommonClasses.CommonMethods();
                bool Flag = obj_CommonMethod.WebServiceConnection();
                NavigationService.Navigate(new Transaction.MachineSelection());
            }
            else if (CommonClasses.CommonVariable.Result == "INVALID USER ID")
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtUserID.Text = "";
                txtUserID.Focus();
            }
            else if (CommonClasses.CommonVariable.Result == "INVALID PASSWORD")
            {
                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPassword.Password = "";
                txtPassword.Focus();
            }
            else if (CommonClasses.CommonVariable.Result == "FIRST TIME LOGIN")
            {
                if ((txtUserID.Text.ToUpper() == "SARBLR") && (txtPassword.Password.ToUpper() == "SARBLR"))
                {
                    CommonClasses.CommonVariable.UserName = "SARBLR";
                    CommonClasses.CommonVariable.Rights = "USER MASTER,GROUP MASTER";
                    CommonClasses.CommonVariable.UserID = "SARBLR";
                    NavigationService.Navigate(new MainWindow());
                }
                else
                {
                    CommonClasses.CommonMethods.MessageBoxShow("FIRST TIME LOGIN. PLEASE ENTER VALID USER ID OR PASSWORD", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    return;
                }
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
            if (txtPassword.IsFocused == true)
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Version Version = Assembly.GetExecutingAssembly().GetName().Version;
                txtVersion.Text = String.Format(this.txtVersion.Text, Version.Major, Version.Minor, Version.Build, Version.Revision);
                ShowCapslock();
                txtUserID.Focus();
                //CommonClasses.CommonMethods.MessageBoxShow(AppDomain.CurrentDomain.BaseDirectory, CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {

             
                if (txtUserID.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtUserID.Focus();
                    return;
                }
                if (txtPassword.Password == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER THE PASSWORD", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtPassword.Focus();
                    return;
                }
                ValidateLogin();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

               // System.Diagnostics.Process.GetCurrentProcess().Kill();
                System.Windows.Forms.SendKeys.SendWait("^w");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void LinkForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserID.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtUserID.Focus();
                    return;
                }
                ENTITY_LAYER.Login.Login.UserID = txtUserID.Text;
                ENTITY_LAYER.Login.Login.Type = "GetRights";
                CommonClasses.CommonVariable.Result = obj_Login.BL_Login();
                if (CommonClasses.CommonVariable.Result.Contains("FORGOT PASSWORD"))
                {
                    NavigationService.Navigate(new ForgotPassword());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("ACCESS DENIED", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void LinkChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserID.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER THE USER ID", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtUserID.Focus();
                    return;
                }
                ENTITY_LAYER.Login.Login.UserID = txtUserID.Text;
                ENTITY_LAYER.Login.Login.Type = "GetRights";
                CommonClasses.CommonVariable.Result = obj_Login.BL_Login();
                if (CommonClasses.CommonVariable.Result.Contains("CHANGE PASSWORD"))
                {
                    NavigationService.Navigate(new ChangePassword());

                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("ACCESS DENIED", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LOGIN", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.L) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.L))
            {
                btnLogin_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.E) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.E))
            {
                btnExit_Click(sender, e);
            }
        }

        private void TxtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                btnLogin_Click(sender, e);
        }
        #endregion

    }
   
}
