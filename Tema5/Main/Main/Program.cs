using System;
using OpenTK;

namespace Main
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var window = new Window3D())
            {
                window.Run(30.0, 0.0);
                //////programul deseneaza un mic cub de culori diferite random
                //////cu ajutorul tastei C se poate schimba culoarea de sus a cubului (tot una random)
                /////iar in consola sunt notate toate codurile RGB pentru toate culorile chiar si cele
                //////generate aleatoriu de tasta C
            }
        }
    }
}