using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class Test
    {
        public Mark Mark { get; set; }
        public TestTypes Type { get; set; }

        public Func<dynamic, List<string>, Mark> Examine { get; set; }
        public dynamic param { get; set; }
        public string paramName { get; set; }

        public Test()
        {

        }

        public void Begin(List<string> log)
        {
            try
            {
                Mark = Examine(param, log);
            }
            catch (Exception e)
            {
                log.Add(e.Message);
            }

        }
    }

    public enum TestTypes
    {
        Range = 0,
        Doctor = 1,
        Fuzz = 2
    };
}