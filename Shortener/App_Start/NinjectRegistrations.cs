using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Domain;

namespace Shortener.App_Start
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ILinkRepository>().To<Models.Repository.LinkRepository>();
        }
    }
}