using System.IO;
using TrainsData.Schema;

namespace Trains.Parsers
{
  public class FileInputParser
  {
    public static DirectedGraph Parse(string filePath)
    {
      var rawInput = File.ReadAllText(filePath);
      return StringInputParser.Parse(rawInput);
    }
  }
}