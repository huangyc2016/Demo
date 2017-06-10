using Hyc.App.Actions.Demo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.App.Actions
{
    public class MyOptions
    {
        public string sqlConnectionHyc { get; set; }

        public string sqlConnectionLeo { get; set; }
    }

    public class HandlerOptions
    {
        public string Name { get; set; }
        public string Handler { get; set; }

        public int Minute { get; set; }

        public DateTime? Runtime { get; set; }

    }
}
