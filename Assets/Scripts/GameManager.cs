using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int puntos;
    public TMP_Text valorpuntos;
    private int vida;
    public TMP_Text valorvida;
    void Start()
    {
        puntos = 0;
        vida = 3;
        valorvida.text = "3";
        valorpuntos.text = "0";
    }

    public void restavidas()
    {
        vida--;
        valorvida.text = vida.ToString();
    }
    public void sumamonedas()
    {
        puntos++;
        valorpuntos.text = puntos.ToString();
    }
}
