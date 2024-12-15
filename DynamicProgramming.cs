using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public class DynamicProgramming
    {
        //scoreEvents = [1,7,7]
        //        Amouunt = 5
        // Answer = 3
        public static int MinCoins(int[] coins, int amount)
        {
            // Initialize DP array with a large value (Infinity)
            int[] dp = new int[amount + 1];
            Array.Fill(dp, int.MaxValue);
            dp[0] = 0; // Base case: 0 coins to make amount 0

            // Process each coin
            foreach (int coin in coins)
            {
                for (int i = coin; i <= amount; i++)
                {
                    if (dp[i - coin] != int.MaxValue)
                    {
                        dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                    }
                }
            }

            // If dp[amount] is still Infinity, return -1
            return dp[amount] == int.MaxValue ? -1 : dp[amount];
        }

        //        Input: [1, 2, 3, 4, 5, 6]
        //        Output: 5
        //Explanation: You buy on day 0 and sell on day 5 for a profit of 6 - 1 = 5.
        public static int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length < 2)
                return 0;

            int minPrice = int.MaxValue;
            int maxProfit = 0;

            foreach (int price in prices)
            {
                // Update the minimum price encountered so far
                if (price < minPrice)
                    minPrice = price;

                // Calculate profit and update maximum profit
                int profit = price - minPrice;
                if (profit > maxProfit)
                    maxProfit = profit;
            }
            return maxProfit;
        }

        //Input: m = 3, n = 3
        //Output: 6
        //Total unique paths to any given cell:
        // -----------
        //| 1 | 1 | 1 |
        //| 1 | 2 | 3 |
        //| 1 | 3 | 6 |
        //-----------
        public static int UniquePaths(int m, int n)
        {
            // Create a 2D array for DP
            int[,] dp = new int[m, n];

            // Initialize the first row and first column
            for (int i = 0; i < m; i++)
                dp[i, 0] = 1;
            for (int j = 0; j < n; j++)
                dp[0, j] = 1;

            // Fill the rest of the dp array
            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }

            // The bottom-right corner contains the result
            return dp[m - 1, n - 1];
        }

        //Input:
        //[
        //  [5],
        //  [1,6],
        //  [4,3,10],
        //  [3,2,4,1]
        //]
        //Output: 11
        //Explanation:
        //[
        //      [5*],
        //    [1*,  6],
        //   [4,3*, 10],
        //  [3,2*, 4, 1]
        //]

        static int MinimumTotal(List<List<int>> triangle)
        {
            // Start from the second-last row and move upward
            for (int row = triangle.Count - 2; row >= 0; row--)
            {
                for (int col = 0; col < triangle[row].Count; col++)
                {
                    // Update the current element to include the minimum path sum
                    triangle[row][col] += Math.Min(triangle[row + 1][col], triangle[row + 1][col + 1]);
                }
            }

            // The top element now contains the minimum path sum
            return triangle[0][0];
        }


        //Input:
        //scoreEvents = [2,3,7]
        //        finalScore = 12

        //Output: 4

        //Explanation:
        //There are 4 possible ways that the game ended with a final score of 12:
        //1.) [2, 2, 2, 2, 2, 2]
        //2.) [3, 3, 3, 3]
        //3.) [2, 2, 2, 3, 3]
        //4.) [2, 3, 7]


        public static int CountWaysToReachScore(int[] scoreEvents, int finalScore)
        {
            // Create a DP array to store the number of ways to reach each score
            int[] dp = new int[finalScore + 1];
            dp[0] = 1; // Base case: There's one way to reach a score of 0 (using no events)

            // Iterate through each score event
            foreach (int eventScore in scoreEvents)
            {
                for (int score = eventScore; score <= finalScore; score++)
                {
                    dp[score] += dp[score - eventScore];
                }
            }

            return dp[finalScore];
        }
    }
}
