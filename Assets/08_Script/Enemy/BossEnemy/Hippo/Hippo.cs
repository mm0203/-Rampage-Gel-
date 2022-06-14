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

        eMax
    }

    // �U���֐����X�g
    private List<Action> AttackList;

    GameObject Cube;
    EnemyBase enemyBase;

    float attackTime = 6.0f;

    void Start()
    {

        // �G�l�~�[�x�[�X���擾
        enemyBase = this.GetComponent<EnemyBase>();

        AttackList = new List<Action>();
        AttackList.Add(FireWaveAttack);
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
    }

    // �U���̊֐��������_���ɌĂ�
    void HippoAttack()
    {
        int range = UnityEngine.Random.Range(0, (int)eHippoAttack.eMax - 1);

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
        Cube.transform.position = transform.position + transform.forward * Cube.transform.localScale.z;    // �{�X�O���֐���

        // �R���|�[�l���g����
        Cube.AddComponent<FireWave>();
        Cube.GetComponent<FireWave>().SetPlayer(enemyBase.GetComponent<EnemyBase>().player);
        Cube.GetComponent<FireWave>().SetEnemy(this.gameObject);
        Cube.GetComponent<BoxCollider>().isTrigger = true;
        enemyBase.bAttack = true;

        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Rigidbody>().useGravity = false;
        Cube.GetComponent<Rigidbody>().isKinematic = true;

        //Cube.GetComponent<MeshRenderer>().enabled = false;
    }
}
