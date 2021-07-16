using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY_LAYER.Reports
{
  public  class Reports
    {
        static string _machineGroupName, _Machinename, _ShiftName, _FromDate, _Todate, _Type,_ModelNo,_Station,_Time,_ReportType,_Month,_HeaderType;

        public static string MachineGroupName { get => _machineGroupName; set => _machineGroupName = value; }
        public static string Machinename { get => _Machinename; set => _Machinename = value; }
        public static string ShiftName { get => _ShiftName; set => _ShiftName = value; }
        public static string FromDate { get => _FromDate; set => _FromDate = value; }
        public static string Todate { get => _Todate; set => _Todate = value; }
        public static string Type { get => _Type; set => _Type = value; }
        public static string ModelNo { get => _ModelNo; set => _ModelNo = value; }
        public static string Station { get => _Station; set => _Station = value; }
        public static string Time { get => _Time; set => _Time = value; }
        public static string ReportType { get => _ReportType; set => _ReportType = value; }
        public static string Month { get => _Month; set => _Month = value; }
        public static string HeaderType { get => _HeaderType; set => _HeaderType = value; }
    }
}
