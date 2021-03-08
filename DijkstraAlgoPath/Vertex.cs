using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraAlgoPath
{
    public class Vertex
    {
        public string SourceName { get; set; }
        public int EdgeWeight { get; set; }
        public string Name { get; set; }
        public int NumberOfStops { get; set; }
        public int Distance { get; set; }
    }
}
