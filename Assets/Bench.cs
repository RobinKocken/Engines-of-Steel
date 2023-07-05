using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour, IInteractable
{
    public void Interact(GameManager gameManager)
    {
        Crafting(gameManager);
    }

    void Crafting(GameManager gameManager)
    {
        gameManager.SwitchStatePlayer(GameManager.PlayerState.ui, UIManager.ExternalUIState.craft);
    }
}
