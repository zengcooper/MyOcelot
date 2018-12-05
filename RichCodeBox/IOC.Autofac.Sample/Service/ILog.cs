using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Autofac.Sample.Service
{
    public interface ILog
    {
        void Error(Exception exception);
    }
}
