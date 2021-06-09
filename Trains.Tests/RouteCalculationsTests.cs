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
  }
}