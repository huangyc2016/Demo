using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.AppServices.HandlerAction
{
    
    public class FactoryAction
    {
        private string _Interval;
        public FactoryAction(string Interval)
        {
            this._Interval = Interval;
        }
        public IRunAction Intit()
        {
            IRunAction irun = null;
            if (_Interval == "minute")
            {
                irun = new RunActionMinute();
            }
            else if (_Interval == "second")
            {
                irun = new RunActionSecond();
            }
            return irun;
        }
    }
}
