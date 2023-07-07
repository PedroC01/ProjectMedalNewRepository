using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SpawnSideEachPlayer : MonoBehaviour
{
    [Header("Add Comps")]
    [SerializeField] GameObject sideMenuPrefab;
    [Space(20)]
    public PlayerInput input;
    public GameObject menuIsInstantiated = null;
    GameObject instantiated = null;

    public void AssignPlayerInputVersus(PlayerInput playerInput)
    {
        //Instantiate Second Menu e Atribuir Input:
        GameObject obj = GameObject.Find("Panel_Parent");
        instantiated = Instantiate(sideMenuPrefab, obj.transform);

        //Atribuir primeiro butão selecionado
        Debug.Log("Primeiro butao selecionado neste menu!");
        sideMenuPrefab.GetComponentInChildren<Button>().Select();
        menuIsInstantiated = instantiated;
        //antes
        /*  sideMenuPrefab.GetComponent<PlayerSidePainelMenu>().SetGetPlayerIndex(playerInput.playerIndex);//Aqui teoricamente ja atribui tbm o index....
          sideMenuPrefab.GetComponent<PlayerSidePainelMenu>().input = playerInput;*/
        sideMenuPrefab.GetComponent<PlayerSideMenuData>().UpdateCharacterShow(playerInput.playerIndex);//Aqui teoricamente ja atribui tbm o index....
        sideMenuPrefab.GetComponent<PlayerSideMenuData>().playerInputUsingThis = playerInput;
    }

    bool founded = false;
    void Update()
    {
        if (VersusStartScene.instance != null)
        {
            if (VersusStartScene.instance.onStartScene)
            {
                VersusStartScene.instance.PlayerJoinedLobbyWaitXtime(input);
                //Criar paineis:
                if (GameObject.Find("Panel_Parent") && !founded)
                {
                    Debug.LogWarning("Feito!");
                    GameObject obj = GameObject.Find("Panel_Parent");
                    AssignPlayerInputVersus(input);
                    founded = true;
                }
            }
        }
    }


}
