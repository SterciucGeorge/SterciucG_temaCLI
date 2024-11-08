using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Main
{
    internal class Camera
    {
        private Matrix4 perspective;
        private Matrix4 lookAt;

        public Camera()
        {
            UpdateProjection(800, 600);
            lookAt = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
        }

        public void UpdateProjection(int width, int height)
        {
            double aspect_ratio = width / (double)height;
            perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);
        }
    }
}