using System.Collections.Generic;
using TrainsData.Schema;

namespace Trains.Util
{
  public class RouteCalculations
  {
    private readonly DirectedGraph _routes;
    
    public RouteCalculations(DirectedGraph routes)
    {
      _routes = routes;
    }

    //calculates the distance of a trip following the given stops; returns -1 if no route exists
    public int CalculateRouteDistance(IEnumerable<string> stops)
    {
      var total = 0;
      var firstStop = true;
      Node currentNode = null;

      foreach(var stop in stops)
      {
        if (firstStop)
        {
          currentNode = _routes.GetNode(stop);
          if (currentNode == null) return -1;
          firstStop = false;
        }
        else
        {
          var edge = currentNode.GetEdgeByName(stop);
          if (edge == null) return -1;
          total += edge.Length;
          currentNode = edge.Destination;
        }
      }

      return total;
    }
    
    //given start point, end point, and distance range (min, max) return a set of all routes matching range
    
    //dijkstra stuff: shortest path from start to end, if any
  }
}