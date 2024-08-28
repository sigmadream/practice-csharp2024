using DS2024.Sort.Comparison;

namespace DS2024.Tests.Sort.Comparison;

public class InsertionSortTests
{
    [Test]
    public static void ArraySorted([Random(0, 1000, 100, Distinct = true)] int n)
    {
        // Arrange
        var sorter = new InsertionSort<int>();
        var (correctArray, testArray) = RandomArray.GetArrays(n);

        // Act
        sorter.Sort(testArray, Comparer<int>.Create((x, y) => x.CompareTo(y)));


        // Assert
        Assert.That(testArray, Is.EquivalentTo(correctArray));
    }
}