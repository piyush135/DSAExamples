using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class GraphBFS
    {
        public static bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            HashSet<int> visited = new HashSet<int>();
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(0);
            visited.Add(0);

            while (queue.Count > 0)
            {
                int room = queue.Dequeue();
                foreach (int key in rooms[room])
                {
                    if (!visited.Contains(key))
                    {
                        visited.Add(key);
                        queue.Enqueue(key);
                    }
                }
            }

            return visited.Count == rooms.Count;
        }

        public static int[][] FloodFill(int[][] image, int row, int col, int newColor)
        {
            // add the current row and col in queue
            // move the current cordinate in up, down, left and right 
            // check if current color is not equal to new color 
            // flip it 
            int rows = image.Length, cols = image[0].Length;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((row, col));

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();

                image[r][c] = newColor;
                var nr = r + 1;
                var nc = c;

                // down condition
                if (nr > 0 && nr <= rows && image[nr][c] != newColor)
                    queue.Enqueue((nr, c));
                // Up condition
                nr = r - 1;
                if (nr > 0 && nr <= rows && image[nr][c] != newColor)
                    queue.Enqueue((nr, c));
                // right condition
                nc = c + 1;
                if (nc > 0 && nc <= cols && image[r][nc] != newColor)
                    queue.Enqueue((r, nc));
                // Left condition
                nc = c - 1;
                if (nc > 0 && nc <= rows && image[r][nc] != newColor)
                    queue.Enqueue((r, nc));

            }
            return image;
        }

        // Input:
        //  2
        // / \
        //3   4

        // Output:
        // [
        //   [2],
        //   [3, 4]
        // ]
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null) return result;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                var currentLevel = new List<int>();

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode currentNode = queue.Dequeue();
                    currentLevel.Add(currentNode.val);

                    if (currentNode.left != null)
                    {
                        queue.Enqueue(currentNode.left);
                    }

                    if (currentNode.right != null)
                    {
                        queue.Enqueue(currentNode.right);
                    }
                }

                result.Add(currentLevel);
            }

            return result;
        }

        public static bool IsBipartite(List<int>[] graph)
        {
            // iterate graph to lenght
            // assign color 

            if (graph == null) return false;

            var n = graph.Length;

            Queue<int> queue = new Queue<int>();
            int[] color = new int[n];

            bool BFS(int start)
            {
                queue.Enqueue(start);
                color[start] = 1;

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    foreach (var neighbor in graph[current])
                    {
                        if (color[neighbor] == 0)
                        {
                            // assign the oposite color
                            color[neighbor] = -color[current];
                            queue.Enqueue(neighbor);

                        }
                        else if (color[neighbor] == color[current])
                            return false;
                    }
                }
                return true;

            }

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0) // If not visited
                {
                    if (!BFS(i))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsCyclic(List<int>[] graph)
        {
            int n = graph.Length;
            if (n == 0) return false;

            int[] color = new int[n];
            Queue<int> queue = new Queue<int>();


            bool BSFCyclic(int start)
            {
                color[start] = 1;
                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    foreach (var neighbor in graph[current])
                    {
                        if (color[neighbor] == 0)
                        {
                            color[neighbor] = 1;
                            queue.Enqueue(neighbor);
                        }
                        else
                            return true;
                    }
                }
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    if (BSFCyclic(i))
                        return true;
                }
            }
            return false;
        }

        public static int WordLadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> wordSet = new HashSet<string>(wordList);
            if (!wordSet.Contains(endWord)) return 0;

            Queue<(string word, int level)> queue = new Queue<(string, int)>();
            queue.Enqueue((beginWord, 0));

            while (queue.Count > 0)
            {
                var (currentWord, level) = queue.Dequeue();

                foreach (string neighbor in GetNeighbors(currentWord, wordSet))
                {
                    if (neighbor == endWord) return level + 1;

                    queue.Enqueue((neighbor, level + 1));
                    wordSet.Remove(neighbor); // Mark as visited
                }
            }

            return 0; // No path found
        }

        private static IEnumerable<string> GetNeighbors(string word, HashSet<string> wordSet)
        {
            char[] wordArray = word.ToCharArray();
            for (int i = 0; i < wordArray.Length; i++)
            {
                char originalChar = wordArray[i];
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (c == originalChar) continue;
                    wordArray[i] = c;
                    string newWord = new string(wordArray);
                    if (wordSet.Contains(newWord)) yield return newWord;
                }
                wordArray[i] = originalChar; // Restore original character
            }
        }


        public static List<int> TopologicalSort(int nodes, List<int>[] graph)
        {
            int[] inDegree = new int[nodes];
            foreach (var neighbors in graph)
            {
                foreach (int neighbor in neighbors)
                {
                    inDegree[neighbor - 1]++;
                }
            }

            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < nodes; i++)
            {
                if (inDegree[i] == 0) queue.Enqueue(i);
            }

            List<int> topologicalOrder = new List<int>();
            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                topologicalOrder.Add(node + 1);

                foreach (int neighbor in graph[node])
                {
                    inDegree[neighbor - 1]--;
                    if (inDegree[neighbor - 1] == 0) queue.Enqueue(neighbor - 1);
                }
            }

            if (topologicalOrder.Count == nodes)
                return topologicalOrder;

            throw new InvalidOperationException("The graph contains a cycle!");
        }

        public static IList<int> DistanceK(TreeNode root, int x, int k)
        {
            var parentMap = new Dictionary<TreeNode, TreeNode>();
            var targetNode = FindTargetAndBuildParentMap(root, null, x, parentMap);
            return FindNodesAtDistanceK(targetNode, k, parentMap);
        }

        private static TreeNode FindTargetAndBuildParentMap(TreeNode node, TreeNode parent, int x, Dictionary<TreeNode, TreeNode> parentMap)
        {
            if (node == null) return null;

            // Add the current node's parent to the map
            if (parent != null)
            {
                parentMap[node] = parent;
            }

            // Check if this is the target node
            if (node.val == x)
            {
                return node;
            }

            // Search left and right subtrees
            var left = FindTargetAndBuildParentMap(node.left, node, x, parentMap);
            if (left != null) return left;

            return FindTargetAndBuildParentMap(node.right, node, x, parentMap);
        }

        private static IList<int> FindNodesAtDistanceK(TreeNode target, int k, Dictionary<TreeNode, TreeNode> parentMap)
        {
            var result = new List<int>();
            var visited = new HashSet<TreeNode>();
            var queue = new Queue<(TreeNode Node, int Distance)>();

            queue.Enqueue((target, 0));
            visited.Add(target);

            while (queue.Count > 0)
            {
                var (currentNode, distance) = queue.Dequeue();

                if (distance == k)
                {
                    result.Add(currentNode.val);
                }

                // If distance exceeds k, stop processing
                if (distance > k)
                {
                    break;
                }

                // Traverse neighbors (left, right, parent)
                if (currentNode.left != null && !visited.Contains(currentNode.left))
                {
                    queue.Enqueue((currentNode.left, distance + 1));
                    visited.Add(currentNode.left);
                }

                if (currentNode.right != null && !visited.Contains(currentNode.right))
                {
                    queue.Enqueue((currentNode.right, distance + 1));
                    visited.Add(currentNode.right);
                }

                if (parentMap.ContainsKey(currentNode) && !visited.Contains(parentMap[currentNode]))
                {
                    queue.Enqueue((parentMap[currentNode], distance + 1));
                    visited.Add(parentMap[currentNode]);
                }
            }

            return result;
        }
    } 
}
