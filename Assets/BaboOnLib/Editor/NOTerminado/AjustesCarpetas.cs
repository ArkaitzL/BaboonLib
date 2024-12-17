using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(fileName = "Ajustes[Nuevo]", menuName = "AjustesCarpetas", order = 1)]
public class AjustesCarpetas : ScriptableObject
{
    // Variable que hace referencia a la instancia de ListaCarpetas
    [Space][Header("Cambia la Paleta de las carpetas: ")]
    [SerializeField] private ListaCarpetas listaCarpetas;
    [Header(" Colores de las carpetas: ")]
    // [HideInInspector]
    [SerializeField] public ColorCarpeta[] colores = {
        //new ColorCarpeta("Blanco", new Color(0.95f, 0.95f, 0.95f)), // Blanco no puro
        new ColorCarpeta("Negro", new Color(0.1f, 0.1f, 0.1f)),     // Negro no puro
        new ColorCarpeta("Azul", new Color(0.4f, 0.6f, 1.0f)),  // Soft Blue
        new ColorCarpeta("Rojo", new Color(0.9f, 0.2f, 0.2f)),  // Vivid Red
        new ColorCarpeta("Verde", new Color(0.2f, 0.8f, 0.4f)),  // Emerald Green
        new ColorCarpeta("Amarillo", new Color(1.0f, 0.9f, 0.3f)),  // Yellow
        new ColorCarpeta("Naranja", new Color(1.0f, 0.5f, 0.2f)),  // Soft Orange
        new ColorCarpeta("Morado", new Color(0.6f, 0.4f, 1.0f)),  // Purple
        new ColorCarpeta("Rosa", new Color(1.0f, 0.6f, 0.6f)),  // Coral Pink
        new ColorCarpeta("Gris", new Color(0.6f, 0.6f, 0.6f)),  // Soft Gray
    };
    [Header("Ajustar posicion y tamaño de los iconos")]
    [SerializeField][Range(0.5f, 2f)] private float tamN = 1;
    [SerializeField] [Range(0.8f, 1.08f)] private float xPosN = 1f;
    [SerializeField][Range(0.2f, 1.5f)] private float yPosN = 1f;
    [Header("Iconos disponibles: ")]
    [SerializeField] public List<Texture2D> iconos = new List<Texture2D>();

    // Cambia el valor en el Inspector
    private void OnValidate()
    {
        // Asignar el contenido de listaCarpetas
        if (listaCarpetas != null) { 
            Carpetas.ListaCarpetas = listaCarpetas;
        }

        // Asigna los colores diponibles
        if (colores != null) {
            CarpetaContextMenu.SetColores(colores);
        }

        if (iconos != null)
        {
            CarpetaContextMenu.Iconos = iconos.ToArray();
        }

        // Cambia tamaño y posicion de los iconos
        Carpetas.tamN = tamN;
        Carpetas.xPosN = xPosN;
        Carpetas.yPosN = yPosN;
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

