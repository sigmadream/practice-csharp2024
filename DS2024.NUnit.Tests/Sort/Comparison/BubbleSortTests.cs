using DS2024.Sort.Comparison;

namespace DS2024.NUnit.Tests.Sort.Comparison;

public class BubbleSortTests
{
    [Test]
    public static void ArraySorted([Random(0, 1000, 100, Distinct = true)] int n)
    {
        // Arrange
        var sorter = new BubbleSort<int>();
        var (correctArray, testArray) = RandomArray.GetArrays(n);

        // Act
        sorter.Sort(testArray, Comparer<int>.Create((x, y) => x.CompareTo(y)));


        // Assert
        Assert.That(testArray, Is.EquivalentTo(correctArray));
    }
}