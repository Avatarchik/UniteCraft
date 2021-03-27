using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsOnclicked : MonoBehaviour
{
    private GameObject _startbtn;
    private GameObject load_text;
    public string changeSceneName;

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        load_text = GameObject.Find("LoadingText");
        _startbtn = GameObject.Find("Startbtn");
    }

    // Update is called once per frame
    public void Update()
    {
        _startbtn.GetComponent<Button>().onClick.AddListener(StartLoading);
    }

    private void StartLoading()
    {
        _startbtn.GetComponent<CanvasGroup>().alpha = 0;
        load_text.GetComponent<CanvasGroup>().alpha = 1;
        SceneManager.LoadScene(changeSceneName);
    }
}