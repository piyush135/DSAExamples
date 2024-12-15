// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using System.Net.Http.Headers;
using System.Security;


int[] coins = { 2, 3,7 };
int amount = 12;

Console.WriteLine(DynamicProgramming.CountWaysToReachScore(coins, amount)); // Output: 4

//IList<string> timePoints = new List<string> { "00:04", "23:58", "12:03", "12:04" };
//int p2 = FindMinDifference(timePoints);
//Console.WriteLine("Minimum time difference: " + p2);


//string s = "coffee";
//int k = 2;

//int p1 = LongestSubstringWithKDistinct(s, k);
//Console.WriteLine("Longest substring with " + k + " distinct characters: " + p1);


//int[] nums = { 3, 1, 4, 1, 3, 5 };
//int k = 2;
//int p = FindPairsWithDifference(nums, k);
//Console.WriteLine("Number of Unique Pairs: " + p); // Output: 2


MedianFinder mf = new MedianFinder();

// Input sequence
mf.AddNumber(14);
Console.WriteLine("Median: " + mf.FindMedian()); // Outputs: 14.0

mf.AddNumber(6);
Console.WriteLine("Median: " + mf.FindMedian()); // Outputs: 10.0

mf.AddNumber(30);
Console.WriteLine("Median: " + mf.FindMedian()); // Outputs: 14.0

List<int[]> input = new List<int[]>()
        {
            new int[] { 1, 5, 9 },
            new int[] { 4, 8, 12 },
            new int[] { 7, 10, 13 }
        };

var result = Element.FindSmallestRange(input);
Console.WriteLine($"The smallest range is: [{result.Item1}, {result.Item2}]");


TreeNode root = new TreeNode(6,
           new TreeNode(3),
           new TreeNode(9,
               new TreeNode(8,
                   new TreeNode(7, null, null),
                   null),
               new TreeNode(11,
                   new TreeNode(10),
                   new TreeNode(12)
               )
           )
       );

Console.WriteLine(TreeNode.RemoveNode(root, 6));



// Example usage

#region Array and String
static int hackerlandRadioTransmitters(int[] x, int k)
{
    // Sort the house locations
    Array.Sort(x);

    int numTransmitters = 0;
    int n = x.Length;
    int i = 0;

    while (i < n)
    {
        numTransmitters++;  // Place a transmitter, so increment the counter

        // Step 1: Find the farthest house that can be the center of the transmitter
        int loc = x[i];

        // Move 'i' to the right while the house is within the range of the current transmitter's center
        while (i < n && x[i] <= loc + k)
        {
            i++;
        }

        // Step 2: Place the transmitter at x[i-1] because this is the farthest house we can cover
        int p = x[i - 1];

        // Step 3: Move 'i' to the right to cover all houses within the range of the transmitter placed at 'p'
        while (i < n && x[i] <= p + k)
        {
            i++;
        }
    }

    return numTransmitters;  // Return the minimum number of transmitters
}


static bool NewNextPermutation(int[] nums)
{
    //var newList = new List<int>();
    var n = nums.Length;

    var pivot = n - 2;

    while(pivot < nums.Length  && nums[pivot]>= nums[pivot + 1])
    {
        pivot--;
    }

    if (pivot < 0)
        return false;

    var successor = n -1;

    if(nums[successor] <=  nums[pivot])
    {
        successor--;
    }

    Swap(nums, pivot, successor);
    Reverse(nums, pivot + 1, n - 1);

    // get the pivot 
    // get the successor 
    // swap pivot with successor
    // reserve 
    return true;
}

static bool NextPermutation(int[] nums)
{
    int n = nums.Length;

    // Step 1: Find the pivot
    int pivot = n - 2;
    while (pivot >= 0 && nums[pivot] >= nums[pivot + 1])
    {
        pivot--;
    }

    // If no pivot is found, the array is the largest permutation
    if (pivot < 0)
    {
        Array.Reverse(nums); // Reset to the smallest permutation
        return false;
    }

    // Step 2: Find the successor
    int successor = n - 1;
    while (nums[successor] <= nums[pivot])
    {
        successor--;
    }

    // Step 3: Swap pivot and successor
    Swap(nums, pivot, successor);

    // Step 4: Reverse the suffix
    Reverse(nums, pivot + 1, n - 1);

    return true;
}

