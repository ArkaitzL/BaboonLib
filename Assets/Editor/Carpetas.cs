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
        // Obtiene el �cono de carpeta predeterminado
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

            DatosCarpeta datos = ListaCarpetas.GetValue(carpeta);
            if (datos == null) continue;

            // Color carpeta
            if (datos.color != null)
            {
                Color32? color = CarpetaContextMenu.GetColor(
                    datos.color
                );
                if (color != null)
                {
                    Pintar(
                        rect,
                        (Color32) color,
                        AssetDatabase.FindAssets("", new[] { ruta }).Length == 0
                    );
                }
            }

            // Icono
            if (datos.icono != null)
            {
                Color32? color = CarpetaContextMenu.GetColor(
                  datos.colorIcono
                );

                Icono(
                    rect,
                    datos.icono,
                    color
                );
            }

            // Como ya ha dibujado algo termina con esta carpeta
            break;
        }
    }

    private static void Pintar(Rect rect, Color32 color, bool vacio)
    {
        // Calcula el tama�o 
        float tamano = rect.height;                                 

        // Centra el �cono
        float xPos = rect.x;
        float yPos = rect.y;

        if (rect.height != 16f) // Carpeta Principal
        {
            tamano -= 14;
        }

        if (rect.height == 16 && rect.x < 20) //Icono peque�o - Carpeta Principal
        {
            xPos += 3;
        }

        // Aplica un color al �cono
        GUI.color = color;

        // Dibuja el �cono centrado en el �rea de la carpeta
        GUI.DrawTexture(
            new Rect(xPos, yPos, tamano, tamano),
            !vacio ? iconoCarpeta : carpetaVacia
        );
        GUI.color = Color.white; // Restaura el color despu�s de dibujar
    }

    private static void Icono(Rect rect, Texture2D icono, Color32? color) 
    {
        // Tama�o del icono
        float tamano = rect.height;

        // Centra el �cono
        float xPos = rect.x;
        float yPos = rect.y;

        if (rect.height != 16f) // Carpeta Principal
        {
            tamano *= 0.5f;

            xPos = rect.x + (rect.width - tamano) / 0.8f; // Centrado horizontalmente
            yPos = rect.y + (rect.height - tamano) / 2; // Centrado verticalmente
        }
        else if(rect.x > 20) // Carpeta secundaria
        {
            tamano *= 0.6f;

            xPos = rect.x + 9f; // Centrado horizontalmente
            yPos = rect.y + (rect.height - tamano); // Centrado verticalmente
        }

        if (rect.height == 16 && rect.x < 20) //Icono peque�o - Carpeta Principal
        {
            tamano *= 0.6f;

            xPos = rect.x + 11f; // Centrado horizontalmente
            yPos = rect.y + (rect.height - tamano); // Centrado verticalmente
        }



        // Aplica un color al �cono
        if (color != null) GUI.color = (Color32)color;

        // Dibuja el �cono centrado en el �rea de la carpeta
        GUI.DrawTexture(
            new Rect(xPos, yPos, tamano, tamano),
            icono
        );
        GUI.color = Color.white; // Restaura el color despu�s de dibujar
    }
}
