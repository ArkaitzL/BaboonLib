using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBA2 : MonoBehaviour
{
    //[SerializeField] SUBPRUEBA subprueba1, subprueba2;

    [Serializable]
    public class SUBPRUEBA : SaveScript
    {
        public bool activo;
        public string nombre;
    }
}
