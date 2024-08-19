namespace DS2024.Sort.Comparison
{
    public interface IComparison
    {
        void sort(int[] array, IComparer<int> comparer);
    }
}
