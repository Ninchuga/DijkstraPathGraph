using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijkstraAlgoPath
{
    public class DijkstraGraph
    {
        public readonly List<Vertex> Vertices;
        private readonly Dictionary<string, List<Vertex>> _neighbours;
        private readonly HashSet<Vertex> _visitedVertices;
        private readonly List<Vertex> _unvisitedVertices;

        public DijkstraGraph(IEnumerable<Vertex> vertices)
        {
            Vertices = vertices.ToList();
            _visitedVertices = new HashSet<Vertex>();
            _unvisitedVertices = new List<Vertex>(vertices);
            _neighbours = new Dictionary<string, List<Vertex>>();
        }

        /// <summary>
        /// Add an edge/vertex with the weight into the graph
        /// </summary>
        /// <param name="startingVertexName">starting point</param>
        /// <param name="vertexName">name of the vertex that we want to add</param>
        /// <param name="weight">edge weight</param>
        /// <param name="distance">distance between edges/verteces, infinity/max by default</param>
        public void AddEdgeWithWeight(string startingVertexName, string vertexName, int weight, int distance)
        {
            var vertex = new Vertex { SourceName = startingVertexName, Name = vertexName, EdgeWeight = weight, Distance = distance };
            if (_neighbours.ContainsKey(startingVertexName))
                _neighbours[startingVertexName].Add(vertex);
            else
                _neighbours.Add(startingVertexName, new List<Vertex> { vertex });
        }

        /// <summary>
        /// Calculates the distance between verteces in the graph
        /// </summary>
        public void CalculateDistanceBetweenVerteces()
        {
            while (_unvisitedVertices.Any())
            {
                var smallestDistanceVertex = _unvisitedVertices.OrderBy(x => x.Distance).FirstOrDefault();
                _unvisitedVertices.Remove(smallestDistanceVertex);

                int skipVerticesNumber = _visitedVertices.Count + 1;
                int addedEdges = 1;
                foreach (var vertex in Vertices.Skip(skipVerticesNumber)) // skip visited vertices and add edges to the current selected vertex
                {
                    int edgeWeight = addedEdges + 1; // increment edge distance weight by 1
                    AddEdgeWithWeight(smallestDistanceVertex.Name, vertex.Name, edgeWeight, distance: int.MaxValue);
                    if (addedEdges == 2) // each edge has two additional edges/paths max
                        break;
                    addedEdges += 1;
                }


                var vertexNeighbours = GetNeighboursFor(smallestDistanceVertex.Name);
                if (vertexNeighbours.Any())
                {
                    foreach (var neighbours in vertexNeighbours)
                    {
                        var newDistanceFromSource = smallestDistanceVertex.Distance + neighbours.EdgeWeight;
                        var vertex = Vertices.FirstOrDefault(vertex => vertex.Name.Equals(neighbours.Name));
                        if (vertex.Distance > newDistanceFromSource)
                        {
                            vertex.Distance = newDistanceFromSource;

                            if (smallestDistanceVertex.Name != Vertices.First().Name)
                                vertex.NumberOfStops += 1;
                        }
                    }
                }

                _visitedVertices.Add(smallestDistanceVertex);
            }
        }

        /// <summary>
        /// Get the vertex neighbours
        /// </summary>
        /// <param name="vertexName"></param>
        /// <returns></returns>
        public List<Vertex> GetNeighboursFor(string vertexName) =>
            _neighbours.ContainsKey(vertexName) ? _neighbours[vertexName] : new List<Vertex>();
    }
}
