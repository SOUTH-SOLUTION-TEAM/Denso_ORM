using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENSO_ORM.CommonClasses
{
 public class CommonVariable
    {
        #region Common_Variables
        public static string DataSaved = "DATA SAVED SUCCESSFULLY";
        public static string DataDeleted = "DATA DELETED SUCCESSFULLY";
        public static string DataUpdated = "DATA UPDATED SUCCESSFULLY";
        public static string Duplicate = "DATA ALREADY EXIST";
        public static string DeleteConfirm = "DO YOU WANT TO DELETE SELECTED DATA?";
        public static string RowSelection = " PLEASE SELECT ROW FROM LIST VIEW FOR YOUR TRANSACTION!!!";
        public static string UserID = "";
        public static string UserName = "";
        public static string UserType = "";
        public static string Rights = "";
        public static int RefNo = 0;
        public static string Active = "Active";
        public static string InActive = "InActive";
        public static string Result = "";
        public static string MachineGroup = "";
        public static string MachineName = "";
        public static string Station = "";
        public static string NoofItems = "0";
        public static string Puls = "0";
        public static string CycleTime = "0";
        public static string ModelName = "";
        public static string productionPlan = "";
        public static string TransactioType = "";
        public static bool KeyActive = false;
        public static string ShiftName = "";
        public static string Time = "";
        public static StartUp.Login obj_Login = new StartUp.Login();
        public static Transaction.MachineSelection objMachine = new Transaction.MachineSelection();
        public static Transaction.Probem_Code obj_prd = null;
        public static Transaction.Probem_Code obj_prd1 = null;
        public static string Break = "";
        public static int PblCOunt = 0;
        public static string MachinePlane = "";
        public static string MachineStatus = "";
        public enum CustomStriing
        {
            YESNO,
            OKCancel,
            OK,
            Error,
            Successfull,
            Information,
            Question,
            Exclamatory,
        }
        #endregion
    }
}
