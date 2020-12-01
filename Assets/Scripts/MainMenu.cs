using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button startGameButton;

    [SerializeField]
    Button settingsButton;

    [SerializeField]
    Button controlsButton;

    [SerializeField]
    Button testButton;

    GameObject loadingScreen;
    LoadingScreen loadingScreenComponent;

    void HandleStartGameButtonClicked()
    {
        SceneManager.LoadSceneAsync("SelectCharacter");
    }

    void HandleControlsButtonClicked()
    {
        SceneManager.LoadSceneAsync("Controls");
    }

    void HandleSettingsButtonClicked()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    void HandleTestButtonClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }


    private void OnEnable()
    {
        startGameButton.onClick.AddListener(HandleStartGameButtonClicked);
        settingsButton.onClick.AddListener(HandleSettingsButtonClicked);
        controlsButton.onClick.AddListener(HandleControlsButtonClicked);
        testButton.onClick.AddListener(HandleTestButtonClicked);
        loadingScreen = GameObject.Find("LoadingCanvas");

        if(loadingScreen != null)
        {
            loadingScreenComponent = loadingScreen.GetComponent<LoadingScreen>();
        }

        if(loadingScreenComponent != null)
        {
            loadingScreenComponent.HideSpinner();
        }


    }
    void OnDisable()
    {
        startGameButton.onClick.RemoveListener(HandleStartGameButtonClicked);
        settingsButton.onClick.RemoveListener(HandleSettingsButtonClicked);
        controlsButton.onClick.RemoveListener(HandleControlsButtonClicked);
        testButton.onClick.RemoveListener(HandleTestButtonClicked);
    }
}
