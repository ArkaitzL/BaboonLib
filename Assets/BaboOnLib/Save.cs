using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;

//TAREAS:

// 3.- Funcion de accion (Para instancias etc...)
// 4.- UI (ver boton eliminar y contenido variables)
// 5.- PlayerPref de documentacion
//      - Importar Objeto con Save
//      - Importar ¿Newtonsoft.Json?
//      - Documentacion
//          - Basico
//          - Accion
// 6.- Mejorar eliminar

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class SaveAttribute : Attribute
{
    public SaveAttribute() { }
}

[DefaultExecutionOrder(-99)]
public class Save : MonoBehaviour {

    private Dictionary<Type, Data> objeto = new Dictionary<Type, Data>();
    public struct Data {
        public object clase; 
        public Dictionary<string, FieldInfo> variables; 
    }
    [Serializable] public class Almacenado {

        public List<Valores> valores = new List<Valores>();
        [Serializable] public class Valores {
            public string nombre;
            public string valor;
            public string tipo;

            private const string json_listas = "*|JsonListasClave|*";

            // Guardar valores
            public Valores(string nombre, object valor)
            {
                this.nombre = nombre;
                tipo = valor.GetType().AssemblyQualifiedName;

                if (valor is string || valor.GetType().IsPrimitive) { // Primitivos
                    this.valor = valor.ToString();
                }
                else { // Objetos
                    this.valor = JsonConvert.SerializeObject(valor);
                }
            }
            // Cargar valores
            public object Valor()
            {
                Type tipo_valor = Type.GetType(tipo);
                if (tipo_valor == null && tipo != json_listas) {
                    Debug.LogWarning($"[BL] No se pudo encontrar el tipo: {tipo}");
                    return null;
                }

                if(tipo_valor.IsPrimitive) { // Primitivos
                    return Convert.ChangeType(valor, tipo_valor);
                }
                else if (tipo_valor == typeof(string))  { // String
                    return valor;
                }
                else  { // Objetos
                    return JsonConvert.DeserializeObject(valor, tipo_valor);
                }
            }

            [Serializable] public class JsonListas {
                public object lista;
                public JsonListas(object lista) {
                    this.lista = lista;
                }
            }
        }
    }

    // COGER VARIABLES A GUARDAR
    private void Awake()
    {
        MonoBehaviour[] objetos = FindObjectsOfType<BaboonLib>();
        // Recorrer todos las clases de BABOONLIB
        foreach (var obj in objetos)
        {
            Type tipo = obj.GetType();
            FieldInfo[] variables = tipo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); // Coge las variables de mi objeto

            // Verificar si el tipo ya está en el diccionario
            if (objeto.ContainsKey(tipo)) {
                Debug.LogWarning($"[BL-SAVE] Se han encontrado dos clases del tipo {tipo}");
                return;
            }

            // Recorrer todos los campos de la clase
            Dictionary<string, FieldInfo> variables_atributo = new Dictionary<string, FieldInfo>();
            foreach (var variable in variables)
            {
                var atributo = variable.GetCustomAttribute<SaveAttribute>();
                if (atributo == null) continue;

                variables_atributo.Add(variable.Name, variable);
            }

            // Guarda la informacion
            objeto.Add(tipo, new Data
            {
                clase = obj, 
                variables = variables_atributo
            });
        }

        // Carga la informacion disponible
        Cargar();
    }

    // GUARDAR LA INFORMACION
    private void OnApplicationPause(bool pause)  {
        if (pause) Guardar();
    }
    private void OnApplicationQuit() {
        Guardar();
    }

    private void Guardar() 
    {
        // Itera sobre todos los objetos
        foreach (var entrada in objeto)
        {
            Type tipo = entrada.Key;
            Data data = entrada.Value;

            // Crea un objeto para almacenar las variables
            Almacenado almacenado = new Almacenado();
            foreach (var variable in data.variables)
            {
                // Obtén el valor 
                object valor = variable.Value.GetValue(data.clase);
                // Añade a la lista
                almacenado.valores.Add(new Almacenado.Valores(
                    variable.Key,
                    valor
                ));
            }

            // Serializa la estructura Data a JSON
            string json = JsonUtility.ToJson(almacenado);
            PlayerPrefs.SetString(tipo.ToString(), json);
        }
    }

    // CARGAR LA INFORMACION
    private void Cargar()
    {
        // Itera sobre todos los objetos
        foreach (var entrada in objeto)
        {
            Type tipo = entrada.Key;
            Data data = entrada.Value;

            // Recupera el JSON de PlayerPrefs
            string json = PlayerPrefs.GetString(tipo.ToString());

            if (string.IsNullOrEmpty(json)) return;

            // Deserializa el JSON
            Almacenado almacenados = JsonUtility.FromJson<Almacenado>(json);

            // Itera sobre la lista deserializada
            foreach (var almacenado in almacenados.valores)
            {
                // Busca el FieldInfo correspondiente a la variable por su nombre
                if (data.variables.TryGetValue(almacenado.nombre, out FieldInfo variable))
                {
                    object valor = almacenado.Valor();
                    if (valor == null) continue;
                    variable.SetValue(data.clase, valor);
                }
            }
        }
    }

    // ELIMINAR LA INFORMACION
    [ContextMenu("Eliminar")]
    private void Eliminar()
    {
        PlayerPrefs.DeleteAll();
    }

    // VER EN EL INSPECTOR
    private void UI()
    {

    }

    // GUARDAR ACCION
    [Serializable]
    public class Acciones<D, R>
    {
        [NonSerialized] Func<D, R> funcion;
        [NonSerialized] List<R> resultados = new List<R>();
        List<D> datos = new List<D>();

        public Acciones(Func<D, R> funcion) {
            this.funcion = funcion;
            Load();
        }

        public void Add(D dato) {

            if (funcion == null) Debug.LogWarning("Añade una funcion para poder gurdar la accion");

            R resultado = funcion.Invoke(dato);
            datos.Add(dato);
            resultados.Add(resultado);
        }

        public R Get(int index) {
            return resultados[index];
        }

        public void Load() {
            Debug.Log(datos.Count);
            foreach (var dato in datos)
            {
                R resultado = funcion.Invoke(dato);
                resultados.Add(resultado);
            }
        }
    }
}