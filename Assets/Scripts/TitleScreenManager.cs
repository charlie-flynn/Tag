using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;


public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button[] _sceneButtons;

    [SerializeField]
    private Button[] _otherButtons;



    private void Start()
    {
        // add the load scene function as a listener to each button, then parent it to the scene window
        for (int i = 0; i < _sceneButtons.Length; i++)
        {
            _sceneButtons[i].onClick.AddListener(delegate { SceneManager.LoadScene(i); });
            _sceneButtons[i].enabled = false;

            if (_sceneButtons[i].TryGetComponent(out Image image))
                image.enabled = false;

            _sceneButtons[i].GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    public void SwitchToSceneSelection()
    {
        for (int i = 0; i < _sceneButtons.Length; i++)
        {
            bool value = _sceneButtons[i].enabled ? false : true;

            _sceneButtons[i].enabled = value;

            if (_sceneButtons[i].TryGetComponent(out Image image))
                image.enabled = value;

            _sceneButtons[i].GetComponent<TextMeshProUGUI>().enabled = value;
        }
        for (int i = 0; i < _otherButtons.Length; i++)
        {
            bool value = _otherButtons[i].enabled ? false : true;

            _otherButtons[i].enabled = value;

            if (_otherButtons[i].TryGetComponent(out Image image))
                image.enabled = value;

            _otherButtons[i].GetComponent<TextMeshProUGUI>().enabled = value;
        }
    }
}
