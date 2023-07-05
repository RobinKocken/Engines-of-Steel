using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public GameManager.PlayerState previousState;

    public enum InternalUIState
    {
        none,
        journal,
        inventory,
        map,
        option,
    }
    public InternalUIState internalUIState;

    public enum ExternalUIState
    {
        none,
        build,
        craft,
        oven,
    }
    public ExternalUIState externalUIState;

    public GameObject uiPlayer;
    public InternalUI internalUI;
    public ExternalUI externalUI;
    public AnimationUI animationUI;

    public Oven oven;

    void Start()
    {
        SwitchStateUI(InternalUIState.none, ExternalUIState.none, GameManager.PlayerState.player);

        animationUI.bar.GetComponent<Image>().color = animationUI.barColor;

        for(int x = 0; x < animationUI.back.Length; x++)
        {
            for(int y = 0; y < animationUI.back[x].childCount; y++)
            {
                if(animationUI.back[x].GetChild(y).TryGetComponent(out Image image))
                {
                    image.color = animationUI.backColor;
                }
            }
        }
    }

    public TMP_Text fpsCounter;
    float fps;
    float updateTimer = 0.2f;

    void Update()
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer <= 0)
        {
            fps = 1 / Time.unscaledDeltaTime;
            fpsCounter.text = $"FPS: {Mathf.Round(fps)}";
            updateTimer = 0.2f;
        }
    }

    public void InternalUIUpdate(KeyCode journalKey ,KeyCode inventoryKey, KeyCode mapKey, KeyCode optionkey)
    {
        if(internalUIState != InternalUIState.option) 
        {
            // Journal Key //
            if(Input.GetKeyDown(journalKey))
            {
                // Check if Journal is already active, if active close Internal UI and if not then go to Journal State // 
                if(internalUIState != InternalUIState.journal)
                {
                    SwitchStateUI(InternalUIState.journal, ExternalUIState.none, previousState);
                }
                else
                {
                    SwitchStateUI(InternalUIState.none, ExternalUIState.none, previousState);
                    gameManager.SwitchStatePlayer(previousState, UIManager.ExternalUIState.none);
                }
            }

            // Inventory Key //
            if(Input.GetKeyDown(inventoryKey))
            {
                // Check if Invnetory is already active, if active close Internal UI and if not then go to Invnetory State // 
                if(internalUIState != InternalUIState.inventory)
                {
                    SwitchStateUI(InternalUIState.inventory, ExternalUIState.none, previousState);
                }
                else
                {
                    SwitchStateUI(InternalUIState.none, ExternalUIState.none,previousState);
                    gameManager.SwitchStatePlayer(previousState, UIManager.ExternalUIState.none);
                }
            }

            // Map Key //
            if(Input.GetKeyDown(mapKey))
            {
                // Check if Map is already active, if active close Internal UI and if not then go to Map State // 
                if(internalUIState != InternalUIState.map)
                {
                    SwitchStateUI(InternalUIState.map, ExternalUIState.none, previousState);
                }
                else
                {
                    SwitchStateUI(InternalUIState.none, ExternalUIState.none, previousState);
                    gameManager.SwitchStatePlayer(previousState, UIManager.ExternalUIState.none);
                }
            }

            inventoryManager.InventoryUpdate();
        }      
        else if(internalUIState == InternalUIState.option)
        {
            if(Input.GetKeyDown(optionkey))
            {
                SwitchStateUI(InternalUIState.none, ExternalUIState.none, previousState);
                gameManager.SwitchStatePlayer(previousState, UIManager.ExternalUIState.none);                
            }
        } 
    }

    public void ExternalUIUpdate(KeyCode interactionKey)
    {
        if (Input.GetKeyDown(interactionKey))
        {
            SwitchStateUI(InternalUIState.none, ExternalUIState.none, previousState);
            gameManager.SwitchStatePlayer(previousState, UIManager.ExternalUIState.none);
        }
    }

    public void SwitchStateUI(InternalUIState iInternalUIState, ExternalUIState eExternalUIState, GameManager.PlayerState state)
    {
        internalUIState = iInternalUIState;
        externalUIState = eExternalUIState;

        previousState = state;

        if(internalUIState != InternalUIState.none)
        {
            Player(false);
            switch(internalUIState)
            {
                case InternalUIState.journal:
                {
                    StopAllCoroutines();
                    StartCoroutine(BarAnimation(0));
                    
                    Internal(true, false, false, false);
                    break;
                }
                case InternalUIState.inventory:
                {
                    StopAllCoroutines();
                    StartCoroutine(BarAnimation(1));

                    Internal(false, true, false, false);
                    break;
                }
                case InternalUIState.map:
                {
                    StopAllCoroutines();
                    StartCoroutine(BarAnimation(2));

                    Internal(false, false, true, false);
                    break;
                }
                case InternalUIState.option:
                {
                    Internal(false, false, false, true);
                    break;
                }
            }
        }
        else if(externalUIState != ExternalUIState.none)
        {
            Player(false);
            switch(externalUIState)
            {
                case ExternalUIState.build:
                {
                    External(true, false, false);
                    break;
                }
                case ExternalUIState.craft:
                {
                    External(false, true, false);
                    break;
                }
                case ExternalUIState.oven:
                {
                    External(false, false, true);
                    break;
                }
            }
        }
        else
        {
            Internal(false, false, false, false);
            External(false, false, false);
            Player(true);
        }
    }

    void Internal(bool journalActive, bool inventoryActive, bool mapActive, bool optionActive)
    {
        if(journalActive || inventoryActive || mapActive || optionActive)
            internalUI.uiInternal.SetActive(true);
        else
            internalUI.uiInternal.SetActive(false);

        if(!optionActive)
            internalUI.uiTop.SetActive(true);
        else if(optionActive)
            internalUI.uiTop.SetActive(false);

        internalUI.uiJournal.SetActive(journalActive);
        internalUI.uiInventory.SetActive(inventoryActive);
        internalUI.uiMap.SetActive(mapActive);
        internalUI.uiOption.SetActive(optionActive);
    }

    void External(bool builActive, bool craftActive, bool farmActive)
    {
        if(builActive || craftActive || farmActive)
            externalUI.uiExternal.SetActive(true);
        else
            externalUI.uiExternal.SetActive(false);

        externalUI.uiCraft.SetActive(craftActive);
        externalUI.uiOven.SetActive(farmActive);
    }

    public void Player(bool active)
    {
        uiPlayer.SetActive(active);
    }

    public void TopButtons(int stateValue)
    {
        SwitchStateUI((InternalUIState)stateValue, ExternalUIState.none, previousState);
    }

    IEnumerator BarAnimation(int i)
    {
        animationUI.coroutineRun = true;
        animationUI.back[animationUI.currentTargetIndex].gameObject.SetActive(false);
        animationUI.back[i].gameObject.SetActive(true);
        animationUI.currentTargetIndex = i;

        while(animationUI.coroutineRun)
        {
            animationUI.bar.localPosition = Vector2.MoveTowards(animationUI.bar.localPosition, animationUI.barTarget[i].localPosition, animationUI.barSpeed * Time.deltaTime);
            animationUI.bar.sizeDelta = Vector2.MoveTowards(animationUI.bar.sizeDelta, animationUI.barTarget[i].sizeDelta, animationUI.barSpeed * Time.deltaTime);

            if(animationUI.bar.localPosition == animationUI.barTarget[i].localPosition && animationUI.bar.sizeDelta == animationUI.barTarget[i].sizeDelta)
            {
                animationUI.coroutineRun = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

[System.Serializable]
public class InternalUI
{
    public GameObject uiTop;
    public GameObject uiInternal;
    public GameObject uiJournal;
    public GameObject uiInventory;
    public GameObject uiMap;
    public GameObject uiOption;
}

[System.Serializable]
public class ExternalUI 
{
    public GameObject uiExternal;
    public GameObject uiCraft;
    public GameObject uiOven;
}

[System.Serializable]
public class AnimationUI 
{
    public RectTransform bar;
    public Color barColor;
    public float barSpeed;
    public bool coroutineRun;

    public int currentTargetIndex;

    public RectTransform[] barTarget;
    public RectTransform[] back;
    public Color backColor;
}


