using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LivesController : MonoBehaviour
{
    int p1Lives;
    int p2Lives;

    [SerializeField]
    List<GameObject> p1LivesList;

    [SerializeField]
    List<GameObject> p2LivesList;
    [SerializeField]
    Sprite iblis;

    [SerializeField]
    Sprite shauna;

    [SerializeField]
    GameObject iblisObject;

    [SerializeField]
    GameObject shaunaObject;

    [SerializeField]
    OutOfBoundsController outOfBoundsController1;

    [SerializeField]
    OutOfBoundsController outOfBoundsController2;

    [SerializeField]
    OutOfBoundsController outOfBoundsController3;

    [SerializeField]
    OutOfBoundsController outOfBoundsController4;


    public Action<GameObject> RemoveCharacterFromAIList;
    public Action<GameObject> RespawnCharacter;
    public Action<int> RemoveDamageTextAtIndex;

    void Start()
    {
        p1Lives = GameState.lives;
        p2Lives = GameState.lives;



        InitializeLivesForCharacter(p1Lives, p1LivesList, GameState.P1_Character);
        InitializeLivesForCharacter(p2Lives, p2LivesList, GameState.P2_Character);
    }

    void OnEnable()
    {
        outOfBoundsController1.RemoveLife += HandleRemoveLife;
        outOfBoundsController2.RemoveLife += HandleRemoveLife;
    }

    void OnDisable()
    {
        outOfBoundsController1.RemoveLife -= HandleRemoveLife;
        outOfBoundsController2.RemoveLife -= HandleRemoveLife;
    }

    void Update()
    {
        if (GameState.winners.Count == 1)
        {
            SceneManager.LoadSceneAsync("CelebrationScreen");
        }
    }

    void InitializeLivesForCharacter(int lives, List<GameObject> characterLives, GameState.Character character)
    {
        foreach (GameObject go in characterLives)
        {
            if (character == GameState.Character.Iblis)
            {
                SetSprite(go, shauna);
            }
            else if (character == GameState.Character.Shauna)
            {
                SetSprite(go, iblis);
            }

            go.SetActive(false);
        }

        for (int i = 0; i < lives; i++)
        {
            characterLives[i].SetActive(true);
        }
    }

    void SetSprite(GameObject go, Sprite sprite)
    {
        Image img = go.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = sprite;
        }
    }

    void HandleRemoveLife(GameState.Character character)
    {
        if (character == GameState.P1_Character)
        {
            p1Lives--;

            if (p1Lives > 0)
            {
                p1LivesList[p1Lives].SetActive(false);
                GameObject newChar = GetGameObjectForCharacter(GameState.P1_Character);
                if (newChar != null)
                {
                    RespawnCharacter?.Invoke(newChar);
                }
            }
            else
            {
                p1LivesList[0].SetActive(false);
                RemoveCharacterFromAIList?.Invoke(GameState.P1);
                GameState.winners.Remove(GameState.P1_Character.ToString());
                RemoveDamageTextAtIndex?.Invoke(0);
            }
        }
        else if (character == GameState.P2_Character)
        {
            p2Lives--;

            if (p2Lives > 0)
            {
                p2LivesList[p2Lives].SetActive(false);
                GameObject newChar = GetGameObjectForCharacter(GameState.P2_Character);
                if (newChar != null)
                {
                    RespawnCharacter?.Invoke(newChar);
                }
            }
            else
            {
                p2LivesList[0].SetActive(false);
                RemoveCharacterFromAIList?.Invoke(GameState.P2);
                GameState.winners.Remove(GameState.P2_Character.ToString());
                RemoveDamageTextAtIndex?.Invoke(1);
            }
        }
        
        
    }

    private GameObject GetGameObjectForCharacter(GameState.Character character)
    {
        if (character == GameState.Character.Iblis)
        {
            return iblisObject;
        }
        else if (character == GameState.Character.Shauna)
        {
            return shaunaObject;
        }
        else
        {
            Debug.Log("Something is wrong....");
            return null;
        }
    }
}
