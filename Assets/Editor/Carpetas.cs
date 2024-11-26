using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Carpetas : MonoBehaviour
{
    private class CarpetaInfo {
        public string nombre;
        public Color color;

        public CarpetaInfo(string nombre, Color color)
        {
            this.nombre = nombre;
            this.color = color;
        }
    }
    // Info de las carpetas   [CAMBIAR]
    private static readonly CarpetaInfo[] carpetas = {              // <-- INFO
        new CarpetaInfo(
            "PruebaCarpetas",
            new Color(0.5f, 0.8f, 0.5f)
        )
    };
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
        string ruta = AssetDatabase.GUIDToAssetPath(guid);
        // Si no es carpeta salimos
        if (!AssetDatabase.IsValidFolder(ruta)) return;
        // Coge el nombre de la carpeta
        string carpeta = System.IO.Path.GetFileName(ruta);

        for (int i = 0; i < carpetas.Length; i++)
        {
            //Si no es una de nuestras carpetas continua
            if (carpeta != carpetas[i].nombre) continue;

            Pintar(rect, i);

            // Como ya ha dibujado algo termina con esta carpeta
            break;
        }
    }

    private static void Pintar(Rect rect, int i) 
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
        GUI.color = carpetas[i].color; 

        // Dibuja el ícono centrado en el área de la carpeta
        GUI.DrawTexture(new Rect(xPos, yPos, tamano, tamano), iconoCarpeta);
        GUI.color = Color.white; // Restaura el color después de dibujar
    }
}
