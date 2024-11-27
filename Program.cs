using ChucksMasterMind;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

int attempts = 0;
var secret = GetSecret();//gets one random array of 4 numbers.
DisplayRules();


while (attempts < 10)
{
    Console.WriteLine("Enter your guess");//Prompt for guess
    int guess = 0;
    try
    {
        var goodguess = false;
        while (!goodguess)//validate guess
        {
            guess = Convert.ToInt32(Console.ReadLine());//get 4 digits.
            //TODO validation of the guess.
            goodguess = true;
        }
    }
    catch (Exception)//catch if bad char.
    {
        throw;
    }

    Console.WriteLine(DisplayHint(guess));//display the hint to user.


    attempts++;//increase guess count
}
String DisplayHint(int guess)
{
    int[] result = guess.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();//converts int to int[] and the actual number not byte code.
    StringBuilder hint = new StringBuilder();

    ResetSecretMatches();//clear the slate because you are rechecking the matches with every guess.


    //foreach (int item in result)
    //{
    //    if (secret.Contains(item))
    //    {

    //        hint.Append("-");
    //    }
    //}
    for (int i = 0; i < result.Length; i++)//find the PLUS hint. correct number and POS.
    {
        if (secret.SecretPart[i].Value == result[i] && secret.SecretPart[i].Index == i && secret.SecretPart[i].MatchedPlus == false)
        {
            hint.Append('+');
            secret.SecretPart[i].MatchedPlus = true;
            secret.SecretPart[i].Compared = true;
        }
        else
        {
            
        }
    }

    for (int i = 0; i < result.Length; i++)//find the minus hint. correct number but not POS.
    {
        foreach (Number item in secret.SecretPart)
        {
            if (item.Value == result[i] && item.MatchedMinus == false && item.MatchedPlus == false)
            {
                hint.Append('-');
                secret.SecretPart[i].MatchedMinus = true;
                secret.SecretPart[i].Compared = true;
            }
        }
    }




    return hint.ToString();
}

void ResetSecretMatches()
{
    foreach (Number item in secret.SecretPart)
    {
        item.MatchedMinus = false;
        item.MatchedPlus = false;
        item.Compared = false;
    }
}
Secret GetSecret()
{
    var numbers = new int[] { 1, 2, 3, 4, 5, 6, };

    var selection = Random.Shared.GetItems(numbers, 4);//new method in net 8
    Debug.WriteLine(JsonSerializer.Serialize(selection));

    Secret secret = new Secret();
    Number num;
    for (int i = 0;i < selection.Length; i++)
    {
        num = new Number(selection[i], i);
        secret.SecretPart.Add(num);
    }

    Debug.WriteLine(JsonSerializer.Serialize(secret));

    return secret;
}
void DisplayRules()
{
    Console.WriteLine("**************************************************************");
    Console.WriteLine("***  Your guess must have 4 digits.                        ***");
    Console.WriteLine("***  Your guess can only use numbers 1-6                   ***");
    Console.WriteLine("***                                                        ***");
    Console.WriteLine("**************************************************************");
}