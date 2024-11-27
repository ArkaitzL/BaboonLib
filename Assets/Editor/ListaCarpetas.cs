using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Lista[X]", menuName = "Listacarpetas", order = 1)]
public class ListaCarpetas : ScriptableObject
{
    // Esta lista actuará como el "diccionario" interno
    [SerializeField] public List<Datoscarpeta> datos;

    // Clase para almacenar los datos
    [System.Serializable]
    public class Datoscarpeta
    {
        public string nombre;
        public Color32 color;
    }

    // Método para agregar o reemplazar un elemento
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

    // Método para eliminar un elemento por su nombre
    public void Remove(string nombre)
    {
        int index = FindIndexByNombre(nombre);

        if (index != -1)
        {
            datos.RemoveAt(index);
        }
    }

    // Método para verificar si existe una clave (nombre)
    public bool ContainsKey(string nombre)
    {
        return FindIndexByNombre(nombre) != -1;
    }

    // Método para obtener el número de elementos
    public int Count()
    {
        return datos.Count;
    }

    // Método privado para encontrar el índice de un elemento por su nombre
    private int FindIndexByNombre(string nombre)
    {
        return datos.FindIndex(x => x.nombre == nombre);
    }

    // Método para obtener el valor (color) de una clave (nombre)
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
