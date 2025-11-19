using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void reiniciar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Salir() {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
