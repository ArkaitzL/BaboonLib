using UnityEngine;
using UnityEditor;

public class CarpetaContextMenu : Editor
{
    // COLORES  ***

    // Colores diponibles
    private static ColorCarpeta[] colores;
    public static Color32 GetColor(string nombre) {
        // Buscar el ColorCarpeta que tenga el nombre que coincide
        foreach (ColorCarpeta cc in colores)
        {
            if (cc.nombre != nombre) continue;
            return cc.color;
        }

        // Si no se encuentra, se puede devolver un valor por defecto
        return default(Color32);
    }
    public static void SetColores(ColorCarpeta[] nuevosColores) {
        colores = nuevosColores;

        foreach (ColorCarpeta cc in colores)
        {
            if (cc.textura != null) return;

            Texture2D textura = new Texture2D(16, 16, TextureFormat.RGBA32, false);
            for (int y = 0; y < textura.height; y++)
            {
                for (int x = 0; x < textura.width; x++)
                {
                    textura.SetPixel(x, y, cc.color);
                }
            }
            textura.Apply();
            cc.textura = textura;
        }
    }


    // Logica de cambio de color
    private static void CambiarColor(string color, string carpeta)
    {
        // A�ade la carpeta
        Carpetas.ListaCarpetas.Add(carpeta, color);
        ActualizarSO();
    }

    // Restaurar color
    private static void RestaurarColor(string carpeta)
    {
        // Elimina la carpeta
        if (!Carpetas.ListaCarpetas.ContainsKey(carpeta)) return;
        Carpetas.ListaCarpetas.Remove(carpeta);
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
                // Si el clic derecho es dentro de la carpeta, abrir el men� contextual
                GenericMenu menu = new GenericMenu();

                // Agregar opciones al men�
                menu.AddItem(new GUIContent("Color/Restaurar"), false, () => RestaurarColor(carpeta));
                foreach (ColorCarpeta cc in colores)
                {
                    // Convertir el color a formato hexadecimal
                    string colorHex = ColorUtility.ToHtmlStringRGB(cc.color);
                    string coloredText = $"<color=#{colorHex}>{cc.nombre}</color>";
                    menu.AddItem(
                        new GUIContent($"Color/{cc.nombre}", cc.textura),
                        false, 
                        () => CambiarColor(cc.nombre, carpeta)
                    );
                }

                // Mostrar el men�
                menu.ShowAsContext();

                currentEvent.Use();
            }
        }
    }

    private static void ActualizarSO() {
        // Marca el ScriptableObject como sucio (modificado)
        EditorUtility.SetDirty(Carpetas.ListaCarpetas);
        // Guarda los cambios en el disco
        AssetDatabase.SaveAssets();
    }

    // GLOBAL  ***

}
