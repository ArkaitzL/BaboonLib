using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Lista[X]", menuName = "Listacarpetas", order = 1)]
public class ListaCarpetas : ScriptableObject
{
    // Esta lista actuar� como el "diccionario" interno
    [SerializeField] public List<Datoscarpeta> datos;

    // Clase para almacenar los datos
    [System.Serializable]
    public class Datoscarpeta
    {
        public string nombre;
        public Color32 color;
    }

    // M�todo para agregar o reemplazar un elemento
    public void Add(string nombre, Color32 color)
    {
        // Verifica si el nombre ya existe
        int index = FindIndexByNombre(nombre);

        if (index != -1)  // Si existe, reemplaza el valor
        {
            datos[index].color = color;
        }
        else  // Si no existe, agrega un nuevo elemento
        {
            datos.Add(new Datoscarpeta { nombre = nombre, color = color });
        }
    }

    // M�todo para eliminar un elemento por su nombre
    public void Remove(string nombre)
    {
        int index = FindIndexByNombre(nombre);

        if (index != -1)
        {
            datos.RemoveAt(index);
        }
    }

    // M�todo para verificar si existe una clave (nombre)
    public bool ContainsKey(string nombre)
    {
        return FindIndexByNombre(nombre) != -1;
    }

    // M�todo para obtener el n�mero de elementos
    public int Count()
    {
        return datos.Count;
    }

    // M�todo privado para encontrar el �ndice de un elemento por su nombre
    private int FindIndexByNombre(string nombre)
    {
        return datos.FindIndex(x => x.nombre == nombre);
    }

    // M�todo para obtener el valor (color) de una clave (nombre)
    public Color32 GetValue(string nombre)
    {
        int index = FindIndexByNombre(nombre);
        if (index != -1)
        {
            return datos[index].color;
        }

        // Devuelve un valor por defecto si no se encuentra
        return default(Color32);
    }
}
