using H3XStealer;
using System;
using System.Windows.Forms;

#nullable disable
namespace H3XTools;

internal static class Program
{
  [STAThread]
  private static void Main()
  {
    try
    {
      Application.EnableVisualStyles();
      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      ApplicationConfiguration.Initialize();
      
      SplashScreen splash = new SplashScreen();
      splash.ShowDialog();
      
      Application.Run((Form) new MainForm());
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show($"> An error occurred while starting the application: {ex.Message} <", "H3X Startup - Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }
}
