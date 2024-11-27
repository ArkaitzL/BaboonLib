using UnityEngine;

//[CreateAssetMenu(fileName = "Ajustes[Nuevo]", menuName = "AjustesCarpetas", order = 1)]
public class AjustesCarpetas : ScriptableObject
{
    // Variable que hace referencia a la instancia de ListaCarpetas
    [SerializeField] private ListaCarpetas listaCarpetas;

    // Cambia el valor en el Inspector
    private void OnValidate()
    {
        // Asignar el contenido de listaCarpetas
        if (listaCarpetas == null) return;
        Carpetas.listaCarpetas = listaCarpetas;
    }
    // Recargar unity
    // ** PENDIENTE **

}

