using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChucksMasterMind
{
    public class Secret
    {
        //List<Number> numbers;
        public Secret()
        {
            this.SecretPart = new List<GuessNumber>();
        }
        public List<GuessNumber> SecretPart { get; set; }
    }
    public class GuessNumber
    {
        public int Index { get; set; }
        public GuessNumber(int value, int index) { this.Value = value; this.Index = index; }
        public int Value { get; set; } = 0;
        public bool Compared { get; set; } = false;
        public bool MatchedPlus { get; set; } = false;
        public bool MatchedMinus { get; set; } = false;
        public bool GuessMatchedAlready { get; set; } = false;
    }
    public class Guess
    {
        public Guess()
        {
            this.GuessPart = new List<GuessNumber>();
        }
        public List<GuessNumber> GuessPart { get; set; }
    }
}
