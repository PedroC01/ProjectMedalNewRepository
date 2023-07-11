using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VersusManager : MonoBehaviour
{
    public static VersusManager instance;

    public GameObject MiddleMenu;

    //Singleton:
    void Awake()
    {
        instance = this;//singleton!
        Debug.Log("Queremos dont destroy on load? ou queremos destruir e ao voltar menu refaz?!  confirmar depois e rever compotamentos!!!!");
       DontDestroyOnLoad(this);
    }
    //
    //
    [SerializeField] GameObject PlayerMenuPrefab;
    [Space(2)]
    [Header("CountDown And LoadBar:")]
    public TMP_Text countdownText;
    public Slider LoadBar;
    [Space(2)]
    public List<PlayerInput> listJoinedPlayers = new List<PlayerInput>();
   
    void Start()
    {
        countdownText.gameObject.SetActive(false);
        LoadBar.gameObject.SetActive(false);
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        // Instantiate the player prefab
        GameObject newPlayer = Instantiate(PlayerMenuPrefab, this.transform.position, Quaternion.identity);
        newPlayer.transform.parent = this.transform;
        PlayerInput playerInput = newPlayer.GetComponent<PlayerInput>();
        Debug.Log(playerInput + "  e on index: " + playerInput.playerIndex);
        newPlayer.GetComponent<PlayerDataVersus>().KnowPlayerInputAndIndex(playerInput, playerInput.playerIndex);
        newPlayer.GetComponent<PlayerDataVersus>().saveFromManager = newPlayer.gameObject.GetComponent<PlayerInput>().actions;
        listJoinedPlayers.Add(playerInput);//Add a lista de jogadores:
    }

    public void BTTellPlayersToCreatesideMenu()
    {
        int i = 0;
        foreach (var player in listJoinedPlayers)
        {
            i++;
            if (i == 2)
            {
                var objParent = GameObject.Find("Canvas");
                GameObject middlepanel = Instantiate(MiddleMenu, objParent.transform);
            }
            player.GetComponent<PlayerDataVersus>().CreatesideMenuCalledButton();
        }
    }

    public void RemoveLastPlayer()
    {
        if (listJoinedPlayers.Count > 1)
        {
            int lastIndex = listJoinedPlayers.Count - 1;
            listJoinedPlayers.RemoveAt(lastIndex);
        }
    }

    public void OnOFFSingle()
    {
        if (GameObject.Find("SinglePlayerEventSystem") == null)
        {
            return;
        }
        if (GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled)
        {
            GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled = false;
            GameObject.Find("SinglePlayerEventSystem").GetComponent<InputSystemUIInputModule>().enabled = false;
        }
        else
        {
            GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled = true;
            GameObject.Find("SinglePlayerEventSystem").GetComponent<InputSystemUIInputModule>().enabled = true;
        }
    }

    private void Update()
    {
        AllReady();
    }

    public int amountReady = 0;
    void AllReady()
    {   
        if (amountReady == 2) 
        {
            amountReady = 0;
            countdownText.gameObject.SetActive(true);
            LoadBar.gameObject.SetActive(true);
            StartCoroutine(LoadNextSceneWithCountdown());
            countdownText.gameObject.SetActive(false);
            LoadBar.gameObject.SetActive(false);
            ScenesManagerController.instance.LoadVersus();
        }
    }









    //Not working
    //Not working
    //Not working
    //Not working
    //Not working
    //Not working
    private IEnumerator LoadNextSceneWithCountdown()
    {
        int countdownSeconds = 3;
        float elapsedTime = 0f;
        bool isLoading = false;
        AsyncOperation asyncOperation = null;
        while (elapsedTime < countdownSeconds || isLoading)
        {
            countdownText.text = Mathf.CeilToInt(countdownSeconds - elapsedTime).ToString();

            if (!isLoading)
            {
                asyncOperation =  SceneManager.LoadSceneAsync(ScenesManagerController.instance.GameScene);
                asyncOperation.allowSceneActivation = false;
                isLoading = true;
            }
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            LoadBar.value = progress;

            if (asyncOperation.isDone)
            {
                break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (asyncOperation != null && !asyncOperation.allowSceneActivation)
        {
            yield return new WaitForSeconds(countdownSeconds - elapsedTime);
            asyncOperation.allowSceneActivation = true;
        }     
    }

}
