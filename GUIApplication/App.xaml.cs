using System;
using System.Windows;
using TPApplicationCore;

namespace GUIApplication
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                ApplicationContext.Init();
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.StackTrace);
            }
            
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            ApplicationContext.CONTEXT.Dispose();
        }
    }
}
