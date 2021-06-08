using System.Collections.Generic;
using System.Linq;

namespace TrainsData.Schema
{
	public class Node
  {
    public readonly string Name;
    
    private readonly List<Edge> _edges;

		public Node(string name)
		{
			Name = name;
      _edges = new List<Edge>();
		}

    public IEnumerable<Edge> Edges => _edges;

    public Edge GetEdgeByName(string name)
    {
      return _edges.FirstOrDefault(e => e.Destination.Name == name);
    }
    
    internal void AddEdge(Edge edge)
    {
      _edges.Add(edge); //sanity checks happen in DirectedGraph.cs
    }
	}
}