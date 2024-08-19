
using DS2024.Sort.Comparison;

namespace DS2024.Tests
{
    public static class BubbleSortTest
    {
        [Fact]
        public static void DoTest()
        {
            int[] inputArray = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            BubbleSort bubbleSort = new();
            int[] sortedArray = bubbleSort.sort(inputArray);            
            Assert.Equivalent(sortedArray, inputArray);
        }
    }
}
