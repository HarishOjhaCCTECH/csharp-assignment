using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_stltoobj
{
    class Triangle
    {
        public uint Normal { get; init; } // property: Normal of a Triangle plane
        public uint V1 { get; init; }     // property: Vertex1 of a Triangle 
        public uint V2 { get; init; }     // property: Vertex2 of a Triangle
        public uint V3 { get; init; }     // property: Vertex3 of a Triangle

        public Triangle() { }             // default constructor
        ~Triangle() { }                   // destructor
    }
}
