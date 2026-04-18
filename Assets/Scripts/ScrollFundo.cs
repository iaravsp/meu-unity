using UnityEngine;

public class ScrollFundo : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade;
    
    private float largura;
    private Vector2 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
        
        // Pega a largura exata da imagem para saber quando teleportar
        largura = GetComponent<SpriteRenderer>().bounds.size.x;

        // Cria uma cópia da imagem e coloca exatamente ao lado direito dela
        // Isso evita que fique um buraco preto na tela quando a imagem se mover
        GameObject clone = Instantiate(gameObject);
        Destroy(clone.GetComponent<ScrollFundo>()); // Impede que o clone crie clones infinitos!
        clone.transform.parent = this.transform;
        clone.transform.position = new Vector2(posicaoInicial.x + largura, posicaoInicial.y);
    }

    void Update()
    {
        // Move o fundo constantemente para a esquerda
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        // Quando a imagem original sai totalmente da tela, ela "teleporta" de volta para o começo
        if (transform.position.x <= posicaoInicial.x - largura)
        {
            transform.position = posicaoInicial;
        }
    }
}