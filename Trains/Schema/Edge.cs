using System;

namespace Trains
{
	public class Edge
	{
    public readonly int Length;
    public readonly Node Destination;
    
    public Edge(int length, Node destination)
    {
      Length = length;
      Destination = destination;
    }
	}
}