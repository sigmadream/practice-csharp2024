using DS2024.Sort.Comparison;

namespace DS2024.NUnit.Tests.Sort.Comparison;

public class BubbleSortTests
{
    public static (int[] correctArray, int[] testArray) GetArrays(int n)
    {
        var testArr = new int[n];
        var correctArray = new int[n];

        for (var i = 0; i < n; i++)
        {
            var t = TestContext.CurrentContext.Random.Next(1_000_000);
            testArr[i] = t;
            correctArray[i] = t;
        }

        return (correctArray, testArr);
    }

    [Test]
    public static void ArraySorted([Random(0, 1000, 100, Distinct = true)] int n)
    {
        // Arrange
        var sorter = new BubbleSort<int>();
        var (correctArray, testArray) = GetArrays(n);

        // Act
        sorter.Sort(testArray, Comparer<int>.Create((x, y) => x.CompareTo(y)));


        // Assert
        Assert.That(testArray, Is.EquivalentTo(correctArray));
    }
}