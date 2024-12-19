using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SaveEditor
{
    // Diccionario para mantener el �ltimo nombre conocido de los GameObjects con SaveGO
    private static Dictionary<GameObject, string> nombres = new Dictionary<GameObject, string>();

    // Constructor est�tico
    static SaveEditor()
    {
        // Suscribirse al evento de cambios en la jerarqu�a
        EditorApplication.hierarchyChanged += OnHierarchyChanged;

        // Inicializar el diccionario al inicio
        GuardarNombres();
    }

    // M�todo que se llama cuando la jerarqu�a cambia
    private static void OnHierarchyChanged()
    {
        // Obtener todos los objetos con el componente SaveGO o derivados de �l
        SaveGO[] objetos = Object.FindObjectsOfType<SaveGO>();

        // Recorrer cada uno de los objetos de SaveGO
        foreach (var saveGO in objetos)
        {
            // Comprobar si el GameObject ha cambiado de nombre
            GameObject go = saveGO.gameObject;
            if (nombres.ContainsKey(go))
            {
                // Si el nombre no ha cambiado
                if (go.name == nombres[go]) continue;

                // Llamamos a la funcion para editar el guardado
                CambiarNombre(nombres[go], go.name);

                // Actualizamos el nombre en el diccionario
                nombres[go] = go.name;

                continue;
            }

            // Si no est� en el diccionario, lo agregamos
            nombres.Add(go, go.name);
        }
    }

    // M�todo para inicializar el diccionario con los objetos existentes al inicio
    private static void GuardarNombres()
    {
        // Obtener todos los objetos con el componente SaveGO o derivados de �l
        SaveGO[] objetos = Object.FindObjectsOfType<SaveGO>();

        // Agregar los objetos al diccionario
        foreach (var saveGO in objetos)
        {
            GameObject go = saveGO.gameObject;

            // Comprobar si esta
            if (nombres.ContainsKey(go)) return;
            // Lo a�ade
            nombres.Add(go, go.name);
        }
    }

    // Cambia el nombre de playerpref
    public static void CambiarNombre(string viejo, string nuevo)
    {
        //string lista = PlayerPrefs.GetString(SaveScript.MisEspacios, "");

        //// Si hay datos, los carga
        //if (string.IsNullOrEmpty(lista)) return;

        //// Convertir la cadena de nuevo a una lista de strings
        //List<string> espacios = new List<string>(lista.Split(','));

        //for (int i = 0; i < espacios.Count; i++)
        //{
        //    string nuevoNombre = ObtenerNombre(espacios[i], nuevo);
        //    string viejoNombre = ObtenerNombre(espacios[i], viejo);

        //    if (!PlayerPrefs.HasKey(viejoNombre)) return;

        //    // Obt�n el valor asociado con la clave vieja
        //    string valor = PlayerPrefs.GetString(viejoNombre);

        //    // Elimina la clave vieja
        //    PlayerPrefs.DeleteKey(viejoNombre);

        //    // Guarda el valor en la nueva clave
        //    PlayerPrefs.SetString(nuevoNombre, valor);

        //    // Guardar los cambios en PlayerPrefs
        //    PlayerPrefs.Save();
        //}
    }

    public static string ObtenerNombre(string espacio, string nombre) => $"{espacio}:{nombre}";  // Modificar tambien el de SaveScript
}
