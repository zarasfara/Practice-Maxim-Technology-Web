namespace WebAppTechnology.Sort;

public static class TreeSort
{
    private class TreeNode
    {
        public readonly char Value;
        public TreeNode? Left;
        public TreeNode? Right;

        public TreeNode(char value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public static string Sort(string input)
    {
        char[] array = input.ToCharArray();
        if (array.Length == 0) return input;

        TreeNode root = new TreeNode(array[0]);

        for (int i = 1; i < array.Length; i++)
        {
            Insert(root, array[i]);
        }

        int index = 0;
        InOrderTraversal(root, array, ref index);
        return new string(array);
    }

    private static void Insert(TreeNode node, char value)
    {
        if (value < node.Value)
        {
            if (node.Left == null)
            {
                node.Left = new TreeNode(value);
            }
            else
            {
                Insert(node.Left, value);
            }
        }
        else
        {
            if (node.Right == null)
            {
                node.Right = new TreeNode(value);
            }
            else
            {
                Insert(node.Right, value);
            }
        }
    }

    private static void InOrderTraversal(TreeNode node, char[] array, ref int index)
    {
        if (node == null) return;

        InOrderTraversal(node.Left!, array, ref index);
        array[index++] = node.Value;
        InOrderTraversal(node.Right!, array, ref index);
    }
}