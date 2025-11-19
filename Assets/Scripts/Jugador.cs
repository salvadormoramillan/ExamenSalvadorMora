using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    [Header("---MOVIMIENTO---")]
    public float velocidad = 5f;
    private float movimientox;
    private Rigidbody2D rb2d;

    [Header("---SALTO---")]
    public float fuerzasalto = 5f;
    private bool ensuelo = false;

    [Header("---COMPRUEBASUELO---")]
    public Transform Compruebasuelo;
    public float radiosuelo = 0.1f;
    public LayerMask capasuelo;

    [Header("---VIDA---")]
    public TMP_Text valorvida;
    private int vida;

    [Header("---AUDIO---")]
    public AudioSource audioSource;
    public AudioClip musicamario;
    public AudioClip SonidoMoneda;
    

    //animacion
    private Animator animator;

    //posicionInicial
    private Vector3 posicionInicial;
    void Start()
    {
        vida = 3;
        valorvida.text = "3";

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posicionInicial = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicamario;
        audioSource.loop = true;
        audioSource.volume = 0.5f;  
        audioSource.Play();

    }

    void Update()
    {
        rb2d.velocity = new Vector2(movimientox * velocidad, rb2d.velocity.y);
        if (Compruebasuelo != null) {
            ensuelo = Physics2D.OverlapCircle(Compruebasuelo.position, radiosuelo, capasuelo);
        }
        if(movimientox == 0)
        {
            animator.SetBool("estacorriendo", false);
        }
    }

    private void OnMove(InputValue inputmovimiento){

        movimientox = inputmovimiento.Get<Vector2>().x;

        if(movimientox != 0)
        {
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Sign(movimientox) * Mathf.Abs(escala.x);
            transform.localScale = escala;
            animator.SetBool("estacorriendo", true);
        }
    }

    private void OnJump(InputValue inputsalto) {
        if (ensuelo)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, fuerzasalto);
        }
    }

    public void restavidas()
    {
        vida--;
        valorvida.text = vida.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Moneda"))
        {
            FindAnyObjectByType<GameManager>().sumamonedas();
            audioSource.PlayOneShot(SonidoMoneda);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("SueloMuerte"))
        {
            transform.position = posicionInicial;
            rb2d.velocity = Vector2.zero;
            restavidas();
            if(vida == 0)
            {
                SceneManager.LoadScene("Derrota");
            }
            
        }
        if (collision.CompareTag("Castillo"))
        {
            SceneManager.LoadScene("Victoria");
        }
        if (collision.CompareTag("Enemigo"))
        {
            transform.position = posicionInicial;
            rb2d.velocity = Vector2.zero;
            restavidas();
            if (vida == 0)
            {
                SceneManager.LoadScene("Derrota");
            }
        }
    }

}
