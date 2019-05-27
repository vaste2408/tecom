using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class Test
    {
        public string Name;
        public Mark Mark;
        public TestTypes Type;

        public Func<dynamic, List<string>, Mark> Examine;
        public dynamic param;

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

    public class RangeTest : Test
    {
        public short[] borders;
        public RangeTest()
        {
            Type = TestTypes.Range;
            Examine = (a, l) =>
            {
                var mark = new Mark();

                if (a >= borders[1] && a <= borders[2])
                    mark.SetScore((sbyte)Marks.Хор);

                else if ((a >= borders[0] && a < borders[1]) ||
                    (a > borders[2] && a <= borders[3]))
                {
                    l.Add((a >= borders[0] && a < borders[1]
                        ? "Candidate " + Name + " is in [" + borders[0].ToString() + ";" + borders[1].ToString() + ") range"
                        : "Candidate " + Name + "  is in [" + borders[2].ToString() + ";" + borders[3].ToString() + ") range")
                        + " : passable");
                    mark.SetScore((sbyte)Marks.Уд);
                }

                else if (a > borders[3] || a < borders[0])
                {
                    l.Add((a > borders[3]
                        ? "Candidate " + Name + "  is more than " + borders[3].ToString()
                        : "Candidate " + Name + "  is less than " + borders[0].ToString())
                        + " : unexceptable");
                    mark.SetScore((sbyte)Marks.Неуд);
                }

                return mark;
            };
        }
    }

    public class DoctorTest : Test
    {
        public sbyte[] maxDiseases;
        public string[] inspectDiseases;

        public DoctorTest()
        {
            Type = TestTypes.Doctor;
            Examine = (a,l) =>
            {
                var mark = new Mark();
                var uniques = ((string[]) a.oDiseasesAndHabbits.Split(' ')).Distinct();
                var disCount = String.IsNullOrWhiteSpace(a.oDiseasesAndHabbits) ? 0 : uniques.Intersect(inspectDiseases).Count();

                if (disCount <= maxDiseases[0])
                    mark.SetScore((sbyte)Marks.Хор);

                else if (disCount == maxDiseases[1])
                {
                    l.Add("Candidate has " + maxDiseases[1] + " illnes(-es) : passable");
                    mark.SetScore((sbyte)Marks.Уд);
                }

                else if (disCount > maxDiseases[2])
                {
                    l.Add("Candidate has more than " + maxDiseases[2] + " illnes(-es) : unexceptable");
                    mark.SetScore((sbyte)Marks.Неуд);
                }

                return mark;
            };
        }
    }

    public enum TestTypes
    {
        Range = 0,
        Doctor = 1,
        Fuzz = 2
    };

    public class TestSystem
    {
        private List <Test> Tests { get; set; }
        private List <string> ErrLog { get; set; }
        public Cosmo Candidate { get; set; }
        public List <string> Errors
        {
            get { return ErrLog; }
        }
        private List <Mark> Scores
        {
            get {
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

        public void PullTest (Test nt)
        {
            Tests.Add(nt);
        }
    }
}