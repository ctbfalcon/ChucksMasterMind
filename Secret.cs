using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChucksMasterMind
{
    /// <summary>
    /// object to hold a collection of secret number objects. values indexes and flags
    /// </summary>
    public class Secret
    {
        //List<Number> numbers;
        public Secret()
        {
            this.SecretPart = new List<GuessNumber>();
        }
        public List<GuessNumber> SecretPart { get; set; }
    }
    /// <summary>
    /// object to hold the value index and flags of a number guess or secret.
    /// </summary>
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
    /// <summary>
    /// object to hold a collection of guess objects. Values and indexes and flags
    /// </summary>
    public class Guess
    {
        public Guess()
        {
            this.GuessPart = new List<GuessNumber>();
        }
        public List<GuessNumber> GuessPart { get; set; }
    }
}
