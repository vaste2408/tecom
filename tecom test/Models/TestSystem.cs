using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class TestSystem
    {
        private List<Test> Tests { get; set; }
        private List<string> ErrLog { get; set; }
        public Cosmo Candidate { get; set; }
        public List<string> Errors
        {
            get { return ErrLog; }
        }
        private List<Mark> Scores
        {
            get
            {
                var listMarks = new List<Mark>();
                foreach (var test in Tests)
                {
                    listMarks.Add(test.Mark);
                }
                return listMarks;
            }
            set { }
        }

        public void Initiate()
        {
            foreach (var test in Tests)
            {
                test.Begin(ErrLog);
            }
        }

        public bool CheckSuccess()
        {
            if (Scores.Count(m => m.Score == (sbyte)Marks.Неуд) > 0 || Scores.Count(m => m.Score == (sbyte)Marks.Уд) > 2)
                return false;
            return true;
        }

        public TestSystem()
        {
            Tests = new List<Test>();
            ErrLog = new List<string>();
        }

        public void PullTest(Test nt)
        {
            Tests.Add(nt);
        }
    }
}