static void Swap(int[] nums, int i, int j)
{
    int temp = nums[i];
    nums[i] = nums[j];
    nums[j] = temp;
}

static void Reverse(int[] nums, int start, int end)
{
    while (start < end)
    {
        Swap(nums, start, end);
        start++;
        end--;
    }
}

#endregion

#region LinkList
static ListNode CreateLinkedList(int[] values)
{
    if (values.Length == 0)
        return null;

    ListNode head = new ListNode(values[0]);
    ListNode current = head;
    for (int i = 1; i < values.Length; i++)
    {
        current.Next = new ListNode(values[i]);
        current = current.Next;
    }

    return head;
}

static ListNode RotateRight(ListNode head, int k)
{
    if (head == null || head.Next == null || k == 0)
        return head;

    // Step 1: Calculate the length of the list
    int length = 1;
    ListNode tail = head;
    while (tail.Next != null)
    {
        tail = tail.Next;
        length++;
    }

    // Step 2: Find effective rotations
    k = k % length;
    if (k == 0)
        return head;

    // Step 3: Make the list circular
    tail.Next = head;

    // Step 4: Find the new tail (length - k steps from the head)
    int stepsToNewTail = length - k;
    ListNode newTail = head;
    for (int i = 1; i < stepsToNewTail; i++)
    {
        newTail = newTail.Next;
    }

    // Step 5: Break the circle and set the new head
    ListNode newHead = newTail.Next;
    newTail.Next = null;

    return newHead;
}

static ListNode Rotate(ListNode head, int k )
{
    var current = head;
    // get three variable 
    ListNode rotateHead = new ListNode(0);
    ListNode rotateTailHead = new ListNode(0);
    ListNode rotateTail = rotateTailHead;
    ListNode rotateNode = rotateHead;

    //ListNode TailNode = head;
    //var size = 1;
    //while ( TailNode.Next != null )
    //{
    //    TailNode = TailNode.Next;
    //    size++;
    //}

    //if (k % size == 0)
    //    return head;

    //TailNode.Next = head;

    //ListNode newNode = head;

    //for (int i = 1; i < (k%size); i++)
    //{
    //    newNode = newNode.Next;
    //}

    //ListNode newNodehead = newNode.Next;
    //newNode.Next = null;

    //return newNode;
  
    // if k is zero and size of list or multiple of list size rotation is not required 

    if (head == null || head.Next == null || k == 0)
        return head;

    while (k > 0)
    {
        rotateNode.Next = current;
        rotateNode = rotateNode.Next;
        current = current.Next;
        k --;
    }
    rotateNode.Next = null;
    while(current != null)
    {
        rotateTail.Next = current;
        rotateTail = rotateTail.Next;
        current = current.Next;
    }
    if(rotateTail != null && rotateTail.Next == null)
        rotateTail.Next = rotateHead.Next;

    return rotateTailHead.Next;
}

static ListNode Partition(ListNode head, int x)
{
    if (head == null)
        return null;

    // Dummy nodes for less and greater/equal lists
    ListNode lessHead = new ListNode(0);
    ListNode greaterHead = new ListNode(0);

    // Pointers to build the two lists
    ListNode less = lessHead;
    ListNode greater = greaterHead;

    // Traverse the original list
    while (head != null)
    {
        if (head.Value < x)
        {
            less.Next = head;
            less = less.Next;
        }
        else
        {
            greater.Next = head;
            greater = greater.Next;
        }
        head = head.Next;
    }

    // End the greater list
    greater.Next = null;

    // Merge the two lists
    less.Next = greaterHead.Next;

    // Return the head of the new list
    return lessHead.Next;
}

static ListNode NewPartition(ListNode head, int x)
{
    // add the less than head 
    // add the less than Node 
    // add the gretar than node 
    // add the greater than head 

    ListNode lessHead = new ListNode(0);
    ListNode greaterHead = new ListNode(0);
    ListNode less = lessHead;
    ListNode greater = greaterHead;

    while(head != null)
    {
        if(head.Value < x)
        {
            less.Next = head;
            less = less.Next;
        }
        else
        {
           greater.Next = head;
           greater = greater.Next;
        }
        head = head.Next;
    }

    less.Next = greaterHead.Next;

    return lessHead.Next;
}

