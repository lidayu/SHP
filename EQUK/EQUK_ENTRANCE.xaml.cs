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
using System.Reflection;

using ABC.EQUK.Entity;
using ABC.EQUK.Control;


namespace ABC.EQUK
{
    /// <summary>
    /// Interaction logic for EQUK_ENTRANCE.xaml
    /// </summary>
    public partial class EQUK_ENTRANCE : UserControl
    {
        Dictionary<string, TransactionInfo> tranDic;

        public EQUK_ENTRANCE()
        {
            InitializeComponent();

            LoadTransGroup();
        }


        void LoadTransGroup()
        {
            tranDic = new Dictionary<string, TransactionInfo>();

            string tranLoc = AppDomain.CurrentDomain.BaseDirectory + "\\conf\\transaction.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(tranLoc);


            XmlNode aTGroup = xmlDoc.SelectSingleNode("/Transactions/TGroup");
            XmlNodeList TranSet = aTGroup.SelectNodes("//transaction");
            foreach (XmlNode aNode in TranSet)
            {
                string nodeID = aNode.Attributes["ID"].Value;
                if (!tranDic.ContainsKey(nodeID))
                {
                    string nodeName = aNode.Attributes["Name"].Value;
                    string nodeImage = aNode.Attributes["Image"].Value;
                    string nodeCommand = aNode.Attributes["Command"].Value;
                    string nodePara = aNode.Attributes["Parameter"].Value;

                    TransactionInfo aTran = new TransactionInfo();
                    aTran.ID = nodeID;
                    aTran.Name = nodeName;
                    aTran.Image = nodeImage;
                    aTran.Command = nodeCommand;
                    aTran.Parameter = nodePara;
                    
                    //加入字典，防止重复加载
                    tranDic.Add(nodeID, aTran);

                    TabItem aTabItem = new TabItem();
                    string imgURL = AppDomain.CurrentDomain.BaseDirectory + "\\" + aTran.Image;
                    ImageBrush aIB = new ImageBrush(new BitmapImage(new Uri(imgURL)));
                    aIB.Stretch = Stretch.Uniform;
                    aTabItem.Background = aIB;
                    aTabItem.Header = aTran.Name;
                    aTabItem.Name = aTran.ID;
                    Style tabStyle = (Style)this.FindResource("TabItemStyle");
                    aTabItem.Style = tabStyle;

                    aTabItem.MouseUp += TabItem_MouseUp;

                    //Button aButton = new Button();
                    //aButton.Content = aNode.Attributes["Name"].Value;
                    //aButton.Name = nodeID;
                    //aButton.Click += sysButton_Click;

                    //动态设置Marin
                    Thickness buttonMargin = new Thickness(15, 30, 15, 0);
                    //aButton.Margin = buttonMargin;

                    tranGrid.Children.Add(aTabItem);
                    
                }
            }
        }


        private void TabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentTabItem = sender as TabItem;
            string TabItemID = currentTabItem.Name;

            TransactionInfo aTran = tranDic[TabItemID];
            string[] tranCommand=aTran.Command.Split(new Char[]{','});
            string tranClassName=tranCommand[0];
            string tranPartLoc=tranCommand[1];
            string tranLoc=AppDomain.CurrentDomain.BaseDirectory+"\\trans\\"+tranPartLoc+".dll";

            Assembly asm= Assembly.LoadFile(tranLoc);
            var tranType = asm.GetType(tranClassName);
            var instance = asm.CreateInstance(tranClassName);
            //EventInfo eventInfo = tranType.GetEvent("Close");
            //eventInfo.AddEventHandler(instance, new EventHandler(


            bdTranLoader.Child = (UIElement)instance ;

            tranGrid.Visibility = Visibility.Collapsed;
            bdTranLoader.Visibility = Visibility.Visible;

            HideNavButton();


        }

        void ShowEntrance()
        {
            tranGrid.Visibility = Visibility.Visible;
            bdTranLoader.Visibility = Visibility.Collapsed;

            //显示导航按钮
            ShowNavButton();
 
        }

        /// <summary>
        ///隐藏左右导航按钮
        /// </summary>
        void HideNavButton()
        {
            leftButton.Visibility = Visibility.Collapsed;
            rightButton.Visibility = Visibility.Collapsed;
 
        }


        /// <summary>
        /// 显示左右导航按钮
        /// </summary>
        void ShowNavButton()
        {
            leftButton.Visibility = Visibility.Visible;
            rightButton.Visibility = Visibility.Visible;

        }
    }
}
