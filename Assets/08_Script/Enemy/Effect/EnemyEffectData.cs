using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateEnemyEffectData")]

public class EnemyEffectData : ScriptableObject
{
    [Header("エフェクト")]
    [SerializeField] List<GameObject> EffectList;

    public List<GameObject> GetEffectList { get { return EffectList; } }
}
