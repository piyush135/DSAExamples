using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int value, TreeNode Left = null, TreeNode Right = null)
        {
            val = value;
            left = Left;
            right = Right;
        }

        public static int maxDiameter = 0;
        private static List<int> longestPath = new List<int>();
        private static List<int> InOrderList = new List<int>();
        public static List<int> DiameterOfBinaryTree(TreeNode root)
        {
            return CalculateHeightAndPath(root);
            //return maxDiameter;
        }

        public static int CalculateHeight(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            // Recursively calculate the height of left and right subtrees
            int leftHeight = CalculateHeight(node.left);
            int rightHeight = CalculateHeight(node.right);

            // Update the maximum diameter
            maxDiameter = Math.Max(maxDiameter, leftHeight + rightHeight);

            // Return the height of the current subtree
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        private static List<int> CalculateHeightAndPath(TreeNode node)
        {
            if (node == null)
            {
                return new List<int>();
            }

            // Recursively calculate the height and path of the left and right subtrees
            var leftPath = CalculateHeightAndPath(node.left);
            var rightPath = CalculateHeightAndPath(node.right);

            // Calculate the diameter at this node
            int currentDiameter = leftPath.Count + rightPath.Count;

            // Update the maximum diameter and the longest path if a new diameter is found
            if (currentDiameter > maxDiameter)
            {
                maxDiameter = currentDiameter;

                // Combine leftPath, current node, and rightPath to form the longest path
                longestPath = new List<int>(leftPath);
                longestPath.Add(node.val);
                longestPath.AddRange(rightPath);
            }

            // Return the height path of the current subtree
            var resultPath = new List<int>(leftPath.Count > rightPath.Count ? leftPath : rightPath);
            resultPath.Add(node.val);
            return resultPath;
        }

        public static List<int> GetInOrderTraversal(TreeNode Node)
        {
            CalculateInOrder(Node, InOrderList);
            return InOrderList;
        }

        private static void CalculateInOrder(TreeNode node, List<int> inOrderList) 
        {
            if (node == null)
                return ;

            CalculateInOrder(node.left, inOrderList);
            inOrderList.Add(node.val);
            CalculateInOrder(node.right, inOrderList);
        }

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            return BuildTreeHelper(preorder, inorder, 0, 0, inorder.Length - 1);
        }

        private TreeNode BuildTreeHelper(int[] preorder, int[] inorder, int preStart, int inStart, int inEnd)
        {
            if (preStart >= preorder.Length || inStart > inEnd)
            {
                return null;
            }

            // The first element in the preorder is the root
            int rootVal = preorder[preStart];
            TreeNode root = new TreeNode(rootVal);

            // Find the index of the root in the inorder traversal
            int inIndex = Array.IndexOf(inorder, rootVal);

            // Recursively build the left and right subtrees
            root.left = BuildTreeHelper(preorder, inorder, preStart + 1, inStart, inIndex - 1);
            root.right = BuildTreeHelper(preorder, inorder, preStart + 1 + (inIndex - inStart), inIndex + 1, inEnd);

            return root;
        }

        public static TreeNode RemoveNode(TreeNode node, int value ) 
        {
            if (node == null)
                return null;

            if(value < node.val)
                node.left = RemoveNode(node.left, value);
            else if (value > node.val)
                node.right = RemoveNode(node.right, value);
            else
            {
                if (node.left == null && node.right == null)
                    return null;
                if (node.left != null && node.right == null)
                    return node.left;
                if (node.right != null && node.left == null)
                    return node.right;


                // if node both left and right exist, then we need to find the replace of node 
                var replacementNode = FindReplacement(node);
                node.val = replacementNode.val;

                // once root node replaced, we need to remove node from right subtree
                RemoveNode(node.right, replacementNode.val);
            }

            return node;
        }

        private static TreeNode FindReplacement(TreeNode node)
        {
            if (node == null || node.right == null)
                return null;
            node = node.right;
            while(node.left != null)
            {
                node = node.left;
            }
            return node;
        }             
        

    }

    public class Element
    {
        public int Value; // The value of the element.
        public int ListIndex; // The index of the list it came from.
        public int ElementIndex; // The index of the element in the list.

        public Element(int value, int listIndex, int elementIndex)
        {
            Value = value;
            ListIndex = listIndex;
            ElementIndex = elementIndex;
        }

        public static Tuple<int, int> FindSmallestRange(List<int[]> lists)
        {
            // Priority queue (min-heap) to store elements from each list
            var minHeap = new PriorityQueue<Element, int>();

            // Variables to track the current range
            int currentMax = int.MinValue;
            int rangeStart = -1, rangeEnd = -1;

            // Initialize the min-heap with the first element from each list
            for (int i = 0; i < lists.Count; i++)
            {
                minHeap.Enqueue(new Element(lists[i][0], i, 0), lists[i][0]);
                currentMax = Math.Max(currentMax, lists[i][0]);
            }

            // Process the heap and find the smallest range
            while (true)
            {
                // Extract the minimum element from the heap
                var minElement = minHeap.Dequeue();

                // Calculate the current range
                int currentRange = currentMax - minElement.Value;
                if (rangeStart == -1 || currentRange < rangeEnd - rangeStart)
                {
                    rangeStart = minElement.Value;
                    rangeEnd = currentMax;
                }

                // If we've exhausted one list, we can't form a range anymore
                if (minElement.ElementIndex + 1 >= lists[minElement.ListIndex].Length)
                {
                    break;
                }

                // Insert the next element from the same list into the heap
                int nextValue = lists[minElement.ListIndex][minElement.ElementIndex + 1];
                minHeap.Enqueue(new Element(nextValue, minElement.ListIndex, minElement.ElementIndex + 1), nextValue);
                currentMax = Math.Max(currentMax, nextValue);
            }

            return Tuple.Create(rangeStart, rangeEnd);
        }
    }

    class MedianFinder
    {
        // Max-Heap for the smaller half
        private PriorityQueue<int, int> low = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));

        // Min-Heap for the larger half
        private PriorityQueue<int, int> high = new PriorityQueue<int, int>();

        public void AddNumber(int num)
        {
            // Add the number to the appropriate heap
            if (low.Count == 0 || num <= low.Peek())
            {
                low.Enqueue(num, num * -1); // Max-Heap stores negative priorities
            }
            else
            {
                high.Enqueue(num, num); // Min-Heap stores positive priorities
            }

            // Balance the heaps
            if (low.Count > high.Count + 1)
            {
                high.Enqueue(low.Dequeue(), low.Peek());
            }
            else if (high.Count > low.Count)
            {
                low.Enqueue(high.Dequeue(), high.Peek() * -1);
            }
        }

        public double FindMedian()
        {
            // If the number of elements is odd, the larger heap's root is the median
            if (low.Count > high.Count)
            {
                return low.Peek();
            }
            else if (low.Count == high.Count)
            {
                return (low.Peek() + high.Peek()) / 2.0;
            }
            else
            {
                throw new InvalidOperationException("Heap sizes are inconsistent.");
            }
        }
    }
}
