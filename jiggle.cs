// Mouse jiggler minimalista: cada N segundos mueve el mouse y lo devuelve a su sitio.
// Usa SendInput (input real), asi Windows/Teams lo cuentan como actividad.
using System;
using System.Runtime.InteropServices;
using System.Threading;

static class Jiggle
{
    const int IntervaloSeg = 180;   // cada 3 minutos (Teams marca ausente a los 5)
    const int Distancia = 40;       // pixeles que se mueve

    [StructLayout(LayoutKind.Sequential)] struct POINT { public int X, Y; }

    [StructLayout(LayoutKind.Sequential)]
    struct INPUT { public uint type; public uint pad; /* alineacion x64 */ public int dx, dy; public uint mouseData, flags, time; public IntPtr extra; }

    [DllImport("user32.dll")] static extern bool GetCursorPos(out POINT p);
    [DllImport("user32.dll")] static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] static extern uint SendInput(uint n, INPUT[] i, int size);
    [DllImport("user32.dll")] static extern int GetSystemMetrics(int idx);

    static void MoverRelativo(int dx, int dy)
    {
        var i = new INPUT[1];
        i[0].type = 0;          // INPUT_MOUSE
        i[0].dx = dx;
        i[0].dy = dy;
        i[0].flags = 0x0001;    // MOUSEEVENTF_MOVE (relativo)
        SendInput(1, i, Marshal.SizeOf(typeof(INPUT)));
    }

    static void Main()
    {
        bool creado;
        // Evita que se abran dos instancias a la vez.
        using (new Mutex(true, "Local\\CursorActivateJiggle", out creado))
        {
            if (!creado) return;
            while (true)
            {
                Thread.Sleep(IntervaloSeg * 1000);

                POINT a;
                GetCursorPos(out a);

                // Si esta en la mitad derecha de la pantalla, se mueve hacia la izquierda (y viceversa):
                // asi nunca choca con el borde y el movimiento siempre ocurre.
                int ancho = GetSystemMetrics(0);
                int dir = (a.X > ancho / 2) ? -1 : 1;

                MoverRelativo(dir * Distancia, 0);
                Thread.Sleep(300);

                // Vuelve exactamente a donde estaba.
                // (posicion absoluta: el movimiento relativo se distorsiona con la aceleracion del mouse)
                SetCursorPos(a.X, a.Y);
            }
        }
    }
}
