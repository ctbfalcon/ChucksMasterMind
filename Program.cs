using ChucksMasterMind;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    private static void Main(string[] args)
    {
        int attempts = 0;
        var secret = GetSecret();//gets one random array of 4 numbers.
        DisplayRules();


        while (attempts < 10)
        {
            Console.WriteLine("Enter your guess: ");//Prompt for guess
            int guess = 0;

            var goodguess = false;
            while (!goodguess)//validate guess
            {
                try
                {
                    guess = Convert.ToInt32(Console.ReadLine());//get 4 digits.
                    goodguess = ValidateGuess(guess);
                    if (!goodguess) {
                        Console.WriteLine("Your guess was not formatted correctly. Refer to the rules.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Your guess contained a non numeric value.  Please try again.");
                }
            }

            var result = DisplayHint(guess, secret);
            if (result == "") {
                Console.WriteLine("Nothing Matched. Try again.");
            }
            else {
                Console.WriteLine("Your hint: " + result);//display the hint to user.
            }

            attempts++;//increase attempts count

            if (result == "++++")
            {
                Console.WriteLine("You win!");
                Console.WriteLine("Game Over");
                break;
            }
            if (attempts == 10) {
                Console.WriteLine("Attempts reached. Sorry you loose");
                break; }
        }
    }

    public static bool ValidateGuess(int guess)
    {
        // Convert the number to a string
        string numberStr = guess.ToString();

        // Check if the number is exactly 4 digits long
        if (numberStr.Length != 4)
        {
            return false;
        }

        // Check if the number contains any alphabetic characters or digits outside 1-6
        foreach (char digit in numberStr)
        {
            if (!char.IsDigit(digit) || digit < '1' || digit > '6') // Check for non-digit or invalid range
            {
                return false;
            }
        }

        // If all conditions are met, the number is valid
        return true;
    }

    public static string DisplayHint(int guess, Secret secret)
    {
        int[] result = guess.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();//converts int to int[] and the actual number not byte code.
        StringBuilder hint = new StringBuilder();

        Guess Guessresult = BuildGuess(result);

        ResetSecretMatches(secret);//clear the slate because you are rechecking the matches with every guess.
                             //ResetGuessMatches();

        foreach (GuessNumber guessclass in Guessresult.GuessPart)//find the PLUS hint. correct number and POS.
        {
            foreach (GuessNumber sec in secret.SecretPart)
            {
                if (sec.Value == guessclass.Value && sec.Index == guessclass.Index && sec.MatchedPlus == false)
                {
                    hint.Append('+');
                    sec.MatchedPlus = true;
                    sec.Compared = true;
                    guessclass.GuessMatchedAlready = true;
                }
                else
                {

                }
            }
        }

        foreach (GuessNumber guessclass in Guessresult.GuessPart)//find the minus hint. correct number but not POS.
        {
            foreach (GuessNumber item in secret.SecretPart)
            {
                if (item.Value == guessclass.Value && item.MatchedMinus == false && item.MatchedPlus == false && guessclass.GuessMatchedAlready == false)
                {
                    hint.Append('-');
                    item.MatchedMinus = true;
                    item.Compared = true;
                    Debug.WriteLine("Matched on " + JsonSerializer.Serialize(item) + " with result " + guessclass.Value);
                }
            }
        }
        return hint.ToString();
    }
    public static void ResetGuessMatches()
    {
        throw new NotImplementedException();
    }

    public static Guess BuildGuess(int[] result)
    {
        Guess guess = new Guess();
        for (int i = 0; i < result.Length; i++)
        {
            guess.GuessPart.Add(new GuessNumber(result[i], i));
        }
        return guess;
    }

    public static void ResetSecretMatches(Secret secret)
    {
        foreach (GuessNumber item in secret.SecretPart)
        {
            item.MatchedMinus = false;
            item.MatchedPlus = false;
            item.Compared = false;
        }
    }
    public static Secret GetSecret()
    {
        var numbers = new int[] { 1, 2, 3, 4, 5, 6, };

        var selection = Random.Shared.GetItems(numbers, 4);//new method in net 8
        Debug.WriteLine(JsonSerializer.Serialize(selection));

        Secret secret = new Secret();
        GuessNumber num;
        for (int i = 0; i < selection.Length; i++)
        {
            num = new GuessNumber(selection[i], i);
            secret.SecretPart.Add(num);
        }

        Debug.WriteLine(JsonSerializer.Serialize(secret));

        return secret;
    }
    public static void DisplayRules()
    {
        Console.WriteLine("**************************************************************");
        Console.WriteLine("***  Your guess must have 4 digits.                        ***");
        Console.WriteLine("***  Your guess can only use numbers 1-6                   ***");
        Console.WriteLine("***  You only get 10 attempts.                             ***");
        Console.WriteLine("***  Numbers in the guess only count once.                 ***");
        Console.WriteLine("***  Number can show up more than once in the secret       ***");
        Console.WriteLine("**************************************************************");
    }
}