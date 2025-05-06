namespace NutriFitApp.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent(); 
        Routing.RegisterRoute("Login", typeof(Views.LoginPage));
        Routing.RegisterRoute("Dashboard", typeof(Views.DashboardPage));
    }
}
