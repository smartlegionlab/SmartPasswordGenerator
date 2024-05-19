// Smart Password Generator
using System;
using System.Security.Cryptography;
using System.Text;


class StringDataMaster
{
    public Random rand = new Random();
    public string numbers = "1234567890";
    public string simbols = "@$!%*#?&-";
    public string smallLeters = "abcdefjhijklmnopqrstuvwxyz";
    public string bigLeters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public char[] GetNumbers()
    {
        return this.numbers.ToCharArray();
    }

    public char[] GetSimbols()
    {
        return this.simbols.ToCharArray();
    }

    public char[] GetSmallLaters()
    {
        return this.smallLeters.ToCharArray();
    }

    public char[] GetBigLaters()
    {
        return this.bigLeters.ToCharArray();
    }

    public char[] GetAllStrings()
    {
        string letters = this.numbers + this.simbols + this.smallLeters + this.bigLeters;
        return letters.ToCharArray();
    }
}


class BaseRandomGen
{
    StringDataMaster stringDataMaster = new StringDataMaster();

    private string GetRandomString(char[] items, int length)
    {
        string randomString = "";
        for (int i = 1; i <= length; i++)
        {
            int letter_num = this.stringDataMaster.rand.Next(0, items.Length - 1);
            randomString += items[letter_num];
        }

        return randomString;
    }

    public string CreateRandomString(int length)
    {
        char[] allStrings = this.stringDataMaster.GetAllStrings();
        string newString = this.GetRandomString(allStrings, length);
        return newString;
    }

    public string CreateRandomNumbers(int length)
    {
        char[] numbers = this.stringDataMaster.GetNumbers();
        string newString = this.GetRandomString(numbers, length);
        return newString;
    }

    public string CreateRandomSimbols(int length)
    {
        char[] simbols = this.stringDataMaster.GetSimbols();
        string newString = this.GetRandomString(simbols, length);
        return newString;
    }

    public string CreateRandomSmallLetters(int length)
    {
        char[] smallLetters = this.stringDataMaster.GetSmallLaters();
        string newString = this.GetRandomString(smallLetters, length);
        return newString;
    }

    public string CreateRandomBigLetters(int length)
    {
        char[] bigLetters = this.stringDataMaster.GetBigLaters();
        string newString = this.GetRandomString(bigLetters, length);
        return newString;
    }
}


class SmartRandomGen
{

    StringDataMaster stringDataMaster = new StringDataMaster();

    private int GetSeed(string input)
    {
        byte[] hash;
        using (SHA256 sha256 = SHA256.Create())
        {
            hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        int seed = BitConverter.ToInt32(hash, 0);
        return seed;
    }

    private string GetSmartRandomString(char[] items, int length, string seed)
    {
        int seedHash = this.GetSeed(seed);
        Random rand = new Random(seedHash);
        StringBuilder randomString = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int letter_num = rand.Next(0, items.Length);
            randomString.Append(items[letter_num]);
        }

        return randomString.ToString();
    }

    public string CreateSmartRandomString(int length, string secret)
    {
        char[] allString = this.stringDataMaster.GetAllStrings();
        string newString = this.GetSmartRandomString(allString, length, secret);
        return newString;
    }

}

class SmartPassGen
{
    BaseRandomGen baseRandomGen = new BaseRandomGen();
    SmartRandomGen smartRandomGen = new SmartRandomGen();

    public string CreatePassword(int length)
    {
        return baseRandomGen.CreateRandomString(length);
    }

    public string CreateSmartPassword(int length, string secret)
    {
        return smartRandomGen.CreateSmartRandomString(length, secret);
    }
}

class SmartPasswordGeneratorApp
{
    static void Main(string[] args)
    {
        SmartPassGen smartPassGen = new SmartPassGen();
        string password = "";
        Console.WriteLine("*** Smart Password Generator ***");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("To select a mode,\nenter the number corresponding\nmenu item and press <Enter>: ");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("1. Smart Password Generator.");
        Console.WriteLine("2. Default password generator.");
        Console.WriteLine("0. Exit.");
        Console.WriteLine("--------------------------------");
        string input = Console.ReadLine();
        Console.WriteLine("--------------------------------");
        if (input == "1")
        {
            Console.WriteLine("Smart Password Generator");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Enter password length: ");
            Console.WriteLine("--------------------------------");
            int passwordLength = 0;
            try
            {
                passwordLength = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("--------------------------------");
            }
            catch (Exception e)
            {
                passwordLength = 12;
                Console.WriteLine("Error! You have not entered a password length. I use 12 characters.");
                Console.WriteLine("--------------------------------");
            }
            if (passwordLength <= 0)
            {
                passwordLength = 12;
                Console.WriteLine("Error! Invalid password length. I use 12 characters.");
                Console.WriteLine("--------------------------------");
            }
            else if (passwordLength > 1000)
            {
                passwordLength = 1000;
                Console.WriteLine("Error! The password cannot be longer than 1000 characters. I use 1000 characters.");
                Console.WriteLine("--------------------------------");
            }
            string secretPhrase = "";
            Console.WriteLine("Enter secret phrase: ");
            Console.WriteLine("--------------------------------");
            secretPhrase = Console.ReadLine();
            Console.WriteLine("--------------------------------");
            password = smartPassGen.CreateSmartPassword(passwordLength, secretPhrase);
            Console.WriteLine(password);
        } else if (input == "2") {
            Console.WriteLine("Default password generator");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Enter password length: ");
            Console.WriteLine("--------------------------------");
            int passwordLength = 0;
            try
            {
                passwordLength = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("--------------------------------");
            }
            catch (Exception e)
            {
                passwordLength = 12;
                Console.WriteLine("Error! You have not entered a password length. I use 12 characters.");
                Console.WriteLine("--------------------------------");
            }
            if (passwordLength <= 0)
            {
                passwordLength = 12;
                Console.WriteLine("Error! Invalid password length. I use 12 characters.");
                Console.WriteLine("--------------------------------");
            }
            else if (passwordLength > 1000)
            {
                passwordLength = 1000;
                Console.WriteLine("Error! The password cannot be longer than 1000 characters. I use 1000 characters.");
                Console.WriteLine("--------------------------------");
            }
            password = smartPassGen.CreatePassword(passwordLength);
            Console.WriteLine(password);
        } else
        {
            Console.WriteLine("Exit");
        }
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Press <Enter> to exit...");
        Console.Read();
        Console.WriteLine("--------------------------------");
        Console.WriteLine("=== Smart Legion Lab ===");
    }
}