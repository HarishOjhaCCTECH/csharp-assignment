namespace ConsoleApp_stltoobj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Triangulation triguCube = new Triangulation();
            Convertor.STLreader(@"D:\harish_ojha_workspace\csharp\t8-stl2obj\stl2obj\ConsoleApp-stltoobj\Cube.stl", ref triguCube);
            Convertor.OBJwriter(@"D:\harish_ojha_workspace\csharp\t8-stl2obj\stl2obj\ConsoleApp-stltoobj\cube.obj", ref triguCube);
        }
    }
}
