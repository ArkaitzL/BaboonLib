using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBA2 : Save
{
    [SerializeField] string nombre;
    [SerializeField] SUBPRUEBA subprueba;

    [Serializable]
    public class SUBPRUEBA 
    {
        public bool activo;
    }
}
