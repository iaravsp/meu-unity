using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject painelDerrota; // A tela de "Você Morreu"
    public GameObject painelVitoria; // A tela de "Fase Concluída"

    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoFrutas; // Onde vai aparecer as frutas faltando

    // ---> NOVAS VARIÁVEIS AQUI <---
    public TextMeshProUGUI textoTempoFinalDerrota;
    public TextMeshProUGUI textoTempoFinalVitoria;

    void Start()
    {
        if (painelDerrota != null) painelDerrota.SetActive(false);
        if (painelVitoria != null) painelVitoria.SetActive(false);

        // É vital chamar o Init() aqui quando a fase carrega para resetar o tempo
        GameController.Init();
    }

    void Update()
    {
        // 1. Faz o relógio rodar nos bastidores enviando o tempo do frame
        GameController.AtualizarTempo(Time.deltaTime);

        // 2. Escreve os números na tela (HUD)
        if (textoTempo != null)
        {
            textoTempo.text = "Tempo: " + Mathf.Ceil(GameController.tempoRestante).ToString() + "s";
        }
        if (textoFrutas != null)
        {
            textoFrutas.text = "Faltam: " + GameController.collectableCount + " frutas";
        }

        // 3. Verifica fim de jogo e liga as telas com o tempo final
        if (GameController.IsDefeat)
        {
            painelDerrota.SetActive(true);

            // Cola o tempo congelado no texto do painel
            if (textoTempoFinalDerrota != null)
            {
                textoTempoFinalDerrota.text = "Tempo Final: " + Mathf.Ceil(GameController.tempoRestante).ToString() + "s";
            }
        }
        else if (GameController.IsVictory)
        {
            painelVitoria.SetActive(true);

            // Cola o tempo congelado no texto do painel
            if (textoTempoFinalVitoria != null)
            {
                textoTempoFinalVitoria.text = "Tempo Restante: " + Mathf.Ceil(GameController.tempoRestante).ToString() + "s";
            }
        }
    }
}