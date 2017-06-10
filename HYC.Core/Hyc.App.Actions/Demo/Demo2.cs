using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.App.Actions.Demo
{
    public class Demo2: IActions
    {
        private static int index;
        public Object Execute()
        {
            System.Threading.Thread.Sleep(1000);
            index++;
            Console.WriteLine("DEMO2");
            return null;
        }
    }
}
