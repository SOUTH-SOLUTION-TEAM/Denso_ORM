using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ENTITY_LAYER.Masters
{
    public static class Masters
    {
        #region Variables
        static int _RefNo, _MachineGrID, _Pulse, _ScrapCount, _ReworkCount;

        static string _Recipientno, _BaseGhratio, _Type, _UserID, _UserName, _GroupID, _Password, _Rights, _LoginID, _PartNo, _MachineGroup, _MachineName, _Status, _ModelName, _OperationType, _OperationCode, _OperationName, _Shifttiming, _Break1, _Break2, _Break3, _Break4, _Break5, _ProcessType, _ColorName, _Percentage, _ShiftName, _TotPices, _Date, _MachineOperation, _IP, _Port, _Plcaddress, _MachineType, _Timepoints, _RecipientType, _RecipientName, _Recipientmsg, _Remarks, _ReworkTime, _DefectCode, _PresentStandardTime, _BaseStandardTime, _PLCProcess, _NoOfadvancekanban, _NoofNormalkanban, _FreqHrs, _PouchNo, _Qty, _NoofKanban, _TotalQty, _Fortype, _Station, _Process, _TotalWorkingHrs,_Section;
        static DataTable _Dt;
        static double _Cycletime, _Piece;

        public static int RefNo { get => _RefNo; set => _RefNo = value; }
        public static int MachineGrID { get => _MachineGrID; set => _MachineGrID = value; }
        public static string TotalWorkingHrs { get => _TotalWorkingHrs; set => _TotalWorkingHrs = value; }
        public static string BaseGhratio { get => _BaseGhratio; set => _BaseGhratio = value; }
        public static string Type { get => _Type; set => _Type = value; }
        public static string UserID { get => _UserID; set => _UserID = value; }
        public static string UserName { get => _UserName; set => _UserName = value; }
        public static string GroupID { get => _GroupID; set => _GroupID = value; }
        public static string Password { get => _Password; set => _Password = value; }
        public static string Rights { get => _Rights; set => _Rights = value; }
        public static string LoginID { get => _LoginID; set => _LoginID = value; }
        public static string PartNo { get => _PartNo; set => _PartNo = value; }
        public static string MachineGroup { get => _MachineGroup; set => _MachineGroup = value; }
        public static string MachineName { get => _MachineName; set => _MachineName = value; }
        public static string Status { get => _Status; set => _Status = value; }
        public static string ModelName { get => _ModelName; set => _ModelName = value; }
        public static string OperationType { get => _OperationType; set => _OperationType = value; }
        public static string OperationCode { get => _OperationCode; set => _OperationCode = value; }
        public static string OperationName { get => _OperationName; set => _OperationName = value; }
        public static string Shifttiming { get => _Shifttiming; set => _Shifttiming = value; }
        public static string Break1 { get => _Break1; set => _Break1 = value; }
        public static string Break2 { get => _Break2; set => _Break2 = value; }
        public static string Break3 { get => _Break3; set => _Break3 = value; }
        public static string Break4 { get => _Break4; set => _Break4 = value; }
        public static string Break5 { get => _Break5; set => _Break5 = value; }
        public static string ProcessType { get => _ProcessType; set => _ProcessType = value; }
        public static string ColorName { get => _ColorName; set => _ColorName = value; }
        public static string Percentage { get => _Percentage; set => _Percentage = value; }
        public static string ShiftName { get => _ShiftName; set => _ShiftName = value; }
        public static DataTable Dt { get => _Dt; set => _Dt = value; }
        public static string TotPices { get => _TotPices; set => _TotPices = value; }
        public static double Cycletime { get => _Cycletime; set => _Cycletime = value; }
        public static string Date { get => _Date; set => _Date = value; }
        public static string MachineOperation { get => _MachineOperation; set => _MachineOperation = value; }
        public static string IP { get => _IP; set => _IP = value; }
        public static string Port { get => _Port; set => _Port = value; }
        public static string Plcaddress { get => _Plcaddress; set => _Plcaddress = value; }
        public static string MachineType { get => _MachineType; set => _MachineType = value; }
        public static double Piece { get => _Piece; set => _Piece = value; }
        public static int Pulse { get => _Pulse; set => _Pulse = value; }
        public static int ScrapCount { get => _ScrapCount; set => _ScrapCount = value; }
        public static int ReworkCount { get => _ReworkCount; set => _ReworkCount = value; }

        public static string Recipientno { get => _Recipientno; set => _Recipientno = value; }
        public static string ReworkTime { get => _ReworkTime; set => _ReworkTime = value; }
        public static string DefectCode { get => _DefectCode; set => _DefectCode = value; }

        public static string Timepoints { get => _Timepoints; set => _Timepoints = value; }
        public static string RecipientType { get => _RecipientType; set => _RecipientType = value; }
        public static string RecipientName { get => _RecipientName; set => _RecipientName = value; }

        public static string Recipientmsg { get => _Recipientmsg; set => _Recipientmsg = value; }
        public static string Remarks { get => _Remarks; set => _Remarks = value; }
        public static string PresentStandardTime { get => _PresentStandardTime; set => _PresentStandardTime = value; }
        public static string BaseStandardTime { get => _BaseStandardTime; set => _BaseStandardTime = value; }
        public static string PLCProcess { get => _PLCProcess; set => _PLCProcess = value; }





        public static string NoOfadvancekanban { get => _NoOfadvancekanban; set => _NoOfadvancekanban = value; }
        public static string NoofNormalkanban { get => _NoofNormalkanban; set => _NoofNormalkanban = value; }
        public static string FreqHrs { get => _FreqHrs; set => _FreqHrs = value; }
        public static string PouchNo { get => _PouchNo; set => _PouchNo = value; }
        public static string Qty { get => _Qty; set => _Qty = value; }
        public static string NoofKanban { get => _NoofKanban; set => _NoofKanban = value; }
        public static string TotalQty { get => _TotalQty; set => _TotalQty = value; }
        public static string Fortype { get => _Fortype; set => _Fortype = value; }
        public static string Station { get => _Station; set => _Station = value; }
        public static string Process { get => _Process; set => _Process = value; }
        public static string Section { get => _Section; set => _Section = value; }



        #endregion
    }
}
