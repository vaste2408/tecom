using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using tecom_test.Models;

namespace tecom_test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Cosmo candidate)
        {
            try
            {
                ViewBag.Candidate = candidate.Name;
                //create test pool
                var tests = new TestSystem
                {
                    Candidate = candidate
                };

                //range test
                var WeigthTest = new RangeTest
                {
                    Name = "Weigth"
                    ,
                    param = candidate.Weigth
                    ,
                    borders = new short[] {
                        70, 75, 90, 100
                    }
                };

                var TallTest = new RangeTest
                {
                    Name = "Tall"
                    ,
                    param = candidate.Length
                    ,
                    borders = new short[] {
                        160, 170, 185, 190
                    }
                };

                var AgeTest = new RangeTest
                {
                    Name = "Age"
                    ,
                    param = candidate.Age
                    ,
                    borders = new short[] {
                        23, 25, 35, 37
                    }
                };

                var EyesTest = new RangeTest
                {
                    Name = "Vision"
                    ,
                    param = candidate.Vision
                    ,
                    borders = new short[] {
                        0, 1, 1, 1
                    }
                };

                //doctor
                var SmokingTest = new DoctorTest
                {
                    Name = "Smoke"
                    ,
                    param = candidate
                    ,
                    inspectDiseases = new string[]{"smoke" }
                    ,
                    maxDiseases = new sbyte[] { 0, 2, 0 }
                };

                var TherapyTest = new DoctorTest
                {
                    Name = "Therapy"
                    ,
                    param = candidate
                    ,
                    inspectDiseases = new string[]{"cold"
                            , "bronchitis"
                            , "virus"
                            , "allergy"
                            , "quinsy"
                            , "insomnia" }
                    ,
                    maxDiseases = new sbyte[] {2, 3, 3}
                };

                var PsychoTest = new DoctorTest
                {
                    Name = "Psychology"
                    ,
                    param = candidate
                    ,
                    inspectDiseases = new string[]{"alcoholism"
                            , "insomnia"
                            , "narcomania"
                            , "injury" }
                    ,
                    maxDiseases = new sbyte[] {0, 1, 1}
                };

                //fuzz test
                var WeightAndHabbits = new Test
                {
                    Name = "Wight and bad habbits"
                    ,
                    Type = TestTypes.Fuzz
                    ,
                    param = candidate
                    ,
                    Examine = (a, l) =>
                    {
                        var mark = new Mark();

                        if (a.oDiseasesAndHabbits.Contains("smoke")
                            && (a.oDiseasesAndHabbits.Contains("virus") || a.oDiseasesAndHabbits.Contains("cold"))
                            && (a.Weigth > 120 || a.Weigth < 60))
                        {
                            if (a.oDiseasesAndHabbits.Contains("smoke"))
                                l.Add("Candidate smokes, he's ill and his weight is " +
                                    (a.Weigth > 120 ? "over than 120" : "less than 60")
                                    + " : unexceptable");
                            mark.SetScore((sbyte)Marks.Неуд);
                        }

                        else if ((a.oDiseasesAndHabbits.Contains("virus") || a.oDiseasesAndHabbits.Contains("cold") && a.Weigth > 110))
                        {
                            l.Add("Candidate is ill and his weight is over than 110 : passable");
                            mark.SetScore((sbyte)Marks.Уд);
                        }

                        else
                            mark.SetScore((sbyte)Marks.Хор);

                        return mark;
                    }
                };

                //pull tests into pool
                tests.PullTest(WeigthTest);
                tests.PullTest(TallTest);
                tests.PullTest(AgeTest);
                tests.PullTest(EyesTest);
                tests.PullTest(SmokingTest);
                tests.PullTest(TherapyTest);
                tests.PullTest(PsychoTest);
                tests.PullTest(WeightAndHabbits);

                tests.Initiate();

                //analyse tests for marks to make a desision
                if (!tests.CheckSuccess())
                {
                    ViewBag.Result = "failed";
                    ViewBag.Errors = tests.Errors.ToArray();
                }
                else
                    ViewBag.Result = "approved";

                return View("Result");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}