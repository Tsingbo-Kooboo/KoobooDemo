using Kooboo.CMS.Common.Runtime.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Blog
{
     public class AssemblyRegistrer : IDependencyRegistrar
    {
        public int Order
        {
            get { return 100; }
        }

        public void Register(IContainerManager containerManager, Kooboo.CMS.Common.Runtime.ITypeFinder typeFinder)
        {
            GlobalFilters.Filters.Add(new GenerateHtmlAttribute(), 1);
        }
    }
}
