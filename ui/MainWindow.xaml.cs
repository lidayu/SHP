using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using dSee.CefGlue;

using ABC.SHP.Entity;


namespace ABC.SHP.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, SysInfo> sysDic;
        public MainWindow()
        {
            this.MaxWidth = SystemParameters.PrimaryScreenWidth;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
            InitializeComponent();

            //加载所配置系统
            LoadSys();
            
            

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            menu1.IsOpen = true;
        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            if (SHPFramework.CefRuntimeLoaded)
            {
                CefRuntime.Shutdown();
            }
            this.Close();
        }

        private void miniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }             

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("lidayu");
        }

        private void TabItem_TouchUp(object sender, TouchEventArgs e)
        {
            LoadSys();
        }

        private void TabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LoadSys();
        }

        /// <summary>
        /// 根据配置文件加载子系统
        /// </summary>
        void LoadSys()
        {
            sysDic= new Dictionary<string, SysInfo>();
            SysSetGrid.Children.Clear();
            string sysConfig = System.AppDomain.CurrentDomain.BaseDirectory + "\\conf\\SysInfo.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sysConfig);


            int loadSeq = 0;
            XmlNodeList  sysSet=xmlDoc.SelectNodes("//SubSys");
            foreach (XmlNode aNode in sysSet)
            {                
                string nodeID=aNode.Attributes["ID"].Value;
                if (!sysDic.ContainsKey(nodeID))
                {
                    string nodeName = aNode.Attributes["Name"].Value;
                    string nodeType = aNode.Attributes["Type"].Value;
                    string nodeCore = aNode.Attributes["Core"].Value;
                    string nodeCommand = aNode.Attributes["Command"].Value;
                    string nodePara = aNode.Attributes["Parameter"].Value;

                    SysInfo aSys = new SysInfo();
                    aSys.sName = nodeName;
                    aSys.sType = nodeType;
                    aSys.sCore = nodeCore;
                    aSys.sCommand = nodeCommand;
                    aSys.sParameter = nodePara;

                    //加入字典，防止重复加载
                    sysDic.Add(nodeID,aSys);

                    Button aButton = new Button();
                    aButton.Content = aNode.Attributes["Name"].Value;
                    aButton.Name = nodeID;
                    aButton.Click += sysButton_Click;

                    //动态设置Marin
                    Thickness buttonMargin=new Thickness(15,30,15,0);
                    aButton.Margin = buttonMargin;
                    if(loadSeq%2==0)
                        aButton.Style = this.FindResource("SysButton1") as Style;
                    else
                        aButton.Style = this.FindResource("SysButton2") as Style;
                    aButton.ApplyTemplate();

                    SysSetGrid.Children.Add(aButton);

                    loadSeq++; 
                }   
 
            }
            
        }

        /// <summary>
        /// 单击加载特定子系统按键的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sysButton_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = sender as Button;
            string buttonID = currentButton.Name;
           
            //获取SHP所显示的各个子系统并加载
            SysLoader sysWin=new SysLoader(sysDic[buttonID]);
            sysWin.Show();        
          

        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.Topmost = false;
            }
            else
            {
                //获取当前显示器屏幕大小
                this.MaxWidth = SystemParameters.PrimaryScreenWidth;
                this.MaxHeight = SystemParameters.PrimaryScreenHeight;
               
                
                this.WindowState = WindowState.Maximized;
                this.Topmost = true;

            }

        }    
    }
}
