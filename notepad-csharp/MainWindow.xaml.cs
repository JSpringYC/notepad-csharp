using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace notepad_csharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Editor editor;

        public MainWindow()
        {
            InitializeComponent();
            editor = new Editor(ContentText);
        }

        /// <summary>
        /// 菜单：文件-新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFileMenu_Click(object sender, RoutedEventArgs e)
        {
            if (SaveConfirm())
            {
                editor.Clear();
                this.Title = "无标题 - 记事本";
            }
        }

        /// <summary>
        /// 菜单：文件-退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            if (SaveConfirm())
            {
                this.Close();
            }
        }

        /// <summary>
        /// 菜单：格式-自动换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WrapMenu_Click(object sender, RoutedEventArgs e)
        {
            if (WrapMenu.IsChecked)
            {
                ContentText.TextWrapping = TextWrapping.WrapWithOverflow;
            }
            else
            {
                ContentText.TextWrapping = TextWrapping.NoWrap;
            }
        }

        /// <summary>
        /// 菜单：查看-状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StatusBarMenu.IsChecked)
            {
                StatusBarContainer.Visibility = Visibility.Visible;
            }
            else
            {
                StatusBarContainer.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 保存确认
        /// </summary>
        /// <returns></returns>
        private bool SaveConfirm()
        {
            if (!editor.Save())
            {
                MessageBoxResult result = MessageBox.Show("是否将更改保存到 " + editor.GetName() + "？", "记事本", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
                if (result == MessageBoxResult.Yes)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Title = "另存为";
                    fileDialog.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
                    fileDialog.FilterIndex = 1;
                    fileDialog.RestoreDirectory = true;
                    fileDialog.AddExtension = true;
                    fileDialog.DefaultExt = ".txt";

                    bool? save = fileDialog.ShowDialog();
                    if(save.HasValue && save.Value)
                    {
                        var fileName = fileDialog.FileName;
                        this.Title = new FileInfo(fileName).Name + " - 记事本";
                        return editor.Save(fileName);
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 菜单：文件-打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (editor.IsTextChanged() && !SaveConfirm())
            {
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "打开";
            fileDialog.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            bool? open = fileDialog.ShowDialog();
            if (open.HasValue && open.Value)
            {
                var fileName = fileDialog.FileName;
                editor.Clear();
                this.Title = new FileInfo(fileName).Name + " - 记事本";
                editor.Open(fileName);
            }
        }

        /// <summary>
        /// 菜单：文件-保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveConfirm();
        }
    }
}
