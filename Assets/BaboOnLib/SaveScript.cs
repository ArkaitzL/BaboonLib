using System;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {}

[DefaultExecutionOrder(-99)]
public class SaveScript : MonoBehaviour
{
    // Cargar las variables al iniciar
    private void Awake()
    {
        Cargar();
    }

    // Guarda las variables cuando la aplicación se pausa o cierra
    private void OnApplicationPause(bool pause)
    {
        if (pause) Guardar();
    }

    private void OnApplicationQuit()
    {
        Guardar();
    }

    // Guardar todas las instancias de Save
    private void Guardar()
    {
        Save[] objetos = FindObjectsOfType<Save>();
        Almacen almacen = new Almacen();

        foreach (Save objeto in objetos)
        {
            // Serializa cada instancia usando su tipo real
            string json = JsonUtility.ToJson(objeto);
            string tipo = objeto.GetType().AssemblyQualifiedName;

            almacen.data.Add(new Data(tipo, json));
        }

        // Guarda el JSON en PlayerPrefs
        string almacenJson = JsonUtility.ToJson(almacen);
        PlayerPrefs.SetString("SaveData", almacenJson);
    }

    private void Cargar()
    {
        string almacenJson = PlayerPrefs.GetString("SaveData");
        if (string.IsNullOrEmpty(almacenJson)) return;

        Almacen almacen = JsonUtility.FromJson<Almacen>(almacenJson);

        foreach (Data data in almacen.data)
        {
            // Recupera el tipo original de la clase hija
            Type tipo = Type.GetType(data.tipo);
            if (tipo == null) continue;

            // Busca una instancia existente de la clase
            Save objeto = (Save)FindObjectOfType(tipo);
            if (objeto == null) continue;

            // Sobrescribe los valores con el JSON cargado
            JsonUtility.FromJsonOverwrite(data.json, objeto);
        }
    }

    // Eliminar todos los datos guardados
    [ContextMenu("Eliminar")]
    private void Eliminar()
    {
        Debug.Log("DATOS ELIMINADOS");
        PlayerPrefs.DeleteKey("SaveData");
    }

    [Serializable]
    private class Data
    {
        public string tipo;
        public string json;

        public Data(string tipo, string json)
        {
            this.tipo = tipo;
            this.json = json;
        }
    }

    [Serializable]
    private class Almacen
    {
        public List<Data> data = new List<Data>();
    }
}
