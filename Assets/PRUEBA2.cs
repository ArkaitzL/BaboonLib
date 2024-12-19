using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBA2 : MonoBehaviour
{
    //[SerializeField] SUBPRUEBA subprueba1, subprueba2;

    [Serializable]
    public class SUBPRUEBA : Save
    {
        public bool activo;
        public string nombre;
    }
}
