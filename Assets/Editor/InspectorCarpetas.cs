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

        GUILayout.Space(20);

        GUILayout.Label("Este es un label de explicaci�n", EditorStyles.boldLabel);

        if (GUILayout.Button("Presionar bot�n"))
        {
            Debug.Log("HOLA");
        }
    }
}
