namespace WebAppTechnology.Sort;

public static class QuickSort
{
    public static string Sort(string input)
    {
        char[] array = input.ToCharArray();
        QuickSortAlgorithm(array, 0, array.Length - 1);
        return new string(array);
    }

    private static void QuickSortAlgorithm(char[] array, int left, int right)
    {
        if (left < right)
        {
            char pivot = array[right];
            int i = left;
            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    i++;
                }
            }

            (array[i], array[right]) = (array[right], array[i]);

            QuickSortAlgorithm(array, left, i - 1);
            QuickSortAlgorithm(array, i + 1, right);
        }
    }
}