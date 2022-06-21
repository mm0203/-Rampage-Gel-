//======================================================================
// EnemyEffect.cs
//======================================================================
// �J������
//
// 2022/03/30 author�F�����@�G�t�F�N�g���������ǉ�
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [Header("�G�t�F�N�g�f�[�^")]
    [SerializeField] EnemyEffectData EffectData;

    // �G�t�F�N�g���X�g
    List<GameObject> EffectList;

    public enum eEffect
    {
        eFireBall = 0,
        eFlame,
        eScratch,
        eFirePiller,
        eIcePiller,
        eWave01,
        eWave02,

        eMax_Effect
    }

    //--------------------------------------------------
    // �G�t�F�N�g����
    // �����F�G�t�F�N�g�ԍ�, �G�I�u�W�F�N�g, ���Ŏ���
    //--------------------------------------------------
    public GameObject CreateEffect(eEffect num, GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectData.GetEffectList[(int)num], obj.transform.position, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }

    //--------------------------------
    // �G�t�F�N�g�����iPos�ύXVer�j
    //--------------------------------
    public GameObject CreateEffect(eEffect num, Vector3 pos ,GameObject obj, float time = 5.0f)
    {
        GameObject Effect = Instantiate(EffectData.GetEffectList[(int)num], pos, obj.transform.rotation);
        Destroy(Effect, time);

        return Effect;
    }
}
