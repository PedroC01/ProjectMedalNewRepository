using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetabeeStats", menuName = "ScriptableObjects/MetabeeStats")]
public class MetabeeStats : CharacterStatsSO
{
   
    [Header("perBullet")]
    public float lastBulletCritMultiplyer;

    [Header("Base Attacks, Defenses, and Speed Values")]

    public float BaseDamageNorthAttack;
    public float baseDefense;
    public float baseDamageWestAttack;
    public int baseMagSizeSmg;
    public float baseDamageEastAttack;
    public int baseMagSizeRev;
    public float baseMovementSpeed;

    [Header("Low Energy Attacks, Defenses, and Speed Values")]
    public float lowEnergyDefense;
    public float lowEnergyDamageWestAttack;
    public int lowEnergyMagSizeSmg;
    public float lowEnergyDamageEastAttack;
    public int lowEnergyMagSizeRev;
    public float lowEnergyMovementSpeed;

    [Header("MedaForce Attacks, Defenses, and Speed Values")]
    public float MedaForceNorthAttack;
    public float medaForceDefense;
    public float medaForceDamageWestAttack;
    public int medaForceMagSizeSmg;
    public float medaForceDamageEastAttack;
    public int medaForceMagSizeRev;
    public float medaForceMovementSpeed;

    [Header("MedaForce Related")]
    public float MedaForceDuration = 5f;
}
