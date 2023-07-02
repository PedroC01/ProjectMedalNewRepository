using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsSO", menuName= "ScriptableObjects/CharactersStats")]
public class CharacterStatsSO : ScriptableObject
{
    public string CharacterName;
    public float CharacterMovementSpeed;
    public float BaseDefense;
    public GameObject characterPrefab;
    public int characterReferenceNumber;
}


