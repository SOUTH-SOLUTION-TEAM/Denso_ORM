using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LAYER.Reports
{
    public class Reports
    {
        #region Objects
        DATA_LAYER.DatabaseConnectivity.DatabaseConnections obj_DB = new DATA_LAYER.DatabaseConnectivity.DatabaseConnections();
        #endregion

        #region Report
        public DataSet BL_Reports()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_Reports", ENTITY_LAYER.Reports.Reports.MachineGroupName, ENTITY_LAYER.Reports.Reports.Machinename, ENTITY_LAYER.Reports.Reports.ShiftName, ENTITY_LAYER.Reports.Reports.FromDate, ENTITY_LAYER.Reports.Reports.Todate, ENTITY_LAYER.Reports.Reports.Type, ENTITY_LAYER.Reports.Reports.ModelNo, ENTITY_LAYER.Reports.Reports.Station, ENTITY_LAYER.Reports.Reports.Time, ENTITY_LAYER.Reports.Reports.ReportType, ENTITY_LAYER.Reports.Reports.Month, ENTITY_LAYER.Reports.Reports.HeaderType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
