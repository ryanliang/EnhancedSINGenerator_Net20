using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedSINGenerator
{
    class Luhn
    {
        static Random rand = new Random(); // Create only one Random object to improve randomness.
        const char DUMMY_DIGIT = '0'; 

        /// <summary>
        /// Return true if an integer passes Luhn Validation.
        /// For Luhn algorithm, please see <see href="http://en.wikipedia.org/wiki/Luhn_algorithm">Luhn Algorithm on Wiki</see>
        /// <remarks>Use string format to represent the integer to avoid the limitation
        /// to the number of digits allowed. For instance, an int (of 32) can have only up to 10 digit, while a string 
        /// is just an char array and can have more than 10 digits easily.</remarks>
        /// </summary>
        /// <param name="number">string version of the integer to be validated</param>
        /// <returns>True if the integer passes Luhn validation</returns>
        public static bool IsValidLuhnNumber(string number)
        {
            return MyCalculateCheckSum(number) == 0;
        }

        /// <summary>
        /// Generate a random integer in string format that will pass Luhn validation
        /// <remarks>Use string format to represent the integer to avoid the limitation
        /// to the number of digits allowed. For instance, an int (of 32) can have only up to 10 digit, while a string 
        /// is just an char array and can have more than 10 digits easily.</remarks>
        /// </summary>
        /// <param name="numDigit">Number of digit in the integer</param>
        /// <returns>The generated integer</returns>
        public static string GenerateLuhnNumber(int numDigit)
        {
            // TODO: zl Sep 23, 2010
            // Luhn numbers are not Canadian SINs.  Should refactor and create a CanadianSIN subclass
            // that will exclude 0 and 8 as the first digit.  Luhn class should be generic and no need to exclude
            // these two leading digits.
            char[] validFirstDigits = { '1', '2', '3', '4', '5', '6', '7', '9' };            
            char firstDigit = validFirstDigits[rand.Next(0, validFirstDigits.Length - 1)];

            return GenerateLuhnNumber(numDigit, firstDigit);
            
        }

        /// <summary>
        /// Specifiy the first digit and then generate a random integer in 
        /// string format that will pass Luhn validation.  
        /// <remarks>Use string format to represent the integer to avoid the limitation
        /// to the number of digits allowed. For instance, an int (of 32) can have only up to 10 digit, while a string 
        /// is just an char array and can have more than 10 digits easily.</remarks>
        /// </summary>
        /// <param name="numDigit">Number of digit in the integer</param>
        /// <param name="FirstDigit">First digit of the integer</param>
        /// <returns>The generated integer</returns>
        public static string GenerateLuhnNumber(int numDigit, char firstDigit)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(firstDigit);
            sb.Append(GenerateRandomChars(numDigit - 2));  // -1 for first digit and another -1 for checksum(last digit)
            string tempNumber = sb.ToString() + DUMMY_DIGIT; // pad a zero to the end for the checksum calculation                                                   
            sb.Append(MyCalculateCheckSum(tempNumber));

            return sb.ToString();
        }

        /// <summary>
        /// Calculate the check sum of a given integer in string format.  
        /// </summary>
        /// <param name="number">The integer to be checked</param>
        /// <returns>The checksum for the given number to pass the Luhn validation</returns>
        protected static int CalculateCheckSum(string number)
        {
            return 1;
        }

        /// <summary>
        /// Calculate the check sum of a given integer in string format.  
        /// </summary>
        /// <param name="number">The integer to be checked</param>
        /// <returns>The checksum for the given number to pass the Luhn validation</returns>
        protected static int MyCalculateCheckSum(string number)
        {
            char[] digits = number.ToCharArray();
            int sum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                int curr = (int)Char.GetNumericValue(digits[i]);
                if (i % 2 != 0) // digit at even position (note position start at 0 instead of 1 in C#)
                {                    
                    sum += curr * 2 > 9 ? curr * 2 - 9 : curr * 2; // -9 is a clever way of adding the two digit of the value (curr*2)
                                                                   // i.e if curr = 7 then 7*2 = 14 => 1 + 4 = 5 = 14 -9 
                }
                else
                { // digit at odd position 
                    sum += curr;
                }
            }

            return (sum % 10) == 0 ? 0 : 10 - (sum % 10);
        }

        /// <summary>
        /// Generate an random integer of length numDigit in string format.
        /// </summary>
        /// <param name="numDigit">Number of digit in the integer</param>
        /// <returns>Generated Random Number</returns>
        protected static string GenerateRandomChars(int numDigit)
        {
            StringBuilder sb = new StringBuilder();
            char c; 
            for (int i = 0; i < numDigit; i++)
            {   
                c = Convert.ToChar(rand.Next(48, 57)); // 48 to 57 are the acsii char for 0 to 9
                sb.Append(c);  
            }

            return sb.ToString();
        }
    }
}
