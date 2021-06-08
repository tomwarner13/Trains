using System;
using System.Collections.Generic;

namespace Trains
{
	public class Node
	{
		public readonly string Name;
    public readonly List<Edge> Edges;

		public Node(string name)
		{
			Name = name;
      Edges = new List<Edge>();
		}
	}
}