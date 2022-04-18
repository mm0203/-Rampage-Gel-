using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create PlayerData")]

public class PlayerData : ScriptableObject
{
    public class PlayerStatus
    {
        // 基本ステータス(最大)
        private int nMaxLevel = 1;
        private int nMaxHp = 40;
        private int nMaxStamina = 450;
        private int nMaxAttack = 22;
        private int nMaxExp = 10;
        private float fMaxBurstPower = 5;
        private float fMaxBurstRadius = 1;

        // 基本ステータス(現在)
        private int nLevel = 1;
        private int nHp = 40;
        private int nStamina = 450;
        private int nAttack = 22;
        private float fSpeed = 1.0f;
        private int nExp = 0;
        private float fBurstStock = 5.0f;
        private float fBurstDamage = 1.0f;
        private float fBurstPower = 5;
        private float fBurstRadius = 1;

        public float fHp { get; set; } = 1000;
    }
}
