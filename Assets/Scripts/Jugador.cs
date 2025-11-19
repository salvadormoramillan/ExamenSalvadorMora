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

    //animacion
    private Animator animator;

    //posicionInicial
    private Vector3 posicionInicial;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posicionInicial = transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Moneda"))
        {
            FindAnyObjectByType<GameManager>().sumamonedas();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("SueloMuerte"))
        {
            transform.position = posicionInicial;
            rb2d.velocity = Vector2.zero;

            var game = FindAnyObjectByType<GameManager>();
            if (game != null)
            {
                game.restavidas();
            }
            else {
                SceneManager.LoadScene("Derrota");
            }
        }
    }

}
