using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionController : MonoBehaviour
{
    [SerializeField]
    Texture2D cursorImage;

    [SerializeField]
    Texture2D cursorGrabImage;

    [SerializeField]
    Texture2D cursorPointImage;

    [SerializeField]
    IndicatorController p1IndicatorController;

    [SerializeField]
    IndicatorController p2IndicatorController;

    [SerializeField]
    List<CharacterImage> characterImagesList;

    bool isGrabbingP1Indicator;
    bool isGrabbingP2Indicator;


    [SerializeField]
    Image p1Image;

    [SerializeField]
    Image p2Image;

    [SerializeField]
    Sprite iblisSprite;

    [SerializeField]
    Sprite shaunaSprite;

    [SerializeField]
    GameObject scrim;

    [SerializeField]
    Button startGameButton;

    GameState.Character tempCharacter;

    LoadingScreen loadingScreen;

    bool buttonSet = false;

    void Awake()
    {
        loadingScreen = GameObject.Find("LoadingCanvas").GetComponent<LoadingScreen>();

        if (loadingScreen != null)
        {
            loadingScreen.HideSpinner();
        }

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);

        p1Image.color = new Color(0, 0, 0, 0);
        p2Image.color = new Color(0, 0, 0, 0);

    }

    void OnEnable()
    {
        p1IndicatorController.IndicatorWasClicked += HandleUserClickedIndicatorP1;
        p2IndicatorController.IndicatorWasClicked += HandleUserClickedIndicatorP2;

        foreach (CharacterImage characterImage in characterImagesList)
        {
            characterImage.IndicatorOverCharacter += HandleIndicatorOverCharacter;
        }
    }

    void OnDisable()
    {
        p1IndicatorController.IndicatorWasClicked -= HandleUserClickedIndicatorP1;
        p2IndicatorController.IndicatorWasClicked -= HandleUserClickedIndicatorP2;

        foreach (CharacterImage characterImage in characterImagesList)
        {
            characterImage.IndicatorOverCharacter -= HandleIndicatorOverCharacter;
        }
    }

    void Update()
    {
        if (p1Image.color.a != 0 && p2Image.color.a != 0 && !isGrabbingP1Indicator && !isGrabbingP2Indicator)
        {
            scrim.SetActive(true);
            Cursor.SetCursor(cursorPointImage, Vector2.zero, CursorMode.Auto);
            if (!buttonSet)
            {
                startGameButton.onClick.AddListener(HandleStartButtonClicked);
                buttonSet = true;
            }
        }
    }

    void HandleUserClickedIndicatorP1(bool isGrabbed)
    {
        HandleUserClickedIndicator(isGrabbed);

        if (isGrabbed)
        {
            isGrabbingP1Indicator = true;
            isGrabbingP2Indicator = false;
        }
        else
        {
            isGrabbingP1Indicator = false;
            isGrabbingP2Indicator = false;
        }
    }

    void HandleUserClickedIndicatorP2(bool isGrabbed)
    {
        HandleUserClickedIndicator(isGrabbed);

        if (isGrabbed)
        {
            isGrabbingP2Indicator = true;
            isGrabbingP1Indicator = false;
        }
        else
        {
            isGrabbingP1Indicator = false;
            isGrabbingP2Indicator = false;

        }
    }


    void HandleUserClickedIndicator(bool isGrabbed)
    {
        if (isGrabbed)
        {
            Cursor.SetCursor(cursorGrabImage, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        }
    }

    void HandleIndicatorOverCharacter(string name)
    {
        switch (name)
        {
            case "Iblis":
                SetImage(iblisSprite);
                tempCharacter = GameState.Character.Iblis;
                break;
            case "Shauna":
                SetImage(shaunaSprite);
                tempCharacter = GameState.Character.Shauna;
                break;
            case "Reset":
                SetImage(null);
                tempCharacter = GameState.Character.None;
                break;
        }

        if (isGrabbingP1Indicator)
        {
            GameState.P1_Character = tempCharacter;
        }
        else if (isGrabbingP2Indicator)
        {
            GameState.P2_Character = tempCharacter;
        }
    }

    void SetImage(Sprite image)
    {
        if (isGrabbingP1Indicator)
        {
            p1Image.sprite = image;

            if (image == null)
            {
                p1Image.color = new Color(0, 0, 0, 0);
            }
            else
            {
                p1Image.color = new Color(255, 255, 255, 255);
            }
        }
        else if (isGrabbingP2Indicator)
        {
            p2Image.sprite = image;

            if (image == null)
            {
                p2Image.color = new Color(0, 0, 0, 0);
            }
            else
            {
                p2Image.color = new Color(255, 255, 255, 255);
            }
        }
    }

    void HandleStartButtonClicked()
    {
        if (loadingScreen != null)
        {
            loadingScreen.ShowSpinner();
        }

        SceneManager.LoadSceneAsync("SelectStage");
    }
}

