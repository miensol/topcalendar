#region

using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject.Modules;

#endregion

namespace TopCalendar.Server.Bootstrap
{
    public class DefaultNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServiceLocator>().To<NinjectServiceLocator>().InSingletonScope();
        }
    }
}