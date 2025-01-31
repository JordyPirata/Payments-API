namespace Payments_API.Services;

using Payments_API.Interfaces;
public class ServiceInstaller
{
    public static void Install()
    {
        ServiceLocator.Register<IPhoenixService>(new PhoenixService());
        ServiceLocator.Register<IOpenPayService>(new OpenPayService());
    }
}