﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Ninject.Modules;
using DataAccess.Concrete.AdoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Ninject
{
    public class NinjectBusinessModule : NinjectModule
    {
        public override void Load()
        {
            //Product for


            //User for


            //Category for
            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<AdCategoryDal>().InSingletonScope();

        }
    }
}
