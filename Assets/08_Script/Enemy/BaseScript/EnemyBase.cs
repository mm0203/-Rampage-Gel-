//======================================================================
// EnemyBase.cs
//======================================================================
// �J������
//
// 2022/03/05 author�F�����x ����J�n�@�G�̃x�[�X�����ǉ�
// 2022/03/11 author�F�����x �o�[�X�g�����ǉ�
// 2022/03/15 author�F�����x �X�e�[�^�X�����ύX
// 2022/03/28 auther�F�|���@���}�@�o���l�@�\�ǉ�
// 2022/03/24 author�F�����x ���ʉ������̒ǉ�
// 2022/03/31 author�F�����x ��苗�������ƓG�����ł���悤��
// 2022/04/04 author�F�����x ���{�X�p�ɏ�������
// 2022/04/15 author�F���쏫�V �}�X�^�[�f�[�^����X�e�[�^�X���擾
// 2022/04/21 author�F�����x GetEnemyData���쐬(EnamyManager.cs�ɂĎg�p)
// 2022/04/28 author�F�|���@�U�����Ă��Ȃ��s���eAnimetion�ɗv�f�ǉ��őΉ�
// 2022/05/10 author�F�|���@BossBase����
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]

public class EnemyBase : MonoBehaviour
{

    // �A�j���[�V�����̎��
    enum eAnimetion
    {
        eDefult,
        eMove,
        eAttack,
    }

    // �G�̃}�X�^�[�f�[�^
    [SerializeField] public EnemyData enemyData;
    public EnemyData GetEnemyData { get { return enemyData; } }

    public GameObject player{ get; set; }
    public EnemyManager manager { get; set; }
    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;

    // Lv��HP�ƍU����
    public float nHp { get; set; }
    public int nAttack { get; set; }
    public int nLv { get; set; } = 1;

    // �O�t���[���̍��W
    private Vector3 vOldPos;

    // ������΂���Ă��瓮���o���b��
    private float fBurstTime = 0.2f;
    private float fSetOldBurstTime;

    // �U������
    public bool bAttack { get; set; }

    // �U���͈͂ɓ����Ă���A��x�ڂ̍U����
    private bool bFirstAttack = false;

    [Header("���S�����ʉ�")]
    [SerializeField] private AudioClip DeathSE;

    [Header("�U�����J�n���鋗��")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackDis = 3.0f;

    [Header("�U���p�x")]
    [SerializeField, Range(0.0f, 10.0f)] private float fAttackTime = 3.0f;
    private float fAttackCount;

    // ���ŋ���
    float fDistance = 20.0f;

    // �{�X�����p
    [SerializeField] bool bBoss = false;
    [SerializeField] bool bNavOn = true;
    [Header("�{�X�^�[�Q�b�g�}�[�J�[")]
    [SerializeField] Canvas Marker;

    //*���}
    [SerializeField] GameObject Portals;
    bool bPortal = false;

    //----------------------------
    // ������
    //----------------------------
    void Start()
    {
        nLv = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().nEnemyLevel;

        // �X�e�[�^�X������
        nHp = enemyData.BossHp + (nLv * (enemyData.BossHp / 10));
        nAttack = enemyData.BossAttack + (nLv * (enemyData.nUpAttack / 10));

        // �i�r���b�V���ݒ�
        myAgent = GetComponent<NavMeshAgent>();
   
        // �X�s�[�h�ݒ�
        myAgent.speed = enemyData.fSpeed;
    
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        fAttackCount = fAttackTime;

        player = GameObject.FindWithTag("Player");
        
        
        // �{�X�ł��邩
        if (bBoss == true)
        {
            // �G�^�[�Q�b�g�}�[�J�[����
            Marker = Instantiate(Marker, Vector3.zero, Quaternion.identity);

            // �{�X�����Z�b�g
            Marker.GetComponentInChildren<TargetMarker>().target = gameObject.transform;
        }

        //
    }

    //----------------------------
    // �X�V
    //----------------------------
    void Update()
    {
        // �Ȃ����擾�ł��Ȃ��o�O�̑΍�
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        Burst();
        Move();
        Death();
        DistanceDeth();   
    }

    //----------------------------
    // ���S
    //----------------------------
    private void Death()
    {
        // HP0�ȉ��ŏ���
        if (nHp <= 0)
        {
            if(bBoss == true)
            {
                // �|�[�^������
                Instantiate(Portals, this.gameObject.transform.position, Quaternion.identity);
                bPortal = true;
                // �^�[�Q�b�g�}�[�J�[����
                Destroy(Marker.gameObject);
            }

            // �o���l����
            player.GetComponent<PlayerExp>().AddExp(10); // �G�ɂ��ς���

            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(DeathSE, transform.position);

            Destroy(this.gameObject);
        }
    }

    //----------------------------
    //  ��苗�����ꂽ�����
    //----------------------------
    private void DistanceDeth()
    {
        if(bBoss == false)
        {
            // �v���C���[�Ƃ̍����v�Z
            Vector2 vdistance = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);

            // ���ŏ���
            if (vdistance.x > fDistance || vdistance.x < -fDistance || vdistance.y > fDistance || vdistance.y < -fDistance)
            {
                // ���X�g����폜
                manager.NowEnemyList.Remove(gameObject);
                Destroy(this.gameObject);
            }
        }
       
    }


