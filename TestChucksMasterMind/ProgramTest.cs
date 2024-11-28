using ChucksMasterMind;

namespace Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [DataRow(5555, "")]
        [DataRow(5235, "++")]
        [DataRow(4235, "++-")]
        [DataRow(4231, "++--")]
        [DataRow(4444, "+")]
        [DataRow(1234, "++++")]
        [DataRow(4321, "----")]
        public void DisplayHintTest(int i, string expected)
        {
            Secret testsecret = new();
            testsecret.SecretPart.Add(new GuessNumber(1, 0));
            testsecret.SecretPart.Add(new GuessNumber(2, 1));
            testsecret.SecretPart.Add(new GuessNumber(3, 2));
            testsecret.SecretPart.Add(new GuessNumber(4, 3));

            var hint = Program.DisplayHint(i, testsecret);
            Assert.IsTrue(hint == expected, "The " + i + " guess failed." + hint + " does not equal " + expected);

            //var hint = Program.DisplayHint(5555, testsecret);
            //Assert.IsTrue(hint == "", "The 5555 guess failed.");
            //hint = Program.DisplayHint(5235, testsecret);
            //Assert.IsTrue(hint == "++", "The 5235 guess failed." + hint + " does not equal ++");
            //hint = Program.DisplayHint(4235, testsecret);
            //Assert.IsTrue(hint == "++-", "The 4235 guess failed." + hint + " does not equal ++-");
            //hint = Program.DisplayHint(4231, testsecret);
            //Assert.IsTrue(hint == "++--", "The 4231 guess failed." + hint + " does not equal ++--");
            //hint = Program.DisplayHint(4444, testsecret);
            //Assert.IsTrue(hint == "+", "The 4444 guess failed." + hint + " does not equal +");
            //hint = Program.DisplayHint(1234, testsecret);
            //Assert.IsTrue(hint == "++++", "The 4444 guess failed." + hint + " does not equal ++++");
            //hint = Program.DisplayHint(4321, testsecret);
            //Assert.IsTrue(hint == "----", "The 4321 guess failed." + hint + " does not equal ----");
        }

        [TestMethod()]
        [DataRow(1234, true)]
        [DataRow(2468, false)]
        [DataRow(123, false)]
        [DataRow(9999, false)]
        public void ValidateGuessTest(int n, bool b)
        {
            var result = Program.ValidateGuess(n);
            Assert.AreEqual(result, b);
        }
    }
}