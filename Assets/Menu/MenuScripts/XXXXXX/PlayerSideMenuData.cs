using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;




//script por limpar funciona com butoes e a navagacao nestes, ao em vez de inputs!
//script por limpar funciona com butoes e a navagacao nestes, ao em vez de inputs!
//script por limpar funciona com butoes e a navagacao nestes, ao em vez de inputs!



public class PlayerSideMenuData : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInputUsingThis;
    public int playerIndexUsingThis;
    [SerializeField] public TMP_Text txtPlayerIndex;
    [SerializeField] public TMP_Text CharName;
    [SerializeField] Image ImgInMenu;
    [Space(5)]
    //Bts:---->Tornar Slides:
    [SerializeField] Button bt_SelectChamp;
    [SerializeField] Button bt_NextSelectChamp;
    [SerializeField] Button bt_BackSelectChamp;
 
    [Space(5)]
    //bts:
    [SerializeField] Button bt_Ready;
    [Space(5)]
    [SerializeField] GameObject panel_ReadyUnready;
    [SerializeField] Button bt_UnReady;
    //Por Add Bts:
    [SerializeField] Button bt_ColorSelect;
    //Novo->Para slider de characters:
    [SerializeField] public List<GameObject> prefabsMedaBots;

    private void Awake()
    {
        panel_ReadyUnready.SetActive(false);
        panelColorSelect.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(DelayInput());

        UpdateCharacterShow(selectOption);

        bt_NextSelectChamp.onClick.AddListener(() =>
        {
            RightOption();
        });

        bt_BackSelectChamp.onClick.AddListener(() =>
        {
            LeftOption();
        });
        bt_Ready.onClick.AddListener(() =>
        {
            SendCharSelectInfoToPlayrMedabotSelected(charSelected);
            panel_ReadyUnready.SetActive(true);
            bt_UnReady.Select();

        });
        bt_UnReady.onClick.AddListener(() =>
        {
            UnReady();
            panel_ReadyUnready.SetActive(false);
            bt_Ready.Select();

        });
    }

    int selectOption = 0;
    int charSelected = 0;
    void RightOption()
    {
        Debug.Log("NextOption");
        selectOption++;
        //Caso chegemos a ultima opçáo
        if (selectOption >= prefabsMedaBots.Count)
        {
            selectOption = 0;
        }
        UpdateCharacterShow(selectOption);
    }

    public void LeftOption()
    {
        Debug.Log("BackOption");
        selectOption--;
        //Caso chegemos a ultima opçáo
        if (selectOption < 0)
        {
            selectOption = prefabsMedaBots.Count-1;
        }
        UpdateCharacterShow(selectOption);
    }
    public void UpdateCharacterShow(int index)
    {
        charSelected = index;
        ImgInMenu.sprite = prefabsMedaBots[index].GetComponent<Image>().sprite;//funciona mas esta invisivel?
        //Actualizar Index
        //txtPlayerIndex.text = playerIndexUsingThis.ToString();
        if (playerIndexUsingThis == 0)
        {
            txtPlayerIndex.text = "Player One";
        }
        else if (playerIndexUsingThis == 1)
        {
            txtPlayerIndex.text = "Player Two";
        }
        //Actualizar MedaBotName
        CharName.text = prefabsMedaBots[index].GetComponent<SelectedMedaBotFromMenu>().CharacterName;

    }

    //Sera necessáriooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    void SendCharSelectInfoToPlayrMedabotSelected(int index)
    {
        if (isRandomChamp)
        {
            isRandomChamp = false;
            int rand = UnityEngine.Random.Range(0, prefabsMedaBots.Count);
            playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().CharacterName = prefabsMedaBots[rand].GetComponent<SelectedMedaBotFromMenu>().CharacterName;
        }
        else
        {
            playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().CharacterName = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().CharacterName;
        }
       // playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().CharacterName = prefabsMedaBots[charSelected].GetComponent<SelectMedabotToShowSideMenu>().CharacterName;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().prefabCharacterSlected = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().prefabCharacter;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().imgMedaInGame = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().imgMedaBig;
        VersusManager.instance.amountReady++;
    }
    void UnReady()
    {
        VersusManager.instance.amountReady--;
    }



    //-----------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------

    float ignoreTime = 0.8f;
    bool inptEnable = false;

    IEnumerator DelayInput()
    {
        yield return new WaitForSeconds(0.3f);
        inptEnable = true;
    }

    //QUASE DE CERTEZA QUE TEREMOS DE TER DELAY 1 SEC ANYTES DE UPDATE INICIAR PARA QUE NAO SEJA LOGO CHAMADO ENTER, ETC|!!!!!!!!!!!!!!!!!!!!!!

    //Saber qual o player-> consoante isso  apresentado num dos lados do menu as sua seta correspondende P1->  []   <-p2
    //Agora existem 2 posições para cada neste menu lateral das setas:
    //Random Select e Character Select
    //Tanto Random como Character select leva-nos a Color Select
    //Posteriormente ready ao selecionar a cor
    //Poderemos voltar a trás
    //P1->Deverá ter tbm maneira de poder sair deste menu.

    //Nota->ir apontando onde devem ser feitos os sons.
    //Som provavelmente deverá ter delay tbm? para nao chamar vezes seguidas?
    [SerializeField] AudioSource navigateSound;
    [SerializeField] AudioSource enterSound;
    [SerializeField] AudioSource readySound;
    [SerializeField] AudioSource unReadySound;


    //Preciso de saber quando player esta no menu lateral, a escolher cores, ou ready
    bool selectingChamp = true;//é sempre primeira
    bool isRandomChamp = false;
    bool selectingColor = false;//2
    bool playerIsReady = false;//3

    int ArrowPosition = 0;//0-select char /1-Random select
    //Basicamente 2 posicoes para as arrows na ui
    //Atenção a ordem dadas que é inversa para cada jogador, P1 - pos 0 em cima, e P2- pos  0 embaixo (champ selection invertidos)
    public List<GameObject> arrowsToDisplayUI = new List<GameObject>();


    //Por implementar:---------------
    public GameObject panelColorSelect;
    public List<GameObject> colorChoices = new List<GameObject>();//esq dir devera mudar cor selecionada e acutalizar lista
    int actulIndexColorSelect = 0;


    //Chamadas de sons, testar depois com audio.
    void CallNavigateSound()
    {
      //  navigateSound.Play();
    }
    void CallEnterSound()
    {
      //  enterSound.Play();
    }
    void CallReadySound()
    {
       // readySound.Play();
    }
    void CallUnReadySound()
    {
      //  unReadySound.Play();
    }


    void Update()
    {
        if (!inptEnable) { return;}

        //Aqui consoante o "state atual no menu a navegação deverá mudar "char select ou cores ou trocar entre estados de selecao(cores champ etc)
        if (playerInputUsingThis.actions["Navigate"].triggered)
        {
            CallNavigateSound();

            Vector2 inputDirection = playerInputUsingThis.actions["Navigate"].ReadValue<Vector2>();

            //So devem ser usado up e down quando player esta em selectchamp
            if (inputDirection == Vector2.up)
            {
                // Handle up navigation
                if (selectingChamp)
                {
                    IncreasePlayerArrow();
                }
            }
            else if (inputDirection == Vector2.down)
            {
                // Handle down navigation
                if (selectingChamp)
                {
                    DecreasePlayerArrow();
                }
            }


            else if (inputDirection == Vector2.left)
            {
                // Handle left navigation

                //Estados diferentes para input agir de acordo:
                if (selectingChamp)
                {
                    //trocar champion se estiver na 1 opçao das stas
                    if (ArrowPosition == 0)//0 = em cima champ select
                    {
                        LeftOption();
                    }

                }
                //
                else if (selectingColor)
                {
                    //Trocar cores left e right
                }
             


            }
            else if (inputDirection == Vector2.right)
            {
                // Handle right navigation

                //Estados diferentes para input agir de acordo:
                if (selectingChamp)
                {
                    //trocar champion se estiver na 1 opçao das stas
                    if (ArrowPosition == 0)//0 = em cima champ select
                    {
                        RightOption();
                    }

                }
                else if (selectingColor)
                {
                    //Trocar cores left e right
                   // bt_Ready.Select();//<---evitar double enter?!

                }
            
            }
        }
        //Enter:
        if (playerInputUsingThis.actions["Submit"].triggered)
        {
            CallEnterSound();

            if (ArrowPosition == 0 && selectingChamp)
            {
                //ChampSelected->enviar cores
                //SendCharSelectInfoToPlayrMedabotSelected(charSelected);
                panelColorSelect.SetActive(true);
                return;
            }
            //Random select
            if (ArrowPosition ==1 && selectingChamp)
            {
                //random select e envia cores  //falta random!!!!!!!!!!!
                //SendCharSelectInfoToPlayrMedabotSelected(charSelected);
                panelColorSelect.SetActive(true);

                return;
            }
            if (playerIsReady)
            {

                return;
            }
        }

        //Cancel//voltar atraz-------------------------------------------------------
        if (playerInputUsingThis.actions["Cancel"].triggered)
        {
            if (ArrowPosition == 0)
            {
                //Se p1 pode voltar atraz do menu
                //Se player 2 nao fara nada pois nao pode sair deste menu
            }
            //Random select
            if (ArrowPosition == 1)
            {
              //igual acima
            }
            //Se tiver nas cores volta a champSelect
            //Se tiver em ready podera unready
        }
        SwitchUIArrow();
    }


  
    void IncreasePlayerArrow()
    {
        if (ArrowPosition >= 1)
        {
            ArrowPosition = 0;
        }
        else
        {
            ArrowPosition++;
        }
    }
    void DecreasePlayerArrow()
    {
        if (ArrowPosition <= 0)
        {
            ArrowPosition = 1;
        }
        else
        {
            ArrowPosition--;
        }

    }
    //Em update ou chamar apos cada mudança, sempre que playerActualArrowState for alterado chama-lo SwitchUIArrow(playerActualArrowState);
    void SwitchUIArrow()
    {
        //Onde devera estar a arrow
        for (int i = 0; i < arrowsToDisplayUI.Count; i++)
        {
            if (i == ArrowPosition)
            {
                arrowsToDisplayUI[i].SetActive(true);
            }
            else
            {
                arrowsToDisplayUI[i].SetActive(false);
            }
        }

    }








    //podera ser util
    /*
    public void SetPlayerColor(Material color)
    {
        if (!inptEnable) { return; }
        PlayerConfigManager.instance.SetPlayerColor(PlayerIndex, color);
        bt_Ready.Select();<----------------------vamos precisar disto para evitar o double Enter!!!!!!!!!!!
    }*/

}
