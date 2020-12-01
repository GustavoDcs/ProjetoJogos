using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    [SerializeField]
    Button startButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(HandlerStartButtonClicked);
        GameState.lives = 3;
        GameState.time = 1;
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(HandlerStartButtonClicked);
    }

    void HandlerStartButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
