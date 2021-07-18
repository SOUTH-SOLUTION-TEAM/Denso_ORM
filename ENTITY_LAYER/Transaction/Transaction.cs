using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY_LAYER.Transaction
{
    public class Transaction
    {

        #region Variables
        static string _machineGroup, _modelName, _cyscleTime, _time, _MachineName, _ProductionPlan, _PLCData, _Client, _Shiftname, _Type, _ProblemCode, _RefNo, _NoofItem, _Puls, _Cycletime, _Manpower, _Fortype, _Station, _Process, _Date, _TOTAL_MANPOWER_GIVEN_BY_COMPANY, _ABSENT, _INLINE_OPERATOR,
_OFFLINE_LINE_PARTS_FEEDER,_OUTLINE_LINE_SUPPORTER,_DIRECT_MANPOWER,_SUPERVISOR,_INLINE_MANPOWER,_OFFLINE_LINE_PARTS_FEEDER_MANHOUR,_OUTLINE_LINE_SUPPORTER_MANHOUR,_SUPERVISOR_MANHOUR,_INLINE_MANPOWER_TAKEN_FROM_BANK,
_INLINE_BORROWED_AFTER_MAN_POWER_BANKING,_OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK,_OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING,_OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES,_DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM,_INLINE_MP_GIVEN_TO_BANK,
_OFFLINE_MIZ_GIVEN_TO_BANK,_OUTLINE_SUPPORTER_LENDING,_OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING,_INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members,_SHIFT_WORKIG_HRS,
_LUNCH_TIME_30_Minutes,_TOTAL_WORKING_HOURS,_KY_1_7_Minutes,_MORNING_MEETING_5_Minutes,_TEA_BREAK_10_Minutes,_TEA_BREAK_10_Minutes1,_A5S_CLOSING_5_Minutes,_BREAK_TIME_WITHOUT_LUNCH,_FIXED_TIME,
_ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY,_EXCESS_MP_AFTER_MP_CALCULATION,_OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc,_MULTISKILLING,_ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY,_SKILL_COMPETETION_QCC_BUSINESS_TRIP,_INVENTORY,
_PLANNED_SPECIAL_5S,_DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE,_PLANNED_TRIAL_BY_PE,_ENERGY_SHUTDOWN_GOVT_POWER_ONLY,_FIRE_ASSEMBLY_EVACUATION_DRILL,_NPD_TRIALS,_MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR,_COMPANY_COST_SAVING_ACTIVITY,
_KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH,_KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION,_NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL,_PLANNED_ACTIVITY_BY_MAINTENANCE,
_PLANNED_ACTIVITY_BY_QA_2ND_QA,_VISITOR_ISO_CUSTOMER_AUDIT_TIME,_MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY,_IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc,_PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY,_A5S_BANK_RETURNED_MP,
_MULTISKILLING_BANK_RETURNED_MP,_NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS,_AVAIABLE_WORKING_DIRECT_HRS,_GOOD_PRODUCTON_HRS,_TOTAL_LOSS_HRS,_PART_REJECTION_SCRAP_REWORK,_LOSS_HRS_RATIO,_AVAIABLE_WORKING_HRS,
_GOOD_PRODUCTON_HRS_FOR_DAY,_TOTAL_LOSS_HRS_FOR_DAY,_TOTAL_LOSS_HRS_RATIO,_LINE_IN_CHARGE,_LINE_IN_CHARGE_MAN_HOURS,_USED_AS_MANPOWER,_USED_AS_MANHOUR,_USED_AS_OBSERVER_MANHOUR,_KPI,_DIRECT_HRS,
_GOOD_HRS,_DPR,_BASE_GH_RATIO,_GH_AGAINST_BASE,_DIRECT_HRS1,_GOOD_HRS1,_GH_AGAINST_BASE1,_DPR1,_LHR,_LHR_DAILY,_LEND,_BANK,_OJT,_EXCLUDE,_PURE_EXCLUDE,_MIZ_DH,_OUTLINE_DH,_FQA_GH,_INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING, _FromDate, _Todate, _USED_AS_OBSERVER,_Man,_Hour,_OperationName,_OperationType,_NPDDirHr,_NPDExcHr, _ImporvedGoodHour, _StandardHours, _TotalStandardHour, _NonStandardHour;
        #endregion

        #region Properties
        public static string MachineGroup { get => _machineGroup; set => _machineGroup = value; }
        public static string ModelName { get => _modelName; set => _modelName = value; }
        public static string CyscleTime { get => _cyscleTime; set => _cyscleTime = value; }
        public static string Time { get => _time; set => _time = value; }
        public static string MachineName { get => _MachineName; set => _MachineName = value; }
        public static string ProductionPlan { get => _ProductionPlan; set => _ProductionPlan = value; }
        public static string PLCData { get => _PLCData; set => _PLCData = value; }
        public static string Client { get => _Client; set => _Client = value; }
        public static string Shiftname { get => _Shiftname; set => _Shiftname = value; }
        public static string Type { get => _Type; set => _Type = value; }
        public static string ProblemCode { get => _ProblemCode; set => _ProblemCode = value; }
        public static string RefNo { get => _RefNo; set => _RefNo = value; }
        public static string NoofItem { get => _NoofItem; set => _NoofItem = value; }
        public static string Puls { get => _Puls; set => _Puls = value; }
        public static string Cycletime { get => _Cycletime; set => _Cycletime = value; }
        public static string Manpower { get => _Manpower; set => _Manpower = value; }
        public static string Fortype { get => _Fortype; set => _Fortype = value; }
        public static string Station { get => _Station; set => _Station = value; }
        public static string Process { get => _Process; set => _Process = value; }
        public static string Date { get => _Date; set => _Date = value; }

        public static string TOTAL_MANPOWER_GIVEN_BY_COMPANY { get => _TOTAL_MANPOWER_GIVEN_BY_COMPANY; set => _TOTAL_MANPOWER_GIVEN_BY_COMPANY = value; }
        public static string ABSENT { get => _ABSENT; set => _ABSENT = value; }
        public static string INLINE_OPERATOR { get => _INLINE_OPERATOR; set => _INLINE_OPERATOR = value; }
        public static string OFFLINE_LINE_PARTS_FEEDER { get => _OFFLINE_LINE_PARTS_FEEDER; set => _OFFLINE_LINE_PARTS_FEEDER = value; }
        public static string OUTLINE_LINE_SUPPORTER { get => _OUTLINE_LINE_SUPPORTER; set => _OUTLINE_LINE_SUPPORTER = value; }
        public static string DIRECT_MANPOWER { get => _DIRECT_MANPOWER; set => _DIRECT_MANPOWER = value; }
        public static string SUPERVISOR { get => _SUPERVISOR; set => _SUPERVISOR = value; }
        public static string INLINE_MANPOWER { get => _INLINE_MANPOWER; set => _INLINE_MANPOWER = value; }
        public static string OFFLINE_LINE_PARTS_FEEDER_MANHOUR { get => _OFFLINE_LINE_PARTS_FEEDER_MANHOUR; set => _OFFLINE_LINE_PARTS_FEEDER_MANHOUR = value; }
        public static string OUTLINE_LINE_SUPPORTER_MANHOUR { get => _OUTLINE_LINE_SUPPORTER_MANHOUR; set => _OUTLINE_LINE_SUPPORTER_MANHOUR = value; }
        public static string SUPERVISOR_MANHOUR { get => _SUPERVISOR_MANHOUR; set => _SUPERVISOR_MANHOUR = value; }
        public static string INLINE_MANPOWER_TAKEN_FROM_BANK { get => _INLINE_MANPOWER_TAKEN_FROM_BANK; set => _INLINE_MANPOWER_TAKEN_FROM_BANK = value; }
        public static string INLINE_BORROWED_AFTER_MAN_POWER_BANKING { get => _INLINE_BORROWED_AFTER_MAN_POWER_BANKING; set => _INLINE_BORROWED_AFTER_MAN_POWER_BANKING = value; }
        public static string OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK { get => _OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK; set => _OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK = value; }
        public static string OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING { get => _OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING; set => _OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING = value; }
        public static string OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES { get => _OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES; set => _OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES = value; }
        public static string DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM { get => _DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM; set => _DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM = value; }
        public static string INLINE_MP_GIVEN_TO_BANK { get => _INLINE_MP_GIVEN_TO_BANK; set => _INLINE_MP_GIVEN_TO_BANK = value; }
        public static string OFFLINE_MIZ_GIVEN_TO_BANK { get => _OFFLINE_MIZ_GIVEN_TO_BANK; set => _OFFLINE_MIZ_GIVEN_TO_BANK = value; }
        public static string OUTLINE_SUPPORTER_LENDING { get => _OUTLINE_SUPPORTER_LENDING; set => _OUTLINE_SUPPORTER_LENDING = value; }
        public static string OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING { get => _OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING; set => _OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING = value; }
        public static string INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members { get => _INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members; set => _INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members = value; }
        public static string SHIFT_WORKIG_HRS { get => _SHIFT_WORKIG_HRS; set => _SHIFT_WORKIG_HRS = value; }
        public static string LUNCH_TIME_30_Minutes { get => _LUNCH_TIME_30_Minutes; set => _LUNCH_TIME_30_Minutes = value; }
        public static string TOTAL_WORKING_HOURS { get => _TOTAL_WORKING_HOURS; set => _TOTAL_WORKING_HOURS = value; }
        public static string KY_1_7_Minutes { get => _KY_1_7_Minutes; set => _KY_1_7_Minutes = value; }
        public static string MORNING_MEETING_5_Minutes { get => _MORNING_MEETING_5_Minutes; set => _MORNING_MEETING_5_Minutes = value; }
        public static string TEA_BREAK_10_Minutes { get => _TEA_BREAK_10_Minutes; set => _TEA_BREAK_10_Minutes = value; }
        public static string TEA_BREAK_10_Minutes1 { get => _TEA_BREAK_10_Minutes1; set => _TEA_BREAK_10_Minutes1 = value; }
        public static string A5S_CLOSING_5_Minutes { get => _A5S_CLOSING_5_Minutes; set => _A5S_CLOSING_5_Minutes = value; }
        public static string BREAK_TIME_WITHOUT_LUNCH { get => _BREAK_TIME_WITHOUT_LUNCH; set => _BREAK_TIME_WITHOUT_LUNCH = value; }
        public static string FIXED_TIME { get => _FIXED_TIME; set => _FIXED_TIME = value; }
        public static string ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY { get => _ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY; set => _ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY = value; }
        public static string EXCESS_MP_AFTER_MP_CALCULATION { get => _EXCESS_MP_AFTER_MP_CALCULATION; set => _EXCESS_MP_AFTER_MP_CALCULATION = value; }
        public static string OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc { get => _OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc; set => _OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc = value; }
        public static string MULTISKILLING { get => _MULTISKILLING; set => _MULTISKILLING = value; }
        public static string ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY { get => _ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY; set => _ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY = value; }
        public static string SKILL_COMPETETION_QCC_BUSINESS_TRIP { get => _SKILL_COMPETETION_QCC_BUSINESS_TRIP; set => _SKILL_COMPETETION_QCC_BUSINESS_TRIP = value; }
        public static string INVENTORY { get => _INVENTORY; set => _INVENTORY = value; }
        public static string PLANNED_SPECIAL_5S { get => _PLANNED_SPECIAL_5S; set => _PLANNED_SPECIAL_5S = value; }
        public static string DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE { get => _DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE; set => _DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE = value; }
        public static string PLANNED_TRIAL_BY_PE { get => _PLANNED_TRIAL_BY_PE; set => _PLANNED_TRIAL_BY_PE = value; }
        public static string ENERGY_SHUTDOWN_GOVT_POWER_ONLY { get => _ENERGY_SHUTDOWN_GOVT_POWER_ONLY; set => _ENERGY_SHUTDOWN_GOVT_POWER_ONLY = value; }
        public static string FIRE_ASSEMBLY_EVACUATION_DRILL { get => _FIRE_ASSEMBLY_EVACUATION_DRILL; set => _FIRE_ASSEMBLY_EVACUATION_DRILL = value; }
        public static string NPD_TRIALS { get => _NPD_TRIALS; set => _NPD_TRIALS = value; }
        public static string MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR { get => _MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR; set => _MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR = value; }
        public static string COMPANY_COST_SAVING_ACTIVITY { get => _COMPANY_COST_SAVING_ACTIVITY; set => _COMPANY_COST_SAVING_ACTIVITY = value; }
        public static string KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH { get => _KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH; set => _KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH = value; }
        public static string KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION { get => _KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION; set => _KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION = value; }
        public static string NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL { get => _NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL; set => _NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL = value; }
        public static string PLANNED_ACTIVITY_BY_MAINTENANCE { get => _PLANNED_ACTIVITY_BY_MAINTENANCE; set => _PLANNED_ACTIVITY_BY_MAINTENANCE = value; }
        public static string PLANNED_ACTIVITY_BY_QA_2ND_QA { get => _PLANNED_ACTIVITY_BY_QA_2ND_QA; set => _PLANNED_ACTIVITY_BY_QA_2ND_QA = value; }
        public static string VISITOR_ISO_CUSTOMER_AUDIT_TIME { get => _VISITOR_ISO_CUSTOMER_AUDIT_TIME; set => _VISITOR_ISO_CUSTOMER_AUDIT_TIME = value; }
        public static string MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY { get => _MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY; set => _MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY = value; }
        public static string IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc { get => _IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc; set => _IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc = value; }
        public static string PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY { get => _PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY; set => _PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY = value; }
        public static string A5S_BANK_RETURNED_MP { get => _A5S_BANK_RETURNED_MP; set => _A5S_BANK_RETURNED_MP = value; }
        public static string MULTISKILLING_BANK_RETURNED_MP { get => _MULTISKILLING_BANK_RETURNED_MP; set => _MULTISKILLING_BANK_RETURNED_MP = value; }
        public static string NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS { get => _NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS; set => _NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS = value; }
        public static string AVAIABLE_WORKING_DIRECT_HRS { get => _AVAIABLE_WORKING_DIRECT_HRS; set => _AVAIABLE_WORKING_DIRECT_HRS = value; }
        public static string GOOD_PRODUCTON_HRS { get => _GOOD_PRODUCTON_HRS; set => _GOOD_PRODUCTON_HRS = value; }
        public static string TOTAL_LOSS_HRS { get => _TOTAL_LOSS_HRS; set => _TOTAL_LOSS_HRS = value; }
        public static string PART_REJECTION_SCRAP_REWORK { get => _PART_REJECTION_SCRAP_REWORK; set => _PART_REJECTION_SCRAP_REWORK = value; }
        public static string LOSS_HRS_RATIO { get => _LOSS_HRS_RATIO; set => _LOSS_HRS_RATIO = value; }
        public static string AVAIABLE_WORKING_HRS { get => _AVAIABLE_WORKING_HRS; set => _AVAIABLE_WORKING_HRS = value; }
        public static string GOOD_PRODUCTON_HRS_FOR_DAY { get => _GOOD_PRODUCTON_HRS_FOR_DAY; set => _GOOD_PRODUCTON_HRS_FOR_DAY = value; }
        public static string TOTAL_LOSS_HRS_FOR_DAY { get => _TOTAL_LOSS_HRS_FOR_DAY; set => _TOTAL_LOSS_HRS_FOR_DAY = value; }
        public static string TOTAL_LOSS_HRS_RATIO { get => _TOTAL_LOSS_HRS_RATIO; set => _TOTAL_LOSS_HRS_RATIO = value; }
        public static string LINE_IN_CHARGE { get => _LINE_IN_CHARGE; set => _LINE_IN_CHARGE = value; }
        public static string LINE_IN_CHARGE_MAN_HOURS { get => _LINE_IN_CHARGE_MAN_HOURS; set => _LINE_IN_CHARGE_MAN_HOURS = value; }
        public static string USED_AS_MANPOWER { get => _USED_AS_MANPOWER; set => _USED_AS_MANPOWER = value; }
        public static string USED_AS_MANHOUR { get => _USED_AS_MANHOUR; set => _USED_AS_MANHOUR = value; }
        public static string USED_AS_OBSERVER_MANHOUR { get => _USED_AS_OBSERVER_MANHOUR; set => _USED_AS_OBSERVER_MANHOUR = value; }
        public static string KPI { get => _KPI; set => _KPI = value; }
        public static string DIRECT_HRS { get => _DIRECT_HRS; set => _DIRECT_HRS = value; }
        public static string GOOD_HRS { get => _GOOD_HRS; set => _GOOD_HRS = value; }
        public static string DPR { get => _DPR; set => _DPR = value; }
        public static string BASE_GH_RATIO { get => _BASE_GH_RATIO; set => _BASE_GH_RATIO = value; }
        public static string GH_AGAINST_BASE { get => _GH_AGAINST_BASE; set => _GH_AGAINST_BASE = value; }
        public static string DIRECT_HRS1 { get => _DIRECT_HRS1; set => _DIRECT_HRS1 = value; }
        public static string GOOD_HRS1 { get => _GOOD_HRS1; set => _GOOD_HRS1 = value; }
        public static string GH_AGAINST_BASE1 { get => _GH_AGAINST_BASE1; set => _GH_AGAINST_BASE1 = value; }
        public static string DPR1 { get => _DPR1; set => _DPR1 = value; }
        public static string LHR { get => _LHR; set => _LHR = value; }
        public static string LHR_DAILY { get => _LHR_DAILY; set => _LHR_DAILY = value; }
        public static string LEND { get => _LEND; set => _LEND = value; }
        public static string BANK { get => _BANK; set => _BANK = value; }
        public static string OJT { get => _OJT; set => _OJT = value; }
        public static string EXCLUDE { get => _EXCLUDE; set => _EXCLUDE = value; }
        public static string PURE_EXCLUDE { get => _PURE_EXCLUDE; set => _PURE_EXCLUDE = value; }
        public static string MIZ_DH { get => _MIZ_DH; set => _MIZ_DH = value; }
        public static string OUTLINE_DH { get => _OUTLINE_DH; set => _OUTLINE_DH = value; }
        public static string FQA_GH { get => _FQA_GH; set => _FQA_GH = value; }
        public static string INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING { get => _INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING; set => _INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING = value; }

        public static string FromDate { get => _FromDate; set => _FromDate = value; }
        public static string Todate { get => _Todate; set => _Todate = value; }
        public static string USED_AS_OBSERVER { get => _USED_AS_OBSERVER; set => _USED_AS_OBSERVER = value; }

        public static string HOUR { get => _Hour; set => _Hour = value; }
        public static string MAN { get => _Man; set => _Man = value; }
        public static string OPERATIONNAME { get => _OperationName; set => _OperationName = value; }
        public static string OPERATIONTYPE { get => _OperationType; set => _OperationType = value; }
        public static string NPDDIRHR { get => _NPDDirHr; set => _NPDDirHr = value; }
        public static string NPDEXCHR { get => _NPDExcHr; set => _NPDExcHr = value; }
        public static string ImporvedGoodHour { get => _ImporvedGoodHour; set => _ImporvedGoodHour = value; }
        public static string StandardHours { get => _StandardHours; set => _StandardHours = value; }
        public static string TotalStandardHour { get => _TotalStandardHour; set => _TotalStandardHour = value; }
        public static string NonStandardHour { get => _NonStandardHour; set => _NonStandardHour = value; }

        #endregion
    }
}
