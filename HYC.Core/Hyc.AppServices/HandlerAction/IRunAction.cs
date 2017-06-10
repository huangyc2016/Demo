using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices.HandlerAction
{
    public interface IRunAction
    {
        void Run(IJob obj, HandlerOptions handler);
    }
}
