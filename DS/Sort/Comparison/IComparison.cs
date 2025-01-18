namespace DS.Sort.Comparison;

public interface IComparisonSorter<T>
{
    void Sort(T[] array, IComparer<T> comparer);
}
