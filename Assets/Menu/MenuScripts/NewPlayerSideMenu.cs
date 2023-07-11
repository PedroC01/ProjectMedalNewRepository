using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class NewPlayerSideMenu : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInputUsingThis;
    public int playerIndexUsingThis;
    [SerializeField] public TMP_Text txtPlayerIndex;
    [SerializeField] public TMP_Text CharName;
    [SerializeField] Image ImgInMenu;

    [SerializeField] Button bt_Ready;
    [Space(5)]
    [SerializeField] GameObject panel_ReadyUnready;
    [SerializeField] Button bt_UnReady;

    [SerializeField] public List<GameObject> prefabsMedaBots;

    int selectOption = 0;
    int charSelected = 0;

    float ignoreTime = 0.8f;
    bool inptEnable = false;

    [SerializeField] AudioSource navigateSound;
    [SerializeField] AudioSource enterSound;
    [SerializeField] AudioSource readySound;
    [SerializeField] AudioSource unReadySound;


    //Preciso de saber quando player esta no menu lateral, a escolher cores, ou ready
    bool selectingChamp = true;//é sempre primeira
    bool isRandomChamp = false;
    bool selectingColor = false;//2
    bool playerIsReady = false;//3

    int ArrowPositionSelect = 0;//0-select char /1-Random select
    int ArrowPositionColorSelect = 0;//trocar entre cores para selecionar e actualizar UI mostrada(infos)

    //Basicamente 2 posicoes para as arrows na ui
    //Atenção a ordem dadas que é inversa para cada jogador, P1 - pos 0 em cima, e P2- pos  0 embaixo (champ selection invertidos)
    public List<GameObject> arrowsToDisplayUI = new List<GameObject>();


    //Por implementar:---------------
    public GameObject panelColorSelect;





    //----------------Listas para Mudar----------------------------------------------------------------------------------------------------------
    public List<GameObject> ListMedabotsImage = new List<GameObject>();//esq dir devera mudar cor selecionada e acutalizar lista
    public List<int> listColorsNumber = new List<int>();//list Sprite para mudar cor visivel em img e qual sera usada in game
    int actualIndexColorSelect = 0;


    private void Awake()
    {
        panel_ReadyUnready.SetActive(false);
        panelColorSelect.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(DelayInput());
        UpdateCharacterShow(selectOption);
        ColorChoicesInit();
    }



    void UnReady()
    {
        VersusManager.instance.amountReady--;
    }

    //WHAT BT'S DO!!!!!!
    void Update()
    {
        if (!inptEnable) { return; }

        //Aqui consoante o "state atual no menu a navegação deverá mudar "char select ou cores ou trocar entre estados de selecao(cores champ etc)
        if (playerInputUsingThis.actions["Navigate"].triggered)
        {
            ScenesManagerController.instance.Play_MenuNavigateSound();

            Vector2 inputDirection = playerInputUsingThis.actions["Navigate"].ReadValue<Vector2>();

            //Arrow Up e Down:
            if (inputDirection == Vector2.up)
            {
                StartCoroutine(DelayInput());

                // Handle up navigation
                if (selectingChamp)
                {
                    IncreasePlayerArrow();
                }
            }
            else if (inputDirection == Vector2.down)
            {
                StartCoroutine(DelayInput());

                // Handle down navigation
                if (selectingChamp)
                {
                    DecreasePlayerArrow();
                }
            }

            //Left e Right:
            //Select Champ or Select Color
            else if (inputDirection == Vector2.left)
            {
                // Handle left navigation

                //Estados diferentes para input agir de acordo:
                if (selectingChamp)
                {
                    //trocar champion se estiver na 1 opçao das stas
                    if (ArrowPositionSelect == 0)//0 = em cima champ select
                    {
                      //  LeftOption();
                    }
                }
                //
                else if (selectingColor)
                {
                    //Trocar cores left e right
                    StartCoroutine(DelayInput());
                    ColorChoicesUpdateLeft();
                }

            }
            else if (inputDirection == Vector2.right)
            {
                // Handle right navigation

                //Estados diferentes para input agir de acordo:
                if (selectingChamp)
                {
                    //trocar champion se estiver na 1 opçao das stas
                    if (ArrowPositionSelect == 0)//0 = em cima champ select
                    {
                    //    RightOption();
                    }

                }
                else if (selectingColor)
                {
                    //Trocar cores left e right
                    // bt_Ready.Select();//<---evitar double enter?!
                    StartCoroutine(DelayInput());
                    ColorChoicesUpdateRight();
                }

            }
        }
        //
        //
        //Enter:
        if (playerInputUsingThis.actions["Submit"].triggered)
        {
            ScenesManagerController.instance.Play_MenuSubmitSound();

            #region Maneira Simples o que ocorre quando cada posicao de seta em X UI:
            //Metabee:
            //Agora a forçar metabee= pos[0], Random = pos[1], Rokusho = Pos[2]

            //Metabee
            if (ArrowPositionSelect == 0 && selectingChamp)
            {
                //ChampSelected->enviar para color select
                //SendCharSelectInfoToPlayrMedabotSelected(charSelected);
             
                selectingChamp = false;
                selectingColor = true;
                StartCoroutine(DelayInput());
                UpdateCharacterShow(0);//Metabee

                //  panelColorSelect.SetActive(true);
                SwitchUIArrowOFF();
                //return;
            }      //Random select
            if (ArrowPositionSelect == 1 && selectingChamp)
            {
                StartCoroutine(DelayInput());
                isRandomChamp = true;
                //random select e ->enviar para color select
                //SendCharSelectInfoToPlayrMedabotSelected(charSelected);
                int rand = UnityEngine.Random.Range(0, prefabsMedaBots.Count);
                charSelected = rand;
                selectingChamp = false;
                selectingColor = true;
                UpdateCharacterShow(rand);

                //  panelColorSelect.SetActive(true);
                SwitchUIArrowOFF();
              //  return;
            }
            //Rokusho
            if (ArrowPositionSelect == 2 && selectingChamp)
            {
                //ChampSelected->enviar para color select
                //SendCharSelectInfoToPlayrMedabotSelected(charSelected);
                selectingChamp = false;
                selectingColor = true;
                StartCoroutine(DelayInput());
                UpdateCharacterShow(1);//Rokusho

                //panelColorSelect.SetActive(true);
                SwitchUIArrowOFF();
               // return;
            }
      
            //Tem de ser com bt escondido ou deteta logo o enter, ou colocar delay de input novamente?
            if (selectingColor)
            {
                selectingColor = false;
                playerIsReady = true;
                StartCoroutine(DelayInput());
                panelColorSelect.SetActive(false);
                //Guarda info diz ready
                //------
                //------
                //-------
                //------
                //------
                //-------//------
                //------
                //-------//------
                //------
                //-------//------
                //------
                //-------//------
                //------
                //-------
               //prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().medaColor = ListMedabotsImage(1);//list imgage color
                SendCharSelectInfoToPlayrMedabotSelected(charSelected);
                panel_ReadyUnready.SetActive(true);

                return;
            }


            #endregion


        }
        //
        //
        //
        //Cancel//voltar atras-------------------------------------------------------
        if (playerInputUsingThis.actions["Cancel"].triggered)
        {
            ScenesManagerController.instance.Play_MenuCancelSound();

            if (selectingChamp)
            {
                //se for playerinput index = 0 podera voltar ao menu anterior, mostrar menu tem a certeza?
                //Garntir que nessa altura é destruido o outro pinput se voltar para tras


                return;
            }

          /*  if (selectingColor)
            {
                SwitchUIArrowOn();
                StartCoroutine(DelayInput());
                selectingColor = false;
                selectingChamp = true;
                panelColorSelect.SetActive(false);
                return;
            }*/

            if (playerIsReady)
            {
                //maneira simples ou tirar:
                SwitchUIArrowOn();
                selectingChamp = true;
                selectingColor = false;
                //maneira simples ou tirar:



                StartCoroutine(DelayInput());
               // selectingColor = true;
                playerIsReady = false;
                panelColorSelect.SetActive(true);
                panel_ReadyUnready.SetActive(false);
                UnReady();
                return;
            }
        }
        SwitchUIArrow();
    }















    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    IEnumerator DelayInput()
    {
        inptEnable = false;
        yield return new WaitForSeconds(0.3f);
        inptEnable = true;
    }
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!
    //Corrigir o analogico ao mudar!!!!























    //Sera necessáriooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo e acrescentar cores, sera chamado quando Ready!
    //O Que envia a info!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //O Que envia a info!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //O Que envia a info!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //O Que envia a info!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //O Que envia a info!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void SendCharSelectInfoToPlayrMedabotSelected(int index)
    {              
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().CharacterName = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().CharacterName;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().prefabCharacterSlected = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().prefabCharacter;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().imgMedaInGame = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().imgMedaBig;
        //prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().medaColor = colorImages[0].GetComponent<Image>().color;//cor assim?
      //  playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().medaColor = prefabsMedaBots[charSelected].GetComponent<SelectedMedaBotFromMenu>().medaColor;
       //Falta aqui agora enviar tambem a color info!
       VersusManager.instance.amountReady++;
    }
    
    
    
    
    
    
    //
    //
    //Antes e melhor.................
    //isto esta funcional, pois e por index!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Ver onde o chamo conforme left/right mas agr top/down!
    public void UpdateCharacterShow(int index)
    {
        charSelected = index;
        ImgInMenu.sprite = prefabsMedaBots[index].GetComponent<Image>().sprite;//funciona mas esta invisivel?
        //Actualizar Index
        if (playerIndexUsingThis == 0)
        {
            txtPlayerIndex.text = "Player One";
        }
        else if (playerIndexUsingThis == 1)
        {
            txtPlayerIndex.text = "Player Two";
        }
        //Actualizar MedaBot Name text
        CharName.text = prefabsMedaBots[index].GetComponent<SelectedMedaBotFromMenu>().CharacterName;
    }
    //
    //Escolher X Agora de Forma Obrigatoria:



  








    /*
     
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
     */

    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    //REVER
    //REVER
    //REVER
    //REVER
    //REVER
    //REVER
    //REVER
    //REVER

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
            selectOption = prefabsMedaBots.Count - 1;
        }
        UpdateCharacterShow(selectOption);
    }


    //Rever a Representaçao das setas conforme input!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    bool changeArrow = true;
    void IncreasePlayerArrow()//--------------------------------------
    {
        if (ArrowPositionSelect <= 0)
        {
            ArrowPositionSelect = 2;
        }
        else
        {
            ArrowPositionSelect--;
        }


      /*  if (ArrowPositionSelect >= 2)
        {
            ArrowPositionSelect = 0;
        }
        else
        {
            ArrowPositionSelect++;
        }*/
    }
    void DecreasePlayerArrow()//-----------------------------------------------------------
    {
        if (ArrowPositionSelect >= 2)
        {
            ArrowPositionSelect = 0;
        }
        else
        {
            ArrowPositionSelect++;
        }

        /*
        if (ArrowPositionSelect <= 0)
        {
            ArrowPositionSelect = 2;
        }
        else
        {
            ArrowPositionSelect--;
        }*/
    }

   








    //Em update ou chamar apos cada mudança, sempre que playerActualArrowState for alterado chama-lo SwitchUIArrow(playerActualArrowState);
    void SwitchUIArrow()
    {
        if (changeArrow)
        {
            for (int i = 0; i < arrowsToDisplayUI.Count; i++)
            {
                if (i == ArrowPositionSelect)
                {
                   // arrowsToDisplayUI[i].SetActive(true);
                    arrowsToDisplayUI[i].GetComponent<Image>().enabled  = true;
                }
                else
                {
                   // arrowsToDisplayUI[i].SetActive(false);
                    arrowsToDisplayUI[i].GetComponent<Image>().enabled = false;

                }
            }
        }
    }
    void SwitchUIArrowOFF()
    {
        changeArrow = false;
        for (int i = 0; i < arrowsToDisplayUI.Count; i++)
        {
               // arrowsToDisplayUI[i].SetActive(false);
         //   arrowsToDisplayUI[i].GetComponent<Image>().enabled = false;
            arrowsToDisplayUI[i].GetComponent<Image>().enabled = false;
        }
    }
    void SwitchUIArrowOn() 
    {
        changeArrow = true;
        //arrowsToDisplayUI[ArrowPositionSelect].SetActive(true);
        arrowsToDisplayUI[ArrowPositionSelect].GetComponent<Image>().enabled = true;

    }

































































    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
    //---------ok
    void ColorChoicesInit()
    {
        for (int i = 0; i < ListMedabotsImage.Count; i++)
        {
            //------
            //------
            //-------
            //------
            //------
            //-------//------
            //------
            //-------//------
            //------
            //-------//------
            //------
            //-------//------
            //------
            //-------
            // ListMedabotsImage[i].gameObject.GetComponent<Image>() = listColorsNumber[i];
        }
    }
    void ColorChoicesUpdateRight()
    {
        List<Color> originalColors = new List<Color>();
        foreach (var image in ListMedabotsImage)
        {
            originalColors.Add(image.gameObject.GetComponent<Image>().color);
        }
        for (int i = 0; i < ListMedabotsImage.Count; i++)
        {
            int newColorIndex = (i + 1) % originalColors.Count;
            ListMedabotsImage[i].gameObject.GetComponent<Image>().color = originalColors[newColorIndex];
        }
    }
    void ColorChoicesUpdateLeft()
    {
        List<Color> originalColors = new List<Color>();
        foreach (var image in ListMedabotsImage)
        {
            originalColors.Add(image.gameObject.GetComponent<Image>().color);
        }

        for (int i = 0; i < ListMedabotsImage.Count; i++)
        {
            int newColorIndex = (i - 1 + originalColors.Count) % originalColors.Count;
            ListMedabotsImage[i].gameObject.GetComponent<Image>().color = originalColors[newColorIndex];
        }
    }

}
