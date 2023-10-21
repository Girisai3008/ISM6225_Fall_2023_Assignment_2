using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            // Question 2:
            Console.WriteLine("Question 2");
            string parenthesis = "()";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            // Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            // Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            // Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            // Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            // Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            // Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -10^9 <= lower <= upper <= 10^9
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity: O(1)
        */

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            int numCount = nums.Length; // Counting No of elements in the given array.
            IList<IList<int>> missingRanges = new List<IList<int>>(); //Considering the new list for storing the missing values

            if (numCount == 0)
            {
                // If the nums array is empty, it means that there are no missing values
                missingRanges.Add(new List<int> { lower, upper }); // Add the entire range as a missing range.
                return missingRanges;
            }

            if (nums[0] > lower)
            {
                // If the first number in nums array is greater than the lower bound value,
                // i.e, there is a missing values blw lower bound and first value. 
                missingRanges.Add(new List<int> { lower, nums[0] - 1 });
            }

            for (int i = 1; i < numCount; ++i)
            {
                // Checking missing ranges between adjacent numbers in array nums.
                if ((long)nums[i] - nums[i - 1] > 1)
                {
                    // missing range between the two adjacent numbers.
                    missingRanges.Add(new List<int> { nums[i - 1] + 1, nums[i] - 1 });
                }
            }

            if (nums[numCount - 1] < upper)
            {
                // If the last number in nums array is less than the upper bound,
                // i.e,there is missing range between the last number and the upper bound.
                missingRanges.Add(new List<int> { nums[numCount - 1] + 1, upper });
            }

            return missingRanges;
        }


        /*
        Question 2
        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid. An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.

        Example 1:
        Input: s = "()"
        Output: true
        Example 2:
        Input: s = "()[]{}"
        Output: true
        Example 3:
        Input: s = "(]"
        Output: false

        Constraints:
        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity: O(n), space complexity: O(1)
        */

        public static bool IsValid(string s)
        {
            // Creating a stack to store the opening brackets
            Stack<char> newStack = new Stack<char>();

            foreach (char currentChar in s)
            {
                if (currentChar == '(' || currentChar == '[' || currentChar == '{')
                {
                    // By using the Push function, pushing the brackets into the stack.
                    newStack.Push(currentChar);
                }
                else
                {
                    if (newStack.Count == 0)
                        // No matching opening bracket --> Invalid.
                        return false;
                    // Getting the opening bracket from the stack.
                    char lastopen = newStack.Pop();

                    // Now checking if the current character is a matching closing bracket for the last opening bracket or not.
                    if (currentChar == ')' && lastopen != '(')
                        return false;
                    if (currentChar == ']' && lastopen != '[')
                        return false;
                    if (currentChar == '}' && lastopen != '{')
                        return false;
                }
            }
            // Finally, if the stack is empty, all brackets are matched.
            return newStack.Count == 0;
        }


        /*
        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day. You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock. Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.

        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.

        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity: O(1)
        */

        public static int MaxProfit(int[] prices)
        {
            // maxprofit to zero
            int maxProfitValue = 0;
            int minPriceValue = Int32.MaxValue;


            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPriceValue)
                {
                    // If the current price is lower than the minimum price,change minimum price value to the current price.
                    minPriceValue = prices[i];
                }
                else if (prices[i] - minPriceValue > maxProfitValue)
                {
                    // If the difference between the current price and the minimum price
                    // is greater than the maximum profit , change  maximum profit.
                    maxProfitValue = prices[i] - minPriceValue;
                }
            }

            // Return the maximum profit.
            return maxProfitValue;
        }


        /*
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number. A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).

        Example 1:
        Input: num = "69"
        Output: true
        Example 2:
        Input: num = "88"
        Output: true
        Example 3:
        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity: O(n), space complexity: O(1)
        */
        public static bool IsStrobogrammatic(string s)
        {
            // Initializing a  pointer to string left end
            int leftValue = 0;
            // Initializing a pointer to string right end

            int rightValue = s.Length - 1;
            while (leftValue <= rightValue)
            {
                // Checking  the characters at the left and right pointers  are creating a  valid strobogrammatic pair or not.
                if (s[leftValue] == '6' && s[rightValue] == '9' ||
                    s[leftValue] == '9' && s[rightValue] == '6' ||
                    s[leftValue] == '0' && s[rightValue] == '0' ||
                    s[leftValue] == '1' && s[rightValue] == '1' ||
                    s[leftValue] == '8' && s[rightValue] == '8')
                {
                    leftValue++;
                    rightValue--;
                }
                else
                {
                    // If they not form a valid strobogrammatic pair, return false.
                    return false;
                }
            }

            // If it is not false return true
            return true;
        }


        /*
        Question 5:
        Given an array of integers nums, return the number of good pairs. A pair (i, j) is called good if nums[i] == nums[j] and i < j.

        Example 1:
        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:
        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array is good.
        Example 3:
        Input: nums = [1,2,3]
        Output: 0

        Constraints:
        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity: O(n), space complexity: O(n)
        */

        public static int NumIdenticalPairs(int[] nums)
        {
            // Initializing array to count the occurrences of each number .
            int[] counting = new int[101];
            int goodValues = 0;

            foreach (int num in nums)
            {
                // Adding  the no of occurrences of the current number to goodValues.
                goodValues += counting[num];
                counting[num]++;
            }
            // Returning the total count value of good values.
            return goodValues;
        }



        /*
        Question 6:
        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:
        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:
        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:
        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.

        Constraints:
        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity: O(nlogn), space complexity: O(n)
        */

        public static int ThirdMax(int[] nums)
        {
            // Initializing three variables for storing first, second, and third maximum values
            long maxOneValue = long.MinValue;
            long maxTwoValue = long.MinValue;
            long maxThreeValue = long.MinValue;

            foreach (int num in nums)
            {
                if (num > maxOneValue)
                {
                    // If the current number is greater than the first maximum value , update three max values.
                    maxThreeValue = maxTwoValue;
                    maxTwoValue = maxOneValue;
                    maxOneValue = num;
                }
                else if (num < maxOneValue && num > maxTwoValue)
                {
                    // current number is in b/w the first and second maximum, change the second, third max values.
                    maxThreeValue = maxTwoValue;
                    maxTwoValue = num;
                }
                else if (num < maxTwoValue && num > maxThreeValue)
                {
                    // current number is in b/w the second and third maximum, change  the third max value.
                    maxThreeValue = num;
                }
            }

            if (maxThreeValue != long.MinValue)
            {
                // If third maximum value is valid , return its value.
                return (int)maxThreeValue;
            }
            else
            {
                // If third maximum value is not there, return the first maximum.
                return (int)maxOneValue;
            }
        }

        /*
        Question 7:
        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner. Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].

        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:
        Input: currentState = "+"
        Output: []

        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Time complexity: O(n), space complexity: O(n)
        */

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            List<string> possibleMoves = new List<string>();
            for (int i = 0; i < currentState.Length - 1; i++)
            {
                // Checking current character and the next character are valid.
                if (currentState[i] == '+' && currentState[i + 1] == '+')
                {
                    // Creating a char array to represent the next state and also update the '+' to '-'.
                    char[] next = currentState.ToCharArray();
                    next[i] = '-';
                    next[i + 1] = '-';

                    possibleMoves.Add(new string(next));
                }
            }

            return possibleMoves;
        }

        /*
        Question 8:
        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.

        Example 1:
        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"
        Example 2:
        Input: s = "aeiou"
        Output: ""

        Time complexity: O(n), space complexity: O(n)
        */

        public static string RemoveVowels(string s)
        {
            string vowels = "aeiouAEIOU";
            string result = "";

            foreach (char c in s)
            {
                if (!vowels.Contains(c.ToString()))
                {
                    result += c;
                }
            }

            return result;
        }

        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }

    }
}
