using System;
using System.Collections.Generic;
using System.Linq;
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
    //for example, calling with ("A", "D", 10, 25) will return all routes from A to D which are >= 10 distance and < 25
    //note that this includes cyclical routes--for example, A->B->C->A->B->C->D is returned if otherwise valid
    public List<Route> GetRoutesByDistanceRange(string start, string dest, int min, int max)
    {
      var firstStop = _routes.GetNode(start);
      return GetRoutesFromStopByDistanceRange(firstStop, dest, min, max, 0);
    }

    private static List<Route> GetRoutesFromStopByDistanceRange(Node currentStop, string dest, int min, int max,
      int currentDistance)
    {
      var results = new List<Route>();
      
      //if this is a valid route, return it--either way, keep checking because routes can cycle
      if (currentStop.Name == dest && currentDistance >= min && currentDistance < max)
      {
        results.Add(new Route(currentStop));
      }
      
      //recurse through all possible destinations that do not exceed length max
      foreach (var edge in currentStop.Edges.Where(e => currentDistance + e.Length < max))
      {
        var foundRoutes =
          GetRoutesFromStopByDistanceRange(edge.Destination, dest, min, max, currentDistance + edge.Length);
        
        //if any valid routes were found, return them up the call stack
        results.AddRange(foundRoutes.Select(route => route.PrependStop(currentStop)));
      }

      return results;
    }
    
    //same as above, but the constraints are total number of stops
    //so ("A", "D", 2, 4) will return all routes from A to D which are >= 2 stops and < 4
    public List<Route> GetRoutesByStopRange(string start, string dest, int min, int max)
    {
      var firstStop = _routes.GetNode(start);
      return GetRoutesFromStopByStopRange(firstStop, dest, min, max, 0);
    }

    //same as above, but returns based on total number of stops
    //note the first stop is not included in the total
    private static List<Route> GetRoutesFromStopByStopRange(Node currentStop, string dest, int min, int max,
      int currentStops)
    {
      var results = new List<Route>();
      
      //if this is a valid route, return it--either way, keep checking because routes can cycle
      if (currentStop.Name == dest && currentStops >= min && currentStops < max)
      {
        results.Add(new Route(currentStop));
      }
      
      //recurse through all possible destinations, if not at length max
      if (currentStops + 1 < max)
      {
        foreach (var edge in currentStop.Edges)
        {
          var foundRoutes =
            GetRoutesFromStopByStopRange(edge.Destination, dest, min, max, currentStops + 1);

          //if any valid routes were found, return them up the call stack
          results.AddRange(foundRoutes.Select(route => route.PrependStop(currentStop)));
        }
      }

      return results;
    }
    
    //uses dijkstra's algorithm to return the shortest path between two nodes if found, or null if there is no path
    //note the DO prompt includes routes which start/end at the same point, but you can't return a trip of 0 for that
    //therefore, some the special handling added here for start node and zero-length routes
    public Route FindShortestPath(string start, string dest)
    {
      var startNode = _routes.GetNode(start);
      if (startNode == null) throw new ArgumentException($"Start node {start} does not exist!");
      
      var currentRoute = new Route(startNode);

      //set all known distances to infinity (null route), except for the start node which is zero
      var nodeRoutes = _routes.GetNodeNames().ToDictionary(n => n, n => (Route)null);
      nodeRoutes[start] = currentRoute;

      var visitedNodes = new HashSet<string>();

      //recurse until all reachable nodes are visited; return if a shortest path is found
      while (currentRoute != null)
      {
        var currentDistance = currentRoute.TotalDistance;
        var currentNode = currentRoute.Stops.Last();
        if (currentNode.Name == dest && currentRoute.TotalDistance > 0) //we have arrived at our shortest (nonzero) path
        {
          return currentRoute;
        }
        
        //check edges to any unvisited nodes, update routes as necessary
        foreach (var edge in currentNode.Edges.Where(e => !visitedNodes.Contains(e.Destination.Name)))
        {
          var knownRouteToEdge = nodeRoutes[edge.Destination.Name];
          if (knownRouteToEdge == null 
              || knownRouteToEdge.TotalDistance == 0 //this allows us to revisit the starting node later, potentially 
              || knownRouteToEdge.TotalDistance > currentDistance + edge.Length)
          {
            nodeRoutes[edge.Destination.Name] = currentRoute.AddStop(edge);
          }
        }
        
        //mark node visited (unless it is the starting node, because cycles are allowed)
        if(currentRoute.Stops.Count > 1) visitedNodes.Add(currentNode.Name);

        //grab next node (shortest existing unvisited path) out of dictionary
        var nextRoute = (Route) null;
        var minDist = int.MaxValue;
        foreach (var node in nodeRoutes)
        {
          if (node.Value != null 
              && !visitedNodes.Contains(node.Key) 
              && node.Value.TotalDistance > 0 //do not visit the starting node from itself
              && node.Value.TotalDistance < minDist)
          {
            nextRoute = node.Value;
            minDist = node.Value.TotalDistance;
          }
        }

        currentRoute = nextRoute;
      }

      //no route was found
      return null;
    }
  }
}