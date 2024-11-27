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
        // Dibuja las variables
        DrawDefaultInspector();

        // OTROS
        GUILayout.Space(10);

        // LABEL PRUEBAS
        GUILayout.Label("Este es un label de explicación", EditorStyles.boldLabel);

        // BOTON PRUEBAS
        if (GUILayout.Button("Presionar botón"))
        {
            Debug.Log("HOLO");
        }
    }
}
