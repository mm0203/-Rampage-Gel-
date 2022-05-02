using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectBase : MonoBehaviour
{

    [Header("エフェクトシステム")]
    [SerializeField] public EnemyEffect effect;

    public EnemyEffect GetEffect { get { return effect; } }
}
