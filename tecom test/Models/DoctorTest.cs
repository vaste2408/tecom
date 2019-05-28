using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class DoctorTest : Test
    {
        public sbyte[] maxDiseases { get; set; }
        public string[] inspectDiseases { get; set; }

        public DoctorTest()
        {
            Type = TestTypes.Doctor;
            Examine = (a, l) =>
            {
                var mark = new Mark();
                var uniques = ((string[])a.oDiseasesAndHabbits.Split(' ')).Distinct();
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
}