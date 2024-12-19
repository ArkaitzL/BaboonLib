using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class SaveAttribute : Attribute { }

[DefaultExecutionOrder(-99)]
public class Save : MonoBehaviour
{
    private Dictionary<Type, Data> objeto = new Dictionary<Type, Data>();

    [Serializable]
    public class Data
    {
        public object clase;
        public Dictionary<string, FieldInfo> variables;
    }

    [Serializable]
    public class Almacenado
    {
        public List<Valores> valores = new List<Valores>();

        [Serializable]
        public class Valores
        {
            public string nombre;
            public string valor;
            public string tipo;

            public Valores(string nombre, object valor)
            {
                this.nombre = nombre;
                tipo = valor.GetType().AssemblyQualifiedName;

                if (valor is string || valor.GetType().IsPrimitive)
                {
                    this.valor = valor.ToString();
                }
                else
                {
                    this.valor = JsonUtility.ToJson(valor);
                }
            }

            public object Valor()
            {
                Type tipo_valor = Type.GetType(tipo);
                if (tipo_valor == null)
                {
                    Debug.LogWarning($"No se pudo encontrar el tipo: {tipo}");
                    return null;
                }

                if (tipo_valor.IsPrimitive || tipo_valor == typeof(string))
                {
                    return Convert.ChangeType(valor, tipo_valor);
                }
                else
                {
                    return JsonUtility.FromJson(valor, tipo_valor);
                }
            }
        }
    }

    private void Awake()
    {
        MonoBehaviour[] objetos = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in objetos)
        {
            Type tipo = obj.GetType();
            FieldInfo[] variables = tipo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (objeto.ContainsKey(tipo))
            {
                Debug.LogWarning($"[BL-SAVE] Se han encontrado dos clases del tipo {tipo}");
                return;
            }

            Dictionary<string, FieldInfo> variables_atributo = new Dictionary<string, FieldInfo>();
            foreach (var variable in variables)
            {
                var atributo = variable.GetCustomAttribute<SaveAttribute>();
                if (atributo == null) continue;

                variables_atributo.Add(variable.Name, variable);
            }

            objeto.Add(tipo, new Data
            {
                clase = obj,
                variables = variables_atributo
            });
        }

        Cargar();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) Guardar();
    }

    private void OnApplicationQuit()
    {
        Guardar();
    }

    private void Guardar()
    {
        foreach (var entrada in objeto)
        {
            Type tipo = entrada.Key;
            Data data = entrada.Value;

            Almacenado almacenado = new Almacenado();
            foreach (var variable in data.variables)
            {
                object valor = variable.Value.GetValue(data.clase);
                almacenado.valores.Add(new Almacenado.Valores(variable.Key, valor));
            }

            string json = JsonUtility.ToJson(almacenado);
            PlayerPrefs.SetString(tipo.ToString(), json);
        }
    }

    private void Cargar()
    {
        foreach (var entrada in objeto)
        {
            Type tipo = entrada.Key;
            Data data = entrada.Value;

            string json = PlayerPrefs.GetString(tipo.ToString());

            if (string.IsNullOrEmpty(json)) return;

            Almacenado almacenados = JsonUtility.FromJson<Almacenado>(json);

            foreach (var almacenado in almacenados.valores)
            {
                if (data.variables.TryGetValue(almacenado.nombre, out FieldInfo variable))
                {
                    object valor = almacenado.Valor();
                    if (valor != null)
                        variable.SetValue(data.clase, valor);
                }
            }
        }
    }

    [ContextMenu("Eliminar")]
    private void Eliminar()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Se eliminaron todos los datos.");
    }
}
