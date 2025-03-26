using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private GameObject _winTextBackground;

    public UnityEvent OnGameWin;

    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;
    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;
    private Rigidbody _player1Rigidbody;
    private Rigidbody _player2Rigidbody;

    private bool _gameWon = false;

    private void Start()
    {
        if (_player1)
        {
            if (!_player1.TryGetComponent(out _player1Timer))
                Debug.LogError("GameManager: Could not get Player1 Timer System!");
            if (!_player1.TryGetComponent(out _player1TagSystem))
                Debug.LogError("GameManager: Could not get Player1 Tag System!");
            if (!_player1.TryGetComponent(out _player1Controller))
                Debug.LogError("GameManager: Could not get Player1 Player Controller!");            
            if (!_player1.TryGetComponent(out _player1Rigidbody))
                Debug.LogError("GameManager: Could not get Player1 Rigidbody!");
        }
        else
            Debug.LogError("GameManager: Player1 not assigned!");
        if (_player2)
        {
            if (!_player2.TryGetComponent(out _player2Timer))
                Debug.LogError("GameManager: Could not get Player2 Timer System!");
            if (!_player2.TryGetComponent(out _player2TagSystem))
                Debug.LogError("GameManager: Could not get Player2 Tag System!");
            if (!_player2.TryGetComponent(out _player2Controller))
                Debug.LogError("GameManager: Could not get Player2 Player Controller!");
            if (!_player2.TryGetComponent(out _player2Rigidbody))
                Debug.LogError("GameManager: Could not get Player2 Rigidbody!");
        }
        else
            Debug.LogError("GameManager: Player2 not assigned!");

        if (!_winTextBackground)
            Debug.LogWarning("GameManager: Win Text Background not assigned!");
    }

    private void Update()
    {
        // if either timer not assigned, do nothing
        if (!(_player1Timer && _player2Timer))
            return;

        // if game is already won, do nothing
        if (_gameWon)
            return;

        // check if either timer is finished, and win the game if so
        if (_player1Timer.TimeRemaining <= 0.0f)
            Win("Player 1 wins!");
        else if (_player2Timer.TimeRemaining <= 0.0f)
            Win("Player 2 wins!");

    }

    private void Win(string winText)
    {
        // Enable win screen ui and set text to win text
        if (_winTextBackground)
        {
            _winTextBackground.SetActive(true);

            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);
            if (text)
            {
                text.text = winText;
            }
        }

        // turn off player controllers & tag system & timers
        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1TagSystem)
            _player1TagSystem.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        if (_player2Timer)
            _player2Timer.enabled = false;
        if (_player2TagSystem)
            _player2TagSystem.enabled = false;
        if (_player2Controller)
            _player2Controller.enabled = false;
        if (_player2Rigidbody)
            _player2Rigidbody.isKinematic = true;

        OnGameWin.Invoke();
        _gameWon = true;
    }

    public void ResetGame()
    {
        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
