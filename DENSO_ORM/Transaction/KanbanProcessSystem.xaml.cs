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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DENSO_ORM.Transaction
{
    /// <summary>
    /// Interaction logic for KanbanProcessSystem.xaml
    /// </summary>
    public partial class KanbanProcessSystem : Page
    {
        public KanbanProcessSystem()
        {
            InitializeComponent();
            if (CommonClasses.CommonVariable.TransactioType != "DashBoard")
            {
                this.Style = (Style)FindResource("PageStyle");
            }
            else
                imgSmily3.Visibility = Visibility.Hidden;
        }
        #region Variable and Objects
      BUSINESS_LAYER.LogCreation.LogCreation obj_Log = new BUSINESS_LAYER.LogCreation.LogCreation();
        BUSINESS_LAYER.Transaction.Transaction obj_Tran = new BUSINESS_LAYER.Transaction.Transaction();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        private void ShowDateTime()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                Transaction("GetShiftDetails");
                Transaction("KanbanProgress");
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROCESS_SYSTEM", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }
        private void Transaction(string Type)
        {
            if (Type == "GetShiftDetails")
            {

                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                DataTable dt = obj_Tran.BL_DashBoard().Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommonClasses.CommonVariable.ShiftName = dt.Rows[i]["ShiftName"].ToString();
                    CommonClasses.CommonVariable.Time = dt.Rows[i]["Time"].ToString();
                }
            }
            if (Type == "KanbanProgress")
            {
                ENTITY_LAYER.Transaction.Transaction.Type = Type;
                ENTITY_LAYER.Transaction.Transaction.Time = CommonClasses.CommonVariable.Time;
                ENTITY_LAYER.Transaction.Transaction.Shiftname = CommonClasses.CommonVariable.ShiftName;
                ENTITY_LAYER.Transaction.Transaction.MachineGroup = CommonClasses.CommonVariable.MachineGroup;
                ENTITY_LAYER.Transaction.Transaction.MachineName = CommonClasses.CommonVariable.MachineName;
                ENTITY_LAYER.Transaction.Transaction.ModelName = CommonClasses.CommonVariable.ModelName;
                DataSet dt = obj_Tran.BL_DashBoard();
                txtNorModelcol4.Text = "PLEASE SCAN KANBAN";
                txtNorQtycol4.Text = "";
                txtNorkCountcol4.Text = "";
                txtNorModelcol3.Text = "PLEASE SCAN KANBAN";
                txtNorQtycol3.Text = "";
                txtNorkCountcol3.Text = "";
                txtNorModelcol2.Text = "PLEASE SCAN KANBAN";
                txtNorQtycol2.Text = "";
                txtNorkCountcol2.Text = "";
                txtNorModelcol1.Text = "PLEASE SCAN KANBAN";
                txtNorQtycol1.Text = "";
                txtNorkCountcol1.Text = "";


                txtdelayModelcol4.Text = "PLEASE SCAN KANBAN";
                txtdelayQtycol4.Text = "";
                txtdelayKCountcol4.Text = "";
                txtdelayModelcol3.Text = "PLEASE SCAN KANBAN";
                txtdelayQtycol3.Text = "";
                txtdelayKCountcol3.Text = "";
                txtdelayModelcol2.Text = "PLEASE SCAN KANBAN";
                txtdelayQtycol2.Text = "";
                txtdelayKCountcol2.Text = "";
                txtdelayModelcol1.Text = "PLEASE SCAN KANBAN";
                txtdelayQtycol1.Text = "";
                txtdelayKCountcol1.Text = "";

                brdNormal1.Visibility = Visibility.Visible;
                brdNormal2.Visibility = Visibility.Visible;
                brdNormal3.Visibility = Visibility.Visible;
                brdNormal4.Visibility = Visibility.Visible;

                brdDelay0.Visibility = Visibility.Hidden;
                brdDelay1.Visibility = Visibility.Hidden;
                brdDelay2.Visibility = Visibility.Hidden;
                brdDelay3.Visibility = Visibility.Hidden;

                txtNorModelcol4.Foreground = Brushes.Black;
                txtNorModelcol3.Foreground = Brushes.Black;
                txtNorModelcol2.Foreground = Brushes.Black;
                txtNorModelcol1.Foreground = Brushes.Black;
                txtdelayModelcol4.Foreground = Brushes.Black;
                txtdelayModelcol3.Foreground = Brushes.Black;
                txtdelayModelcol2.Foreground = Brushes.Black;
                txtdelayModelcol1.Foreground = Brushes.Black;

                txtNorQtycol4.Foreground = Brushes.Black;
                txtNorQtycol3.Foreground = Brushes.Black;
                txtNorQtycol2.Foreground = Brushes.Black;
                txtNorQtycol1.Foreground = Brushes.Black;
                txtdelayQtycol4.Foreground = Brushes.Black;
                txtdelayQtycol3.Foreground = Brushes.Black;
                txtdelayQtycol2.Foreground = Brushes.Black;
                txtdelayQtycol1.Foreground = Brushes.Black;

                txtNorkCountcol4.Foreground = Brushes.Black;
                txtNorkCountcol3.Foreground = Brushes.Black;
                txtNorkCountcol2.Foreground = Brushes.Black;
                txtNorkCountcol1.Foreground = Brushes.Black;
                txtdelayKCountcol4.Foreground = Brushes.Black;
                txtdelayKCountcol3.Foreground = Brushes.Black;
                txtdelayKCountcol2.Foreground = Brushes.Black;
                txtdelayKCountcol1.Foreground = Brushes.Black;

                if (dt.Tables[1].Rows.Count > 0)
                {
                    if (dt.Tables[1].Rows[0]["NoofAdvance"].ToString() != "0")
                    {
                        Border brdAdance = new Border();
                        brdAdance.BorderBrush = Brushes.Black;
                        brdAdance.Background = Brushes.White;
                        brdAdance.BorderThickness = new Thickness(2);

                        Grid.SetRow(brdAdance, 1);
                        Grid.SetColumn(brdAdance, 0);
                        Grid.SetColumnSpan(brdAdance, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofAdvance"]));
                        grd1.Children.Add(brdAdance);

                        TextBlock txtAdvance = new TextBlock();
                        txtAdvance.Text = "ADVANCE";
                        txtAdvance.Foreground = Brushes.Black;
                        txtAdvance.TextAlignment = TextAlignment.Center;
                        txtAdvance.VerticalAlignment = VerticalAlignment.Center;
                        txtAdvance.HorizontalAlignment = HorizontalAlignment.Center;

                        if (dt.Tables[1].Rows[0]["NoofAdvance"].ToString() == "1")
                            txtAdvance.FontSize = 23;
                        else
                            txtAdvance.FontSize = 40;

                        txtAdvance.FontFamily = new FontFamily("Calibri");
                        txtAdvance.FontWeight = FontWeights.ExtraBold;
                        txtAdvance.Margin = new Thickness(0, -3, 0, 0);
                        //  brdNormal.BorderThickness = 

                        Grid.SetRow(txtAdvance, 1);
                        Grid.SetColumn(txtAdvance, 0);
                        Grid.SetColumnSpan(txtAdvance, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofAdvance"]));
                        grd1.Children.Add(txtAdvance);
                    }
                    if (dt.Tables[1].Rows[0]["NoofNormal"].ToString() != "0")
                    {
                        Border brdNormal = new Border();
                        brdNormal.BorderBrush = Brushes.Black;
                        brdNormal.Background = Brushes.Green;
                        brdNormal.BorderThickness = new Thickness(2);

                        Grid.SetRow(brdNormal, 1);
                        Grid.SetColumn(brdNormal, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofAdvance"]));
                        Grid.SetColumnSpan(brdNormal, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofNormal"]));
                        grd1.Children.Add(brdNormal);

                        TextBlock txtNormal = new TextBlock();
                        txtNormal.Text = "NORMAL";
                        txtNormal.Foreground = Brushes.White;
                        txtNormal.TextAlignment = TextAlignment.Center;
                        txtNormal.VerticalAlignment = VerticalAlignment.Center;
                        txtNormal.HorizontalAlignment = HorizontalAlignment.Center;
                        if (dt.Tables[1].Rows[0]["NoofNormal"].ToString() == "1")
                            txtNormal.FontSize = 23;
                        else
                            txtNormal.FontSize = 40;
                        txtNormal.FontFamily = new FontFamily("Calibri");
                        txtNormal.FontWeight = FontWeights.ExtraBold;
                        txtNormal.Margin = new Thickness(0, -3, 0, 0);
                        Grid.SetRow(txtNormal, 1);
                        Grid.SetColumn(txtNormal, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofAdvance"]));
                        Grid.SetColumnSpan(txtNormal, Convert.ToInt32(dt.Tables[1].Rows[0]["NoofNormal"]));
                        grd1.Children.Add(txtNormal);
                    }
                }
                if (dt.Tables[0].Rows.Count > 0)
                {
                    brdNormal1.Visibility = Visibility.Hidden;
                    brdNormal2.Visibility = Visibility.Hidden;
                    brdNormal3.Visibility = Visibility.Hidden;
                    brdNormal4.Visibility = Visibility.Hidden;
                    if (dt.Tables[0].Rows.Count == 1)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";
                        brdNormal4.Visibility = Visibility.Visible;
                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorkCountcol4.Foreground = Brushes.Black;
                    }
                    if (dt.Tables[0].Rows.Count == 2)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;

                    }
                    if (dt.Tables[0].Rows.Count == 3)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;


                    }
                    if (dt.Tables[0].Rows.Count == 4)
                    {
                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;
                        brdNormal1.Visibility = Visibility.Visible;

                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol1.Text = dt.Tables[0].Rows[3]["modelName"].ToString();
                        txtNorQtycol1.Text = dt.Tables[0].Rows[3]["Qty"].ToString() + " QTY";
                        txtNorkCountcol1.Text = dt.Tables[0].Rows[3]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;
                        txtNorModelcol1.Foreground = Brushes.DarkGreen;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;
                        txtNorQtycol1.Foreground = Brushes.Blue;
                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;
                        txtNorkCountcol1.Foreground = Brushes.Black;


                    }
                    if (dt.Tables[0].Rows.Count == 5)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol1.Text = dt.Tables[0].Rows[3]["modelName"].ToString();
                        txtNorQtycol1.Text = dt.Tables[0].Rows[3]["Qty"].ToString() + " QTY";
                        txtNorkCountcol1.Text = dt.Tables[0].Rows[3]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol4.Text = dt.Tables[0].Rows[4]["modelName"].ToString();
                        txtdelayQtycol4.Text = dt.Tables[0].Rows[4]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol4.Text = dt.Tables[0].Rows[4]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;
                        brdNormal1.Visibility = Visibility.Visible;

                        brdDelay0.Visibility = Visibility.Visible;

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;
                        txtNorModelcol1.Foreground = Brushes.DarkGreen;
                        txtdelayModelcol4.Foreground = Brushes.Red;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;
                        txtNorQtycol1.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;
                        txtNorkCountcol1.Foreground = Brushes.Black;

                        txtdelayQtycol4.Foreground = Brushes.Blue;

                        txtdelayKCountcol4.Foreground = Brushes.Black;

                    }
                    if (dt.Tables[0].Rows.Count == 6)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol1.Text = dt.Tables[0].Rows[3]["modelName"].ToString();
                        txtNorQtycol1.Text = dt.Tables[0].Rows[3]["Qty"].ToString() + " QTY";
                        txtNorkCountcol1.Text = dt.Tables[0].Rows[3]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol4.Text = dt.Tables[0].Rows[4]["modelName"].ToString();
                        txtdelayQtycol4.Text = dt.Tables[0].Rows[4]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol4.Text = dt.Tables[0].Rows[4]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol3.Text = dt.Tables[0].Rows[5]["modelName"].ToString();
                        txtdelayQtycol3.Text = dt.Tables[0].Rows[5]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol3.Text = dt.Tables[0].Rows[5]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;
                        brdNormal1.Visibility = Visibility.Visible;

                        brdDelay0.Visibility = Visibility.Visible;
                        brdDelay1.Visibility = Visibility.Visible;

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;
                        txtNorModelcol1.Foreground = Brushes.DarkGreen;
                        txtdelayModelcol4.Foreground = Brushes.Red;
                        txtdelayModelcol3.Foreground = Brushes.Red;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;
                        txtNorQtycol1.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;
                        txtNorkCountcol1.Foreground = Brushes.Black;

                        txtdelayQtycol4.Foreground = Brushes.Blue;
                        txtdelayQtycol3.Foreground = Brushes.Blue;

                        txtdelayKCountcol4.Foreground = Brushes.Black;
                        txtdelayKCountcol3.Foreground = Brushes.Black;

                    }
                    if (dt.Tables[0].Rows.Count == 7)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol1.Text = dt.Tables[0].Rows[3]["modelName"].ToString();
                        txtNorQtycol1.Text = dt.Tables[0].Rows[3]["Qty"].ToString() + " QTY";
                        txtNorkCountcol1.Text = dt.Tables[0].Rows[3]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol4.Text = dt.Tables[0].Rows[4]["modelName"].ToString();
                        txtdelayQtycol4.Text = dt.Tables[0].Rows[4]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol4.Text = dt.Tables[0].Rows[4]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol3.Text = dt.Tables[0].Rows[5]["modelName"].ToString();
                        txtdelayQtycol3.Text = dt.Tables[0].Rows[5]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol3.Text = dt.Tables[0].Rows[5]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol2.Text = dt.Tables[0].Rows[6]["modelName"].ToString();
                        txtdelayQtycol2.Text = dt.Tables[0].Rows[6]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol2.Text = dt.Tables[0].Rows[6]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;
                        brdNormal1.Visibility = Visibility.Visible;

                        brdDelay0.Visibility = Visibility.Visible;
                        brdDelay1.Visibility = Visibility.Visible;
                        brdDelay2.Visibility = Visibility.Visible;

                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;
                        txtNorModelcol1.Foreground = Brushes.DarkGreen;
                        txtdelayModelcol4.Foreground = Brushes.Red;
                        txtdelayModelcol3.Foreground = Brushes.Red;
                        txtdelayModelcol2.Foreground = Brushes.Red;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;
                        txtNorQtycol1.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;
                        txtNorkCountcol1.Foreground = Brushes.Black;

                        txtdelayQtycol4.Foreground = Brushes.Blue;
                        txtdelayQtycol3.Foreground = Brushes.Blue;
                        txtdelayQtycol2.Foreground = Brushes.Blue;

                        txtdelayKCountcol4.Foreground = Brushes.Black;
                        txtdelayKCountcol3.Foreground = Brushes.Black;
                        txtdelayKCountcol2.Foreground = Brushes.Black;


                    }
                    if (dt.Tables[0].Rows.Count == 8)
                    {
                        txtNorModelcol4.Text = dt.Tables[0].Rows[0]["modelName"].ToString();
                        txtNorQtycol4.Text = dt.Tables[0].Rows[0]["Qty"].ToString() + " QTY";
                        txtNorkCountcol4.Text = dt.Tables[0].Rows[0]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol3.Text = dt.Tables[0].Rows[1]["modelName"].ToString();
                        txtNorQtycol3.Text = dt.Tables[0].Rows[1]["Qty"].ToString() + " QTY";
                        txtNorkCountcol3.Text = dt.Tables[0].Rows[1]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol2.Text = dt.Tables[0].Rows[2]["modelName"].ToString();
                        txtNorQtycol2.Text = dt.Tables[0].Rows[2]["Qty"].ToString() + " QTY";
                        txtNorkCountcol2.Text = dt.Tables[0].Rows[2]["NoofKanban"].ToString() + " NOs";

                        txtNorModelcol1.Text = dt.Tables[0].Rows[3]["modelName"].ToString();
                        txtNorQtycol1.Text = dt.Tables[0].Rows[3]["Qty"].ToString() + " QTY";
                        txtNorkCountcol1.Text = dt.Tables[0].Rows[3]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol4.Text = dt.Tables[0].Rows[4]["modelName"].ToString();
                        txtdelayQtycol4.Text = dt.Tables[0].Rows[4]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol4.Text = dt.Tables[0].Rows[4]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol3.Text = dt.Tables[0].Rows[5]["modelName"].ToString();
                        txtdelayQtycol3.Text = dt.Tables[0].Rows[5]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol3.Text = dt.Tables[0].Rows[5]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol2.Text = dt.Tables[0].Rows[6]["modelName"].ToString();
                        txtdelayQtycol2.Text = dt.Tables[0].Rows[6]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol2.Text = dt.Tables[0].Rows[6]["NoofKanban"].ToString() + " NOs";

                        txtdelayModelcol1.Text = dt.Tables[0].Rows[7]["modelName"].ToString();
                        txtdelayQtycol1.Text = dt.Tables[0].Rows[7]["Qty"].ToString() + " QTY";
                        txtdelayKCountcol1.Text = dt.Tables[0].Rows[7]["NoofKanban"].ToString() + " NOs";

                        brdNormal4.Visibility = Visibility.Visible;
                        brdNormal3.Visibility = Visibility.Visible;
                        brdNormal2.Visibility = Visibility.Visible;
                        brdNormal1.Visibility = Visibility.Visible;

                        brdDelay0.Visibility = Visibility.Visible;
                        brdDelay1.Visibility = Visibility.Visible;
                        brdDelay2.Visibility = Visibility.Visible;
                        brdDelay3.Visibility = Visibility.Visible;

                        brdDelay3.Visibility = Visibility.Visible;
                        brdDelay2.Visibility = Visibility.Visible;
                        txtNorModelcol4.Foreground = Brushes.DarkGreen;
                        txtNorModelcol3.Foreground = Brushes.DarkGreen;
                        txtNorModelcol2.Foreground = Brushes.DarkGreen;
                        txtNorModelcol1.Foreground = Brushes.DarkGreen;
                        txtdelayModelcol4.Foreground = Brushes.Red;
                        txtdelayModelcol3.Foreground = Brushes.Red;
                        txtdelayModelcol2.Foreground = Brushes.Red;
                        txtdelayModelcol1.Foreground = Brushes.Red;

                        txtNorQtycol4.Foreground = Brushes.Blue;
                        txtNorQtycol3.Foreground = Brushes.Blue;
                        txtNorQtycol2.Foreground = Brushes.Blue;
                        txtNorQtycol1.Foreground = Brushes.Blue;

                        txtNorkCountcol4.Foreground = Brushes.Black;
                        txtNorkCountcol3.Foreground = Brushes.Black;
                        txtNorkCountcol2.Foreground = Brushes.Black;
                        txtNorkCountcol1.Foreground = Brushes.Black;

                        txtdelayQtycol4.Foreground = Brushes.Blue;
                        txtdelayQtycol3.Foreground = Brushes.Blue;
                        txtdelayQtycol2.Foreground = Brushes.Blue;
                        txtdelayQtycol1.Foreground = Brushes.Blue;

                        txtdelayKCountcol4.Foreground = Brushes.Black;
                        txtdelayKCountcol3.Foreground = Brushes.Black;
                        txtdelayKCountcol2.Foreground = Brushes.Black;
                        txtdelayKCountcol1.Foreground = Brushes.Black;

                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                ShowDateTime();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROCESS_SYSTEM", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());

            }
        }
        private void ImgSmily3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROCESS_SYSTEM", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                obj_Log.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "KANBAN_PROCESS_SYSTEM", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

    }
}