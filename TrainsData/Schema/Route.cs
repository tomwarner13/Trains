using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;

namespace TrainsData.Schema
{
  public class Route
  {
    public readonly Node Start;
    public readonly int TotalDistance;
    public readonly List<Node> Stops;

    public Route(Node start)
    {
      Start = start;
      TotalDistance = 0;
      Stops = new List<Node>(new[] {start});
    }

    private Route(Node start, int totalDistance, IEnumerable<Node> stops)
    {
      Start = start;
      TotalDistance = totalDistance;
      Stops = stops.ToList();
    }

    public Route AddStop(Edge nextStop)
    {
      return new Route(
        Start,
        TotalDistance + nextStop.Length,
        Stops.Concat(new[] {nextStop.Destination})
      );
    }

    public Route PrependStop(Node newStart)
    {
      var newEdge = newStart.GetEdgeByName(Start.Name);
      if (newEdge == null)
      {
        throw new ArgumentException($"Invalid route! The node {newStart.Name} does not connect to {Start.Name}");
      }

      return new Route(
        newStart,
        newEdge.Length + TotalDistance,
        new List<Node>{newStart}.Concat(Stops).ToList());
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      var firstStop = true;
      foreach (var stop in Stops)
      {
        if (!firstStop) sb.Append(":");
        sb.Append(stop.Name);
        firstStop = false;
      }

      return sb.ToString();
    }
  }
}