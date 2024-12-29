using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class GraphDFS
    {
        public static bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            HashSet<int> visited = new HashSet<int>();
            void DFS(int room)
            {
                visited.Add(room);
                foreach (int key in rooms[room])
                {
                    if (!visited.Contains(key))
                    {
                        DFS(key);
                    }
                }
            }

            DFS(0);
            return visited.Count == rooms.Count;
        }

        public static int[][] FloodFill(int[][] image, int row, int col, int newColor)
        {
            if (image[row][col] == newColor)
                return image;
            int rows = image.Length; int cols = image[0].Length;

            image[row][col] = newColor;

            // Up Condition
            if (row + 1 > 0 && row + 1 < rows)
                FloodFill(image, rows + 1, col, newColor);

            // Down Condition
            if (row - 1 > 0 && row - 1 < rows)
                FloodFill(image, rows - 1, col, newColor);
            // Right Condition
            if (col + 1 > 0 && col + 1 < cols)
                FloodFill(image, row, col + 1, newColor);

            // Left Condition
            if (col - 1 > 0 && col - 1 < cols)
                FloodFill(image, row, col - 1, newColor);

            return image;
        }

        public static bool IsBipartite(List<int>[] graph)
        {
            if (graph == null || graph.Length == 0) return false;

            var n = graph.Length;
            int[] color = new int[n];

            bool DFS(int start)
            {
                // check if current and neighbor are not having same color                

                foreach (var neighbor in graph[start])
                {
                    if (color[neighbor] == 0)
                    {
                        color[neighbor] = -color[start];
                        DFS(neighbor);
                    }
                    else if (color[neighbor] == color[start])
                    {
                        return false;
                    }
                }
                return true;
            }

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    color[i] = 1;
                    if (!DFS(i))
                        return false;
                }
            }
            return true;
        }

        public static bool IsCyclic(List<int>[] graph)
        {
            int n = graph.Length;
            int[] color = new int[n];
            if (n == 0) return false;

            bool DFSCyclic(int start)
            {
                foreach (var neighbor in graph[start])
                {
                    if (color[neighbor] == 0)
                    {
                        color[neighbor] = 1;
                        return DFSCyclic(neighbor);
                    }
                    else
                        return true;
                }
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    color[i] = 1;
                    if (DFSCyclic(i))
                        return true;
                }
            }
            return false;
        }

        public static int WordLadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            // Step 1: Create a mapping of intermediate states to words
            Dictionary<string, List<string>> intermediateMap = new Dictionary<string, List<string>>();

            int wordLength = beginWord.Length;
            foreach (string word in wordList)
            {
                for (int i = 0; i < wordLength; i++)
                {
                    string intermediateState = word.Substring(0, i) + "*" + word.Substring(i + 1);
                    if (!intermediateMap.ContainsKey(intermediateState))
                    {
                        intermediateMap[intermediateState] = new List<string>();
                    }
                    intermediateMap[intermediateState].Add(word);
                }
            }

            // Step 2: BFS to find the shortest path
            Queue<(string word, int level)> queue = new Queue<(string word, int level)>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue((beginWord, 1));
            visited.Add(beginWord);

            while (queue.Count > 0)
            {
                var (currentWord, level) = queue.Dequeue();

                for (int i = 0; i < wordLength; i++)
                {
                    string intermediateState = currentWord.Substring(0, i) + "*" + currentWord.Substring(i + 1);

                    if (intermediateMap.ContainsKey(intermediateState))
                    {
                        foreach (string neighbor in intermediateMap[intermediateState])
                        {
                            if (neighbor == endWord) return level + 1;

                            if (!visited.Contains(neighbor))
                            {
                                visited.Add(neighbor);
                                queue.Enqueue((neighbor, level + 1));
                            }
                        }
                    }
                }
            }

            return 0; // No path found
        }

    }
}
