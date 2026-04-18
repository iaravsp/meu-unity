using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController
{
    public static int collectableCount;
    private static bool playerMorreu;
    public static float tempoRestante;

    public static bool IsVictory
    {
        get { return collectableCount <= 0 && !playerMorreu; }
    }
    public static bool IsDefeat
    {
        get { return playerMorreu || tempoRestante <= 0; }
    }


    public static void Init()
    {
        collectableCount = 4;
        playerMorreu = false;
        tempoRestante = 15f;
    }

    public static void Collect()
    {
        collectableCount--;
    }
    public static void MorteJogador()
    {
        playerMorreu = true;
    }

    // O UIManager vai chamar esta função todo frame para fazer o relógio tictac
    public static void AtualizarTempo(float delta)
    {
        // O tempo só cai se o jogador ainda estiver jogando (nem ganhou, nem perdeu)
        if (!IsVictory && !IsDefeat)
        {
            tempoRestante -= delta;
        }
    }

}
