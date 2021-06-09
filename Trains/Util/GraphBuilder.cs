using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TrainsData.Schema;

namespace Trains.Util
{
  public class GraphBuilder
  {
    private readonly DirectedGraph _graph;
    private readonly HashSet<string> _existingNodes;
    private static readonly Regex ValidRouteRegex = new Regex("^[a-zA-Z]{2}\\d+$", RegexOptions.Compiled);

    public GraphBuilder()
    {
      _graph = new DirectedGraph();
      _existingNodes = new HashSet<string>();
    }

    //expects an input of the form "AB5" for example
    //the first 2 characters are node names (letters), and the trailing integer defines the distance
    public void ParseAndAddRoute(string input)
    {
      if (!ValidRouteRegex.IsMatch(input))
      {
        throw new ArgumentException($"Route input '{input}' was in an invalid format");
      }

      var start = input[0].ToString();
      var dest = input[1].ToString();
      var length = int.Parse(input.Substring(2));

      if (!_existingNodes.Contains(start))
      {
        _graph.AddNode(start);
        _existingNodes.Add(start);
      }
      
      if (!_existingNodes.Contains(dest))
      {
        _graph.AddNode(dest);
        _existingNodes.Add(dest);
      }
      
      _graph.AddEdge(start, dest, length);
    }

    public void ParseAndAddRoutes(IEnumerable<string> inputs)
    {
      foreach (var input in inputs)
      {
        ParseAndAddRoute(input);
      }
    }

    public DirectedGraph Graph() => _graph;
  }
}