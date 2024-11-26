using UnityEditor;
using UnityEngine;
using System.IO;

public class Tags : MonoBehaviour
{
    [MenuItem("BaboonLib/Tags")]
    public static void GenerarEnum()
    {
        const string nombre = "TagsIDs";
        const string ruta = "Assets/BaboonLib/" + nombre + ".cs";

        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

        using (StreamWriter writer = new StreamWriter(ruta))
        {
            writer.WriteLine("public static class " + nombre);
            writer.WriteLine("{");

            foreach (string tag in tags)
            {
                writer.WriteLine($"    public const string {tag.Replace(" ", "_")} = \"{tag}\";");
            }

            writer.WriteLine("}");
        }

        AssetDatabase.Refresh();
        Debug.Log($"{nombre} generado en {ruta}");
    }
}
