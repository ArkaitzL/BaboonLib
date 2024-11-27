using UnityEngine;

//[CreateAssetMenu(fileName = "Ajustes[Nuevo]", menuName = "AjustesCarpetas", order = 1)]
public class AjustesCarpetas : ScriptableObject
{
    // Variable que hace referencia a la instancia de ListaCarpetas
    [Space][Header("Cambia la Paleta de las carpetas: ")]
    [SerializeField] private ListaCarpetas listaCarpetas;
    [Header("Cambia los Colores de las carpetas: ")]
    [SerializeField] private ColorCarpeta[] colores = {
        new ColorCarpeta("Azul", new Color(0.4f, 0.6f, 1.0f)),  // Soft Blue
        new ColorCarpeta("Rojo", new Color(0.9f, 0.2f, 0.2f)),  // Vivid Red
        new ColorCarpeta("Verde", new Color(0.2f, 0.8f, 0.4f)),  // Emerald Green
        new ColorCarpeta("Amarillo", new Color(1.0f, 0.9f, 0.3f)),  // Yellow
        new ColorCarpeta("Naranja", new Color(1.0f, 0.5f, 0.2f)),  // Soft Orange
        new ColorCarpeta("Morado", new Color(0.6f, 0.4f, 1.0f)),  // Purple
        new ColorCarpeta("Rosa", new Color(1.0f, 0.6f, 0.6f)),  // Coral Pink
        new ColorCarpeta("Gris", new Color(0.6f, 0.6f, 0.6f)),  // Soft Gray
    };


    // Cambia el valor en el Inspector
    private void OnValidate()
    {
        // Asignar el contenido de listaCarpetas
        if (listaCarpetas != null) { 
            Carpetas.ListaCarpetas = listaCarpetas;
        }

        //Asigna los colores diponibles
        if (colores != null) {
            CarpetaContextMenu.SetColores(colores);
        }

    }
    // Recargar unity
    // ** PENDIENTE **

}

[System.Serializable]
public class ColorCarpeta
{
    public string nombre;
    public Color32 color;
    [HideInInspector] public Texture2D textura;

    public ColorCarpeta(string nombre, Color32 color)
    {
        this.nombre = nombre;
        this.color = color;
    }

}

