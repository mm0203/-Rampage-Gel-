//======================================================================
// Hippo.cs
//======================================================================
// �J������
//
// 2022/06/14 author�F�����x ����J�n�@
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hippo : MonoBehaviour
{
    // �U���̎��
    enum eHippoAttack
    {
        eFireWave = 0,
        eWaterWave,
        eWay,

        eMax
    }

    // �U���֐����X�g
    private List<Action> AttackList;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;

    GameObject Cube;
    GameObject[] Way;
    int WayNum = 4;
    EnemyBase enemyBase;

    float attackTime = 6.0f;

    // �U����ސ�
    int AttackNum;

    int HP;
    int NowHP;
    bool AddFlag;

    void Start()
    {
        // �G�t�F�N�g�擾
        enemyEffect = GetComponent<EnemyEffectBase>().GetEffect;

        // �G�l�~�[�x�[�X���擾
        enemyBase = this.GetComponent<EnemyBase>();

        // �U���̎�ނ����X�g�ɒǉ�
        AttackList = new List<Action>();
        AttackList.Add(FireWaveAttack);
        AttackList.Add(WaterWaveAttack);

        // HP�ő�l�ۑ�
        HP = gameObject.GetComponent<EnemyBase>().GetEnemyData.nHp;
        AddFlag = false;

        Way = new GameObject[WayNum];
        AttackNum = 2;
    }

    private void Update()
    {
        // �U���I��
        if(enemyBase.bAttack)
        {
            attackTime -= Time.deltaTime;
            if(attackTime < 0)
            {
                enemyBase.bAttack = false;
                attackTime = 6.0f;
            }
        }

        NowHP = gameObject.GetComponent<EnemyBase>().GetEnemyData.nHp;
        // HP�������ȉ��ɂȂ������A�U����ޒǉ�
        if (NowHP <= HP / 2 && !AddFlag)
        {
            AttackList.Add(FourWaveAttack);
            AddFlag = true;
            AttackNum = 3;
            Debug.Log("Y");
        }
        
    }

    // �U���̊֐��������_���ɌĂ�
    void HippoAttack()
    {
        int range = UnityEngine.Random.Range(0, AttackNum);
        AttackList[range]();

    }


    // �t�@�C���[�E�F�[�u
    private void FireWaveAttack()
    {
        // �����蔻��p�L���[�u����
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        Cube.transform.localScale = new Vector3(8.0f, 1.0f, 5.0f);
        Cube.transform.rotation = this.transform.rotation;
        Cube.transform.position = new Vector3(transform.position.x + transform.forward.x * Cube.transform.localScale.x,
                                        transform.position.y,
                                        transform.position.z + transform.forward.z * Cube.transform.localScale.z);    // �{�X�O���֐���

        // �R���|�[�l���g����
        Cube.AddComponent<FireWave>();
        Cube.GetComponent<FireWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<FireWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        Cube.GetComponent<MeshRenderer>().enabled = false;

        // �G�t�F�N�g����
        Vector3 pos = new Vector3(gameObject.transform.position.x + gameObject.transform.forward.x * 2.0f, 
                                  gameObject.transform.position.y, 
                                  gameObject.transform.position.z + gameObject.transform.forward.z * 2.0f);
        enemyEffect.CreateEffect(EnemyEffect.eEffect.eWave02, pos, gameObject, 5.0f);
    }

    // �E�H�[�^�E�F�[�u
    private void WaterWaveAttack()
    {
        // �����蔻��p�L���[�u����
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        Cube.transform.localScale = new Vector3(8.0f, 1.0f, 5.0f);
        Cube.transform.rotation = this.transform.rotation;
        Cube.transform.position = new Vector3(transform.position.x + transform.forward.x * Cube.transform.localScale.x,
                                        transform.position.y,
                                        transform.position.z + transform.forward.z * Cube.transform.localScale.z);    // �{�X�O���֐���

        // �R���|�[�l���g����
        Cube.AddComponent<WaterWave>();
        Cube.GetComponent<WaterWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<WaterWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        Cube.GetComponent<MeshRenderer>().enabled = false;

        // �G�t�F�N�g����
        Vector3 pos = new Vector3(gameObject.transform.position.x + gameObject.transform.forward.x * 3.0f,
                                  gameObject.transform.position.y,
                                  gameObject.transform.position.z + gameObject.transform.forward.z * 3.0f);
        enemyEffect.CreateEffect(EnemyEffect.eEffect.eWave01, pos, gameObject, 5.0f);
    }


    private void FourWaveAttack()
    {
        for(int i = 0; i < WayNum; i++)
        {
            // �����蔻��p�L���[�u����
            Way[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // �T�C�Y�A���W�A�p�x�ݒ�
            Way[i].transform.localScale = new Vector3(2.0f, 1.0f, 1.0f);
            Way[i].transform.Rotate(0, transform.rotation.y + (90.0f * (i + 1)),0);
            Way[i].transform.position = transform.position;

            // �R���|�[�l���g����
            Way[i].AddComponent<FourWayWave>();
            Way[i].GetComponent<FourWayWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
            Way[i].GetComponent<FourWayWave>().SetEnemy(this.gameObject);
            Way[i].GetComponent<BoxCollider>().isTrigger = true;
            enemyBase.bAttack = true;
        }
    }
}
