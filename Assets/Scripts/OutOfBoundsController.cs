using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutOfBoundsController : MonoBehaviour
{
    public Action<GameState.Character> RemoveLife;
    GameState.Character tempCharacter;

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.name)
        {
            case "Iblis":
                tempCharacter = GameState.Character.Iblis;
                break;
            case "Shauna":
                tempCharacter = GameState.Character.Shauna;
                break;

        }

        if (tempCharacter != null)
        {
            RemoveLife?.Invoke(tempCharacter);
        }

        if (col.gameObject != null)
        {
            Destroy(col.gameObject);
        }
    }
}
