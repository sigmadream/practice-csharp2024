
using DS2024.Sort.Comparison;

namespace DS2024.Tests
{
    public static class BubbleSortTest
    {
        [Fact]
        public static void DoTest()
        {
            int[] inputArray = new int[] { 1, 3, 5, 7, 9, 2, 4, 6, 8 };
            int[] outputArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var bubbleSort = new BubbleSort<int>();
            bubbleSort.Sort(inputArray, Comparer<int>.Create((x, y) => x.CompareTo(y)));
            Assert.Equivalent(inputArray, outputArray);
        }
    }
}
