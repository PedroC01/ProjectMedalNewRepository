using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMedabotSelected : MonoBehaviour
{
    [Header("Dont Touch All Recieve After Menu!")]
    public string CharacterName = "MetaFinalName";
    public GameObject prefabCharacterSlected;
    public Sprite imgMedaInGame;
    public PlayerInput pInput;
    public int pInputIndex;
    public Color medaColor;

}
