using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraAlgoPath
{
    class Program
    {
        /// <summary>
        /// Dijkstra graph that uses airports like a vertices names
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex{ Name = "TokyoAirport", NumberOfStops = 0, SourceName = "TokyoAirport", EdgeWeight = 0, Distance = 0 },
                new Vertex{ Name = "LondonAirport1", NumberOfStops = 0, SourceName = "TokyoAirport", EdgeWeight = 0, Distance = int.MaxValue },
                new Vertex{ Name = "LondonAirport2", NumberOfStops = 0, SourceName = "TokyoAirport", EdgeWeight = 0, Distance = int.MaxValue },
                new Vertex{ Name = "LondonAirport3", NumberOfStops = 0, SourceName = "LondonAirport1", EdgeWeight = 0, Distance = int.MaxValue },
                new Vertex{ Name = "LondonAirport4", NumberOfStops = 0, SourceName = "LondonAirport1", EdgeWeight = 0, Distance = int.MaxValue },
            };

            var graph = new DijkstraGraph(vertices);
            graph.CalculateDistanceBetweenVerteces();

            // Write the results in the console
            Console.WriteLine("Vertex\t\t\t Distance from source \t\t Number of stops");
            foreach (var vertex in graph.Vertices)
            {
                Console.WriteLine($"{vertex.Name}\t\t {vertex.Distance}\t\t\t\t {vertex.NumberOfStops}");
            }

            Console.ReadLine();
        }
    }
}
