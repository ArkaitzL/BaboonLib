using UnityEditor;
using UnityEngine;

public class PopupMenuCarpetas : PopupWindowContent
{
    private string carpetaSeleccionada;
    private ColorCarpeta[] colores;

    // Constructor
    public PopupMenuCarpetas(ColorCarpeta[] coloresDisponibles, string carpeta)
    {
        colores = coloresDisponibles;
        carpetaSeleccionada = carpeta;
    }

    // Define el tamaño de la ventana
    public override Vector2 GetWindowSize()
    {
        return new Vector2(193, colores.Length * 10);
    }

    // Dibuja la ventana
    public override void OnGUI(Rect rect)
    {
        EditorGUILayout.LabelField("Colores:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        // Colores 
        int fila = 2;
        if (GUILayout.Button("-", GUILayout.Width(24), GUILayout.Height(24)))
        {
            CarpetaContextMenu.RestaurarColor(carpetaSeleccionada);
            editorWindow.Close();
        }

        foreach (var cc in colores)
        {
            // Botón con el nombre del color
            if (GUILayout.Button(cc.textura, GUILayout.Width(24), GUILayout.Height(24)))
            {
                CarpetaContextMenu.CambiarColor(cc.nombre, carpetaSeleccionada);
                editorWindow.Close();
            }

            // Salto de linea
            if (fila == 7)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }

            fila++;
        }

        GUILayout.EndHorizontal();
    }
}
