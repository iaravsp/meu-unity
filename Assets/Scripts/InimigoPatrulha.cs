using UnityEngine;

public class InimigoPatrulha : MonoBehaviour
{
    public float velocidade = 2f;

    // O Tronco original é desenhado olhando para a ESQUERDA, então ele começa com 'false'
    private bool movendoDireita = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move o inimigo. Se 'movendoDireita' for true, velocidade é positiva. Senão, negativa.
        rb.linearVelocity = new Vector2(movendoDireita ? velocidade : -velocidade, rb.linearVelocity.y);
    }

    // Se ele bater num objeto com a tag "Borda", ele vira de lado
    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.tag == "borda")
        {
            Virar();
        }
    }

    void Virar()
    {
        movendoDireita = !movendoDireita;
        Vector3 escalaAtual = transform.localScale;

        // Garante que a escala não fique invertendo loucamente se bater duas vezes
        if (movendoDireita)
        {
            escalaAtual.x = -Mathf.Abs(escalaAtual.x); // O Tronco original olha pra esquerda, então direita é negativo
        }
        else
        {
            escalaAtual.x = Mathf.Abs(escalaAtual.x);  // Esquerda é positivo
        }

        transform.localScale = escalaAtual;
    }
}