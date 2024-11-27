using System;
using UnityEditor;
using UnityEngine;

public class PopupMenuCarpetas : PopupWindowContent
{
    private string carpeta;
    private ColorCarpeta[] colores;
    private Texture2D[] iconos;

    const int dimenciones = 32;

    // Constructor
    public PopupMenuCarpetas(ColorCarpeta[] colores, Texture2D[] iconos, string carpeta)
    {
        this.colores = colores;
        this.iconos = iconos;
        this.carpeta = carpeta;
    }

    // Define el tamaño de la ventana
    public override Vector2 GetWindowSize()
    {
        return new Vector2(250, (colores.Length * 9) * 2 + (iconos.Length * 9));
    }

    // Dibuja la ventana
    public override void OnGUI(Rect rect)
    {

        // Colores 
        EditorGUILayout.LabelField("Colores:", EditorStyles.boldLabel);
        ImprimirColor(
            () => { CarpetaContextMenu.RestaurarColor(carpeta); },
            (nombre) => { CarpetaContextMenu.CambiarColor(nombre, carpeta); }
        );

        // Iconos
        EditorGUILayout.LabelField("Iconos:", EditorStyles.boldLabel);
        ImprimirIconos();

        // Colores Iconos
        EditorGUILayout.LabelField("Colores de los iconos:", EditorStyles.boldLabel);
        ImprimirColor(
             () => { CarpetaContextMenu.RestaurarIconoColor(carpeta); },
            (nombre) => { CarpetaContextMenu.CambiarIconoColor(nombre, carpeta); }
        );
    }

    private void ImprimirColor(Action restaurar, Action<string> cambiar) 
    {
        GUILayout.BeginHorizontal();

        int fila = 2;
        if (GUILayout.Button("-", GUILayout.Width(dimenciones), GUILayout.Height(dimenciones)))
        {
            restaurar();
            editorWindow.Close();
        }

        foreach (var cc in colores)
        {
            // Botón con el nombre del color
            if (GUILayout.Button(cc.textura, GUILayout.Width(dimenciones), GUILayout.Height(dimenciones)))
            {
                cambiar(cc.nombre);
                editorWindow.Close();
            }

            // Salto de linea
            if (fila == 7)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                fila = 0;
                continue;
            }

            fila++;
        }

        GUILayout.EndHorizontal();
    }

    private void ImprimirIconos()
    {
        GUILayout.BeginHorizontal();

        int fila = 0;
        if (GUILayout.Button("-", GUILayout.Width(dimenciones), GUILayout.Height(dimenciones)))
        {
            CarpetaContextMenu.RestaurarIcono(carpeta);
            editorWindow.Close();
        }

        foreach (var ic in iconos)
        {
            // Botón con el nombre del color
            if (GUILayout.Button(ic, GUILayout.Width(dimenciones), GUILayout.Height(dimenciones)))
            {
                CarpetaContextMenu.CambiarIcono(ic, carpeta);
                editorWindow.Close();
            }

            // Salto de linea
            if (fila == 7)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                fila = 0;
                continue;
            }

            fila++;
        }

        GUILayout.EndHorizontal();
    }
}
