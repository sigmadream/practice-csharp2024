namespace DS.Sort.Comparison;

public class InsertionSort<T> : IComparisonSorter<T>
{
    public void Sort(T[] array, IComparer<T> comparer)
    {
        for (var i = 1; i < array.Length; i++)
        {
            var key = array[i];
            var j = i - 1;

            while (j >= 0 && comparer.Compare(array[j], key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
    }
}
