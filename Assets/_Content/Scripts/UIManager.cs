using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject finishPanel;

    [SerializeField] private TextMeshProUGUI fromSlider;
    [SerializeField] private TextMeshProUGUI toSlider;

    private void Start()
    {
        InitLevel();
    }

    void InitLevel()
    {
        if (PlayerPrefs.GetInt("Level", -1) != -1)
        {
            fromSlider.text = PlayerPrefs.GetInt("Level").ToString();
            toSlider.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        }
        else
        {
            fromSlider.text = "0";
            toSlider.text = "1";
        }
    }

    public void OnFinish()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        
        finishPanel.SetActive(true);
    }

    public void OnReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
