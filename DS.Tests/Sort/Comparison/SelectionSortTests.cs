using DS.Sort.Comparison;

namespace DS.Tests.Sort.Comparison;

public class SelectionSortTests
{

    [Test]
    public static void ArraySorted([Random(0, 1000, 100, Distinct = true)] int n)
    {
        // Arrange
        var sorter = new SelectionSort<int>();
        var (correctArray, testArray) = RandomArray.GetArrays(n);

        // Act
        sorter.Sort(testArray, Comparer<int>.Create((x, y) => x.CompareTo(y)));


        // Assert
        Assert.That(testArray, Is.EquivalentTo(correctArray));
    }
}