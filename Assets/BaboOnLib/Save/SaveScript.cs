using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class SaveScript : MonoBehaviour
{
    // VARIABLES

    // Almacenamiento principal
    [SerializeField] private SaveData data = new SaveData();
    public SaveData Data { 
        get => data;
        set => data = value; 
    }

    // Espacios en memoria
    private List<string> espacio = new List<string>();
    private int usando = 0;

    const string NombreEspacioDefault = "Save";
    public const string MisEspacios = "MisEspacios";

    // Instancia
    public static SaveScript instancia;

    // AWAKE - (Singleton-Espacios-Cargar)

    // Cargar las variables al iniciar
    private void Awake()
    {
        // Crea un Singleton
        Singleton();

        // Administra los espacios en memoria
        Espacios();

        // Carga los datos
        Cargar(typeof(SaveData).Name);
        Cargar(typeof(SaveGO).Name);
    }

    private void Singleton() {

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            return;
        }

        Destroy(gameObject);
    }

    // GUARDAR

    // Guarda las variables cuando la aplicaci�n se pausa o cierra
    private void OnApplicationPause(bool pause)
    {
        if (!pause) return;
        IniciarGuardado();
    }

    private void OnApplicationQuit()
    {
        IniciarGuardado();
    }

    // Inicia el metodo de guardado
    private void IniciarGuardado() 
    {
        SaveGO[] objetos = FindObjectsOfType<SaveGO>();
        string[] gameobjects = objetos.Select(obj => obj.gameObject.name).ToArray();

        Guardar(
            objetos,
            gameobjects
        );
        Guardar(
            new SaveData[] { data }
        );
    }

    // Guardar todas las instancias de Save
    private void Guardar<T>(T[] objetos, string[] gameobjects = null)
    {
        Almacen almacen = new Almacen();
        for (int i = 0; i < objetos.Length; i++)
        {
            // Serializa cada instancia usando su tipo real
            string json = JsonUtility.ToJson(objetos[i]);
            string tipo = objetos[i].GetType().AssemblyQualifiedName;
            string gameobject = (gameobjects != null) ? gameobjects[i] : "";

            almacen.data.Add(new Almacen.Data(
                gameobject,
                tipo,
                json
            ));
        }

        // Nombre del tipo
        string nombre = typeof(T).Name;

        // Guarda el JSON en PlayerPrefs
        PlayerPrefs.SetString(
            ObtenerNombre(nombre),
            JsonUtility.ToJson(almacen)
        );
        PlayerPrefs.Save();
    }

    // CARGAR
    private void Cargar(string nombre)
    {
        // Obten el json
        string json = PlayerPrefs.GetString(ObtenerNombre(nombre));
        if (string.IsNullOrEmpty(json)) return;

        Almacen almacen = JsonUtility.FromJson<Almacen>(json);
        foreach (Almacen.Data data in almacen.data)
        {
            // Recupera el tipo original de la clase hija
            Type tipo = Type.GetType(data.tipo);
            if (tipo == null) return;

            // Cambia el valor de los objetos
            if (typeof(SaveData).Name == nombre) CargarSaveData(data.json);
            else if (typeof(SaveGO).Name == nombre) CargarSaveGO(data.gameobject, tipo, data.json);
        }
    }

    private void CargarSaveData(string json)
    {
        // Sobrescribe los valores con el JSON cargado
        JsonUtility.FromJsonOverwrite(json, data);
    }

    private void CargarSaveGO(string gameobject, Type tipo, string json)
    {
        // Comprueba el nombre del gameobject
        if (gameobject == null || gameobject == "") return;

        // Recupera los objetos de SaveGO
        SaveGO[] objetos = (SaveGO[])FindObjectsOfType(tipo);
        if (objetos == null || objetos.Length == 0) return;

        // Filtra para solo aplicarselo a los que necesiten
        SaveGO[] final = objetos.Where(obj => gameobject == obj.gameObject.name).ToArray();
        if (final == null || final.Length != 1) return;

        if (final.Length != 1) Debug.LogWarning($"[SAVE] Existen varias instancias de {gameobject}");

        // Sobrescribe los valores con el JSON cargado
        foreach (var obj in final)
        {
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    // OTROS

    // Obten el nombre del playerpref
    private string ObtenerNombre(string nombre) => $"{espacio[usando]}:{nombre}";

    // ESPACIOS

    // Eliminar todos los datos guardados
    [ContextMenu("Eliminar Todo")]
    private void EliminarTodo()
    {
        foreach (string nombre in espacio)  {
            PlayerPrefs.DeleteKey(nombre);
        }
        PlayerPrefs.Save();
    }

    private void Espacios()
    {
        // Crea un espacio en memoria si no existe
        if (espacio.Count == 0) NuevoEspacio(NombreEspacioDefault);
    }

    // Crear espacios en memoria
    private void NuevoEspacio(string nombre)
    {
        espacio.Add(NombreEspacio(nombre));
    }

    // Asegura que los nombres sean unicos
    private string NombreEspacio(string nombre)
    {
        const string Modificar = " (NEW)";

        if (UsandoEspacio(nombre))
        {
            return NombreEspacio(nombre + Modificar); ;
        }

        return nombre;
    }

    // Comprueba si existe un nombre igual
    private bool UsandoEspacio(string nombre) => espacio.Any((string usado) => usado == nombre);

    [Serializable]
    private class Almacen
    {
        public List<Data> data = new List<Data>();

        [Serializable]
        public class Data
        {
            public string gameobject;
            public string tipo;
            public string json;

            public Data(string gameobject, string tipo, string json)
            {
                this.gameobject = gameobject;
                this.tipo = tipo;
                this.json = json;
            }
        }
    }
}


