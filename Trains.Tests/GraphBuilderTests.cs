using System;
using System.Linq;
using Trains.Util;
using Xunit;

namespace Trains.Tests
{
  public class GraphBuilderTests
  {
    [Fact]
    public void TestGraphBuilderWithDealerOnInputs()
    {
      var inputs = new[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);

      var graph = builder.Graph();
      Assert.Equal(5, graph.GetNodeNames().Count());
      
      var nodeA = graph.GetNode("A");
      var edgeAB = nodeA.GetEdgeByName("B");
      Assert.Equal("B", edgeAB.Destination.Name);
      Assert.Equal(5, edgeAB.Length);
      
      var edgeAD = nodeA.GetEdgeByName("D");
      Assert.Equal("D", edgeAD.Destination.Name);
      Assert.Equal(5, edgeAD.Length);

      var nodeD = edgeAD.Destination;
      var edgeDE = nodeD.GetEdgeByName("E");
      Assert.Equal("E", edgeDE.Destination.Name);
      Assert.Equal(6, edgeDE.Length);
      
      Assert.Null(nodeD.GetEdgeByName("A"));
    }

    [Fact]
    public void TestGraphBuilderWithBonusInputs()
    {
      var inputs = new string[] { "AB27", "BC100" };
      var builder = new GraphBuilder();
      
      builder.ParseAndAddRoutes(inputs);

      Assert.Throws<ArgumentException>(() => builder.ParseAndAddRoute("A5X"));
      Assert.Throws<ArgumentException>(() => builder.ParseAndAddRoute("nah fam"));
      Assert.Throws<ArgumentException>(() => builder.ParseAndAddRoute("555555555"));
      
      var graph = builder.Graph();
      Assert.Equal(3, graph.GetNodeNames().Count());
      
      var nodeA = graph.GetNode("A");
      var edgeAB = nodeA.GetEdgeByName("B");
      Assert.Equal("B", edgeAB.Destination.Name);
      Assert.Equal(27, edgeAB.Length);
      
      var nodeB = graph.GetNode("B");
      var edgeBC = nodeB.GetEdgeByName("C");
      Assert.Equal("C", edgeBC.Destination.Name);
      Assert.Equal(100, edgeBC.Length);
    }
  }
}