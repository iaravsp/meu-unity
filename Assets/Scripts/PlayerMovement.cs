using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    AudioSource meuAudioSource;
    public AudioClip audioMorte;

    public float speed = 5f;
    public float jumpForce = 10f;

    private bool isGrounded;
    private bool jumpRequest;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        meuAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Capturamos o input no Update (pois ele roda todo frame de tela)
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        // 1. Movimento Horizontal usando velocidade
        float moveHorizontal = Input.GetAxis("Horizontal");
        // Pega a escala atual exata que você configurou lá no Inspector
        Vector3 escalaAtual = transform.localScale;

        if (moveHorizontal > 0)
        {
            // Mathf.Abs garante que o número seja positivo (olhando para a direita)
            escalaAtual.x = Mathf.Abs(escalaAtual.x);
        }
        else if (moveHorizontal < 0)
        {
            // O sinal de menos garante que fique negativo (olhando para a esquerda)
            escalaAtual.x = -Mathf.Abs(escalaAtual.x);
        }

        // Devolve a escala com o tamanho original, mas virada para o lado certo
        transform.localScale = escalaAtual;

        rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);

        // 2. Verificação do Chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 3. Atualização das Animações
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsGrounded", isGrounded);

        // 4. Lógica de Pulo
        if (jumpRequest)
        {
            if (isGrounded)
            {
                // Aplica a força do pulo preservando o movimento horizontal
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            // Independentemente de ter conseguido pular ou não, limpamos o pedido
            // Isso evita que o botão fique "preso" e ele pule sozinho ao pousar
            jumpRequest = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coletável")
        {
            meuAudioSource.Play();
            GameController.Collect();
            Destroy(other.gameObject);
        }
        else if (other.tag == "abismo")
        {
            meuAudioSource.PlayOneShot(audioMorte);
            GameController.MorteJogador();
        }
    }
    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Se o corpo em que batemos tem a tag Inimigo...
        if (colisao.gameObject.tag == "inimigo")
        {
            meuAudioSource.PlayOneShot(audioMorte);
            GameController.MorteJogador();
        }
    }

}