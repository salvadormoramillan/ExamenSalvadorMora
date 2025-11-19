using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int puntos;
    public TMP_Text valorpuntos;
    
    void Start()
    {
        puntos = 0;
        valorpuntos.text = "0";
    }

   
    public void sumamonedas()
    {
        puntos++;
        valorpuntos.text = puntos.ToString();
    }
}
