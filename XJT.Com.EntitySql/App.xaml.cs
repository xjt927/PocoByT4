using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EnvDTE;
using XJT.Com.EntitySql.View;
using Thread = System.Threading.Thread;
using Window = System.Windows.Window;

namespace XJT.Com.EntitySql
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            GifView gifView = new GifView();
            gifView.Show();

            Task.Factory.StartNew(() => CloseView(gifView, mainWindow));


            //SplashScreen s = new SplashScreen("./images/25460.jpg");
            //s.Show(true, true);
            //s.Close(new TimeSpan(0, 0, 3));
        }

        private void CloseView(Window window, Window mainWindow)
        {
            Thread.Sleep(3000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                window.Close();
                mainWindow.Show();
            }));
        }
    }
}
