﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using Ninject.Core.Behavior;
using ClientApp.RemoteServerRef;

namespace ClientApp.Ninject
{
    /// <summary>
    /// Definiuje reguly bindowania typow przez Ninject.
    /// </summary>
    public class ClientAppDependencies : StandardModule
    {
        public override void Load()
        {
            // LocalServer jest teraz singletonem (ale w przyszlosci nie musi)
            Bind<IServer>().To<LocalServer>().Using<SingletonBehavior>();
            Bind<ServerBase>().To<LocalServer>().Using<SingletonBehavior>();
            
            // Klasa NewEntryCreator bindowana do samej siebie
            Bind<NewEntryCreator>().ToSelf();
        }
    }
}
