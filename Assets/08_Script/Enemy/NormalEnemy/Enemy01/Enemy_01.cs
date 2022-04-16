
//======================================================================
// Enemy_01.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̂Ђ������U������
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Enemy_01 : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    //------------------------
    // ������
    //------------------------
    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();

        // �G�t�F�N�g�擾�iEnemyBase.cs���j
        enemyEffect = enemyBase.GetEffect;
    }

    //----------------------------------------------
    // �Ђ���������(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void AttackEnemy01()
    {
        // �����蔻��p�L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �L���[�u���\����
        cube.GetComponent<MeshRenderer>().enabled = false;

        // �L���[�u�̃T�C�Y�A��]�A�ʒu��ݒ�
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * 1.5f, transform.position.y, transform.position.z + transform.forward.z * 1.5f);
        
        // Scratch�R���|�[�l���g�ǉ�
        cube.AddComponent<Scratch>();

        // �����Z�b�g
        cube.GetComponent<Scratch>().SetPlayer(enemyBase.GetComponent<EnemyBase>().GetPlayer);
        cube.GetComponent<Scratch>().SetEnemy(this.gameObject);

        // ���̑��R���|�[�l���g����
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().useGravity = false;
        cube.GetComponent<Rigidbody>().isKinematic = true;
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �G�t�F�N�g����
        //Vector3 pos = new Vector3(transform.position.x)
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eScratch, gameObject,1.5f);
    }
}