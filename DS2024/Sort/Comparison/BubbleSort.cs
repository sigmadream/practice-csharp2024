namespace DS2024.Sort.Comparison;

public class BubbleSort<T> : IComparisonSorter<T>
{
    public void Sort(T[] array, IComparer<T> comparer)
    {
        for (var i = 0; i < array.Length - 1; i++)
        {
            bool wasChanged = false;
            for (var j = 0; j < array.Length - i - 1; j++)
            {
                if (comparer.Compare(array[j], array[j + 1]) > 0)
                {
                    (array[j + 1], array[j]) = (array[j], array[j + 1]);
                    wasChanged = true;
                }
            }

            if (!wasChanged)
            {
                break;
            }
        }
    }
}
