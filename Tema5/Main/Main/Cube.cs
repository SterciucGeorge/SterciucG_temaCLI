using System;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Main
{
    internal class Cube
    {
        private float[,] vertices;
        private readonly int[,] indices = {
            { 0, 1, 2 }, { 0, 2, 3 }, //spate
            { 4, 5, 6 }, { 4, 6, 7 }, //fata
            { 0, 1, 5 }, { 0, 5, 4 }, //jos
            { 2, 3, 7 }, { 2, 7, 6 }, //sus
            { 1, 2, 6 }, { 1, 6, 5 }, //dreapta
            { 3, 0, 4 }, { 3, 4, 7 }  //stanga
        };

        //private readonly Color[] colors = 
        //{Color.Red, Color.Green, Color.Blue, Color.Yellow,Color.Cyan, Color.Magenta, Color.White, Color.Black};

        private Color topFaceColor; //culoare sus
        private Color[,] vertexColors;//culoare pt fiecare face

        public Cube(string filePath)
        {
            LoadVertices(filePath);
            InitializeVertexColors();
            topFaceColor = Color.White;
        }
        private void InitializeVertexColors()
        {
            Random random = new Random();
            vertexColors = new Color[vertices.GetLength(0), 1];//tablou de culori

            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                //random culoare pt fiecare vertex
                vertexColors[i, 0] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                //afisarea in consola
                Console.WriteLine($"Vertex {i}: R={vertexColors[i, 0].R}, G={vertexColors[i, 0].G}, B={vertexColors[i, 0].B}");
            }
        }
        private void LoadVertices(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                vertices = new float[lines.Length, 3];

                for (int i = 0; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 3)
                    {
                        throw new FormatException($"Linia {i + 1} nu contine 3 coordonate");
                    }

                    vertices[i, 0] = float.Parse(parts[0]);
                    vertices[i, 1] = float.Parse(parts[1]);
                    vertices[i, 2] = float.Parse(parts[2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcare {ex.Message}");
                vertices = new float[0, 3];
            }
        }

        public void ChangeTopFaceColor()//generator random pentru culoarea de sus
        {
            Random random = new Random();
            topFaceColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            Console.WriteLine($"Noua culoare de sus este: R={topFaceColor.R}, G={topFaceColor.G}, B={topFaceColor.B}");

        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.Translate(0, 0, 0);
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < indices.GetLength(0); i++)
            {
                for (int j = 0; j < 3; j++) 
                {
                    int vertexIndex = indices[i, j];
                    if (i == 6) //index fata de sus
                    {
                        GL.Color3(topFaceColor);
                    }
                    else
                    {
                        GL.Color3(vertexColors[vertexIndex, 0]);
                    }
                    GL.Vertex3(vertices[vertexIndex, 0], vertices[vertexIndex, 1], vertices[vertexIndex, 2]);

                }
            }
            GL.End();
            GL.PopMatrix();
        }
    }
}