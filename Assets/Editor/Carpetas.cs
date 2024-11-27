using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Carpetas : MonoBehaviour
{
    [SerializeField] public static ListaCarpetas listaCarpetas;
    private static Texture2D iconoCarpeta;

    static Carpetas()
    {
        // Se llama cada vez que se dibuja un elemento en pantalla
        EditorApplication.projectWindowItemOnGUI += Buscar;
        // Obtiene el ícono de carpeta predeterminado
        iconoCarpeta = EditorGUIUtility.FindTexture("Folder Icon");
    }

    private static void Buscar(string guid, Rect rect)
    {
        // Sale si no tiene carpetas que dibujar
        if (listaCarpetas == null) {
            Debug.LogWarning("Asigna una lista en Ajustes");
            return;
        }
        if (listaCarpetas.Count() == 0) return;

        // Busca la ruta de la carpeta
        string ruta = AssetDatabase.GUIDToAssetPath(guid);

        // Si no es carpeta salimos
        if (!AssetDatabase.IsValidFolder(ruta)) return;

        // Coge el nombre de la carpeta
        string carpeta = System.IO.Path.GetFileName(ruta);

        for (int i = 0; i < listaCarpetas.Count(); i++)
        {
            //Si no es una de nuestras carpetas continua
            if (!listaCarpetas.ContainsKey(carpeta)) continue;

            Pintar(rect, listaCarpetas.GetValue(carpeta));

            // Como ya ha dibujado algo termina con esta carpeta
            break;
        }
    }

    private static void Pintar(Rect rect, Color32 color)
    {

        // Calcula el tamaño 
        float tamano = rect.height;                                 // <--TAMAÑO

        if (rect.x > 40f) // Carpeta Principal
        {
            tamano *= 0.8f;
        }

        // Centra el ícono
        float xPos = rect.x;
        float yPos = rect.y;

        // Aplica un color al ícono
        GUI.color = color;

        // Dibuja el ícono centrado en el área de la carpeta
        GUI.DrawTexture(new Rect(xPos, yPos, tamano, tamano), iconoCarpeta);
        GUI.color = Color.white; // Restaura el color después de dibujar
    }
}
