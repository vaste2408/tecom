using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class RangeTest : Test
    {
        public short[] borders { get; set; }
        public RangeTest()
        {
            Type = TestTypes.Range;
            Examine = (a, l) =>
            {
                var mark = new Mark();

                if (a >= borders[1] && a <= borders[2])
                    mark.Score = (sbyte)Marks.Хор;

                else if ((a >= borders[0] && a < borders[1]) ||
                    (a > borders[2] && a <= borders[3]))
                {
                    l.Add((a >= borders[0] && a < borders[1]
                        ? "Candidate " + paramName + " is in [" + borders[0].ToString() + ";" + borders[1].ToString() + ") range"
                        : "Candidate " + paramName + "  is in [" + borders[2].ToString() + ";" + borders[3].ToString() + ") range")
                        + " : passable");
                    mark.Score = (sbyte)Marks.Уд;
                }

                else if (a > borders[3] || a < borders[0])
                {
                    l.Add((a > borders[3]
                        ? "Candidate " + paramName + "  is more than " + borders[3].ToString()
                        : "Candidate " + paramName + "  is less than " + borders[0].ToString())
                        + " : unexceptable");
                    mark.Score = (sbyte)Marks.Неуд;
                }

                return mark;
            };
        }
    }
}