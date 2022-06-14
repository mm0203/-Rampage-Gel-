using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{

    // �_���[�W��^����Ԋu
    float fInterval = 0.5f;
    float fTime;

    GameObject player;
    GameObject enemy;
    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }

    private void Start()
    {
        // 5�b�ŏ���
        Destroy(gameObject, 5.0f);
    }

    // Start is called before the first frame update
    void Update()
    {
        // �G���S���A�����蔻��p�̃L���[�u��������
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // �_���[�W����
            player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack);

            //player.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1500);
        }
    }

    // �Β��ɓ����葱���Ă���Ƃ�
    private void OnTriggerStay(Collider other)
    {
        // �v���C���[�ɓ���������
        if (other.tag == "Player")
        {
            fTime -= Time.deltaTime;

            // �ݒ�C���^�[�o�����Ƀ_���[�W��^����(0.5�b)
            if (fTime < 0.0f)
            {
                // �_���[�W����
                player.GetComponent<PlayerHP>().OnDamage(enemy.GetComponent<EnemyBase>().GetEnemyData.nAttack / 10);

                fTime = fInterval;
            }

        }
    }
}
