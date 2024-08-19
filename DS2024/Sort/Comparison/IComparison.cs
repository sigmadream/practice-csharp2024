namespace DS2024.Sort.Comparison;

public interface IComparisonSorter
{
    int[] Sort(int[] array, IComparer<int> comparer);
}
