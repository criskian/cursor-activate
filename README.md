# cursor-activate

Mouse jiggler mínimo para Windows. Evita que Microsoft Teams te marque como "Ausente" por inactividad.

Cada **3 minutos** mueve el mouse 40 px y lo devuelve a su posición exacta. Si el cursor está en la mitad derecha de la pantalla se mueve hacia la izquierda (y viceversa), así que nunca choca con un borde. Usa `SendInput`, o sea input real, que Windows y Teams cuentan como actividad.

## Uso

| Acción | Cómo |
|---|---|
| **Activar** | Doble clic en `ACTIVAR.bat` |
| **Desactivar** | Doble clic en `DESACTIVAR.bat` |

- La primera vez compila `jiggle.exe` automáticamente (1 segundo, usa el .NET que ya trae Windows; no hay que instalar nada).
- Corre oculto, sin ventana, y usa ~13 MB de RAM y 0 % de CPU (duerme entre movimientos).
- Si apagas o reinicias el PC se detiene solo, así que no queda nada instalado ni en el inicio de Windows.
- Ejecutar `ACTIVAR.bat` dos veces no abre dos instancias.

## Ajustes

En `jiggle.cs`, arriba del todo:

```csharp
const int IntervaloSeg = 180;   // cada cuántos segundos
const int Distancia = 40;       // píxeles que se mueve
```

Si lo cambias, borra `jiggle.exe` y vuelve a ejecutar `ACTIVAR.bat` para recompilar.
