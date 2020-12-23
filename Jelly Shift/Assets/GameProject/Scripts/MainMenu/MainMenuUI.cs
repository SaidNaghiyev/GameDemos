using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText, lastScoreText;

    private void Start()
    {
        bestScoreText.text = PlayerPrefs.GetInt("Record", 0).ToString();
        lastScoreText.text = PlayerPrefs.GetInt("currentScore", 0).ToString();
    }

    public void pressStart()
    {
        SceneManager.LoadScene(1);
    }

    public void resetPrefabs()
    {
        PlayerPrefs.DeleteAll();
        Start();
    }
}
