﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using DataModel;
using DataModel.UnitOfWork;
using BusinessServices.Contracts;
using Resolver;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IOUMemberServices, OUMemberServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
        }
    }
}
