using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebas2 : BaboonLib
{
    [Save] public string texto;
    [Save] public float flotante;
    [Save] public double doble;
    [Save] public List<string> lista;
    [Save] public int[] array;

    public void Accion()
    {
        texto = "adios";
        flotante = 6.7f;
        doble = 787.2;
        lista = new List<string>() { "hola", "adios" };
        array = new int[3] { 1, 2, 3 };
    }

}
