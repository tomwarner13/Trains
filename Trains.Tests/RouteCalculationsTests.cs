using System.Linq;
using Trains.Util;
using Xunit;

namespace Trains.Tests
{
  public class RouteCalculationsTests
  {
    [Fact]
    public void TestRouteDistanceWithDealerOnData()
    {
      var inputs = new[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);
      var graph = builder.Graph();

      var calc = new RouteCalculations(graph);
      Assert.Equal(9, calc.CalculateRouteDistance(new[] {"A", "B", "C"}));
      Assert.Equal(5, calc.CalculateRouteDistance(new[] {"A", "D"}));
      Assert.Equal(13, calc.CalculateRouteDistance(new[] {"A", "D", "C"}));
      Assert.Equal(22, calc.CalculateRouteDistance(new[] {"A", "E", "B", "C", "D"}));
      Assert.Equal(-1, calc.CalculateRouteDistance(new[] {"A", "E", "D"}));
    }
    
    [Fact]
    public void TestRouteDistanceRangeWithDealerOnData()
    {
      var inputs = new[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);
      var graph = builder.Graph();

      var calc = new RouteCalculations(graph);
      //note that the DO requirements ask for a "distance of less than 30" but 0-distance routes are implicitly excluded
      //so a route from C to C which goes nowhere does not count--thus the min length of 1
      Assert.Equal(7, calc.GetRoutesByDistanceRange("C", "C", 1, 30).Count);
      
      //make sure it works where no routes can be found
      Assert.Equal(0, calc.GetRoutesByDistanceRange("A", "E", 1, 2).Count);
    }
    
    [Fact]
    public void TestRouteStopRangeWithDealerOnData()
    {
      var inputs = new[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);
      var graph = builder.Graph();

      var calc = new RouteCalculations(graph);
      //note that distance calculations in the prompt are exclusive (dist < max) but stops are inclusive (stops <= max)
      //i decided the calculation class should work in a consistent way (exclusive), and adjustments can happen before it
      Assert.Equal(2, calc.GetRoutesByStopRange("C", "C", 1, 4).Count);
      Assert.Equal(3, calc.GetRoutesByStopRange("A", "C", 4, 5).Count);

      //make sure it works where no routes can be found
      Assert.Equal(0, calc.GetRoutesByDistanceRange("E", "C", 1, 2).Count);
    }
    
    [Fact]
    public void TestShortestPathWithDealerOnData()
    {
      var inputs = new[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);
      var graph = builder.Graph();

      var calc = new RouteCalculations(graph);

      var routeAtoC = calc.FindShortestPath("A", "C");
      Assert.Equal(9, routeAtoC.TotalDistance);
      Assert.Equal(3, routeAtoC.Stops.Count);
      
      var routeBtoB = calc.FindShortestPath("B", "B");
      Assert.Equal(9, routeBtoB.TotalDistance);
      Assert.Equal(4, routeBtoB.Stops.Count);
    }

    [Fact]
    public void TestShortestPathWithOtherData()
    {
      var inputs = new[] { "AB5", "XA200", "BC3", "AC1" };
      var builder = new GraphBuilder();

      builder.ParseAndAddRoutes(inputs);
      var graph = builder.Graph();

      var calc = new RouteCalculations(graph);
      
      //YOU CANT GET TO DESTINATION X MAN
      var routeAtoX = calc.FindShortestPath("A", "X");
      Assert.Null(routeAtoX);
      
      var routeXtoC = calc.FindShortestPath("X", "C");
      Assert.Equal(201, routeXtoC.TotalDistance);
      Assert.Equal(3, routeXtoC.Stops.Count);
    }
  }
}