static ListNode ReverseBetween(ListNode head, int start, int end)
{

    // get the pre node 
    // reverse the node b/w start and end 
    // give the output 
    var prevHead = new ListNode(0);
    var prevTail = prevHead;

    for(int i = 1; i < start; i++)
    {
        prevTail.Next = head;
        prevTail = prevTail.Next;
        head = head.Next;
    }

    ListNode current = prevTail.Next;
    ListNode subTailPrev = null;

    for(int i = start; i <= end; i++)
    {
        ListNode next = current.Next;
        current.Next = subTailPrev;
        subTailPrev = current;
        current = next;
    }
    prevTail.Next.Next = current;
    prevTail.Next = subTailPrev;
    //while(prevTail.Next != null)
    //{
    //    prevTail = prevTail.Next;
    //}
    //prevTail.Next = current;
    return prevHead.Next;
}

static ListNode ReverseBetweenNew(ListNode head, int start, int end)
{
    if (head == null || start == end)
        return head;

    // Dummy node to handle edge cases where start is 1
    ListNode dummy = new ListNode(0);
    dummy.Next = head;

    ListNode prev = dummy;

    // Move `prev` to the node before the `start` position
    for (int i = 1; i < start; i++)
    {
        prev = prev.Next;
    }

    // Start reversing the sublist
    ListNode current = prev.Next;
    ListNode next = null;
    ListNode sublistPrev = null;

    for (int i = 0; i <= end - start; i++)
    {
        next = current.Next;
        current.Next = sublistPrev;
        sublistPrev = current;
        current = next;
    }

    // Connect the reversed sublist back to the main list
    prev.Next.Next = current; // Tail of the reversed part connects to the rest
    prev.Next = sublistPrev; // Previous connects to the head of reversed part

    return dummy.Next;
}

static ListNode SwapPair(ListNode head)
{
    // get the even list 
    //get the odd list 
    // merge odd + even

    ListNode evenhead = new ListNode(0);
    ListNode oddhead = new ListNode(0);
    ListNode eventail = evenhead;
    ListNode oddTail = oddhead;

    int index = 0;

    while(head != null)
    {
        if(index % 2 == 0)
        {
            eventail.Next = head;
            eventail = eventail.Next;
        }
        else {
            oddTail.Next = head;
            oddTail = oddTail.Next;               
        }    
        index++;
        head = head.Next;
    }

    oddTail.Next = null;
    eventail.Next = null;

    ListNode dummyHead = new ListNode(0);
    ListNode dummy = dummyHead;
    evenhead = evenhead.Next;
    oddhead = oddhead.Next;
    while(evenhead != null || oddhead != null) {
        if(oddhead != null)
        {
            dummy.Next = oddhead;
            oddhead = oddhead.Next;
            dummy = dummy.Next;
            
        }
        if(evenhead != null)
        {
            dummy.Next = evenhead;
            evenhead = evenhead.Next;
            dummy = dummy.Next;          
        }
    }
    return dummyHead.Next;
}

#endregion

#region tree

//Problem 1
static bool IsTreeSymmetric(TreeNode node)
{
    if (node == null) 
        return true;
    return ValidateSymmetric(node.left, node.right);
}

static bool ValidateSymmetric(TreeNode left, TreeNode right)
{
    if (left == null && right == null)     
        return true;       
    
    else if (left.val != right.val)
        return false;
    
    return ValidateSymmetric(left.left, right.right) && ValidateSymmetric(left.right, right.left);

}

