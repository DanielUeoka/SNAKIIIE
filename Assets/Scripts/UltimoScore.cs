using UnityEngine;
using TMPro;

public class UltimoScore : MonoBehaviour
{
    public TMP_Text textoScore;

    
    public void AtualizarScore()
    {
        int ultimoScore = PlayerPrefs.GetInt("lastscore", 0);
        textoScore.text = ultimoScore.ToString();
    }
}