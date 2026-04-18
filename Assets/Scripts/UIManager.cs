using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject painelDerrota; // A tela de "Você Morreu"
    public GameObject painelVitoria; // A tela de "Fase Concluída"

    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoFrutas; // Onde vai aparecer as frutas faltando
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (painelDerrota != null) painelDerrota.SetActive(false);
        if (painelVitoria != null) painelVitoria.SetActive(false);

        // É vital chamar o Init() aqui quando a fase carrega para resetar o tempo
        GameController.Init();

    }

    void Update() // Troquei de FixedUpdate para Update, pois UI não envolve física
    {
        // 1. Faz o relógio rodar nos bastidores enviando o tempo do frame
        GameController.AtualizarTempo(Time.deltaTime);

        // 2. Escreve os números na tela
        if (textoTempo != null)
        {
            // Mathf.Ceil arredonda o tempo (ex: 14.8 vira 15) para não mostrar casas decimais feias
            textoTempo.text = "Tempo: " + Mathf.Ceil(GameController.tempoRestante).ToString() + "s";
        }
        if (textoFrutas != null)
        {
            textoFrutas.text = "Faltam: " + GameController.collectableCount;
        }
        // Se o gerente disser que perdeu, liga a tela vermelha
        if (GameController.IsDefeat)
        {
            painelDerrota.SetActive(true);
        }
        // Se o gerente disser que ganhou, liga a tela verde/dourada
        else if (GameController.IsVictory)
        {
            painelVitoria.SetActive(true);
        }
    }
}
