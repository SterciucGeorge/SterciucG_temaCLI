using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Tema2
{
    class Program : GameWindow
    {
        private const int SIZE = 50;
        private bool drawTriangle = false; //e triunghiul desenat ?
        private float triangleX = 0; //coordonatele triunghiului, necesare pt miscarea cu mouse-ul
        private float triangleY = 0;
        private float triangleZ = 0;

        //Culorile pentru fiecare vertex
        private Color vertexColor1;
        private Color vertexColor2;
        private Color vertexColor3;

        public Program() : base(1280, 720, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            //Initializare culori vertex
            vertexColor1 = GetRandomColor();
            vertexColor2 = GetRandomColor();
            vertexColor3 = GetRandomColor();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.PointSize(10);
            DrawAxes();

            if (drawTriangle) //deseneaza triunghiul
            {
                DrawTriangle();
            }

            SwapBuffers();
        }

        private void DrawAxes() //axele
        {
            GL.LineWidth(3.0f);
            //X axis albastru
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.BlueViolet);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(SIZE, 0, 0);
            GL.End();

            //Y axis portocaliu aprins
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.OrangeRed);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, SIZE, 0);
            GL.End();

            //Z axis roz
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.HotPink);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, SIZE);
            GL.End();
        }

        private void DrawTriangle() //deseneaza triunghiul
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(vertexColor1);
            GL.Vertex3(triangleX + SIZE / 4, triangleY, triangleZ); //vertex 1
            GL.Color3(vertexColor2);
            GL.Vertex3(triangleX, triangleY + SIZE / 4, triangleZ); //vertex 2
            GL.Color3(vertexColor3);
            GL.Vertex3(triangleX, triangleY, triangleZ + SIZE / 4); //vertex 3

            GL.End();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e) //cand ambele click-uri sunt apasate este desenat triunghiul
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                drawTriangle = true;

                //random culori
                vertexColor1 = GetRandomColor();
                vertexColor2 = GetRandomColor();
                vertexColor3 = GetRandomColor();
                //afisare mouse
                Console.WriteLine($"V1 Color: R={vertexColor1.R},G={vertexColor1.G},B={vertexColor1.B}");
                Console.WriteLine($"V2 Color: R={vertexColor2.R},G={vertexColor2.G},B={vertexColor2.B}");
                Console.WriteLine($"V3 Color: R={vertexColor3.R},G={vertexColor3.G},B={vertexColor3.B}");
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e) //miscare
        {
            base.OnMouseMove(e);
            if (drawTriangle)
            {
                //converteste coordonate de mouse in coordonate de opengl
                triangleX = (e.X / (float)Width) * SIZE - SIZE / 2;
                triangleY = (1 - e.Y / (float)Height) * SIZE - SIZE / 2;
                triangleZ = 0; //z ramane constant
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e) //schimba culoarea triunghiului la apasarea tastei C
        {
            base.OnKeyDown(e);
            if (e.Key == Key.C)
            {
                vertexColor1 = GetRandomColor();
                vertexColor2 = GetRandomColor();
                vertexColor3 = GetRandomColor();
                //afisare tasta C
                Console.WriteLine($"V1 Color: R={vertexColor1.R}, G={vertexColor1.G}, B={vertexColor1.B}");
                Console.WriteLine($"V2 Color: R={vertexColor2.R}, G={vertexColor2.G}, B={vertexColor2.B}");
                Console.WriteLine($"V3 Color: R={vertexColor3.R}, G={vertexColor3.G}, B={vertexColor3.B}");
            }
        }

        private Color GetRandomColor() //genereaza o culoare aleatoarie
        {
            Random rand = new Random();
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (Program example = new Program())
            {
                example.Run(30.0, 60.0);
            }
        }
    }
}