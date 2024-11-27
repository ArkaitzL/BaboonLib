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
            //Continua si ya tiene uno y es igual
            if (cc.textura != null && cc.textura.GetPixel(0, 0).Equals(cc.color)) continue;

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
    public static void CambiarColor(string color, string carpeta)
    {
        // Añade la carpeta
        Carpetas.ListaCarpetas.Add(carpeta, color);
        ActualizarSO();
    }

    // Restaurar color
    public static void RestaurarColor(string carpeta)
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
                //Abre la ventana flotante con los colores
                PopupWindow.Show(
                     new Rect(currentEvent.mousePosition, Vector2.zero),
                     new PopupMenuCarpetas(colores, carpeta)
                 );
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
