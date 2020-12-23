using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [Header("SpawnSetting")]
    public GameObject[] obs;// Obstacles

    public float timeBetweenSpawns = 1.5f, reduceTimeBy = 0.05f, minTimeBetweenSpawns = 0.6f;
    
    public Transform spawnPos;

    public float obsSpeed = 1000f;

    [Header("UI Control")]
    public List<Transform> cameraPos = new List<Transform>();
    public Transform cameraObj;
    public TextMeshProUGUI txtScore;

    [Header("Misc")]

    public int score = 0;

    private int _camID = 0;


    private int bestScore = 0;
    private int lastScore = 0;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("Record", 0);
        lastScore = PlayerPrefs.GetInt("currentScore", 0);
        if (cameraObj != null)
        {
            _camID = PlayerPrefs.GetInt("CameraID", 0);
            changeCamera(0);
        }
        Spawn();
    }


    public void Spawn()
    {
        if (timeBetweenSpawns - reduceTimeBy >= minTimeBetweenSpawns)
        {
            timeBetweenSpawns -= reduceTimeBy;//increase difficulties every specific time.
        }

        Instantiate(obs[Random.Range(0, obs.Length)], spawnPos.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.forward * -obsSpeed); // Spawn Obstacles

        
        Invoke("Spawn", timeBetweenSpawns);
    }


    public void Die()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Record", bestScore);
        }
        PlayerPrefs.SetInt("currentScore", score);
        SceneManager.LoadScene(0);
    }

    public void addScore(int _x) // increase or decrease score
    {
        score += _x;
        txtScore.text = score.ToString();

        if (score == -1)
        {
            SceneManager.LoadScene(0);
        }
    }

    #region UI
    public void changeCamera(int _x) //Change camera angles
    {
        _camID+=_x;
        if (_camID >= cameraPos.Count)
        {
            _camID = 0;
        }
        cameraObj.position = cameraPos[_camID].position;
        cameraObj.rotation = cameraPos[_camID].rotation;
        PlayerPrefs.SetInt("CameraID", _camID);
    }

    #endregion
}
