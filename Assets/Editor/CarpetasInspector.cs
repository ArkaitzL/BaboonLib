using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DefaultAsset))]
public class CarpetasInspector : Editor
{
    public override void OnInspectorGUI()
    {
        string path = AssetDatabase.GetAssetPath(target);

        // Verifica si el objeto seleccionado es una carpeta
        if (AssetDatabase.IsValidFolder(path))
        {
            // Aqu� puedes personalizar el contenido del Inspector
            EditorGUILayout.LabelField("Carpeta Seleccionada:", EditorStyles.boldLabel);
            EditorGUILayout.TextField("Ruta Completa:", path);

            // A�ade propiedades personalizadas o botones
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Propiedades Personalizadas");
            EditorGUILayout.LabelField("Ejemplo 1:", "Valor 1");
            EditorGUILayout.LabelField("Ejemplo 2:", "Valor 2");

            if (GUILayout.Button("Bot�n de Acci�n"))
            {
                Debug.Log($"�Acci�n ejecutada para la carpeta: {path}!");
            }
        }
        else
        {
            // Si no es una carpeta, usa el comportamiento predeterminado
            base.OnInspectorGUI();
        }
    }
}
