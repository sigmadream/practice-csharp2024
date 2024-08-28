
namespace DS2024.Sort.Comparison;

public class SelectionSort<T> : IComparisonSorter<T>
{
    public void Sort(T[] array, IComparer<T> comparer)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var minIndex = i;
            for (var j = i + 1; j < array.Length; j++)
            {
                if (comparer.Compare(array[minIndex], array[j]) > 0)
                {
                    minIndex = j;
                }
            }
  
            (array[minIndex], array[i]) = (array[i], array[minIndex]);
        }
    }
}