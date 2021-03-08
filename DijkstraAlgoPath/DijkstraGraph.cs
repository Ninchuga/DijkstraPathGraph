using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijkstraAlgoPath
{
    public class DijkstraGraph
    {
        private readonly Dictionary<string, List<Vertex>> _neighbours = new Dictionary<string, List<Vertex>>();
        public HashSet<Vertex> VisitedVertices;
        public List<Vertex> UnvisitedVertices;

        public DijkstraGraph(IEnumerable<Vertex> vertices)
        {
            VisitedVertices = new HashSet<Vertex>();
            UnvisitedVertices = new List<Vertex>(vertices);
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
        /// Get the vertex neighbours
        /// </summary>
        /// <param name="vertexName"></param>
        /// <returns></returns>
        public List<Vertex> GetNeighboursFor(string vertexName) =>
            _neighbours.ContainsKey(vertexName) ? _neighbours[vertexName] : new List<Vertex>();
    }
}
