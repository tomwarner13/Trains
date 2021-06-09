using System;
using System.Linq;
using TrainsData.Schema;
using Xunit;

namespace Trains.Tests
{
  public class GraphTests
  {
    [Fact]
    public void TestGraphCreation()
    {
      var graph = new DirectedGraph();
      graph.AddNode("A");
      graph.AddNode("B");
      
      Assert.Equal(2, graph.GetNodeNames().Count());
      
      graph.AddEdge("A", "B", 3);

      var nodeA = graph.GetNode("A");
      Assert.Equal(1, nodeA.Edges.Count());

      var edge = nodeA.Edges.First();
      Assert.Equal(3, edge.Length);
      Assert.Equal("B", edge.Destination.Name);

      Assert.Throws<ArgumentException>(() => graph.AddEdge("Q", "B", 3));
      Assert.Throws<ArgumentException>(() => graph.AddEdge("A", "Q", 3));
      Assert.Throws<ArgumentException>(() => graph.AddEdge("B", "A", -1));
      Assert.Throws<ArgumentException>(() => graph.AddEdge("A", "B", 3));
      Assert.Throws<ArgumentException>(() => graph.AddEdge("B", "B", 3));
    }
  }
}