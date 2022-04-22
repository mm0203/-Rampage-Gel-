
//======================================================================
// SkullDragon.cs
//======================================================================
// �J������
//
// 2022/03/28 author�F�|���@���} �G�t�F�N�g�����g�ݍ���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDragon : MonoBehaviour
{
    GameObject cube;
    EnemyBase enemyBase;

    // �G�t�F�N�g�֘A
    EnemyEffect enemyEffect;
    GameObject objEffect;

    [Header("�Ή����˂̋���")] [SerializeField] float fDistance = 3.0f;

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
    // �Ή����ˏ���(�A�j���[�V�����ɍ��킹�ČĂяo��)
    //----------------------------------------------
    private void SkullDragonAttack()
    {
        // �����蔻��p�̃L���[�u����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // �T�C�Y�A���W�A�p�x�ݒ�
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 5.0f);
        cube.transform.rotation = this.transform.rotation;
        cube.transform.position = new Vector3(transform.position.x + transform.forward.x * fDistance, transform.position.y, transform.position.z + transform.forward.z * fDistance);

        // �Ή����˂̃R���|�[�l���g��ǉ�
        cube.AddComponent<Flamethrower>();

        // ���Z�b�g
        cube.GetComponent<Flamethrower>().SetEnemy(gameObject);
        cube.GetComponent<Flamethrower>().SetPlayer(enemyBase.player);
        cube.GetComponent<Flamethrower>().SetDiss(fDistance);

        // �G�t�F�N�g����
        objEffect = enemyEffect.CreateEffect(EnemyEffect.eEffect.eFlame, gameObject);
        cube.GetComponent<Flamethrower>().SetEffect(objEffect);

        // ���蔲���锻���
        cube.GetComponent<BoxCollider>().isTrigger = true;

        // �U���t���O��ON�i�G�������Ȃ��Ȃ�j
        enemyBase.bAttack = true;

        // �����蔻��L���[�u���\��
        cube.GetComponent<MeshRenderer>().enabled = false;
    }
}