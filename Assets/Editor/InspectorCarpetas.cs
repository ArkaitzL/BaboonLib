using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AjustesCarpetas))]
public class MiComponenteEditor : Editor
{
    // Variable para almacenar el componente que estamos editando
    private AjustesCarpetas ajustes;

    private void OnEnable()
    {
        // Inicializamos el componente al que pertenece este editor
        ajustes = (AjustesCarpetas)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Ajustes de carpetas: ", EditorStyles.boldLabel);

        // Dibuja las variables
        DrawDefaultInspector();

        //ListaColores();

        //GUILayout.Space(20);

        //GUILayout.Label("Este es un label de explicación", EditorStyles.boldLabel);

        //if (GUILayout.Button("Presionar botón"))
        //{
        //    Debug.Log("HOLA");
        //}
    }

    //private void ListaColores() { 
    //    // Obtén la referencia al objeto que estás editando
    //    AjustesCarpetas ajustesCarpetas = (AjustesCarpetas)target;

    //    // Título
    //    EditorGUILayout.LabelField("Colores de Carpetas", EditorStyles.boldLabel);

    //    // Itera sobre cada color en la lista
    //    for (int i = 0; i < ajustesCarpetas.colores.Length; i++)
    //    {
    //        var colorCarpeta = ajustesCarpetas.colores[i];

    //        EditorGUILayout.BeginHorizontal();

    //        // Campo de texto para el nombre
    //        colorCarpeta.nombre = EditorGUILayout.TextField("Nombre", colorCarpeta.nombre);

    //        // Campo de color
    //        colorCarpeta.color = EditorGUILayout.ColorField("Color", colorCarpeta.color);

    //        // Botón para eliminar el color
    //        if (GUILayout.Button("X", GUILayout.Width(24)))
    //        {
    //            // Eliminar el color de la lista
    //            ArrayUtility.RemoveAt(ref ajustesCarpetas.colores, i);
    //        }

    //        EditorGUILayout.EndHorizontal();
    //    }

    //    // Botón para agregar un nuevo color
    //    if (GUILayout.Button("Agregar Nuevo Color"))
    //    {
    //        ArrayUtility.Add(ref ajustesCarpetas.colores, new ColorCarpeta("Nuevo Color", Color.white));
    //    }

    //    // Aplicar cambios al objeto si algo cambió
    //    if (GUI.changed)
    //    {
    //        Debug.Log("SIIIUU");
    //        EditorUtility.SetDirty(ajustesCarpetas);
    //        AssetDatabase.SaveAssets();
    //    }
    //}
}
