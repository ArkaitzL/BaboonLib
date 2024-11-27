using UnityEngine;
using UnityEditor;

public class CarpetaContextMenu : Editor
{
    // COLORES  ***

    // Colores diponibles
    private class ColoreCarpeta
    {
        public string nombre;
        public Color32 color;

        public ColoreCarpeta(string nombre, Color32 color)
        {
            this.nombre = nombre;
            this.color = color;
        }

    }
    private static ColoreCarpeta[] colores = {
        new ColoreCarpeta("Azul", Color.blue),
        new ColoreCarpeta("Rojo", Color.red),
        new ColoreCarpeta("Verde", Color.green),
    };

    // Logica de cambio de color
    private static void CambiarColor(Color color, string carpeta)
    {
        // Añade la carpeta
        Carpetas.listaCarpetas.Add(carpeta, color);
        ActualizarSO();
    }

    // Restaurar color
    private static void RestaurarColor(string carpeta)
    {
        // Elimina la carpeta
        if (!Carpetas.listaCarpetas.ContainsKey(carpeta)) return;
        Carpetas.listaCarpetas.Remove(carpeta);
        ActualizarSO();
    }

    // COLORES  ***



    // GLOBAL  ***

    // Detectar clic derecho sobre una carpeta
    [InitializeOnLoad]
    public class CarpetaContextMenuHandler
    {
        static CarpetaContextMenuHandler()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect rect)
        {
            // Consigue la ruta y la carpeta
            string ruta = AssetDatabase.GUIDToAssetPath(guid);
            string carpeta = System.IO.Path.GetFileName(ruta);

            // Sale si no es una carpeta
            if (!AssetDatabase.IsValidFolder(ruta)) return;

            Event currentEvent = Event.current;

            if (currentEvent.type == EventType.ContextClick && rect.Contains(currentEvent.mousePosition))
            {
                // Si el clic derecho es dentro de la carpeta, abrir el menú contextual
                GenericMenu menu = new GenericMenu();

                // Agregar opciones al menú
                menu.AddItem(new GUIContent("Color/Restaurar"), false, () => RestaurarColor(carpeta));
                foreach (ColoreCarpeta cc in colores)
                {
                    menu.AddItem(
                        new GUIContent($"Color/{cc.nombre}"),
                        false, () => CambiarColor(cc.color, carpeta)
                    );
                }

                // Mostrar el menú
                menu.ShowAsContext();

                currentEvent.Use();
            }
        }
    }

    private static void ActualizarSO() {
        // Marca el ScriptableObject como sucio (modificado)
        EditorUtility.SetDirty(Carpetas.listaCarpetas);
        // Guarda los cambios en el disco
        AssetDatabase.SaveAssets();
    }

    // GLOBAL  ***

}
