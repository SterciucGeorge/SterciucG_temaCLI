﻿using System;
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
        private float triangleX = 0; //coordonate triunghiu, necesare pt miscarea cu mouse-ul
        private float triangleY = 0;
        private float triangleZ = 0;

        public Program() : base(1280, 720, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
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

            if (drawTriangle)//deseneaza triunghiul
            {
                DrawGradientTriangle();
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

            //z axis roz
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.HotPink);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, SIZE);
            GL.End();
        }

        private void DrawGradientTriangle() // deseneaza triunghiul
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Red);//vertex 1
            GL.Vertex3(triangleX + SIZE / 4, triangleY, triangleZ);// folosim triangleXYZ pt a putea sa il miscam in timp real

            GL.Color3(Color.Green); //vertex 2
            GL.Vertex3(triangleX, triangleY + SIZE / 4, triangleZ);

            GL.Color3(Color.Blue);//vertex 3
            GL.Vertex3(triangleX, triangleY, triangleZ + SIZE / 4);

            GL.End();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e) //cand ambele click-uri sunt apasate este desenat triunghiul
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                drawTriangle = true;
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e) //miscarea triunghiului
        {
            base.OnMouseMove(e);
            if (drawTriangle)
            {
                //converteste coordonate de mouse in coordonate de opengl
                triangleX = (e.X / (float)Width) * SIZE - SIZE / 2; 
                triangleY = (1 -e.Y / (float)Height) * SIZE - SIZE / 2;  
                triangleZ = 0; //z ramane constant
            }
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
