namespace TrainsData.Schema
{
  public class Tree<T>
  {
    public readonly TreeNode<T> RootNode;

    public Tree(T root)
    {
      RootNode = new TreeNode<T>(root);
    }
  }
}