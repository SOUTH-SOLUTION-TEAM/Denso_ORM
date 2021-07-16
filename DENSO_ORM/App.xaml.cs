using DENSO_ORM.StartUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DENSO_ORM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();

        private void StartUP(object sender, StartupEventArgs e)
        {
            try
            {
                if(!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory+"\\Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log");
                }
                string data = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                if (data != "")
                {
                    string[] DataSplit = data.Split(',');
                    if (DataSplit.Length > 0)
                    {
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqldbServer = DataSplit[0].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBUserID = DataSplit[1].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBPassword = DataSplit[2].ToString();
                        ENTITY_LAYER.DatabaseSettings.DatabaseSettings.SqlDBName = DataSplit[3].ToString();
                        CommonClasses.CommonVariable.obj_Login = new StartUp.Login();
                        App.Current.MainWindow.Content = CommonClasses.CommonVariable.obj_Login;
                    }
                    else
                    {
                        CommonClasses.CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

                    }
                }
                else
                {
                    StartUp.DatabaseSetting obj_DatabaseSetting = new StartUp.DatabaseSetting();
                    App.Current.MainWindow.Content = obj_DatabaseSetting;
                }

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

       
        private void Grid_MouseLeftButtonUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
               // System.Diagnostics.Process.GetCurrentProcess().Kill();
                System.Windows.Forms.SendKeys.SendWait("^w");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
