using System.Collections.Generic;

namespace TrainsData.Schema
{
  public class TreeNode<T>
  {
    public readonly T Data;
    private readonly List<TreeNode<T>> _children;
    
    public TreeNode(T data)
    {
      Data = data;
      _children = new List<TreeNode<T>>();
    }

    public void AddChild(T child)
    {
      _children.Add(new TreeNode<T>(child));
    }

    public IReadOnlyList<TreeNode<T>> Children => _children;
  }
}