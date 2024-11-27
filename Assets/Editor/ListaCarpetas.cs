using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Paleta[X]", menuName = "Paletacarpetas", order = 1)]
public class ListaCarpetas : ScriptableObject
{
    // Esta lista actuará como el "diccionario" interno
    //[HideInInspector]
    [SerializeField]
    public List<DatosCarpeta> datos;

    // ADD -> Agreagar un elemento
    public void AddColor(string nombre, string color)
    {
        Add(nombre, color, null, null);
    }

    public void AddIcono(string nombre, Texture2D icono)
    {
        Add(nombre, null, icono, null);
    }

    public void AddIconoColor(string nombre, string colorIcono)
    {
        Add(nombre, null, null, colorIcono);
    }

    private void Add(string nombre, string color, Texture2D icono, string colorIcono) 
    {
        // Verifica si el nombre ya existe
        int index = FindIndexByNombre(nombre);

        if (index != -1)  // Si existe, reemplaza el valor
        {
            if (color != null) datos[index].color = color;
            if (icono != null) datos[index].icono = icono;
            if (colorIcono != null) datos[index].colorIcono = colorIcono;

            return;
        }

        // Si no existe, agrega un nuevo elemento
        datos.Add(new DatosCarpeta { nombre = nombre, color = color, icono = icono, colorIcono = colorIcono });

    }

    // REMOVE -> Elimina un elemento
    public void RemoveColor(string nombre) 
    {
        Remove(nombre, true, false, false);
    }
    public void RemoveIcono(string nombre)
    {
        Remove(nombre, false, true, false);
    }
    public void RemoveIconoColor(string nombre)
    {
        Remove(nombre, false, false, true);
    }

    private void Remove(string nombre, bool color, bool icono, bool colorIcono)
    {
        int index = FindIndexByNombre(nombre);
        if (index != -1)
        {
            if (color) datos[index].color = null;
            if (icono) datos[index].icono = null;
            if (colorIcono) datos[index].colorIcono = null;
        }

        // LO elimina si no queda ningun dato guardao
        if (datos[index].color == null && datos[index].icono == null && datos[index].colorIcono == null)
        {
            datos.RemoveAt(index);
        }
    }

    // CONTAINSKEY -> Busca 
    public bool ContainsKey(string nombre)
    {
        return datos.FindIndex(x => x.nombre == nombre) != -1;
    }

    // Método privado para encontrar el índice de un elemento por su nombre
    private int FindIndexByNombre(string nombre)
    {
        return datos.FindIndex(x => x.nombre == nombre);
    }

    // COUNT -> Longitud de la lista
    public int Count()
    {
        return datos.Count;
    }

    // GETVALUE -> Devuelve todos los datos
    public DatosCarpeta GetValue(string nombre)
    {
        int index = FindIndexByNombre(nombre);
        if (index != -1)
        {
            return datos[index];
        }

        return null;
    }
}

// Clase para almacenar los datos
[System.Serializable]
public class DatosCarpeta
{
    public string nombre;
    public string color;
    public Texture2D icono;
    public string colorIcono;
}
