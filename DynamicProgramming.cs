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


        public static int NumDecoding(string s)
        {
            if (s.Length == 0) return 0;

            int[] dp = new int[s.Length + 1];
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= s.Length; i++)
            {
                if (s[i - 1] - '0' <= 9)
                    dp[i] += dp[i - 1];

                var twoDigit = int.Parse(s.Substring(i - 2, 2));

                if (twoDigit >= 10 && twoDigit <= 26)
                    dp[i] += dp[i - 2];
            }
            return dp[s.Length];
        }

        public static int NumDecord(string s)
        {
            if (s.Length == 0) return 0;
            var n = s.Length;
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                if (s[i - 1] - '0' < 10)
                    dp[i] += dp[i - 1];

                var twoPoint = int.Parse(s.Substring(i - 2, 2));

                if ((twoPoint - '0') > 10 && (twoPoint - '0') <= 26)
                    dp[i] += dp[i - 2];
            }
            return dp[n];
        }

        public static bool DecomposedString(string s, string[] dict)
        {
            if (s.Length == 0) { return false; }

            bool[] dp = new bool[s.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                dp[i] = false;
            }

            var startIndex = 0;
            for (int i = 0; i <= s.Length; i++)
            {
                var v = s.Substring(startIndex, i - startIndex);
                if (dict.Contains(v))
                {
                    dp[i] = true;
                    startIndex = i;
                }
            }

            return dp[s.Length];
        }

        //Input: [4, 3, 4, 2, 6, 1, 2, 1]
        //Output: 2

        //Explanation: index 0 -> index 4 -> index 7

        //Alternative 3-hop path(valid, but longer):
        //index 0 -> index 1 -> index 4 -> index 7
        public static int MinJumps(int[] arr)
        {
            int n = arr.Length;

            // If the array has only one element, we are already at the end
            if (n <= 1)
                return 0;

            // If the first element is 0, we can't move anywhere
            if (arr[0] == 0)
                return -1;

            // Initialize variables
            int jumps = 1;  // Number of jumps needed to reach the end
            int maxReach = arr[0]; // Farthest index we can currently reach
            int steps = arr[0]; // Steps we can take within the current jump range

            // Start iterating through the array
            for (int i = 1; i < n; i++)
            {
                // If we have reached the last index, return the jump count
                if (i == n - 1)
                    return jumps;

                // Update maxReach
                maxReach = Math.Max(maxReach, i + arr[i]);

                // Use one step
                steps--;

                // If no more steps are left
                if (steps == 0)
                {
                    // We must jump to continue
                    jumps++;

                    // If the current index is beyond maxReach, we can't proceed
                    if (i >= maxReach)
                        return -1;

                    // Reinitialize the steps for the next jump
                    steps = maxReach - i;
                }
            }

            return -1; // If we reach here, it means we couldn't reach the end
        }

        //Input: n = 3, k = 2, row = 0, col = 0
        //Output: .0625

        //Explanation:

        //First Move: 2 moves will keep the knight on the board:
        //- (1, 2) (down a row, right 2 columns -> aka right 2, down 1)
        //- (2, 1) (down 2 rows, right 1 column -> aka down 2, right 1)

        //Second Move: From each of the new positions, there are 2 possible moves that keep the knight on-board.

        //Starting at cell(1, 2) :
        //- (-1, -2) (up a row, left 2 columns -> aka left 2, up 1)
        //- (1, -2) (down a row, left 2 columns -> aka left 2, down 1)

        //Starting at cell(2, 1) :
        //- (-2, -1) (up 2 rows, left 1 column -> aka up 2, left 1)
        //- (-2, 1) (up 2 rows, right 1 column -> aka up 2, right 1)

        //Since the knight makes 2 moves, & has 8 choices each move -> there are 8^2 possible 2-move sequences.So 64 total move pattern possibilities.

        //4 sequences keep the knight on the board out of 64 -> 4/64 = 0.0625
        public static double KnightProbabilityOnBoard(int n, int k, int row, int col)
        {
            // Directions for knight moves
            int[][] directions = new int[][]
            {
            new int[] {-2, -1}, new int[] {-2, 1}, new int[] {-1, -2}, new int[] {-1, 2},
            new int[] {1, -2}, new int[] {1, 2}, new int[] {2, -1}, new int[] {2, 1}
            };

            // DP table: dp[k][r][c]
            double[,,] dp = new double[k + 1, n, n];

            // Base case: Probability is 1 at the starting position for k = 0
            dp[0, row, col] = 1;

            // Fill DP table
            for (int moves = 1; moves <= k; moves++)
            {
                for (int r = 0; r < n; r++)
                {
                    for (int c = 0; c < n; c++)
                    {
                        // Compute dp[moves][r][c]
                        foreach (var dir in directions)
                        {
                            int nr = r + dir[0];
                            int nc = c + dir[1];

                            // Check if the move is within bounds
                            if (nr >= 0 && nr < n && nc >= 0 && nc < n)
                            {
                                dp[moves, r, c] += dp[moves - 1, nr, nc] / 8.0;
                            }
                        }
                    }
                }
            }

            // Sum probabilities for all positions on the board after k moves
            double result = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    result += dp[k, r, c];
                }
            }

            return result;
        }

        //Input: [3, 7, 2, 3]

        //Output: true

        //Explanation:

        //[3, 7, 2, 3] -> Alice takes right pile -> [3, 7, 2] -> (Alice: 3, Bob: 0)
        //[3, 7, 2] -> Bob takes left pile -> [7, 2] -> (Alice: 3, Bob: 3)
        //[7, 2] -> Alice takes left pile -> [2] -> (Alice: 10, Bob: 3)
        //[2] -> Bob takes remaining pile -> [] -> (Alice: 10, Bob: 5)
        public static bool StoneGame(int[] piles)
        {
            int n = piles.Length;

            // Create a DP table
            int[,] dp = new int[n, n];

            // Base case: When i == j, dp[i][i] = piles[i]
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = piles[i];
            }

            // Fill the DP table for larger subarrays
            for (int length = 2; length <= n; length++) // Subarray lengths
            {
                for (int i = 0; i <= n - length; i++)
                {
                    int j = i + length - 1; // End of the subarray
                    dp[i, j] = Math.Max(piles[i] - dp[i + 1, j], piles[j] - dp[i, j - 1]);
                }
            }

            // If the final score difference is > 0, Alice can win
            return dp[0, n - 1] > 0;
        }

        //Input:
        //values = [60, 50, 70, 30]
        //        weights = [5, 3, 4, 2]
        //        maxWeight = 8

        //Output: 120
        //Explanation: We take items 1 and 2 (zero-indexed) for a total value of 120 and a total weight of 7.

       
        public static (int maxValue, List<int> items) Knapsack(int[] values, int[] weights, int maxWeight)
        {
            int n = values.Length;
            int[,] dp = new int[n + 1, maxWeight + 1];

            // Build the DP table
            for (int i = 1; i <= n; i++)
            {
                for (int w = 0; w <= maxWeight; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        // Include the item or exclude it
                        dp[i, w] = Math.Max(
                            dp[i - 1, w], // Exclude the item
                            dp[i - 1, w - weights[i - 1]] + values[i - 1] // Include the item
                        );
                    }
                    else
                    {
                        // Exclude the item
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            // Backtrack to find the items included
            int maxValue = dp[n, maxWeight];
            List<int> items = new List<int>();
            int remainingWeight = maxWeight;

            for (int i = n; i > 0 && maxValue > 0; i--)
            {
                if (dp[i, remainingWeight] != dp[i - 1, remainingWeight])
                {
                    // Item i-1 was included
                    items.Add(i - 1);
                    remainingWeight -= weights[i - 1];
                    maxValue -= values[i - 1];
                }
            }

            items.Reverse(); // Reverse to get the items in the correct order
            return (dp[n, maxWeight], items);
        }

        public static (int maxValue, List<int> items) KnapsackBruteForce(int[] values, int[] weights, int maxWeight)
        {
            int n = values.Length;
            int maxValue = 0;
            List<int> bestSubset = new List<int>();

            // Total number of subsets = 2^n
            int totalSubsets = 1 << n;

            // Iterate through all subsets
            for (int subset = 0; subset < totalSubsets; subset++)
            {
                int currentWeight = 0;
                int currentValue = 0;
                List<int> currentSubset = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    // Check if the i-th item is included in the current subset
                    if ((subset & (1 << i)) != 0)
                    {
                        currentWeight += weights[i];
                        currentValue += values[i];
                        currentSubset.Add(i);
                    }
                }

                // Check if this subset is valid and better than the best seen so far
                if (currentWeight <= maxWeight && currentValue > maxValue)
                {
                    maxValue = currentValue;
                    bestSubset = new List<int>(currentSubset);
                }
            }

            return (maxValue, bestSubset);
        }

    }
}
