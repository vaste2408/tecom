using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tecom_test.Models
{
    public class Mark
    {
        public sbyte Score {
            get {
                return GetScore();
            }
            set {
                SetScore(Score);
            }
        }
        private sbyte pScore { get; set; }
        public Mark()
        {

        }
        public Mark (sbyte score)
        {
            Score = score;
        }
        public void SetScore (sbyte score)
        {
            pScore = score;
        }
        public sbyte GetScore()
        {
            return pScore;
        }
    }
    public enum Marks
    {
        Хор = 1,
        Уд = 0,
        Неуд = -1
    };
}