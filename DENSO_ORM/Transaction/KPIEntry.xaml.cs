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
using System.Windows.Shapes;


namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for KPIEntry.xaml
    /// </summary>
    public partial class KPIEntry : Page
    {
        public KPIEntry()
        {
            InitializeComponent();
        }

        #region Variable and Objects
        BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Trns = new BUSINESS_LAYER.Transaction.Transaction();
        BUSINESS_LAYER.Masters.Masters obj_Mast = new BUSINESS_LAYER.Masters.Masters();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        decimal TotalQty = 0, TotalBaseTime = 0, TotalStdTime = 0, TotalRejQty = 0;
        int RefNo = 0;
        string txtSHIFT_WORKIG_HRS = "0", txtLUNCH_TIME_30_Minutes = "0", txtTOTAL_WORKING_HOURS = "0", txtKY_1_7_Minutes = "0", txtMORNING_MEETING_5_Minutes = "0"
              , txtTEA_BREAK_10_Minutes = "0", txtTEA_BREAK_10_Minutes1 = "0", txtA5S_CLOSING_5_Minutes = "0", txtBREAK_TIME_WITHOUT_LUNCH = "0", txtFIXED_TIME = "", 
            txtDIRECT_MANPOWER = "0", txtAVAIABLE_WORKING_HRS = "0", txtGOOD_PRODUCTON_HRS_FOR_DAY = "0", txtTOTAL_LOSS_HRS_FOR_DAY = "0", txtTOTAL_LOSS_HRS_RATIO = "0", 
            txtLINE_IN_CHARGE_MAN_HOURS = "0", txtKPI = "0", txtDIRECT_HRS = "0", txtGOOD_HRS = "0", txtBASE_GH_RATIO = "0.88", txtDIRECT_HRS1 = "0", 
            txtGOOD_HRS1 = "0", txtGH_AGAINST_BASE1 = "0", txtTotalBase="0", txtTotalStd = "0", txtDPR1 = "0", txtLHR_DAILY = "0", txtLEND = "0", txtBANK = "0", txtOJT = "0", txtEXCLUDE = "0", txtPURE_EXCLUDE = "0", txtMIZDH = "0", txtOUTLINEDH = "0", txtFQAGH = "0", txtUSEDASOBSERVERMANHOUR="0", txtImporvedGoodHour="0",txtStandardHours="0",txtTotalStandardHour="0",txtNonStandardHour="0";


        #endregion
        private void TxtOUTLINE_SUPPORTER_LENDING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOUTLINE_SUPPORTER_LENDING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINEMPLENDINGAFTERMANPOWERBANKING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtEXCESS_MP_AFTER_MP_CALCULATION_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtEXCESS_MP_AFTER_MP_CALCULATION.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtMULTISKILLING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtMULTISKILLING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtSKILL_COMPETETION_QCC_BUSINESS_TRIP_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINVENTORY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINVENTORY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPLANNED_SPECIAL_5S_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPLANNED_SPECIAL_5S.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPLANNED_TRIAL_BY_PE_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPLANNED_TRIAL_BY_PE.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtENERGY_SHUTDOWN_GOVT_POWER_ONLY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtFIRE_ASSEMBLY_EVACUATION_DRILL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtNPD_TRIALS_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtNPD_TRIALS.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtCOMPANY_COST_SAVING_ACTIVITY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtCOMPANY_COST_SAVING_ACTIVITY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPLANNED_ACTIVITY_BY_MAINTENANCE_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    

        private void TxtPLANNED_ACTIVITY_BY_QA_2ND_QA_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtVISITOR_ISO_CUSTOMER_AUDIT_TIME_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtA5S_BANK_RETURNED_MP_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtA5S_BANK_RETURNED_MP.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_MIZ_GIVEN_TO_BANK_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_MIZ_GIVEN_TO_BANK.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINE_MP_GIVEN_TO_BANK_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINE_MP_GIVEN_TO_BANK.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINE_BORROWED_AFTER_MAN_POWER_BANKING_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINE_MANPOWER_TAKEN_FROM_BANK_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtSUPERVISOR_MANHOUR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSUPERVISOR_MANHOUR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOUTLINE_LINE_SUPPORTER_MANHOUR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_LINE_PARTS_FEEDER_MANHOUR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINE_MANPOWER_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINE_MANPOWER.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtUSED_AS_OBSERVER_MANPOWER_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtLINE_IN_CHARGE_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtLINE_IN_CHARGE.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtSUPERVISOR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSUPERVISOR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOUTLINE_LINE_SUPPORTER_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOUTLINE_LINE_SUPPORTER.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtOFFLINE_LINE_PARTS_FEEDER_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtOFFLINE_LINE_PARTS_FEEDER.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtINLINE_OPERATOR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtINLINE_OPERATOR.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtABSENT_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtABSENT.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtTOTAL_MANPOWER_GIVEN_BY_COMPANY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowDateTime();
                txtlinegrp.Text = CommonClasses.CommonVariable.MachineGroup;
                txtlinename.Text = CommonClasses.CommonVariable.MachineName;
                Transaction("LoadDetails");
                Calculation();
                txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Focus();

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = txtlinegrp.Text;
                ENTITY_LAYER.Transaction.Transaction.MachineName = txtlinename.Text;
                ENTITY_LAYER.Transaction.Transaction.TOTAL_MANPOWER_GIVEN_BY_COMPANY = txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Text;
                ENTITY_LAYER.Transaction.Transaction.ABSENT = txtABSENT.Text;
                ENTITY_LAYER.Transaction.Transaction.INLINE_OPERATOR = txtINLINE_OPERATOR.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER = txtOFFLINE_LINE_PARTS_FEEDER.Text;
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER = txtOUTLINE_LINE_SUPPORTER.Text;
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MANPOWER = txtDIRECT_MANPOWER;
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR = txtSUPERVISOR.Text;
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER = txtINLINE_MANPOWER.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_LINE_PARTS_FEEDER_MANHOUR = txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text;
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_LINE_SUPPORTER_MANHOUR = txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text;
                ENTITY_LAYER.Transaction.Transaction.SUPERVISOR_MANHOUR = txtSUPERVISOR_MANHOUR.Text;
                ENTITY_LAYER.Transaction.Transaction.INLINE_MANPOWER_TAKEN_FROM_BANK = txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text;
                ENTITY_LAYER.Transaction.Transaction.INLINE_BORROWED_AFTER_MAN_POWER_BANKING = txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK = txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING = txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text;
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES = txtOUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES.Text;
                ENTITY_LAYER.Transaction.Transaction.DIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM = txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text;
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_GIVEN_TO_BANK = txtINLINE_MP_GIVEN_TO_BANK.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_GIVEN_TO_BANK = txtOFFLINE_MIZ_GIVEN_TO_BANK.Text;
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_SUPPORTER_LENDING = txtOUTLINE_SUPPORTER_LENDING.Text;
                ENTITY_LAYER.Transaction.Transaction.OFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING = txtOFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING.Text;
                ENTITY_LAYER.Transaction.Transaction.INDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members = txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text;
           
                ENTITY_LAYER.Transaction.Transaction.SHIFT_WORKIG_HRS = txtSHIFT_WORKIG_HRS;
                ENTITY_LAYER.Transaction.Transaction.LUNCH_TIME_30_Minutes = txtLUNCH_TIME_30_Minutes;
                ENTITY_LAYER.Transaction.Transaction.TOTAL_WORKING_HOURS = txtTOTAL_WORKING_HOURS;
                ENTITY_LAYER.Transaction.Transaction.KY_1_7_Minutes = txtKY_1_7_Minutes;
                ENTITY_LAYER.Transaction.Transaction.MORNING_MEETING_5_Minutes = txtMORNING_MEETING_5_Minutes;
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes = txtTEA_BREAK_10_Minutes;
                ENTITY_LAYER.Transaction.Transaction.TEA_BREAK_10_Minutes1 = txtTEA_BREAK_10_Minutes1;
                ENTITY_LAYER.Transaction.Transaction.A5S_CLOSING_5_Minutes = txtA5S_CLOSING_5_Minutes;
                ENTITY_LAYER.Transaction.Transaction.BREAK_TIME_WITHOUT_LUNCH = txtBREAK_TIME_WITHOUT_LUNCH;
                ENTITY_LAYER.Transaction.Transaction.FIXED_TIME = txtFIXED_TIME;
                ENTITY_LAYER.Transaction.Transaction.ON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY = txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text;
                ENTITY_LAYER.Transaction.Transaction.EXCESS_MP_AFTER_MP_CALCULATION = txtEXCESS_MP_AFTER_MP_CALCULATION.Text;
                ENTITY_LAYER.Transaction.Transaction.OTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc = txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text;
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING = txtMULTISKILLING.Text;
                ENTITY_LAYER.Transaction.Transaction.ALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY = txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text;
                ENTITY_LAYER.Transaction.Transaction.SKILL_COMPETETION_QCC_BUSINESS_TRIP = txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text;
                ENTITY_LAYER.Transaction.Transaction.INVENTORY = txtINVENTORY.Text;
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SPECIAL_5S = txtPLANNED_SPECIAL_5S.Text;
                ENTITY_LAYER.Transaction.Transaction.DELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE = txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text;
                ENTITY_LAYER.Transaction.Transaction.PLANNED_TRIAL_BY_PE = txtPLANNED_TRIAL_BY_PE.Text;
                ENTITY_LAYER.Transaction.Transaction.ENERGY_SHUTDOWN_GOVT_POWER_ONLY = txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text;
                ENTITY_LAYER.Transaction.Transaction.FIRE_ASSEMBLY_EVACUATION_DRILL = txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text;
                ENTITY_LAYER.Transaction.Transaction.NPD_TRIALS = txtNPD_TRIALS.Text;
                ENTITY_LAYER.Transaction.Transaction.MEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR = txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text;
                ENTITY_LAYER.Transaction.Transaction.COMPANY_COST_SAVING_ACTIVITY = txtCOMPANY_COST_SAVING_ACTIVITY.Text;
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH = txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text;
                ENTITY_LAYER.Transaction.Transaction.KANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION = txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text;
                ENTITY_LAYER.Transaction.Transaction.NO_KANBAN_DUE_TO_CUSTOMER_NO_PULL = txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text;
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_MAINTENANCE = txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text;
                ENTITY_LAYER.Transaction.Transaction.PLANNED_ACTIVITY_BY_QA_2ND_QA = txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text;
                ENTITY_LAYER.Transaction.Transaction.VISITOR_ISO_CUSTOMER_AUDIT_TIME = txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text;
                ENTITY_LAYER.Transaction.Transaction.MAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY = txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text;
                ENTITY_LAYER.Transaction.Transaction.IMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc = txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text;
                ENTITY_LAYER.Transaction.Transaction.PLANNED_SNACKS_BREAK_AS_PER_HR_POLICY = txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text;
                ENTITY_LAYER.Transaction.Transaction.A5S_BANK_RETURNED_MP = txtA5S_BANK_RETURNED_MP.Text;
                ENTITY_LAYER.Transaction.Transaction.MULTISKILLING_BANK_RETURNED_MP = txtMULTISKILLING_BANK_RETURNED_MP.Text;
                ENTITY_LAYER.Transaction.Transaction.NON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS = txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text;
                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_DIRECT_HRS = txtAVAIABLE_WORKING_DIRECT_HRS.Text;
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS = txtGOOD_PRODUCTON_HRS.Text;
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS = txtTOTAL_LOSS_HRS.Text;
                ENTITY_LAYER.Transaction.Transaction.PART_REJECTION_SCRAP_REWORK = txtPART_REJECTION_SCRAP_REWORK.Text;
                ENTITY_LAYER.Transaction.Transaction.LOSS_HRS_RATIO = txtLOSS_HRS_RATIO.Text;
               


                ENTITY_LAYER.Transaction.Transaction.AVAIABLE_WORKING_HRS = txtAVAIABLE_WORKING_HRS;
                ENTITY_LAYER.Transaction.Transaction.GOOD_PRODUCTON_HRS_FOR_DAY = txtGOOD_PRODUCTON_HRS_FOR_DAY;
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_FOR_DAY = txtTOTAL_LOSS_HRS_FOR_DAY;
                ENTITY_LAYER.Transaction.Transaction.TOTAL_LOSS_HRS_RATIO = txtTOTAL_LOSS_HRS_RATIO;
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE = txtLINE_IN_CHARGE.Text;
                ENTITY_LAYER.Transaction.Transaction.LINE_IN_CHARGE_MAN_HOURS = txtLINE_IN_CHARGE_MAN_HOURS;
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANPOWER = "0";
                ENTITY_LAYER.Transaction.Transaction.USED_AS_MANHOUR = "0";
                ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER_MANHOUR = txtUSED_AS_OBSERVER_MANPOWER.Text;
                ENTITY_LAYER.Transaction.Transaction.USED_AS_OBSERVER = txtUSED_AS_OBSERVER_MANPOWER.Text;
                ENTITY_LAYER.Transaction.Transaction.KPI = txtKPI;
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS = txtDIRECT_HRS;
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS = txtGOOD_HRS;
                ENTITY_LAYER.Transaction.Transaction.DPR = txtDPR.Text;
                ENTITY_LAYER.Transaction.Transaction.BASE_GH_RATIO = txtBASE_GH_RATIO;
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE = txtGH_AGAINST_BASE.Text;
                ENTITY_LAYER.Transaction.Transaction.DIRECT_HRS1 = txtDIRECT_HRS1;
                ENTITY_LAYER.Transaction.Transaction.GOOD_HRS1 = txtGOOD_HRS1;
                ENTITY_LAYER.Transaction.Transaction.GH_AGAINST_BASE1 = txtGH_AGAINST_BASE1;
                ENTITY_LAYER.Transaction.Transaction.DPR1 = txtDPR1;
                ENTITY_LAYER.Transaction.Transaction.LHR = txtLHR.Text;
                ENTITY_LAYER.Transaction.Transaction.LHR_DAILY = txtLHR_DAILY;
                ENTITY_LAYER.Transaction.Transaction.LEND = txtLEND;
                ENTITY_LAYER.Transaction.Transaction.BANK = txtBANK;
                ENTITY_LAYER.Transaction.Transaction.OJT = txtOJT;
                ENTITY_LAYER.Transaction.Transaction.EXCLUDE = txtEXCLUDE;
                ENTITY_LAYER.Transaction.Transaction.PURE_EXCLUDE = txtPURE_EXCLUDE;
                ENTITY_LAYER.Transaction.Transaction.MIZ_DH = txtMIZDH;
                ENTITY_LAYER.Transaction.Transaction.OUTLINE_DH = txtOUTLINEDH;
                ENTITY_LAYER.Transaction.Transaction.FQA_GH = txtFQAGH;
                ENTITY_LAYER.Transaction.Transaction.INLINE_MP_LENDING_AFTER_MAN_POWER_BANKING = txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text;
                ENTITY_LAYER.Transaction.Transaction.RefNo = RefNo.ToString();
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.NPDDIRHR = txtNPDDirecthour.Text;
                ENTITY_LAYER.Transaction.Transaction.NPDEXCHR = txtNPDExcludeHour.Text;
                ENTITY_LAYER.Transaction.Transaction.ImporvedGoodHour = txtImporvedGoodHour;
                ENTITY_LAYER.Transaction.Transaction.StandardHours = txtStandardHours;
                ENTITY_LAYER.Transaction.Transaction.TotalStandardHour = txtTotalStandardHour;
                ENTITY_LAYER.Transaction.Transaction.NonStandardHour = txtNonStandardHour;
                CommonClasses.CommonVariable.Result = obj_Trns.BL_KPIEntryTransaction();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    ClearAll();
                }
                else if (CommonClasses.CommonVariable.Result == "Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    ClearAll();
                }
                else if (CommonClasses.CommonVariable.Result == "Duplicate")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Duplicate, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                else if (CommonClasses.CommonVariable.Result != "Deleted")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }

            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Trns.BL_KPIEntryDetails().Tables[0];
                dvgKPIDeatils.ItemsSource = dt.DefaultView;
            }
            if (Type == "LoadModelDetails")
            {

                TotalQty = 0; TotalBaseTime = 0; TotalStdTime = 0; TotalRejQty = 0;

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.FromDate = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss"); ;
                ENTITY_LAYER.Transaction.Transaction.Todate = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy HH:mm:ss");
                ENTITY_LAYER.Transaction.Transaction.Shiftname = cmbShiftName.Text;
                DataSet ds = obj_Trns.BL_KPIEntryDetails();
                dvgModelDeatils.ItemsSource = ds.Tables[1].DefaultView;
                if (ds.Tables[0].Rows.Count > 0)
                    txtINLINE_OPERATOR.Text = ds.Tables[0].Rows[0][0].ToString();
                else
                    txtINLINE_OPERATOR.Text = "0";

                if (ds.Tables[2].Rows.Count > 0)
                {
                    txtSHIFT_WORKIG_HRS = ds.Tables[2].Rows[0]["TotalWorkingHour"].ToString();
                    txtLUNCH_TIME_30_Minutes = ds.Tables[2].Rows[0]["Lunch"].ToString();
                    txtTEA_BREAK_10_Minutes = ds.Tables[2].Rows[0]["Teabreak1"].ToString();
                    txtTEA_BREAK_10_Minutes1 = ds.Tables[2].Rows[0]["Teabreak2"].ToString();
                    txtKY_1_7_Minutes = ds.Tables[2].Rows[0]["ky"].ToString();
                    txtMORNING_MEETING_5_Minutes = ds.Tables[2].Rows[0]["MorningMeet"].ToString();
                    txtA5S_CLOSING_5_Minutes = ds.Tables[2].Rows[0]["CloseMeet"].ToString();

                    txtTOTAL_WORKING_HOURS = Convert.ToString(Math.Round(Convert.ToDecimal(txtSHIFT_WORKIG_HRS) - Convert.ToDecimal(txtLUNCH_TIME_30_Minutes), 2));
                    txtBREAK_TIME_WITHOUT_LUNCH = Convert.ToString(Math.Round(Convert.ToDecimal(txtKY_1_7_Minutes) + Convert.ToDecimal(txtMORNING_MEETING_5_Minutes) + Convert.ToDecimal(txtTEA_BREAK_10_Minutes) + Convert.ToDecimal(txtTEA_BREAK_10_Minutes1) + Convert.ToDecimal(txtA5S_CLOSING_5_Minutes), 2));
                    txtFIXED_TIME = Convert.ToString(Math.Round(Convert.ToDecimal(txtBREAK_TIME_WITHOUT_LUNCH) * Convert.ToDecimal(txtINLINE_OPERATOR.Text), 2));
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    txtBASE_GH_RATIO = ds.Tables[3].Rows[0]["BaseGHRatio"].ToString();
                }
                
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    TotalBaseTime = Math.Round(TotalBaseTime + Convert.ToDecimal(ds.Tables[1].Rows[i]["BaseTime"]), 2);
                    TotalQty = Math.Round(TotalQty + Convert.ToDecimal(ds.Tables[1].Rows[i]["OKQty"]), 2);
                    TotalStdTime = Math.Round(TotalStdTime + Convert.ToDecimal(ds.Tables[1].Rows[i]["STDTime"]), 2);
                    TotalRejQty = Math.Round(TotalRejQty + Convert.ToDecimal(ds.Tables[1].Rows[i]["RejQty"]), 2);

                    txtTotalBase = TotalBaseTime.ToString();
                    txtTotalOKQty.Text = TotalQty.ToString();
                    txtTotalStd = TotalStdTime.ToString();
                    txtTotalREjQty.Text = TotalRejQty.ToString();
                }

                    //(AVAIABLE WORKING(DIRECT) HRS / ((GH AGAINST BASE/ BASE GH RATIO))

            }

        }
    
        private void Calculation()
        {
            if (txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text == "")
            {
                txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text = "0";
            }
            else if (txtEXCESS_MP_AFTER_MP_CALCULATION.Text == "")
            {
                txtEXCESS_MP_AFTER_MP_CALCULATION.Text = "0";

            }
            else if (txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text == "")
            {
                txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text = "0";

            }
            else if (txtMULTISKILLING.Text == "")
            {
                txtMULTISKILLING.Text = "0";

            }
            else if (txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text == "")
            {
                txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text = "0";

            }
            else if (txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text == "")
            {
                txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text = "0";

            }
            else if (txtINVENTORY.Text == "")
            {
                txtINVENTORY.Text = "0";

            }
            else if (txtPLANNED_SPECIAL_5S.Text == "")
            {
                txtPLANNED_SPECIAL_5S.Text = "0";

            }
            else if (txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text == "")
            {
                txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text = "0";

            }
            else if (txtPLANNED_TRIAL_BY_PE.Text == "")
            {
                txtPLANNED_TRIAL_BY_PE.Text = "0";

            }
            else if (txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text == "")
            {
                txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text = "0";

            }
            else if (txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text == "")
            {
                txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text = "0";

            }
            else if (txtNPD_TRIALS.Text == "")
            {
                txtNPD_TRIALS.Text = "0";

            }
            else if (txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text == "")
            {
                txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text = "0";

            }
            else if (txtCOMPANY_COST_SAVING_ACTIVITY.Text == "")
            {
                txtCOMPANY_COST_SAVING_ACTIVITY.Text = "0";

            }
            else if (txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text == "")
            {
                txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text = "0";

            }
            else if (txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text == "")
            {
                txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text = "0";

            }
            else if (txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text == "")
            {
                txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text = "0";

            }
            else if (txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text == "")
            {
                txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text = "0";

            }
            else if (txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text == "")
            {
                txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text = "0";

            }
            else if (txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text == "")
            {
                txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text = "0";

            }
            else if (txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text == "")
            {
                txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text = "0";

            }
            else if (txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text == "")
            {
                txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text = "0";

            }
            else if (txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text == "")
            {
                txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text = "0";

            }
            else if (txtA5S_BANK_RETURNED_MP.Text == "")
            {
                txtA5S_BANK_RETURNED_MP.Text = "0";

            }
            else if (txtMULTISKILLING_BANK_RETURNED_MP.Text == "")
            {
                txtMULTISKILLING_BANK_RETURNED_MP.Text = "0";

            }

            else if (txtINLINE_OPERATOR.Text == "")
            {
                txtINLINE_OPERATOR.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtINLINE_MANPOWER.Text == "")
            {
                txtINLINE_MANPOWER.Text = "0";

            }
            else if (txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text == "")
            {
                txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text = "0";

            }
            else if (txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text == "")
            {
                txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text = "0";

            }
            else if (txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text == "")
            {
                txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text = "0";

            }
            else if (txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text == "")
            {
                txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text = "0";

            }
            else if (txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text == "")
            {
                txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = "0";

            }
            else if (txtINLINE_MP_GIVEN_TO_BANK.Text == "")
            {
                txtINLINE_MP_GIVEN_TO_BANK.Text = "0";

            }
            else if (txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text == "")
            {
                txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text = "0";

            }
            else if (txtINLINE_OPERATOR.Text == "")
            {
                txtINLINE_OPERATOR.Text = "0";

            }
            else if (txtBREAK_TIME_WITHOUT_LUNCH == "")
            {
                txtBREAK_TIME_WITHOUT_LUNCH = "0";

            }
            else if (TotalStdTime.ToString() == "")
            {
                TotalStdTime = 0;

            }
            else if (TotalQty.ToString() == "")
            {
                TotalQty = 0;

            }
            else if (TotalRejQty.ToString() == "")
            {
                TotalRejQty = 0;

            }
            else if (TotalBaseTime.ToString() == "0")
            {
                TotalBaseTime = 0;

            }
            else if (txtAVAIABLE_WORKING_DIRECT_HRS.Text == "")
            {
                txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";

            }
            else if (txtGOOD_PRODUCTON_HRS.Text == "")
            {
                txtGOOD_PRODUCTON_HRS.Text = "0";

            }
            else if (txtAVAIABLE_WORKING_HRS == "")
            {
                txtAVAIABLE_WORKING_HRS = "0";

            }
            else if (txtGOOD_PRODUCTON_HRS_FOR_DAY == "")
            {
                txtGOOD_PRODUCTON_HRS_FOR_DAY = "0";

            }
            else if (txtGH_AGAINST_BASE.Text == "")
            {
                txtGH_AGAINST_BASE.Text = "0";

            }
            else if (txtAVAIABLE_WORKING_DIRECT_HRS.Text == "")
            {
                txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";

            }
            else if (txtGOOD_PRODUCTON_HRS.Text == "")
            {
                txtGOOD_PRODUCTON_HRS.Text = "0";

            }
            else if (txtAVAIABLE_WORKING_DIRECT_HRS.Text == "")
            {
                txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";

            }
            else if (txtTOTAL_LOSS_HRS.Text == "")
            {
                txtTOTAL_LOSS_HRS.Text = "0";

            }
            else if (txtOFFLINE_LINE_PARTS_FEEDER.Text == "")
            {
                txtOFFLINE_LINE_PARTS_FEEDER.Text = "0";

            }
            else if (txtOUTLINE_LINE_SUPPORTER.Text == "")
            {
                txtOUTLINE_LINE_SUPPORTER.Text = "0";

            }
            else if (txtINLINE_OPERATOR.Text == "")
            {
                txtINLINE_OPERATOR.Text = "0";

            }
            else if (txtLINE_IN_CHARGE.Text == "")
            {
                txtLINE_IN_CHARGE.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtOFFLINE_LINE_PARTS_FEEDER.Text == "")
            {
                txtOFFLINE_LINE_PARTS_FEEDER.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text == "")
            {
                txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text = "0";

            }
            else if (txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text == "")
            {
                txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text = "0";

            }
            else if (txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text == "")
            {
                txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text = "0";

            }
            else if (txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text == "")
            {
                txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text = "0";

            }
            else if (txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text == "")
            {
                txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text = "0";

            }

            else if (txtOUTLINE_LINE_SUPPORTER.Text == "")
            {
                txtOUTLINE_LINE_SUPPORTER.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text == "")
            {
                txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text = "0";

            }
            else if (txtSUPERVISOR_MANHOUR.Text == "")
            {
                txtSUPERVISOR_MANHOUR.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtSUPERVISOR.Text == "")
            {
                txtSUPERVISOR.Text = "0";

            }
            else if (txtTOTAL_WORKING_HOURS == "")
            {
                txtTOTAL_WORKING_HOURS = "0";

            }
            else if (txtSUPERVISOR_MANHOUR.Text == "")
            {
                txtSUPERVISOR_MANHOUR.Text = "0";

            }

            else if (txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text == "")
            {
                txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = "0";

            }
            else if (txtAVAIABLE_WORKING_DIRECT_HRS.Text == "")
            {
                txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";

            }
            else if (txtGH_AGAINST_BASE.Text == "")
            {
                txtGH_AGAINST_BASE.Text = "0";

            }
            else if (txtBASE_GH_RATIO == "")
            {
                txtBASE_GH_RATIO = "0";

            }
            else if (txtNPDDirecthour.Text == "")
            {
                txtNPDDirecthour.Text = "0";

            }
            else if (txtNPDExcludeHour.Text == "")
            {
                txtNPDExcludeHour.Text = "0";

            }
            else if (txtImporvedGoodHour == "")
            {
                txtImporvedGoodHour = "0";

            }
            else if (txtStandardHours == "")
            {
                txtStandardHours = "0";

            }
            else if (txtTotalStandardHour == "")
            {
                txtTotalStandardHour = "0";

            }
            else if (txtNonStandardHour == "")
            {
                txtNonStandardHour = "0";

            }
            else
            {
                txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = Math.Round(Convert.ToDecimal(txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text) +
                      Convert.ToDecimal(txtEXCESS_MP_AFTER_MP_CALCULATION.Text) +
                      Convert.ToDecimal(txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text) +
                      Convert.ToDecimal(txtMULTISKILLING.Text) +
                      Convert.ToDecimal(txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text) +
                      Convert.ToDecimal(txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text) +
                      Convert.ToDecimal(txtINVENTORY.Text) +
                      Convert.ToDecimal(txtPLANNED_SPECIAL_5S.Text) +
                      Convert.ToDecimal(txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text) +
                      Convert.ToDecimal(txtPLANNED_TRIAL_BY_PE.Text) +
                      Convert.ToDecimal(txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text) +
                      Convert.ToDecimal(txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text) +
                      Convert.ToDecimal(txtNPD_TRIALS.Text) +
                      Convert.ToDecimal(txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text) +
                      Convert.ToDecimal(txtCOMPANY_COST_SAVING_ACTIVITY.Text) +
                      Convert.ToDecimal(txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text) +
                      Convert.ToDecimal(txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text) +
                      Convert.ToDecimal(txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text) +
                      Convert.ToDecimal(txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text) +
                      Convert.ToDecimal(txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text) +
                      Convert.ToDecimal(txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text) +
                      Convert.ToDecimal(txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text) +
                      Convert.ToDecimal(txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text) +
                      Convert.ToDecimal(txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text) +
                      Convert.ToDecimal(txtA5S_BANK_RETURNED_MP.Text) +
                      Convert.ToDecimal(txtMULTISKILLING_BANK_RETURNED_MP.Text), 2).ToString();

                txtNonStandardHour = Math.Round(Convert.ToDecimal(txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text) +
                     Convert.ToDecimal(txtEXCESS_MP_AFTER_MP_CALCULATION.Text) +
                     Convert.ToDecimal(txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text) +
                     Convert.ToDecimal(txtMULTISKILLING.Text) +
                     Convert.ToDecimal(txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text) +
                     Convert.ToDecimal(txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text) +
                     Convert.ToDecimal(txtINVENTORY.Text) +
                     Convert.ToDecimal(txtPLANNED_SPECIAL_5S.Text) +
                     Convert.ToDecimal(txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text) +
                     Convert.ToDecimal(txtPLANNED_TRIAL_BY_PE.Text) +
                     Convert.ToDecimal(txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text) +
                     Convert.ToDecimal(txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text) +
                     Convert.ToDecimal(txtNPD_TRIALS.Text) +
                     Convert.ToDecimal(txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text) +
                     Convert.ToDecimal(txtCOMPANY_COST_SAVING_ACTIVITY.Text) +
                     Convert.ToDecimal(txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text) +
                     Convert.ToDecimal(txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text) +
                     Convert.ToDecimal(txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text) +
                     Convert.ToDecimal(txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text) +
                     Convert.ToDecimal(txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text) +
                     Convert.ToDecimal(txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text) +
                     Convert.ToDecimal(txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text) +
                     Convert.ToDecimal(txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text) +
                     Convert.ToDecimal(txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text) +
                     Convert.ToDecimal(txtA5S_BANK_RETURNED_MP.Text), 2).ToString();

                txtAVAIABLE_WORKING_DIRECT_HRS.Text = Math.Round(Convert.ToDecimal(txtINLINE_OPERATOR.Text) *
                  Convert.ToDecimal(txtTOTAL_WORKING_HOURS) +
                  Convert.ToDecimal(txtINLINE_MANPOWER.Text) +
                  Convert.ToDecimal(txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text) +
                  Convert.ToDecimal(txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text) +
                  Convert.ToDecimal(txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text) +
                  Convert.ToDecimal(txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text) +
                  Convert.ToDecimal(txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text) +
                  Convert.ToDecimal(txtINLINE_MP_GIVEN_TO_BANK.Text) +
                  Convert.ToDecimal(txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text) -
                  (Convert.ToDecimal(txtINLINE_OPERATOR.Text) * Convert.ToDecimal(txtBREAK_TIME_WITHOUT_LUNCH)), 2).ToString();

                txtTotalStandardHour = (Convert.ToDecimal(txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text) + txtFIXED_TIME + txtLINE_IN_CHARGE_MAN_HOURS + txtOUTLINEDH).ToString();

                txtGOOD_PRODUCTON_HRS.Text = Math.Round((TotalStdTime * TotalQty) / 3600, 2).ToString();
                txtPART_REJECTION_SCRAP_REWORK.Text = Math.Round((TotalStdTime * TotalRejQty) / 3600, 2).ToString();
                txtGH_AGAINST_BASE.Text = Math.Round((TotalBaseTime * TotalQty) / 3600, 2).ToString();
                txtFQAGH = Math.Round((TotalStdTime * TotalQty) / 3600, 2).ToString();
                txtAVAIABLE_WORKING_HRS = txtAVAIABLE_WORKING_DIRECT_HRS.Text;
                txtGOOD_PRODUCTON_HRS_FOR_DAY = txtGOOD_PRODUCTON_HRS.Text;
                txtDIRECT_HRS = txtAVAIABLE_WORKING_HRS;
                txtGOOD_HRS = txtGOOD_PRODUCTON_HRS_FOR_DAY;
                txtGH_AGAINST_BASE1 = txtGH_AGAINST_BASE.Text;
                txtImporvedGoodHour = Math.Round((TotalBaseTime - TotalStdTime) * TotalQty).ToString();             //AVAIABLE WORKING(DIRECT) HRS - GOOD PRODUCTON HRS
                txtTOTAL_LOSS_HRS.Text = Math.Round(Convert.ToDecimal(txtAVAIABLE_WORKING_DIRECT_HRS.Text) - Convert.ToDecimal(txtGOOD_PRODUCTON_HRS.Text), 2).ToString();
                if (txtGOOD_PRODUCTON_HRS.Text != "0" && txtAVAIABLE_WORKING_DIRECT_HRS.Text!="0")
                {
                    txtLOSS_HRS_RATIO.Text = Math.Round(Convert.ToDecimal(txtAVAIABLE_WORKING_DIRECT_HRS.Text) - Convert.ToDecimal(txtGOOD_PRODUCTON_HRS.Text) / Convert.ToDecimal(txtAVAIABLE_WORKING_DIRECT_HRS.Text), 2).ToString();
                }
                txtTOTAL_LOSS_HRS_FOR_DAY = txtTOTAL_LOSS_HRS.Text;
                txtTOTAL_LOSS_HRS_RATIO = txtLOSS_HRS_RATIO.Text;
                //LINE IN-CHARGE * TOTAL WORKING HOURS

                txtDIRECT_MANPOWER = Math.Round(Convert.ToDecimal(txtOFFLINE_LINE_PARTS_FEEDER.Text) + Convert.ToDecimal(txtOUTLINE_LINE_SUPPORTER.Text) + Convert.ToDecimal(txtINLINE_OPERATOR.Text), 2).ToString();

                txtLINE_IN_CHARGE_MAN_HOURS = Math.Round(Convert.ToDecimal(txtLINE_IN_CHARGE.Text) * Convert.ToDecimal(txtTOTAL_WORKING_HOURS), 2).ToString();

                //    (OFFLINE(LINE PARTS FEEDER) * TOTAL WORKING HOURS)+OFFLINE(LINE PARTS FEEDER) OT + OFFLINE(MIZ) MANPOWER TAKEN FROM BANK+OFFLINE(MIZ) BORROWED
                // AFTER MAN POWER BANKING
                txtMIZDH = Math.Round((Convert.ToDecimal(txtOFFLINE_LINE_PARTS_FEEDER.Text) * Convert.ToDecimal(txtTOTAL_WORKING_HOURS)) + Convert.ToDecimal(txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text) +
                    Convert.ToDecimal(txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text) + Convert.ToDecimal(txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text), 2).ToString();

                //(OUTLINE (LINE SUPPORTER) * TOTAL WORKING HOURS)+OUTLINE (LINE SUPPORTER) OT+(SUPERVISOR * TOTAL WORKING HOURS)+SUPERVISOR OT
                txtOUTLINEDH = Math.Round((Convert.ToDecimal(txtOUTLINE_LINE_SUPPORTER.Text) * Convert.ToDecimal(txtTOTAL_WORKING_HOURS)) + Convert.ToDecimal(txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text) +
                    (Convert.ToDecimal(txtSUPERVISOR.Text) * Convert.ToDecimal(txtTOTAL_WORKING_HOURS)) + Convert.ToDecimal(txtSUPERVISOR_MANHOUR.Text), 2).ToString();

                //NON PRESENCE HRS - DIRECT (EXCLUDE HOURS)
                txtPURE_EXCLUDE = txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text;
                if (txtAVAIABLE_WORKING_DIRECT_HRS.Text != "0" && txtGH_AGAINST_BASE.Text!="0" && txtBASE_GH_RATIO!="0")
                    txtDPR.Text = Math.Round(Convert.ToDecimal(txtAVAIABLE_WORKING_DIRECT_HRS.Text) / (Convert.ToDecimal(txtGH_AGAINST_BASE.Text) / Convert.ToDecimal(txtBASE_GH_RATIO)), 2).ToString();

                //  h75 lend -INLINE MP GIVEN TO BANK
                //h78 bank -INLINE MP LENDING AFTER MAN POWER BANKING
                //h91 ojt -ON JOB TRAINING(3 DAYS - NEW ENTRY TO COMPANY)
                //h117 exclud -NON PRESENCE HRS -DIRECT(EXCLUDE HOURS)

                txtLEND = txtINLINE_MP_GIVEN_TO_BANK.Text;
                txtBANK = txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text;
                txtOJT = txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text;
                txtEXCLUDE = txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text;
                if ( txtGH_AGAINST_BASE.Text != "0" && txtBASE_GH_RATIO != "0")
                    txtStandardHours = Math.Round((Convert.ToDecimal(txtGH_AGAINST_BASE.Text) / Convert.ToDecimal(txtBASE_GH_RATIO)), 2).ToString();

                if (txtDIRECT_HRS != "0")
                    txtLHR.Text = Math.Round((Convert.ToDecimal(txtDIRECT_HRS) - (Convert.ToDecimal(txtGOOD_HRS)) / Convert.ToDecimal(txtDIRECT_HRS)), 2).ToString();
            }
        }
        private void DvgKPIDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgKPIDeatils.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgKPIDeatils.SelectedItems[0];
                  
                    RefNo = Convert.ToInt32(dr["Refno"].ToString());
                    txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Text = dr["TOTAL MANPOWER GIVEN BY COMPANY"].ToString();
                    txtABSENT.Text = dr["ABSENT"].ToString();
                    txtINLINE_OPERATOR.Text = dr["INLINE OPERATOR"].ToString();
                    txtOFFLINE_LINE_PARTS_FEEDER.Text = dr["OFFLINE (LINE PARTS FEEDER)"].ToString();
                    txtOUTLINE_LINE_SUPPORTER.Text = dr["OUTLINE (LINE SUPPORTER)"].ToString();
                    txtDIRECT_MANPOWER = dr["DIRECT MANPOWER"].ToString();
                    txtSUPERVISOR.Text = dr["SUPERVISOR"].ToString();
                    txtINLINE_MANPOWER.Text = dr["INLINE MANPOWER"].ToString();
                    txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text = dr["OFFLINE (LINE PARTS FEEDER) MANHOUR"].ToString();
                    txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text = dr["OUTLINE (LINE SUPPORTER) MANHOUR"].ToString();
                    txtSUPERVISOR_MANHOUR.Text = dr["SUPERVISOR_MANHOUR"].ToString();
                    txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text = dr["INLINE MANPOWER TAKEN FROM BANK"].ToString();
                    txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text = dr["INLINE BORROWED AFTER MAN POWER BANKING"].ToString();
                    txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text = dr["OFFLINE(MIZ) MANPOWER TAKEN FROM BANK"].ToString();
                    txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text = dr["OFFLINE(MIZ) BORROWED AFTER MAN POWER BANKING"].ToString();
                    txtOUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES.Text = dr["OUTLINE (SUPPORTER) TAKEN FROM OTHER LINES"].ToString();
                    txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text = dr["DIRECT MP WORKED IN LINE (TL / GL / SL / QA / LOGISTICS - BELOW AM)"].ToString();
                    txtINLINE_MP_GIVEN_TO_BANK.Text = dr["INLINE MP GIVEN TO BANK"].ToString();
                    txtOFFLINE_MIZ_GIVEN_TO_BANK.Text = dr["OFFLINE(MIZ) GIVEN TO BANK"].ToString();
                    txtOUTLINE_SUPPORTER_LENDING.Text = dr["OUTLINE (SUPPORTER) LENDING"].ToString();
                    txtOFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING.Text = dr["OFFLINE(MIZ) LENDING AFTER MAN POWER BANKING"].ToString();
                    txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text = dr["INDIRECTWORKEDINLINE"].ToString();
                    txtSHIFT_WORKIG_HRS = dr["SHIFT WORKIG HRS"].ToString();
                    txtLUNCH_TIME_30_Minutes = dr["LUNCH TIME (30 Minutes)"].ToString();
                    txtTOTAL_WORKING_HOURS = dr["TOTAL WORKING HOURS"].ToString();
                    txtKY_1_7_Minutes = dr["KY17Minutes"].ToString();
                    txtMORNING_MEETING_5_Minutes = dr["MORNING MEETING (5 Minutes)"].ToString();
                    txtTEA_BREAK_10_Minutes = dr["TEA BREAK (10 Minutes)"].ToString();
                    txtTEA_BREAK_10_Minutes1 = dr["TEA BREAK (10 Minutes)1"].ToString();
                    txtA5S_CLOSING_5_Minutes = dr["[5S CLOSING 5 Minutes]"].ToString();
                    txtBREAK_TIME_WITHOUT_LUNCH = dr["BREAK TIME WITHOUT LUNCH"].ToString();
                    txtFIXED_TIME = dr["FIXED TIME"].ToString();
                    txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text = dr["ON JOB TRAINING (3 DAYS-NEW ENTRY TO COMPANY)"].ToString();
                    txtEXCESS_MP_AFTER_MP_CALCULATION.Text = dr["EXCESS MP AFTER MP CALCULATION"].ToString();
                    txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text = dr["[OTHER TRAININGS BY HR TIE PE QA MNT etc]"].ToString();
                    txtMULTISKILLING.Text = dr["MULTISKILLING"].ToString();
                    txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text = dr["ALL HANDS / PARASPARA / OTHER COMPANY ACTIVITY"].ToString();
                    txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text = dr["SKILL COMPETETION / QCC / BUSINESS TRIP"].ToString();
                    txtINVENTORY.Text = dr["INVENTORY"].ToString();
                    txtPLANNED_SPECIAL_5S.Text = dr["PLANNED / SPECIAL 5S"].ToString();
                    txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text = dr["DELAY ARRIVAL / EARLY LEAVE (PAYMENT CUT/LEAVE)"].ToString();
                    txtPLANNED_TRIAL_BY_PE.Text = dr["PLANNED TRIAL BY PE"].ToString();
                    txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text = dr["ENERGY SHUTDOWN (GOVT POWER ONLY)"].ToString();
                    txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text = dr["FIRE ASSEMBLY / EVACUATION DRILL"].ToString();
                    txtNPD_TRIALS.Text = dr["NPD TRIALS"].ToString();
                    txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text = dr["MEDICAL CHECKUP / OTHER PLANNED ACTIVITY BY HR"].ToString();
                    txtCOMPANY_COST_SAVING_ACTIVITY.Text = dr["COMPANY COST SAVING ACTIVITY"].ToString();
                    txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text = dr["KANBAN ACHIEVEMENT-EARLY COMPLETION (FIXED FOR THIS MONTH)"].ToString();
                    txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text = dr["[KANBAN ACHIEVEMENT-EARLY COMPLETION BASED ON DAYS CONDITION]"].ToString();
                    txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text = dr["NO KANBAN DUE TO CUSTOMER NO PULL"].ToString();
                    txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text = dr["PLANNED ACTIVITY BY MAINTENANCE"].ToString();
                    txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text = dr["PLANNED ACTIVITY BY QA / 2ND QA"].ToString();
                    txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text = dr["VISITOR / ISO / CUSTOMER AUDIT TIME"].ToString();
                    txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text = dr["MAJOR / PLANNED CLEAN UP BEFORE LONG HOLIDAY"].ToString();
                    txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text = dr["IMPROVEMENT ACTIVITY (KAIZEN/LAYOUT CHANGE etc)"].ToString();
                    txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text = dr["PLANNED SNACKS BREAK AS PER HR POLICY"].ToString();
                    txtA5S_BANK_RETURNED_MP.Text = dr["5S (BANK RETURNED MP)"].ToString();
                    txtMULTISKILLING_BANK_RETURNED_MP.Text = dr["MULTISKILLING (BANK RETURNED MP)"].ToString();
                    txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = dr["NON PRESENCE HRS - DIRECT (EXCLUDE HOURS)"].ToString();
                    txtAVAIABLE_WORKING_DIRECT_HRS.Text = dr["AVAIABLE WORKING (DIRECT) HRS"].ToString();
                    txtGOOD_PRODUCTON_HRS.Text = dr["GOOD PRODUCTON HRS"].ToString();
                    txtTOTAL_LOSS_HRS.Text = dr["TOTAL LOSS HRS"].ToString();
                    txtPART_REJECTION_SCRAP_REWORK.Text = dr["PART REJECTION (SCRAP+REWORK)"].ToString();
                    txtLOSS_HRS_RATIO.Text = dr["LOSS HRS RATIO"].ToString();
                    txtAVAIABLE_WORKING_HRS = dr["AVAIABLE WORKING HRS"].ToString();
                    txtGOOD_PRODUCTON_HRS_FOR_DAY = dr["GOOD PRODUCTON HRS FOR DAY"].ToString();
                    txtTOTAL_LOSS_HRS_FOR_DAY = dr["TOTAL LOSS HRS FOR DAY"].ToString();
                    txtTOTAL_LOSS_HRS_RATIO = dr["TOTAL LOSS HRS RATIO"].ToString();
                    txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text = dr["INLINE MP LENDING AFTER MAN POWER BANKING"].ToString();
                    txtLINE_IN_CHARGE_MAN_HOURS = dr["LINE IN-CHARGE MAN-HOURS"].ToString();
                  //  txtUSED_AS_MANPOWER.Text = dr["USED AS MANPOWER"].ToString();
                    //txtUSEDASMANHOUR.Text = dr["USED AS MANHOUR"].ToString();
                    txtUSEDASOBSERVERMANHOUR = dr["USED AS OBSERVER MANHOUR"].ToString();
                    txtUSED_AS_OBSERVER_MANPOWER.Text = dr["USED AS OBSERVER"].ToString();
                    txtKPI = dr["KPI"].ToString();
                    txtDIRECT_HRS = dr["DIRECT HRS"].ToString();
                    txtGOOD_HRS = dr["GOOD HRS"].ToString();
                    txtDPR.Text = dr["DPR"].ToString();
                    txtBASE_GH_RATIO = dr["BASE GH RATIO"].ToString();
                    txtGH_AGAINST_BASE.Text = dr["GH AGAINST BASE"].ToString();
                    txtDIRECT_HRS1 = dr["DIRECT HRS1"].ToString();
                    txtGOOD_HRS1 = dr["GOOD HRS1"].ToString();
                    txtGH_AGAINST_BASE1 = dr["GH AGAINST BASE1"].ToString();
                    txtDPR1 = dr["DPR1"].ToString();
                    txtLHR.Text = dr["LHR"].ToString();
                    txtLHR_DAILY = dr["LHR DAILY"].ToString();
                    txtLEND = dr["LEND"].ToString();
                    txtBANK = dr["BANK"].ToString();
                    txtOJT = dr["OJT"].ToString();
                    txtEXCLUDE = dr["EXCLUDE"].ToString();
                    txtPURE_EXCLUDE = dr["PURE EXCLUDE"].ToString();
                    txtMIZDH = dr["MIZ DH"].ToString();
                    txtOUTLINEDH = dr["OUTLINE DH"].ToString();
                    txtFQAGH = dr["FQA GH"].ToString();
                    dtpFrom.Text = dr["Fromdate"].ToString();
                    dtpTo.Text = dr["ToDate"].ToString();
                    cmbShiftName.Text= dr["ShiftName"].ToString();
                    txtNPDDirecthour.Text = dr["NPDDirectHr"].ToString();
                    txtNPDExcludeHour.Text = dr["NPDExcludHr"].ToString();
                    BtnGet_Click(null, null);
                    txtLINE_IN_CHARGE.Text = dr["LINE IN-CHARGE"].ToString();



                }
                else
                {

                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgKPIDeatils.SelectedItems.Count > 0)
                {
                    if (dvgKPIDeatils.SelectedItems.Count == 1)
                    {
                        // if (ControlValidation())
                        Transaction("Update");
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgKPIDeatils.SelectedItems.Count == 0)
                {
                    //if (ControlValidation())
                    //{

                   
                    Transaction("Save");
                    //}
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgKPIDeatils.SelectedItems.Count > 0)
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                    if (CommonClasses.CommonVariable.Result == "YES")
                    {
                        for (int i = 0; i < dvgKPIDeatils.SelectedItems.Count; i++)
                        {
                            DataRowView dr = (DataRowView)dvgKPIDeatils.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Delete");

                        }

                        if (CommonClasses.CommonVariable.Result == "Deleted")
                        {
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                            ClearAll();
                        }
                        else
                        {
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Clear();
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtlinegrp.Text != "" || txtlinename.Text != "")
                {
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void ClearAll()
        {
            //chkStatus.IsChecked = true;
            Transaction("LoadDetails");
            Transaction("GetMachineGroupname");
            txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Text = "0";
            txtABSENT.Text = "0";
            txtINLINE_OPERATOR.Text = "0";
            txtOFFLINE_LINE_PARTS_FEEDER.Text = "0";
            txtOUTLINE_LINE_SUPPORTER.Text = "0";
            txtDIRECT_MANPOWER = "0";
            txtSUPERVISOR.Text = "0";
            txtINLINE_MANPOWER.Text = "0";
            txtOFFLINE_LINE_PARTS_FEEDER_MANHOUR.Text = "0";
            txtOUTLINE_LINE_SUPPORTER_MANHOUR.Text = "0";
            txtSUPERVISOR_MANHOUR.Text = "0";
            txtINLINE_MANPOWER_TAKEN_FROM_BANK.Text = "0";
            txtINLINE_BORROWED_AFTER_MAN_POWER_BANKING.Text = "0";
            txtOFFLINE_MIZ_MANPOWER_TAKEN_FROM_BANK.Text = "0";
            txtOFFLINE_MIZ_BORROWED_AFTER_MAN_POWER_BANKING.Text = "0";
            txtOUTLINE_SUPPORTER_TAKEN_FROM_OTHER_LINES.Text = "0";
            txtDIRECT_MP_WORKED_IN_LINE_TL_GL_SL_QA_LOGISTICS_BELOW_AM.Text = "0";
            txtINLINE_MP_GIVEN_TO_BANK.Text = "0";
            txtOFFLINE_MIZ_GIVEN_TO_BANK.Text = "0";
            txtOUTLINE_SUPPORTER_LENDING.Text = "0";
            txtOFFLINE_MIZ_LENDING_AFTER_MAN_POWER_BANKING.Text = "0";
            txtINDIRECT_WORKED_IN_LINE_Prod_Logi_QA_above_AM_Other_dept_members.Text = "0";
            txtSHIFT_WORKIG_HRS = "0";
            txtLUNCH_TIME_30_Minutes = "0";
            txtTOTAL_WORKING_HOURS = "0";
            txtKY_1_7_Minutes = "0";
            txtMORNING_MEETING_5_Minutes = "0";
            txtTEA_BREAK_10_Minutes = "0";
            txtTEA_BREAK_10_Minutes1 = "0";
            txtA5S_CLOSING_5_Minutes = "0";
            txtBREAK_TIME_WITHOUT_LUNCH = "0";
            txtFIXED_TIME = "0";
            txtON_JOB_TRAINING_3_DAYS_NEW_ENTRY_TO_COMPANY.Text = "0";
            txtEXCESS_MP_AFTER_MP_CALCULATION.Text = "0";
            txtOTHER_TRAININGS_BY_HR_TIE_PE_QA_MNT_etc.Text = "0";
            txtMULTISKILLING.Text = "0";
            txtALL_HANDS_PARASPARA_OTHER_COMPANY_ACTIVITY.Text = "0";
            txtSKILL_COMPETETION_QCC_BUSINESS_TRIP.Text = "0";
            txtINVENTORY.Text = "0";
            txtPLANNED_SPECIAL_5S.Text = "0";
            txtDELAY_ARRIVAL_EARLY_LEAVE_PAYMENT_CUT_LEAVE.Text = "0";
            txtPLANNED_TRIAL_BY_PE.Text = "0";
            txtENERGY_SHUTDOWN_GOVT_POWER_ONLY.Text = "0";
            txtFIRE_ASSEMBLY_EVACUATION_DRILL.Text = "0";
            txtNPD_TRIALS.Text = "0";
            txtMEDICAL_CHECKUP_OTHER_PLANNED_ACTIVITY_BY_HR.Text = "0";
            txtCOMPANY_COST_SAVING_ACTIVITY.Text = "0";
            txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_FIXED_FOR_THIS_MONTH.Text = "0";
            txtKANBAN_ACHIEVEMENT_EARLY_COMPLETION_BASED_ON_DAYS_CONDITION.Text = "0";
            txtNO_KANBAN_DUE_TO_CUSTOMER_NO_PULL.Text = "0";
            txtPLANNED_ACTIVITY_BY_MAINTENANCE.Text = "0";
            txtPLANNED_ACTIVITY_BY_QA_2ND_QA.Text = "0";
            txtVISITOR_ISO_CUSTOMER_AUDIT_TIME.Text = "0";
            txtMAJOR_PLANNED_CLEAN_UP_BEFORE_LONG_HOLIDAY.Text = "0";
            txtIMPROVEMENT_ACTIVITY_KAIZEN_LAYOUT_CHANGE_etc.Text = "0";
            txtPLANNED_SNACKS_BREAK_AS_PER_HR_POLICY.Text = "0";
            txtA5S_BANK_RETURNED_MP.Text = "0";
            txtMULTISKILLING_BANK_RETURNED_MP.Text = "0";
            txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = "0";
            txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";
            txtGOOD_PRODUCTON_HRS.Text = "0";
            txtTOTAL_LOSS_HRS.Text = "0";
            txtPART_REJECTION_SCRAP_REWORK.Text = "0";
            txtLOSS_HRS_RATIO.Text = "0";
            txtAVAIABLE_WORKING_HRS = "0";
            txtGOOD_PRODUCTON_HRS_FOR_DAY = "0";
            txtTOTAL_LOSS_HRS_FOR_DAY = "0";
            txtTOTAL_LOSS_HRS_RATIO = "0";
            txtLINE_IN_CHARGE.Text = "0";
            txtLINE_IN_CHARGE_MAN_HOURS = "0";
            //txtUSED_AS_MANPOWER.Text = "";
            //txtUSEDASMANHOUR.Text = "";
            txtUSED_AS_OBSERVER_MANPOWER.Text = "0";
            txtKPI = "0";
            txtDIRECT_HRS = "0";
            txtGOOD_HRS = "0";
            txtDPR.Text = "0";
            txtBASE_GH_RATIO = "0";
            txtGH_AGAINST_BASE.Text = "0";
            txtDIRECT_HRS1 = "0";
            txtGOOD_HRS1 = "0";
            txtGH_AGAINST_BASE1 = "0";
            txtDPR1 = "0";
            txtLHR.Text = "0";
            txtLHR_DAILY = "0";
            txtLEND = "0";
            txtBANK = "0";
            txtOJT = "0";
            txtEXCLUDE = "0";
            txtPURE_EXCLUDE = "0";
            txtMIZDH = "0";
            txtOUTLINEDH = "0";
            txtFQAGH = "0";
            txtINLINEMPLENDINGAFTERMANPOWERBANKING.Text = "0";
            txtTOTAL_MANPOWER_GIVEN_BY_COMPANY.Focus();
            cmbShiftName.Text = "";
            dtpFrom.Text = "";
            dtpTo.Text = "";
            dvgModelDeatils.ItemsSource = null;
            txtTotalOKQty.Text = "0";
            txtGOOD_PRODUCTON_HRS.Text = "0";
            txtTotalREjQty.Text = "0";
            txtNON_PRESENCE_HRS_DIRECT_EXCLUDE_HOURS.Text = "0";
            txtAVAIABLE_WORKING_DIRECT_HRS.Text = "0";
            txtTOTAL_LOSS_HRS.Text = "0";
            txtPART_REJECTION_SCRAP_REWORK.Text = "0";
            txtLOSS_HRS_RATIO.Text = "0";
            txtGH_AGAINST_BASE.Text = "0";
            txtDPR.Text = "0";
            txtLHR.Text = "0";
            txtNPDDirecthour.Text = "0";
            txtNPDExcludeHour.Text = "0";
            txtImporvedGoodHour = "0";
            txtTotalStandardHour = "0";
            txtNonStandardHour = "0";
            txtStandardHours = "0";
            RefNo = 0;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtMULTISKILLING_BANK_RETURNED_MP_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtMULTISKILLING_BANK_RETURNED_MP.Text != "")
                {
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbShiftName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbShiftName.SelectedIndex == 0)//first shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                }
                if (cmbShiftName.SelectedIndex == 1)//Second shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                }
                if (cmbShiftName.SelectedIndex == 2)//Third shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                }
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        //private void TxtUSED_AS_MANPOWER_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if(txtUSED_AS_MANPOWER.Text!="")
        //    {
        //        //USED AS MANPOWER*(TOTAL WORKING HOURS-BREAK TIME WITHOUT LUNCH)
        //        txtUSEDASMANHOUR.Text = Math.Round(Convert.ToDecimal(txtUSED_AS_MANPOWER.Text) * (Convert.ToDecimal(txtTOTAL_WORKING_HOURS.Text) - Convert.ToDecimal(txtBREAK_TIME_WITHOUT_LUNCH.Text)),2).ToString();
        //    }
        //}

        private void TxtUSED_AS_OBSERVER_MANPOWER_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUSED_AS_OBSERVER_MANPOWER.Text != "")
            {
                //USED AS MANPOWER*(TOTAL WORKING HOURS-BREAK TIME WITHOUT LUNCH)
                txtUSEDASOBSERVERMANHOUR = Math.Round(Convert.ToDecimal(txtUSED_AS_OBSERVER_MANPOWER.Text) * (Convert.ToDecimal(txtTOTAL_WORKING_HOURS) - Convert.ToDecimal(txtBREAK_TIME_WITHOUT_LUNCH)), 2).ToString();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                btnSave_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.U) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.U))
            {
                btnUpdate_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                btnExit_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.D))
            {
                btnDelete_Click(sender, e);
            }
        }

      
        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtlinegrp.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE GROUP", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtlinegrp.Focus();
                    return ;
                }
                if (txtlinename.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT LINE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtlinename.Focus();
                    return ;
                }

                if (dtpFrom.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT FROM DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpFrom.Focus();
                    return ;
                }
                if (dtpTo.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT TO DATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    dtpTo.Focus();
                    return ;
                }
                if (cmbShiftName.SelectedIndex == -1)
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    cmbShiftName.Focus();
                    return;
                }
                if (cmbShiftName.SelectedIndex == 0)//first shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                }
                if (cmbShiftName.SelectedIndex == 1)//Second shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 15:00:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                }
                if (cmbShiftName.SelectedIndex == 2)//Third shift
                {
                    if (dtpFrom.Text != "")
                        dtpFrom.Text = dtpFrom.SelectedDate.Value.ToString("dd MMM yyyy") + " 23:30:00";
                    if (dtpTo.Text != "")
                        dtpTo.Text = dtpTo.SelectedDate.Value.ToString("dd MMM yyyy") + " 06:30:00";
                }
                Transaction("LoadModelDetails");
            }
            catch(Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KPI ENTRY", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }

        }
    }
}
