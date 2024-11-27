using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Carpetas : MonoBehaviour
{
    // Lista de las carpetas pintadas
    [SerializeField] private static ListaCarpetas listaCarpetas;
    public static ListaCarpetas ListaCarpetas { get => listaCarpetas; set => listaCarpetas = value; }


    private static Texture2D iconoCarpeta, carpetaVacia;

    static Carpetas()
    {
        // Se llama cada vez que se dibuja un elemento en pantalla
        EditorApplication.projectWindowItemOnGUI += Buscar;
        // Obtiene el ícono de carpeta predeterminado
        iconoCarpeta = EditorGUIUtility.FindTexture("Folder Icon");
        carpetaVacia = EditorGUIUtility.FindTexture("FolderEmpty Icon");
    }

    private static void Buscar(string guid, Rect rect)
    {
        // Sale si no tiene carpetas que dibujar
        if (ListaCarpetas == null) {
            Debug.LogWarning("Asigna una paleta en Ajustes");
            return;
        }
        if (ListaCarpetas.Count() == 0) return;

        // Busca la ruta de la carpeta
        string ruta = AssetDatabase.GUIDToAssetPath(guid);

        // Si no es carpeta salimos
        if (!AssetDatabase.IsValidFolder(ruta)) return;

        // Coge el nombre de la carpeta
        string carpeta = System.IO.Path.GetFileName(ruta);

        for (int i = 0; i < ListaCarpetas.Count(); i++)
        {
            //Si no es una de nuestras carpetas continua
            if (!ListaCarpetas.ContainsKey(carpeta)) continue;

            Pintar(
                rect,
                ListaCarpetas.GetValue(carpeta),
                AssetDatabase.FindAssets("", new[] { ruta }).Length == 0
            );

            // Como ya ha dibujado algo termina con esta carpeta
            break;
        }
    }

    private static void Pintar(Rect rect, Color32 color, bool vacio)
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
        GUI.DrawTexture(
            new Rect(xPos, yPos, tamano, tamano),
            !vacio ? iconoCarpeta : carpetaVacia
       );
        GUI.color = Color.white; // Restaura el color después de dibujar
    }
}
