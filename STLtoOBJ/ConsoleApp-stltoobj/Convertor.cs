using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_stltoobj
{
    class Convertor
    {
        // Dictionary to store the comparison of vertices and normals
        static Dictionary<Point3D, uint> comparisonOfVertex = new Dictionary<Point3D, uint>();
        static Dictionary<Point3D, uint> comparisonOfNormal = new Dictionary<Point3D, uint>();

        // Method to extract coordinates from a string
        static float[] PointExtractor(string pointSubStr) 
        {
            float[] result = new float[3];

            // Extracting x
            int spaceStringIndex = pointSubStr.IndexOf(" ");
            result[0] = float.Parse(pointSubStr.Substring(0, spaceStringIndex));
            pointSubStr = pointSubStr.Substring(spaceStringIndex + 1);

            // Extracting y
            spaceStringIndex = pointSubStr.IndexOf(" ");
            result[1] = float.Parse(pointSubStr.Substring(0, spaceStringIndex));
            pointSubStr = pointSubStr.Substring(spaceStringIndex + 1);

            // Extracting z
            result[2] = float.Parse(pointSubStr);

            return result;
        }

        // Method to check and add a point
        public static void AddPoint(Point3D p, ref Triangulation trigu, ref uint pointIndex, bool isNormal) 
        {
            bool pointFound = false;
            Dictionary<Point3D, uint> comparisonDictionary = isNormal ? comparisonOfNormal : comparisonOfVertex;

            foreach (var k in comparisonDictionary)
            {
                if (k.Key.X == p.X && k.Key.Y == p.Y && k.Key.Z == p.Z)
                {
                    pointIndex = k.Value;
                    pointFound = true;
                    break;
                }
            }

            if (!pointFound)
            {
                comparisonDictionary.Add(p, pointIndex);
                List<Point3D> pointList = isNormal ? trigu.NormalsList : trigu.PointsList;
                pointList.Add(p);
                pointIndex = (uint)(pointList.Count - 1);
            }
        }

        // Method to read STL file
        public static void STLreader(string stlFilePath, ref Triangulation trigu) 
        {
            // Check if the file exists
            if(File.Exists(stlFilePath)) 
            {
                using (StreamReader reader = new StreamReader(stlFilePath))
                {
                    string line;
                    uint pointIndex = 0;
                    uint[] aTriangle = new uint[4];
                    int aTriIndex = 0;
                    
                    while((line = reader.ReadLine()) != null)
                    {
                        int normalStringIndex = line.IndexOf("facet normal");
                        int vertexStringIndex = line.IndexOf("vertex");
                        int endLoopStringIndex = line.IndexOf("endloop");

                        if(normalStringIndex != -1) // Extracting and adding Normal of a Triangle to NormalsList
                        {
                            float[] pointArr = PointExtractor(line.Substring(normalStringIndex + 13));
                            AddPoint(new Point3D() { X = pointArr[0], Y = pointArr[1], Z = pointArr[2] }, ref trigu, ref pointIndex, true);
                            aTriangle[aTriIndex] = pointIndex;
                            aTriIndex++;
                        }
                        else if(vertexStringIndex != -1) // Extracting and adding Vertex of a Triangle PointsList
                        {
                            float[] pointArr = PointExtractor(line.Substring(vertexStringIndex + 7));
                            AddPoint(new Point3D() { X = pointArr[0], Y = pointArr[1], Z = pointArr[2] }, ref trigu, ref pointIndex, false);
                            aTriangle[aTriIndex] = pointIndex;
                            aTriIndex++;
                        }
                        else if(endLoopStringIndex != -1) // Adding a Triangle to the triangulation after collecting all of its elements
                        {
                            trigu.TrianglesList.Add(new Triangle() { Normal = aTriangle[0], V1 = aTriangle[1], V2 = aTriangle[2], V3 = aTriangle[3] });
                            aTriIndex = 0;
                            Array.Clear(aTriangle, 0, aTriangle.Length);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error opening stl file");
            }
        }

        // Method to write OBJ file
        public static void OBJwriter(string objFilePath, ref Triangulation trigu) 
        {
            using (StreamWriter writer = new StreamWriter(objFilePath)) 
            {
                foreach (var i in trigu.PointsList)
                {
                    writer.WriteLine($"v {i.X} {i.Y} {i.Z}");
                }

                foreach (var i in trigu.NormalsList)
                {
                    writer.WriteLine($"vn {i.X} {i.Y} {i.Z}");
                }

                foreach (var i in trigu.TrianglesList)
                {
                    writer.WriteLine($"f {i.V1}//{i.Normal} {i.V2}//{i.Normal} {i.V3}//{i.Normal}");
                }
            }
        }
    }
}
