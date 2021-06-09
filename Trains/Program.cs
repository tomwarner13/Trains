using System;
using Trains.Parsers;
using Trains.Util;

namespace Trains
{
	class MainClass
	{
    /*
     * TO RUN: you may run this program with no arguments to use the default route graph (below)
     * if you want to give it a different input, you may pass in a file path instead
     * make sure the file contains input in the same format (", " as separator)
     */
		public static void Main (string[] args)
    {
      const string defaultStringInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

      TrainPrompts prompts;
      
      if (args.Length > 0) //file path passed in; read graph input from it instead
      {
        var routes = FileInputParser.Parse(args[0]);
        prompts = new TrainPrompts(routes);
      }
      else
      {
        var routes = StringInputParser.Parse(defaultStringInput);
        prompts = new TrainPrompts(routes);
      }

      // 1. The distance of the route A-B-C.
      Console.WriteLine(prompts.FindRouteDistance("A", "B", "C"));
      
      // 2. The distance of the route A-D.
      Console.WriteLine(prompts.FindRouteDistance("A", "D"));
      
      // 3. The distance of the route A-D-C.
      Console.WriteLine(prompts.FindRouteDistance("A", "D", "C"));
      
      // 4. The distance of the route A-E-B-C-D.
      Console.WriteLine(prompts.FindRouteDistance("A", "E", "B", "C", "D"));
      
      // 5. The distance of the route A-E-D.
      Console.WriteLine(prompts.FindRouteDistance("A", "E", "D"));
      
      // 6. The number of trips starting at C and ending at C with a maximum of 3 stops.
      Console.WriteLine(prompts.CountRoutesWithMaxStops("C", "C", 3));
      
      // 7. The number of trips starting at A and ending at C with exactly 4 stops.
      Console.WriteLine(prompts.CountRoutesWithTotalStops("A", "C", 4));
      
      // 8. The length of the shortest route (in terms of distance to travel) from A to C.
      Console.WriteLine(prompts.GetShortestPathDistance("A", "C"));
      
      // 9. The length of the shortest route (in terms of distance to travel) from B to B.
      Console.WriteLine(prompts.GetShortestPathDistance("B", "B"));
      
      // 10. The number of different routes from C to C with a distance of less than 30.
      Console.WriteLine(prompts.CountRoutesWithMaxDistance("C", "C", 30));
    }
	}
}
