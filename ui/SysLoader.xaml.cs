using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dSee.CefGlue;
using dSee.CefGlue.WPF;

using ABC.SHP.Entity;

namespace ABC.SHP
{
	/// <summary>
	/// SysLoader.xaml 的交互逻辑
	/// </summary>
	public partial class SysLoader : Window
	{
        /// <summary>
        /// webkit核浏览组件
        /// </summary>
        dSeeBrowser aDSee;

		public SysLoader()
		{
			InitializeComponent();
            tab_BSW.Visibility = Visibility.Hidden;
            tab_BSI.Visibility = Visibility.Hidden;
			
			// 在此点之下插入创建对象所需的代码。
		}

        public SysLoader(SysInfo aSys)
        {
            InitializeComponent();
            //根据子系统类型BS/CS选择加载方式
            if (aSys.sType == "CS")
            {
                

            }
            else if (aSys.sType == "BS")
            {
                //根据web内核选择加载平台
                if (aSys.sCore == "webkit")
                {
                    if (!SHPFramework.CefRuntimeLoaded)
                    {
                        LoadCefRuntime();
                    }
                    try
                    {
                        aDSee = new dSeeBrowser();
                    }
                    catch
                    { }
                    
                    tab_Grid_webkit.Children.Add(aDSee);
                    aDSee.NavigateTo(aSys.sCommand);
                    
                    SysViewer.SelectedIndex = 2;
                    SHPFramework.dSeeCreated = true;
                }
                else
                {   
                    SysViewer.SelectedIndex = 1;                   
                    wbIeSysLoader.Navigate(aSys.sCommand);
                }

            }
 
        }

       

        private void QKExit(object sender, CanExecuteRoutedEventArgs e)
        {
            //如果创建了dSeeBrowser，必须销毁dSeeBrowser，否则停止cefruntime异常
            if (SHPFramework.dSeeCreated)
            {
                aDSee.Dispose();
                SHPFramework.dSeeCreated = false;
            }

            this.Close();

        }

        /// <summary>
        /// 启动CefRuntime
        /// </summary>
        void LoadCefRuntime()
        {
            try
            {
                CefRuntime.Load();
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);               
            }
            catch (CefRuntimeException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            
            var mainArgs = new CefMainArgs(new string[] { });
            var cefApp = new dCefApp();
            var exitCode = CefRuntime.ExecuteProcess(mainArgs, cefApp);
            string subProcessPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\subProcess.exe";
            var cefSettings = new CefSettings
            {   
                BrowserSubprocessPath=subProcessPath,
                SingleProcess = false,
                WindowlessRenderingEnabled = true,
                MultiThreadedMessageLoop = true,
                LogSeverity = CefLogSeverity.Verbose,
                LogFile = "cef.log",
            };
          
            try
            {
                CefRuntime.Initialize(mainArgs, cefSettings, cefApp);
            }
            catch (CefRuntimeException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);               
            }

            //设置框架cefruntime已加载
            SHPFramework.CefRuntimeLoaded = true;
 
        }
	}
}