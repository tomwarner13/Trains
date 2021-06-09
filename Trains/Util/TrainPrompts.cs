using TrainsData.Schema;

namespace Trains.Util
{
  //methods to handle the specific question types asked by the code test
  public class TrainPrompts
  {
    private readonly RouteCalculations _calc;
    private const string NoSuchRoute = "NO SUCH ROUTE";
    
    public TrainPrompts(DirectedGraph routes)
    {
      _calc = new RouteCalculations(routes);
    }

    public string FindRouteDistance(params string[] stops)
    {
      var result = _calc.CalculateRouteDistance(stops);
      return result < 0 ? NoSuchRoute : result.ToString();
    }

    public string CountRoutesWithMaxStops(string start, string dest, int max)
    {
      return _calc.GetRoutesByStopRange(start, dest, 1, max + 1).Count.ToString();
    }

    public string CountRoutesWithTotalStops(string start, string dest, int total)
    {
      return _calc.GetRoutesByStopRange(start, dest, total, total + 1).Count.ToString();
    }
    
    public string CountRoutesWithMaxDistance(string start, string dest, int max)
    {
      return _calc.GetRoutesByDistanceRange(start, dest, 1, max).Count.ToString();
    }

    public string GetShortestPathDistance(string start, string dest)
    {
      var result = _calc.FindShortestPath(start, dest);
      return result == null ? NoSuchRoute : result.TotalDistance.ToString();
    }
  }
}