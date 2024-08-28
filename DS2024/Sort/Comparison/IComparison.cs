namespace DS2024.Sort.Comparison;

public interface IComparisonSorter<T>
{
    void Sort(T[] array, IComparer<T> comparer);
}
