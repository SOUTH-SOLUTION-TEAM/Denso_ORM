using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LAYER.Transaction
{
    public class Transaction
    {
        #region Objects
        DATA_LAYER.DatabaseConnectivity.DatabaseConnections obj_DB = new DATA_LAYER.DatabaseConnectivity.DatabaseConnections();
        #endregion

        #region OrMonitoring
        public string BL_OrMonitoringTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_OrMonitoring", ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.ModelName, ENTITY_LAYER.Transaction.Transaction.ProductionPlan, ENTITY_LAYER.Transaction.Transaction.CyscleTime, ENTITY_LAYER.Transaction.Transaction.Time, ENTITY_LAYER.Transaction.Transaction.PLCData, ENTITY_LAYER.Transaction.Transaction.Client, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_OrMonitoringDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_OrMonitoring", ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.ModelName, ENTITY_LAYER.Transaction.Transaction.ProductionPlan, ENTITY_LAYER.Transaction.Transaction.CyscleTime, ENTITY_LAYER.Transaction.Transaction.Time, ENTITY_LAYER.Transaction.Transaction.PLCData, ENTITY_LAYER.Transaction.Transaction.Client, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MachineOnandOff
        public string BL_MachineOnandOffTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_MachineONandOff", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.Date, ENTITY_LAYER.Masters.Masters.ShiftName, ENTITY_LAYER.Masters.Masters.Shifttiming, ENTITY_LAYER.Masters.Masters.DefectCode, ENTITY_LAYER.Masters.Masters.Remarks, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_MachineOnandOffDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_MachineONandOff", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.Date, ENTITY_LAYER.Masters.Masters.ShiftName, ENTITY_LAYER.Masters.Masters.Shifttiming, ENTITY_LAYER.Masters.Masters.DefectCode, ENTITY_LAYER.Masters.Masters.Remarks, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DashBoard
        public DataSet BL_DashBoard()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_DashBoard_1", ENTITY_LAYER.Transaction.Transaction.Type,  ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.ModelName, ENTITY_LAYER.Transaction.Transaction.ProblemCode, ENTITY_LAYER.Transaction.Transaction.Cycletime, ENTITY_LAYER.Transaction.Transaction.NoofItem, ENTITY_LAYER.Transaction.Transaction.Puls,  ENTITY_LAYER.Transaction.Transaction.Manpower, ENTITY_LAYER.Transaction.Transaction.RefNo,"","");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Scarp
        public string BL_ScarpTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_Scrap", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.DefectCode, ENTITY_LAYER.Masters.Masters.ScrapCount, ENTITY_LAYER.Masters.Masters.ReworkCount, ENTITY_LAYER.Masters.Masters.ReworkTime, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ScarpDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_Scrap", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.ModelName, ENTITY_LAYER.Masters.Masters.DefectCode, ENTITY_LAYER.Masters.Masters.ScrapCount, ENTITY_LAYER.Masters.Masters.ReworkCount, ENTITY_LAYER.Masters.Masters.ReworkTime, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region smscalldetails
        public DataSet BL_SMSCallDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_SMSandCALConfiguration", ENTITY_LAYER.Masters.Masters.RefNo, ENTITY_LAYER.Masters.Masters.MachineGroup, ENTITY_LAYER.Masters.Masters.MachineName, ENTITY_LAYER.Masters.Masters.RecipientType, ENTITY_LAYER.Masters.Masters.Recipientno, ENTITY_LAYER.Masters.Masters.RecipientName, ENTITY_LAYER.Masters.Masters.Recipientmsg, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Masters.Masters.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 5MAND1E
        public string BL_5M1ETransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_5M1EChanges", ENTITY_LAYER.Transaction.Transaction.RefNo, ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.Fortype, ENTITY_LAYER.Transaction.Transaction.Station, ENTITY_LAYER.Transaction.Transaction.Process, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.Date, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_5M1EDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_5M1EChanges", ENTITY_LAYER.Transaction.Transaction.RefNo, ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.Fortype, ENTITY_LAYER.Transaction.Transaction.Station, ENTITY_LAYER.Transaction.Transaction.Process, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.Date, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPIentry
        public string BL_KPIEntryTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_KPIEntry", ENTITY_LAYER.Transaction.Transaction.RefNo,
                ENTITY_LAYER.Transaction.Transaction.MachineGroup,
                ENTITY_LAYER.Transaction.Transaction.MachineName,
                ENTITY_LAYER.Transaction.Transaction.ModelName,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_MANPOWER_GIVEN_BY_COMPANY,
                ENTITY_LAYER.Transaction.Transaction.ABSENT,
                ENTITY_LAYER.Transaction.Transaction.INLINE_OPERATOR,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER_TAKEN_FROM_BANK,
                ENTITY_LAYER.Transaction.Transaction.INLINE_BORROWED_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_GIVEN_TO_BANK,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_GIVEN_TO_BANK,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_LENDING,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members,
                ENTITY_LAYER.Transaction.Transaction.SHIFT_WORKIG_HRS,
                ENTITY_LAYER.Transaction.Transaction.LUNCH_TIME_30_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_WORKING_HOURS,
                ENTITY_LAYER.Transaction.Transaction.KY_1_7_Minutes,
                ENTITY_LAYER.Transaction.Transaction.MORNING_MEETING_5_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes1,
                ENTITY_LAYER.Transaction.Transaction.A5S_CLOSING_5_Minutes,
                ENTITY_LAYER.Transaction.Transaction.BREAK_TIME_WITHOUT_LUNCH,
                ENTITY_LAYER.Transaction.Transaction.FIXED_TIME,
                ENTITY_LAYER.Transaction.Transaction.ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY,
                ENTITY_LAYER.Transaction.Transaction.EXCESS_MP_AFTER_MP_CALCULATION,
                ENTITY_LAYER.Transaction.Transaction.OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc,
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING,
                ENTITY_LAYER.Transaction.Transaction.ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY,
                ENTITY_LAYER.Transaction.Transaction.SKILL_COMPETETION_QCC_BUSINESS_TRIP,
                ENTITY_LAYER.Transaction.Transaction.INVENTORY,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SPECIAL_5S,
                ENTITY_LAYER.Transaction.Transaction.DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_TRIAL_BY_PE,
                ENTITY_LAYER.Transaction.Transaction.ENERGY_SHUTDOWN_GOVT_POWER_ONLY,
                ENTITY_LAYER.Transaction.Transaction.FIRE_ASSEMBLY_EVACUATION_DRILL,
                ENTITY_LAYER.Transaction.Transaction.NPD_TRIALS,
                ENTITY_LAYER.Transaction.Transaction.MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR,
                ENTITY_LAYER.Transaction.Transaction.COMPANY_COST_SAVING_ACTIVITY,
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH,
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION,
                ENTITY_LAYER.Transaction.Transaction.NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_MAINTENANCE,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_QA_2ND_QA,
                ENTITY_LAYER.Transaction.Transaction.VISITOR_ISO_CUSTOMER_AUDIT_TIME,
                ENTITY_LAYER.Transaction.Transaction.MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY,
                ENTITY_LAYER.Transaction.Transaction.IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY,
                ENTITY_LAYER.Transaction.Transaction.A5S_BANK_RETURNED_MP,
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING_BANK_RETURNED_MP,
                ENTITY_LAYER.Transaction.Transaction.NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS,
                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_DIRECT_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS,
                ENTITY_LAYER.Transaction.Transaction.PART_REJECTION_SCRAP_REWORK,
                ENTITY_LAYER.Transaction.Transaction.LOSS_HRS_RATIO,
                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS_FOR_DAY,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_FOR_DAY,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_RATIO,
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE,
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE_MAN_HOURS,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANHOUR,
                 ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.KPI,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS,
                ENTITY_LAYER.Transaction.Transaction.DPR,
                ENTITY_LAYER.Transaction.Transaction.BASE_GH_RATIO,
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS1,
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS1,
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE1,
                ENTITY_LAYER.Transaction.Transaction.DPR1,
                ENTITY_LAYER.Transaction.Transaction.LHR,
                ENTITY_LAYER.Transaction.Transaction.LHR_DAILY,
                ENTITY_LAYER.Transaction.Transaction.LEND,
                ENTITY_LAYER.Transaction.Transaction.BANK,
                ENTITY_LAYER.Transaction.Transaction.OJT,
                ENTITY_LAYER.Transaction.Transaction.EXCLUDE,
                ENTITY_LAYER.Transaction.Transaction.PURE_EXCLUDE,
                ENTITY_LAYER.Transaction.Transaction.MIZ_DH,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_DH,
                ENTITY_LAYER.Transaction.Transaction.FQA_GH,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Transaction.Transaction.Type, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.FromDate, ENTITY_LAYER.Transaction.Transaction.Todate, ENTITY_LAYER.Transaction.Transaction.NPDDIRHR, ENTITY_LAYER.Transaction.Transaction.NPDEXCHR); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_KPIEntryDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_KPIEntry", ENTITY_LAYER.Transaction.Transaction.RefNo,
                ENTITY_LAYER.Transaction.Transaction.MachineGroup,
                ENTITY_LAYER.Transaction.Transaction.MachineName,
                ENTITY_LAYER.Transaction.Transaction.ModelName,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_MANPOWER_GIVEN_BY_COMPANY,
                ENTITY_LAYER.Transaction.Transaction.ABSENT,
                ENTITY_LAYER.Transaction.Transaction.INLINE_OPERATOR,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER_TAKEN_FROM_BANK,
                ENTITY_LAYER.Transaction.Transaction.INLINE_BORROWED_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_GIVEN_TO_BANK,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_GIVEN_TO_BANK,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_LENDING,
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING,
                ENTITY_LAYER.Transaction.Transaction.INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members,
                ENTITY_LAYER.Transaction.Transaction.SHIFT_WORKIG_HRS,
                ENTITY_LAYER.Transaction.Transaction.LUNCH_TIME_30_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_WORKING_HOURS,
                ENTITY_LAYER.Transaction.Transaction.KY_1_7_Minutes,
                ENTITY_LAYER.Transaction.Transaction.MORNING_MEETING_5_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes,
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes1,
                ENTITY_LAYER.Transaction.Transaction.A5S_CLOSING_5_Minutes,
                ENTITY_LAYER.Transaction.Transaction.BREAK_TIME_WITHOUT_LUNCH,
                ENTITY_LAYER.Transaction.Transaction.FIXED_TIME,
                ENTITY_LAYER.Transaction.Transaction.ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY,
                ENTITY_LAYER.Transaction.Transaction.EXCESS_MP_AFTER_MP_CALCULATION,
                ENTITY_LAYER.Transaction.Transaction.OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc,
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING,
                ENTITY_LAYER.Transaction.Transaction.ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY,
                ENTITY_LAYER.Transaction.Transaction.SKILL_COMPETETION_QCC_BUSINESS_TRIP,
                ENTITY_LAYER.Transaction.Transaction.INVENTORY,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SPECIAL_5S,
                ENTITY_LAYER.Transaction.Transaction.DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_TRIAL_BY_PE,
                ENTITY_LAYER.Transaction.Transaction.ENERGY_SHUTDOWN_GOVT_POWER_ONLY,
                ENTITY_LAYER.Transaction.Transaction.FIRE_ASSEMBLY_EVACUATION_DRILL,
                ENTITY_LAYER.Transaction.Transaction.NPD_TRIALS,
                ENTITY_LAYER.Transaction.Transaction.MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR,
                ENTITY_LAYER.Transaction.Transaction.COMPANY_COST_SAVING_ACTIVITY,
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH,
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION,
                ENTITY_LAYER.Transaction.Transaction.NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_MAINTENANCE,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_QA_2ND_QA,
                ENTITY_LAYER.Transaction.Transaction.VISITOR_ISO_CUSTOMER_AUDIT_TIME,
                ENTITY_LAYER.Transaction.Transaction.MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY,
                ENTITY_LAYER.Transaction.Transaction.IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc,
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY,
                ENTITY_LAYER.Transaction.Transaction.A5S_BANK_RETURNED_MP,
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING_BANK_RETURNED_MP,
                ENTITY_LAYER.Transaction.Transaction.NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS,
                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_DIRECT_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS,
                ENTITY_LAYER.Transaction.Transaction.PART_REJECTION_SCRAP_REWORK,
                ENTITY_LAYER.Transaction.Transaction.LOSS_HRS_RATIO,
                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS_FOR_DAY,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_FOR_DAY,
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_RATIO,
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE,
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE_MAN_HOURS,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANPOWER,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER,
                ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER_MANHOUR,
                ENTITY_LAYER.Transaction.Transaction.KPI,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS,
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS,
                ENTITY_LAYER.Transaction.Transaction.DPR,
                ENTITY_LAYER.Transaction.Transaction.BASE_GH_RATIO,
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE,
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS1,
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS1,
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE1,
                ENTITY_LAYER.Transaction.Transaction.DPR1,
                ENTITY_LAYER.Transaction.Transaction.LHR,
                ENTITY_LAYER.Transaction.Transaction.LHR_DAILY,
                ENTITY_LAYER.Transaction.Transaction.LEND,
                ENTITY_LAYER.Transaction.Transaction.BANK,
                ENTITY_LAYER.Transaction.Transaction.OJT,
                ENTITY_LAYER.Transaction.Transaction.EXCLUDE,
                ENTITY_LAYER.Transaction.Transaction.PURE_EXCLUDE,
                ENTITY_LAYER.Transaction.Transaction.MIZ_DH,
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_DH,
                ENTITY_LAYER.Transaction.Transaction.FQA_GH,
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING, ENTITY_LAYER.Login.Login.UserID, ENTITY_LAYER.Transaction.Transaction.Type, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.FromDate, ENTITY_LAYER.Transaction.Transaction.Todate, ENTITY_LAYER.Transaction.Transaction.NPDDIRHR, ENTITY_LAYER.Transaction.Transaction.NPDEXCHR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region LossHour
        public string BL_LossHourTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_LossHour", ENTITY_LAYER.Transaction.Transaction.RefNo,
                ENTITY_LAYER.Transaction.Transaction.MachineGroup,
                ENTITY_LAYER.Transaction.Transaction.MachineName,
                ENTITY_LAYER.Transaction.Transaction.ProblemCode,
                ENTITY_LAYER.Transaction.Transaction.OPERATIONNAME,
                ENTITY_LAYER.Transaction.Transaction.MAN,
                ENTITY_LAYER.Transaction.Transaction.HOUR,
                ENTITY_LAYER.Transaction.Transaction.Shiftname,
                ENTITY_LAYER.Transaction.Transaction.FromDate,
                ENTITY_LAYER.Transaction.Transaction.Todate,
                ENTITY_LAYER.Login.Login.UserID,
                ENTITY_LAYER.Transaction.Transaction.Type, ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS
               );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_LossHourDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_LossHour", ENTITY_LAYER.Transaction.Transaction.RefNo,
                ENTITY_LAYER.Transaction.Transaction.MachineGroup,
                ENTITY_LAYER.Transaction.Transaction.MachineName,
                ENTITY_LAYER.Transaction.Transaction.ProblemCode,
                ENTITY_LAYER.Transaction.Transaction.OPERATIONNAME,
                ENTITY_LAYER.Transaction.Transaction.MAN,
                ENTITY_LAYER.Transaction.Transaction.HOUR,
                ENTITY_LAYER.Transaction.Transaction.Shiftname,
                ENTITY_LAYER.Transaction.Transaction.FromDate,
                ENTITY_LAYER.Transaction.Transaction.Todate,
                ENTITY_LAYER.Login.Login.UserID,
                ENTITY_LAYER.Transaction.Transaction.Type, ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AndonCall
        public string BL_AndonCall()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_AndonCall", ENTITY_LAYER.Transaction.Transaction.MachineGroup, ENTITY_LAYER.Transaction.Transaction.MachineName, ENTITY_LAYER.Transaction.Transaction.Fortype, ENTITY_LAYER.Transaction.Transaction.Station, ENTITY_LAYER.Transaction.Transaction.ModelName, ENTITY_LAYER.Transaction.Transaction.Time, ENTITY_LAYER.Transaction.Transaction.Shiftname, ENTITY_LAYER.Transaction.Transaction.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
