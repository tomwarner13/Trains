using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public class DirectedGraph
	{
    private readonly Dictionary<string, Node> nodesByName;
    
		public DirectedGraph()
		{
      nodesByName = new Dictionary<string, Node>();
		}
    
    public void AddNode(string name)
    {
      if(nodesByName.ContainsKey(name))
      {
        throw new ArgumentException($"A node named {name} already exists");
      }
      
        nodesByName.Add(name, new Node(name));
    }
    
    public void AddEdge(string start, string destination, int length)
    {
      Node startNode;
      Node destNode;
      
      if(!nodesByName.TryGetValue(start, out startNode))
      {
        throw new ArgumentException($"Could not add edge: starting node {start} does not exist");
      }
      
      if(!nodesByName.TryGetValue(destination, out destNode))
      {
        throw new ArgumentException($"Could not add edge: destination node {destination} does not exist");
      }
      
      if(length <= 0)
      {
        throw new ArgumentException($"Could not add edge with length of {length}: must be a positive integer");
      }
      
      //TODO unit test all this
      if (startNode.Edges.Any(n => n.Destination.Name == destination))
      {
        throw new ArgumentException($"Could not add edge {start}->{destination}: this edge already exists");
      }
      
      startNode.Edges.Add(new Edge(length, destNode)); 
    }
	}
}