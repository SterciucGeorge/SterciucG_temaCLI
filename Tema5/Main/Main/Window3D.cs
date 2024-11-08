using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Main
{
    internal class Window3D : GameWindow
    {
        private Camera camera;
        private Cube cube;
        private Axes axes;
        private bool showCube = true;
        private KeyboardState lastKeyPress;

        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            camera = new Camera();
            cube = new Cube("coordonate.txt");
            axes = new Axes();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            camera.UpdateProjection(Width, Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            HandleInput();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            axes.Draw();
            if (showCube)
            {
                cube.Draw();
            }

            SwapBuffers();
        }

        private void HandleInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }
            if (keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                showCube = !showCube;
            }
            if (keyboard[Key.C])
            {
                cube.ChangeTopFaceColor(); //Cand apesi C schimba culoarea de sus a cubului
            }
            lastKeyPress = keyboard;
        }
    }
}