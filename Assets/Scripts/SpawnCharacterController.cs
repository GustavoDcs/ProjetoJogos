using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnCharacterController : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint1;

    [SerializeField]
    Transform spawnPoint2;


    [SerializeField]
    GameObject respawnPoint1;

    [SerializeField]
    GameObject respawnPoint2;


    [SerializeField]
    LivesController livesController;

    [SerializeField]
    BattleController battleController;

    public Action MakeAIJump;

    [SerializeField]
    GameObject iblisObject;

    [SerializeField]
    GameObject shaunaObject;


    int spawned;

    void Awake()
    {
        SpawnCharacter(GameState.P1_Character, spawnPoint1);
        SpawnCharacter(GameState.P2_Character, spawnPoint2);
    }

    void OnEnable()
    {
        livesController.RespawnCharacter += HandleRespawnCharacter;
    }

    void OnDisable()
    {
        livesController.RespawnCharacter += HandleRespawnCharacter;
    }

    void RespawnCharacter(GameObject go, GameObject respawnPoint, int damageResetPos, int playerNumber)
    {
        GameObject p = Instantiate(go, respawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
        CharacterController characterController = p.GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        AICharacterController aiCharacterController = p.GetComponent<AICharacterController>();

        if (playerNumber == 1)
        {
            p.tag = "Player";

            if (aiCharacterController != null)
            {
                Destroy(aiCharacterController);
            }
        }
        else
        {
            p.tag = "AI";
            aiCharacterController.enabled = true;
        }

        Animator animator = p.GetComponent<Animator>();
        animator.enabled = true;

        p.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        battleController.ResetDamageAtPosition(damageResetPos);
        DamageResolver dmgResolver = p.GetComponent<DamageResolver>();
        if (dmgResolver != null)
        {
            dmgResolver.DoDamage += battleController.HandleApplyDamage;
        }
    }

    void HandleRespawnCharacter(GameObject go)
    {
        if (GameState.P1_Character.ToString() == go.name)
        {
            RespawnCharacter(go, respawnPoint1, 0, 1);
        }
        else if (GameState.P2_Character.ToString() == go.name)
        {
            RespawnCharacter(go, respawnPoint2, 1, 2);
        }
    }

    void SpawnCharacter(GameState.Character character, Transform spawnPoint)
    {
        if (character == GameState.Character.Iblis)
        {
            Instantiate(iblisObject, spawnPoint.position, Quaternion.Euler(0, 0, 0));
        }
        else if (character == GameState.Character.Shauna)
        {
            Instantiate(shaunaObject, spawnPoint.position, Quaternion.Euler(0, 0, 0));
        }


        spawned++;

        if (spawned == 4)
        {
            GameState.Init();
        }

    }
}
