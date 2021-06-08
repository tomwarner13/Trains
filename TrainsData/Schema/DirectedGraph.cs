using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainsData.Schema
{
	public class DirectedGraph
	{
    private readonly Dictionary<string, Node> _nodesByName;
    
		public DirectedGraph()
		{
      _nodesByName = new Dictionary<string, Node>();
		}
    
    public void AddNode(string name)
    {
      if(_nodesByName.ContainsKey(name))
      {
        throw new ArgumentException($"A node named {name} already exists");
      }
      _nodesByName.Add(name, new Node(name));
    }
    
    public void AddEdge(string start, string destination, int length)
    {
      if(!_nodesByName.TryGetValue(start, out var startNode))
      {
        throw new ArgumentException($"Could not add edge: starting node {start} does not exist");
      }
      
      if(!_nodesByName.TryGetValue(destination, out var destNode))
      {
        throw new ArgumentException($"Could not add edge: destination node {destination} does not exist");
      }
      
      if(length <= 0)
      {
        throw new ArgumentException($"Could not add edge with length of {length}: must be a positive integer");
      }
      
      if (startNode.Edges.Any(n => n.Destination.Name == destination))
      {
        throw new ArgumentException($"Could not add edge {start}->{destination}: this edge already exists");
      }

      if (start == destination)
      {
        throw new ArgumentException($"Could not add edge {start}->{destination}: must not be identical nodes");
      }
      
      startNode.AddEdge(new Edge(length, destNode)); 
    }
    
    public Node GetNode(string name)
    {
      if(!_nodesByName.ContainsKey(name))
      {
        throw new ArgumentException($"Node {name} does not exist");
      }

      return _nodesByName[name];
    }

    public IEnumerable<string> GetNodeNames() => _nodesByName.Keys;
  }
}