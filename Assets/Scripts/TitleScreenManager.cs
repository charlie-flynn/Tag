using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button[] _sceneButtons;

    [SerializeField]
    private Button[] _otherButtons;



    private void Start()
    {
        // disable the scene buttons
        for (int i = 0; i < _sceneButtons.Length; i++)
        {
            _sceneButtons[i].enabled = false;

            if (_sceneButtons[i].TryGetComponent(out Image image))
                image.enabled = false;

            _sceneButtons[i].GetComponentInChildren<TextMeshProUGUI>(true).enabled = false;
        }
    }

    public void SwitchToSceneSelection()
    {
        // swap the enabled and disabled values for the two buttons
        for (int i = 0; i < _sceneButtons.Length; i++)
        {
            bool value = _sceneButtons[i].enabled ? false : true;

            _sceneButtons[i].enabled = value;

            if (_sceneButtons[i].TryGetComponent(out Image image))
                image.enabled = value;

            _sceneButtons[i].GetComponentInChildren<TextMeshProUGUI>(true).enabled = value;
        }
        for (int i = 0; i < _otherButtons.Length; i++)
        {
            bool value = _otherButtons[i].enabled ? false : true;

            _otherButtons[i].enabled = value;

            if (_otherButtons[i].TryGetComponent(out Image image))
                image.enabled = value;

            _otherButtons[i].GetComponentInChildren<TextMeshProUGUI>(true).enabled = value;
        }
    }

    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
