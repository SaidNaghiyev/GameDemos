using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;
    [Header("UI")]
    public GameObject winUI;
    [Header("Misc")]
    public FollowLine[] Players;
    private Vector3[] pos;
    private Vector3[] rot;
    private void Awake()
    {
        instance = this;
        pos = new Vector3[Players.Length];
        rot = new Vector3[Players.Length];
        savePositions();
    }
    void savePositions()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            pos[i] = Players[i].transform.position;
            rot[i] = Players[i].transform.eulerAngles;
        }
    }
    public void ResetAll()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].transform.localScale = Vector3.one;
            Players[i].transform.position = pos[i];
            Players[i].transform.eulerAngles = rot[i];
        }
    }
    public void followLine(List<Vector3> list)
    {
        float total = 0;
        float factor = 0.3f;

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].moveTowardsPath(list, total);
            total = total + factor;
        }

    }

    public void winLevel()
    {
        winUI.SetActive(true);
    }

    public void changeLevel(int _id)
    {
        SceneManager.LoadScene(_id);
    }
}
