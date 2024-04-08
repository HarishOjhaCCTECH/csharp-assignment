using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_stltoobj
{
    class Point3D
    {
        public float X { get; init; }// property: x coordinate
        public float Y { get; init; }// property: y coordinate
        public float Z { get; init; }// property: z coordinate

        public Point3D() { }         // default constructor
        ~Point3D() { }               // destructor
    }
}
