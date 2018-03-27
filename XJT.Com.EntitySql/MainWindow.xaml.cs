using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using XJT.Com.EntitySql.Common;
using XJT.Com.EntitySql.DatabaseSource;
using XJT.Com.EntitySql.ExcelSource;

namespace XJT.Com.EntitySql
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Tools.LogAction = LogInfo;
            InitializeComponent();
            this.PathShow.Text = Directory.GetCurrentDirectory();
            IgnoreSheet.Text = "设计说明|AAA机构单元数据表(参考)";
            GenerateCombSource();
            DataProviderCombSource();
        }

        private void ClickGo(object sender, RoutedEventArgs e)
        {
            int selectItemIndex = DataSourceTab.SelectedIndex;
            Tools.Creater = Creater.Text.Trim();
            Tools.GeneratePath = this.PathShow.Text;
            Tools.TableNameLike = TableNameLike.Text.Trim();
            Tools.SelectGenerateComb = Convert.ToInt32(GenerateComb.SelectedValue);
            if (string.IsNullOrWhiteSpace(Tools.Creater))
            {
                System.Windows.MessageBox.Show("请填写创建人！", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                Tools.LogAction("请填写创建人！", EnumCommon.LogEnum.CommonLog);
                return;
            }
            Tools.LogAction("开始", EnumCommon.LogEnum.CommonLog);
            if (selectItemIndex == 0)
            {
                ExcelDoWork();
            }
            else
            {
                DatabaseDoWork();
            }
        }

        private void DatabaseDoWork()
        {
            string tableNames = this.TableNames.Text.ToLower().Trim().Replace('，', ',');
            string connectionStr = ConnectionName.Text.Trim();
            if (string.IsNullOrWhiteSpace(connectionStr))
            {
                Tools.LogAction("请输入数据库连接字符串！", EnumCommon.LogEnum.CommonLog);
                return;
            }
            if (DataProviderComb.SelectedItem == null)
            {
                Tools.LogAction("请选择数据库类型！", EnumCommon.LogEnum.CommonLog);
                return;
            }

            var selectItem = (DictionaryEntry)DataProviderComb.SelectedItem;
            string providerName = selectItem.Value.ToString();
            DatabaseSourceWork databaseWork = new DatabaseSourceWork();
            Task.Factory.StartNew(() => databaseWork.DoWork(connectionStr, providerName, tableNames));
        }

        private void ExcelDoWork()
        {
            ExcelSourceWork mainWork = new ExcelSourceWork();
            var filePathList = this.excelPath.Text.Split('\r');
            var ignoreSheetList = this.IgnoreSheet.Text.Split('|');
            string tableNames = this.TableNames.Text.ToLower().Trim().Replace('，', ',');

            if (string.IsNullOrWhiteSpace(filePathList[0]))
            {
                System.Windows.MessageBox.Show("请选择Excel文件！", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                Tools.LogAction("请选择Excel文件！", EnumCommon.LogEnum.CommonLog);
                return;
            }
            Task.Factory.StartNew(() => mainWork.DoWork(filePathList, ignoreSheetList, tableNames));
        }
        private void GenerateCombSource()
        {
            var list = Tools.GetEnumList<EnumCommon.GenerateEnum>();

            GenerateComb.ItemsSource = list;
            GenerateComb.SelectedValuePath = "Key";
            GenerateComb.DisplayMemberPath = "Value";
            GenerateComb.SelectedIndex = 0;
        }

        private void DataProviderCombSource()
        {
            var list = Tools.GetEnumList<EnumCommon.DataProviderEnum>();

            DataProviderComb.ItemsSource = list;
            DataProviderComb.SelectedValuePath = "Key";
            DataProviderComb.DisplayMemberPath = "Value";
            DataProviderComb.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择存放路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectPath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog mDialog = new FolderBrowserDialog();
            DialogResult result = mDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string mDir = mDialog.SelectedPath.Trim();
            this.PathShow.Text = mDir + "\\Excel2Sql.sql";
        }

        /// <summary>
        /// 选择Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectExcelPath(object sender, RoutedEventArgs e)
        {
            // 在WPF中， OpenFileDialog位于Microsoft.Win32名称空间
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|Excel Files (*.xls)|*.xls"
            };
            if (dialog.ShowDialog() == true)
            {
                this.excelPath.Text = string.Join("\r", dialog.FileNames);
            }
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        private void LogInfo(string strLog, EnumCommon.LogEnum logEnum)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    string log = DateTime.Now + " " + strLog + "\r\n";
                    switch (logEnum)
                    {
                        case EnumCommon.LogEnum.CommonLog:
                            //this.Visibility = Visibility.Visible;
                            CommonLogBox.Text = log + CommonLogBox.Text;
                            break;
                        case EnumCommon.LogEnum.EntityLog:
                            //this.Visibility = Visibility.Visible;
                            EntityLogBox.Text = log + EntityLogBox.Text;
                            break;
                        case EnumCommon.LogEnum.SqlLog:
                            //this.Visibility = Visibility.Visible; 
                            SqlLogBox.Text = log + SqlLogBox.Text;
                            break;
                    }

                }));

                Tools.Log2File(strLog);
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Tools.Log2File(ex.ToString());
                System.GC.Collect();
            }
        }

        private void DataSourceTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = Tools.GetEnumList<EnumCommon.GenerateEnum>();
            int selectItemIndex = DataSourceTab.SelectedIndex;
            if (selectItemIndex == 0)
            {
                SqlLogTab.Visibility = Visibility.Visible;
                GenerateComb.ItemsSource = list;
            }
            else
            {
                SqlLogTab.Visibility = Visibility.Hidden;

                var newList =
                    list.ToList()
                        .FindAll(
                            x =>
                                Convert.ToInt32(x.Key) == (int) EnumCommon.GenerateEnum.Entity ||
                                Convert.ToInt32(x.Key) == (int) EnumCommon.GenerateEnum.Pojo);
                GenerateComb.ItemsSource = newList;
            }
            GenerateComb.SelectedValuePath = "Key";
            GenerateComb.DisplayMemberPath = "Value";
            GenerateComb.SelectedIndex = 0;
        }

        private void WinMain_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = false;

            WinMain.OpacityMask = this.Resources["ClosedBrush"] as LinearGradientBrush;
            Storyboard std = this.Resources["ClosedStoryboard"] as Storyboard;
            std.Completed += delegate { this.Close(); };

            std.Begin();
        }
         
    }
}
