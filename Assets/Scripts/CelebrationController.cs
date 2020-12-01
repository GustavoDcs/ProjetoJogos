using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationController : MonoBehaviour
{
    [SerializeField]
    GameObject oneWinner;

    [SerializeField]
    Sprite iblis;

    [SerializeField]
    Sprite shauna;

    Dictionary<string, Sprite> nameToSpriteMapping = new Dictionary<string, Sprite>();

    [SerializeField]
    List<GameObject> oneWinnerObjects;

    [SerializeField]
    TMPro.TextMeshProUGUI text;

    void OnEnable()
    {
        oneWinner.SetActive(false);

        nameToSpriteMapping.Add("Iblis", iblis);
        nameToSpriteMapping.Add("Shauna", shauna);

        if (GameState.winners.Count == 1)
        {
            oneWinner.SetActive(true);
            text.text = GameState.winners[0] + " Wins!";

            SpriteRenderer spriteRenderer = oneWinnerObjects[0].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = nameToSpriteMapping[GameState.winners[0]];
            }
        }

    }
}
