namespace DS2024.Sort.Comparison
{
    public class BubbleSort
    {
        public int[] sort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                bool wasChanged = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if ((array[j] - array[j + 1]) > 0)
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        wasChanged = true;
                    }
                }

                if (!wasChanged)
                {
                    break;
                }
            }
            return array;
        }
    }
}