    //----------------------------
    // �U��
    //----------------------------
    private void EnemyAttack()
    {
        // �������~�߂�
        myAgent.speed = 0.0f;
        myAgent.velocity = Vector3.zero;

        // �U���J�n������ or �U���͈͂ɓ����Ă���A�ŏ��̍U���̎�
        if (IsAttack() || !bFirstAttack)
        {
            // �U�����[�V����
            animator.SetInteger("Parameter", (int)eAnimetion.eAttack);

            bFirstAttack = true;
        }
    }

    //----------------------------
    // �U���J�n��
    //----------------------------
    private bool IsAttack()
    {
        fAttackCount -= Time.deltaTime;

        // �U���J�n
        if (fAttackCount < 0.0f)
        {
            fAttackCount = fAttackTime;
            return true;
        }
        return false;
    }

    //----------------------------
    // �ړ�
    //----------------------------
    private void Move()
    {
        if (bNavOn == true)
        {
            // �����Ă��邩
            if ((vOldPos.x == transform.position.x || vOldPos.z == transform.position.z))
            {
                //Debug.Log("notmove");
            }
            else
            {
                // �ړ����[�V����
                animator.SetInteger("Parameter", (int)eAnimetion.eMove);
                bFirstAttack = false;
            }

            // �U�����łȂ��Ƃ�
            if (!bAttack)
            {
                // ���̏ꏊ���v�Z
                Vector3 nextPoint = myAgent.steeringTarget;
                Vector3 targetDir = nextPoint - transform.position;

                // ��]
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

                // �v���C���[��ǂ�������
                myAgent.SetDestination(player.transform.position);
            }

            // �v���C���[�Ƃ̋����v�Z
            Vector3 vDiffPos = this.transform.position - player.transform.position;

            // �G�Ƃ̋��������ȉ��Ȃ�U������
            if ((vDiffPos.x <= fAttackDis && vDiffPos.x >= -fAttackDis) && (vDiffPos.z <= fAttackDis && vDiffPos.z >= -fAttackDis))
            {
                EnemyAttack();
            }
            // �U���I���������o��
            else if (myAgent.speed == 0.0f && !bAttack)
            {
                // �X�s�[�h�̍Đݒ�
                myAgent.speed = enemyData.fSpeed;
            }

            vOldPos = this.gameObject.transform.position;
        }
    }

    //----------------------------
    // �o�[�X�g����������Ƃ�
    //----------------------------
    private void Burst()
    {
        // �������Z��ON�̎��i�o�[�X�g���ɕ������Z��ON�ɂȂ�j
        if (!rb.isKinematic)
        {
            
            // 2�b��ɁA�������ZOFF�ɂ���(��)
            fBurstTime -= Time.deltaTime;
            if (fBurstTime < 0.0)
            {          
                fBurstTime = fSetOldBurstTime;
                
                rb.isKinematic = true;         
            }
        }
        else
        {
            fSetOldBurstTime = fBurstTime;
        }
    }
}
