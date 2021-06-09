using System;
using Trains.Util;
using TrainsData.Schema;

namespace Trains.Parsers
{
  public class StringInputParser
  {
    public static DirectedGraph Parse(string input, string separator = ", ")
    {
      var inputs = input.Split(new[] { separator }, StringSplitOptions.None);
      var builder = new GraphBuilder();
      builder.ParseAndAddRoutes(inputs);
      return builder.Graph();
    }
  }
}