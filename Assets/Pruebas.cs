using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebas : BaboonLib
{
    [Save] public string texto;
    [Save] public bool booleano;
    [Save] public int numero;
    [Save] public Vector2 vector;
    [Save] public Otro otro;

    public void Accion() {
        texto = "hola";
        booleano = true;
        numero = 87;
        vector = Vector2.up;
        otro = new Otro(Vector2.down);
    }


    [Serializable]
    public class Otro {
        public Vector2 vector;

        public Otro(Vector2 vector)
        {
            this.vector = vector;
        }
    }
}
