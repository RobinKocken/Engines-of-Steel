using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public OptionManager optionManager;
    public UIManager uiManager;
    public InventoryManager inventoryManager;

    public static CanvasManager canvasManager;

    public enum CanvasState
    {
        mainMenu,
        game,
    }
    public CanvasState canvasState;

    public GameObject mainMenuUI;
    public GameObject gameUI;

    void Awake()
    {
        if(canvasManager)
        {
            Destroy(gameObject);
        }
        else
        {
            canvasManager = this;
            DontDestroyOnLoad(gameObject);    
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene currrentScene, LoadSceneMode mode)
    {
        Scene scene = SceneManager.GetActiveScene();

        if(0 == scene.buildIndex)
            canvasState = CanvasState.mainMenu;
        else if(1 == scene.buildIndex)
            canvasState = CanvasState.game;

        switch(canvasState)
        {
            case CanvasState.mainMenu:
            {
                mainMenuUI.SetActive(true);
                gameUI.SetActive(false);

                break;
            }
            case CanvasState.game:
            {
                mainMenuUI.SetActive(false);
                gameUI.SetActive(true);

                GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

                gameManager.optionManager = optionManager;
                gameManager.uiManager = uiManager;
                gameManager.inventoryManager = inventoryManager;

                uiManager.gameManager = gameManager;

                BaseController baseController = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseController>();
                baseController.basePickUp.inventoryManager = gameManager.inventoryManager;

                float dif = gameManager.compass.transform.eulerAngles.y - gameManager.playerCamera.transform.eulerAngles.y;
                gameManager.compass.transform.eulerAngles = new Vector3(0, gameManager.compass.transform.eulerAngles.y + dif, 0);
                uiManager.compass.comp = gameManager.compass.transform;
                uiManager.compass.player = gameManager.playerCamera.transform;

                foreach(QuestMarker marker in gameManager.quests)
                {
                    uiManager.compass.AddQuestMarker(marker);
                }

                break;
            }
        }
    }
}
