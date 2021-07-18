using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LAYER.Masters
{
    public class Masters
    {
        #region Objects
        DATA_LAYER.DatabaseConnectivity.DatabaseConnections obj_DB = new DATA_LAYER.DatabaseConnectivity.DatabaseConnections();
        #endregion

        #region GroupMaster
        public string BL_GroupMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_GroupMaster", ENTITY_LAYER.Masters.Masters.GroupID, ENTITY_LAYER.Masters.Masters.Rights, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_GroupMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_GroupMaster", ENTITY_LAYER.Masters.Masters.GroupID, ENTITY_LAYER.Masters.Masters.Rights, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UserMaster
        public string BL_UserMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_UserMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.UserID, ENTITY_LAYER.Masters.Masters.UserName, ENTITY_LAYER.Masters.Masters.Password, ENTITY_LAYER.Masters.Masters.GroupID, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_UserMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_UserMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.UserID, ENTITY_LAYER.Masters.Masters.UserName, ENTITY_LAYER.Masters.Masters.Password, ENTITY_LAYER.Masters.Masters.GroupID, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MachineGroup
        public string BL_MachineGroupTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_MachineGroupMaster", ENTITY_LAYER.Masters.Masters.MachineGrID, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.BaseGhratio, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.Section, ENTITY_LAYER.Masters.Masters.MachineType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_MachineGroupDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_MachineGroupMaster", ENTITY_LAYER.Masters.Masters.MachineGrID, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.BaseGhratio, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.Section, ENTITY_LAYER.Masters.Masters.MachineType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ModuleMaster
        public string BL_ModulemasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ModuleMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.Cycletime, ENTITY_LAYER.Masters.Masters.Pulse, ENTITY_LAYER.Masters.Masters.Piece, ENTITY_LAYER.Masters.Masters.PartNo, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.PresentStandardTime, ENTITY_LAYER.Masters.Masters.BaseStandardTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ModuleMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ModuleMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.Cycletime, ENTITY_LAYER.Masters.Masters.Pulse, ENTITY_LAYER.Masters.Masters.Piece, ENTITY_LAYER.Masters.Masters.PartNo, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.PresentStandardTime, ENTITY_LAYER.Masters.Masters.BaseStandardTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProblemDefectMaster
        public string BL_ProblemDefectMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ProblemDefectMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.OperationType, ENTITY_LAYER.Masters.Masters.OperationCode, ENTITY_LAYER.Masters.Masters.OperationName, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.Fortype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ProblemDefectMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ProblemDefectMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.OperationType, ENTITY_LAYER.Masters.Masters.OperationCode, ENTITY_LAYER.Masters.Masters.OperationName, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.Fortype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ShiftMaster
        public string BL_ShiftMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ShiftMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.Shifttiming, ENTITY_LAYER.Masters.Masters.Break1, ENTITY_LAYER.Masters.Masters.Break2, ENTITY_LAYER.Masters.Masters.Break3, ENTITY_LAYER.Masters.Masters.Break4, ENTITY_LAYER.Masters.Masters.Break5, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.ShiftName, ENTITY_LAYER.Masters.Masters.Timepoints, ENTITY_LAYER.Masters.Masters.TotalWorkingHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ShiftMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ShiftMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.Shifttiming, ENTITY_LAYER.Masters.Masters.Break1, ENTITY_LAYER.Masters.Masters.Break2, ENTITY_LAYER.Masters.Masters.Break3, ENTITY_LAYER.Masters.Masters.Break4, ENTITY_LAYER.Masters.Masters.Break5, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Masters.Masters.ShiftName, ENTITY_LAYER.Masters.Masters.Timepoints, ENTITY_LAYER.Masters.Masters.TotalWorkingHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region APKMaster

        public DataSet BL_APKMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_APKMaster", ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ColorMaster
        public string BL_ColorMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ColorMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.ProcessType, ENTITY_LAYER.Masters.Masters.ColorName, ENTITY_LAYER.Masters.Masters.Percentage, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ColormasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ColorMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.ProcessType, ENTITY_LAYER.Masters.Masters.ColorName, ENTITY_LAYER.Masters.Masters.Percentage, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region IPPORTConfiguration
        public string BL_IPPortConfigurationTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_IPandPortConfiguration", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.ProcessType, ENTITY_LAYER.Masters.Masters.MachineType, ENTITY_LAYER.Masters.Masters.IP, ENTITY_LAYER.Masters.Masters.Port, ENTITY_LAYER.Masters.Masters.Plcaddress, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Masters.Masters.PLCProcess, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_IPPortConfigurationDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_IPandPortConfiguration", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.ProcessType, ENTITY_LAYER.Masters.Masters.MachineType, ENTITY_LAYER.Masters.Masters.IP, ENTITY_LAYER.Masters.Masters.Port, ENTITY_LAYER.Masters.Masters.Plcaddress, ENTITY_LAYER.Masters.Masters.Status, ENTITY_LAYER.Masters.Masters.PLCProcess, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SmsAndCalConfiguration
        public string BL_SMSCALLTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_SMSandCALConfiguration", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.RecipientType, ENTITY_LAYER.Masters.Masters.Recipientno, ENTITY_LAYER.Masters.Masters.RecipientName, ENTITY_LAYER.Masters.Masters.Recipientmsg, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_SMSCallDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_SMSandCALConfiguration", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.RecipientType, ENTITY_LAYER.Masters.Masters.Recipientno, ENTITY_LAYER.Masters.Masters.RecipientName, ENTITY_LAYER.Masters.Masters.Recipientmsg, ENTITY_LAYER.Masters.Masters.Type, ENTITY_LAYER.Login.Login.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region MaintanceConformation
        public string BL_MaintananceTconformationransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_MaintainceConfirmation", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.Remarks, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_MaintananceTconformationDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_MaintainceConfirmation", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.MachineOperation, ENTITY_LAYER.Masters.Masters.Remarks, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KanbanProgressmaster
        public string BL_KanbanProgressMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_KanbanProgressMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.NoOfadvancekanban, ENTITY_LAYER.Masters.Masters.NoofNormalkanban, ENTITY_LAYER.Masters.Masters.FreqHrs, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.PouchNo, ENTITY_LAYER.Masters.Masters.Qty, ENTITY_LAYER.Masters.Masters.NoofKanban, ENTITY_LAYER.Masters.Masters.TotalQty, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_KanbanProgressMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_KanbanProgressMaster", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.NoOfadvancekanban, ENTITY_LAYER.Masters.Masters.NoofNormalkanban, ENTITY_LAYER.Masters.Masters.FreqHrs, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.PouchNo, ENTITY_LAYER.Masters.Masters.Qty, ENTITY_LAYER.Masters.Masters.NoofKanban, ENTITY_LAYER.Masters.Masters.TotalQty, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}


