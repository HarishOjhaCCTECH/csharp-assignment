using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_stltoobj
{
    class Triangulation
    {     
        public List<Triangle> TrianglesList { get; set; } // property: contains Triangles
        public List<Point3D> PointsList { get; set; }     // property: contains unique vertices in Point3D form
        public List<Point3D> NormalsList { get; set; }    // property: contains Normal of a triangle plane repesented by Point3D

        // default constructor which initialises all the properties
        public Triangulation()                            
        {
            TrianglesList = new List<Triangle>();
            PointsList = new List<Point3D>();  
            NormalsList = new List<Point3D>(); 
        }
        ~Triangulation() { }  // destructor
    }
}
