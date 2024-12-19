using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    public  class GreedyAlgo
    {

        //Input:
        //[
        //    [0, 3],
        //    [0, 6]
        //]

        //Output:
        //[
        //    [0, 3]
        //]

        //Explanation: The two intervals overlap so either could be removed to yield a valid solution.The most intervals that can be preserved is 1.
        public static List<int[]> NonOverlapIntervel(List<int[]> intervals)
        {
            // Sort intervals by their start value
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            // Initialize a list to store the non-overlapping intervals
            List<int[]> nonOverlapping = new List<int[]>();

            // Iterate through the sorted intervals
            foreach (var interval in intervals)
            {
                // If the list is empty or the current interval does not overlap with the last one
                if (nonOverlapping.Count == 0 || nonOverlapping.Last()[1] < interval[0])
                {
                    nonOverlapping.Add(interval);
                }
            }

            // Output the non-overlapping intervals
            foreach (var interval in nonOverlapping)
            {
                Console.WriteLine($"[{interval[0]}, {interval[1]}]");
            }
            return nonOverlapping;
        }


        static int FindSmallestNumberWithDigitSum(int totalDigits, int digitSum)
        {
            // Calculate the smallest and largest numbers with `totalDigits` digits
            int start = (int)Math.Pow(10, totalDigits - 1);
            int end = (int)Math.Pow(10, totalDigits) - 1;

            // Iterate through all numbers in the range
            for (int num = start; num <= end; num++)
            {
                // Calculate the sum of digits for the current number
                int sum = GetDigitSum(num);
                if (sum == digitSum)
                {
                    return num; // Return the first number that matches
                }
            }

            // If no number is found, return -1 (shouldn't happen for valid input)
            return -1;
        }

        static int GetDigitSum(int num)
        {
            int sum = 0;
            while (num > 0)
            {
                sum += num % 10; // Add the last digit
                num /= 10;       // Remove the last digit
            }
            return sum;
        }


        public static void Prim(int[,] graph, int vertices)
        {
            int[] parent = new int[vertices]; // Stores the MST
            int[] key = new int[vertices];   // Minimum weight edge for each vertex
            bool[] mstSet = new bool[vertices]; // Vertices included in MST

            // Initialize all keys to infinity and mstSet to false
            for (int i = 0; i < vertices; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0; // Start from the first vertex
            parent[0] = -1; // Root of MST

            for (int count = 0; count < vertices - 1; count++)
            {
                // Find the minimum key vertex not in MST
                int u = MinKey(key, mstSet, vertices);
                mstSet[u] = true; // Include vertex in MST

                // Update key and parent for adjacent vertices
                for (int v = 0; v < vertices; v++)
                {
                    if (graph[u, v] != 0 && !mstSet[v] && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }

            PrintMST(parent, graph, vertices);
        }

        private static int MinKey(int[] key, bool[] mstSet, int vertices)
        {
            int min = int.MaxValue, minIndex = -1;
            for (int v = 0; v < vertices; v++)
            {
                if (!mstSet[v] && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        private static void PrintMST(int[] parent, int[,] graph, int vertices)
        {
            Console.WriteLine("Edge \tWeight");
            for (int i = 1; i < vertices; i++)
            {
                Console.WriteLine($"{parent[i]} - {i} \t{graph[i, parent[i]]}");
            }
        }


        //Input: "1432219", k = 3
        //Output: "1219"
        //Explanation: We removed 3 digits("4", "3", "2") in the following positions -> "1 _ _ _ 219". There is no smaller integer we can achieve with 3 removals.
        public string RemoveKdigits(string num, int k)
        {
            if (num.Length == k) return "0"; // Edge case: remove all digits

            Stack<char> stack = new Stack<char>();

            foreach (char digit in num)
            {
                // Remove digits from the stack if they are greater than the current digit
                // and we still need to remove digits (k > 0)
                while (stack.Count > 0 && k > 0 && stack.Peek() > digit)
                {
                    stack.Pop();
                    k--;
                }

                stack.Push(digit);
            }

            // If there are still digits to remove, remove them from the end
            while (k > 0)
            {
                stack.Pop();
                k--;
            }

            // Build the result from the stack
            char[] resultArray = stack.ToArray();
            Array.Reverse(resultArray);
            string result = new string(resultArray);

            // Remove leading zeros
            result = result.TrimStart('0');

            // Return "0" if the result is empty
            return string.IsNullOrEmpty(result) ? "0" : result;
        }


        //Input: A = [1, 3, 2, 3, 4, 5], k = 3
        //Output: true
        //Explanation: We can partition our array into two arrays of consecutive integers[1, 2, 3] and[3, 4, 5].
        public bool CanPartitionIntoConsecutiveSets(int[] A, int k)
        {
            if (A.Length % k != 0) return false; // If total elements aren't divisible by k, return false

            // Use a SortedDictionary to keep numbers sorted and count their frequency
            SortedDictionary<int, int> count = new SortedDictionary<int, int>();
            foreach (int num in A)
            {
                if (!count.ContainsKey(num))
                    count[num] = 0;
                count[num]++;
            }

            // Try to form groups of size k
            foreach (var entry in count)
            {
                int num = entry.Key;
                int freq = entry.Value;

                if (freq > 0)
                {
                    // Try to form a group starting from this number
                    for (int i = 0; i < k; i++)
                    {
                        if (!count.ContainsKey(num + i) || count[num + i] < freq)
                            return false;
                        count[num + i] -= freq; // Reduce frequency of the numbers in the group
                    }
                }
            }

            return true; // Successfully partitioned
        }



    }
}
