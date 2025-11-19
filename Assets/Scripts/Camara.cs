using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform jugador;
    private Vector3 offsett;
    public float suavizado = 0.1f;
    void Start()
    {
        offsett = transform.position - jugador.position;
    }

    void Update()
    {
        if (jugador != null) { 
            Vector3 nuevapos = jugador.position + offsett;
            transform.position = Vector3.Lerp(transform.position, nuevapos,suavizado);
        }        
    }
}
