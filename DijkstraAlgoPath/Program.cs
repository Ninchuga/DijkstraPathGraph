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

            List<Vertex> visitedVertices = new List<Vertex>();
            List<Vertex> unvisitedVertices = new List<Vertex>(vertices);

            while (unvisitedVertices.Any())
            {
                var smallestDistanceVertex = unvisitedVertices.OrderBy(x => x.Distance).FirstOrDefault();
                unvisitedVertices.Remove(smallestDistanceVertex);

                int skipVerticesNumber = visitedVertices.Count + 1;
                int addedEdges = 1;
                foreach (var vertex in vertices.Skip(skipVerticesNumber)) // skip visited vertices and add edges to the current selected vertex
                {
                    int edgeWeight = addedEdges + 1; // increment edge distance weight by 1
                    graph.AddEdgeWithWeight(smallestDistanceVertex.Name, vertex.Name, edgeWeight, distance: int.MaxValue);
                    if (addedEdges == 2) // each edge has two additional edges/paths max
                        break;
                    addedEdges += 1;
                }

                
                var vertexNeighbours = graph.GetNeighboursFor(smallestDistanceVertex.Name);
                if (vertexNeighbours.Any())
                {
                    foreach (var neighbours in vertexNeighbours)
                    {
                        var newDistanceFromSource = smallestDistanceVertex.Distance + neighbours.EdgeWeight;
                        var vertex = vertices.FirstOrDefault(vertex => vertex.Name.Equals(neighbours.Name));
                        if (vertex.Distance > newDistanceFromSource)
                        {
                            vertex.Distance = newDistanceFromSource;

                            if (smallestDistanceVertex.Name != vertices.First().Name)
                                vertex.NumberOfStops += 1;
                        }
                    }
                }

                visitedVertices.Add(smallestDistanceVertex);
            }

            // Write the results in the console
            Console.WriteLine("Vertex\t\t\t Distance from source \t\t Number of stops");
            foreach (var vertex in vertices)
            {
                Console.WriteLine($"{vertex.Name}\t\t {vertex.Distance}\t\t\t\t {vertex.NumberOfStops}");
            }

            Console.ReadLine();
        }
    }
}