// Problem 3
static bool IsValidBSTUsingBFS(TreeNode root)
{
    // using BFS approch using queue, Add item of node at every level and deQueue to compare with min and max value bounded

    if (root== null)
        return false;

    var queue = new Queue<(TreeNode, long, long)>();
    queue.Enqueue((root, long.MinValue, long.MaxValue));

    while(queue.Count > 0)
    {
        var (node, min, max)  = queue.Dequeue();

        if (node == null) continue;
        if (node.val <= min || node.val >= max)
            return false;

        queue.Enqueue((node.left, min, node.val));
        queue.Enqueue((node.right, node.val, max));
    }
    return true;
}
// Problem 3 DFS approch 
static bool IsValidBSTUsingDFs(TreeNode root)
{
    // call validate recursive function 
    return Validate(root, long.MinValue, long.MaxValue);
}
static bool Validate(TreeNode node, long min, long max)
{
    if(node == null) return false;

    if(node.val <= min && node.val >= max)
        return false;

    return Validate(node.left, min, node.val) && Validate(node.right, node.val, max);
}


//Problem 4 

static TreeNode SortedArrayToBST(int[] array)
{
    var left = 0;
    var right = array.Length - 1;
    return ConvertArrayToBst(array, left, right);
}

static List<int?> PrintBSTUsingBFS(TreeNode root)
{
    List<int?> result = new List<int?>();
    if (root == null)
        return null;

    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    while (queue.Count > 0)
    {
        var node = queue.Dequeue();
        if(node != null)
        {
            result.Add(node.val);
            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
        }
    }   
    return result;
}

static TreeNode ConvertArrayToBst(int[] array, int left, int right)
{
    if (left > right)
        return null;
    // find the middle index 
    int mid = left + (right - left) / 2;

    TreeNode root = new TreeNode(array[mid]);

    root.left = ConvertArrayToBst(array, left, mid - 1);
    root.right = ConvertArrayToBst(array, mid + 1, right);

    return root;
}

// Problem 5 
//Find the height of binary tree and Diameter
#endregion

static int FindPairsWithDifference(int[] nums, int k)
{
    if (k < 0) return 0; // No valid pairs for negative k

    HashSet<int> seen = new HashSet<int>();
    HashSet<(int, int)> uniquePairs = new HashSet<(int, int)>();

    foreach (int num in nums)
    {
        // Check for pairs (num + k, num) and (num - k, num)
        if (seen.Contains(num - k))
            uniquePairs.Add((num - k, num));

        if (seen.Contains(num + k))
            uniquePairs.Add((num, num + k));

        // Add the current number to the seen set
        seen.Add(num);
    }

    // Return the count of unique pairs
    return uniquePairs.Count;
}

static int LongestSubstringWithKDistinct(string s, int k)
{
    if (k == 0) return 0; // If k is 0, there's no valid substring.

    Dictionary<char, int> charCount = new Dictionary<char, int>();
    int start = 0; // Left pointer of the window
    int maxLength = 0;

    for (int end = 0; end < s.Length; end++)
    {
        // Add the current character to the window
        if (charCount.ContainsKey(s[end]))
            charCount[s[end]]++;
        else
            charCount[s[end]] = 1;

        // If we have more than k distinct characters, shrink the window
        while (charCount.Count > k)
        {
            charCount[s[start]]--;
            if (charCount[s[start]] == 0)
                charCount.Remove(s[start]);
            start++; // Move the left pointer to the right
        }

        // Update maxLength with the length of the current valid window
        maxLength = Math.Max(maxLength, end - start + 1);
    }

    return maxLength;
}

static int FindMinDifference(IList<string> timePoints)
{
    // Step 1: Convert times to minutes.
    List<int> timesInMinutes = new List<int>();
    foreach (var time in timePoints)
    {
        var timeParts = time.Split(':');
        int hours = int.Parse(timeParts[0]);
        int minutes = int.Parse(timeParts[1]);
        int totalMinutes = hours * 60 + minutes;
        timesInMinutes.Add(totalMinutes);
    }

    // Step 2: Sort the times.
    timesInMinutes.Sort();

    // Step 3: Calculate the minimum difference.
    int minDiff = int.MaxValue;
    for (int i = 1; i < timesInMinutes.Count; i++)
    {
        minDiff = Math.Min(minDiff, timesInMinutes[i] - timesInMinutes[i - 1]);
    }

    // Step 4: Account for the wrap-around difference.
    minDiff = Math.Min(minDiff, (1440 - timesInMinutes[timesInMinutes.Count - 1]) + timesInMinutes[0]);

    return minDiff;
}