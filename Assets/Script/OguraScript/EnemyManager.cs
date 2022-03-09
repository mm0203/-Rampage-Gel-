//======================================================================
// Player.cs
//======================================================================
// ŠJ”­—š—ğ
//
// 2022/03/02 authorF¼–ì«”V ZZì¬
// 2022/03/03 authorF‰œ“c’B– ZZ‚Ìˆ—’Ç‰Á
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// d•¡‹Ö~
[DisallowMultipleComponent]

public class EnemyManager : MonoBehaviour
{
    // “G‚ÌÅ‘å”
    [Header("“G‚Ì”‚ÌMAX")] [SerializeField] int MaxEnemy = 2;
    // oŒ»”ÍˆÍ
    [Header("“G‚ÌoŒ»À•W”ÍˆÍ")] [SerializeField, Range(1.0f, 100.0f)] float InstantiateX = 6.5f;
    [SerializeField, Range(1.0f, 100.0f)] float InstantiateZ = 3.5f;

    // “G‚Ìí—Ş
    [SerializeField] List<GameObject> EnemyList;
    // oŒ»‚µ‚Ä‚¢‚é“G‚ÌƒŠƒXƒg
    public List<GameObject> NowEnemyList;


    GameObject player;
    //public GameObject GetPlayer { get { return player; } }

    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        for(int i = 0; i < MaxEnemy;i++)
        {
            CreateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Œ¸‚Á‚½‚çV‚µ‚­¶¬
        if (NowEnemyList.Count < MaxEnemy)
        {
            CreateEnemy();
        }
    }

    // “G‚ğ¶¬
    private void CreateEnemy()
    {
        enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], CreatePos(), Quaternion.identity);
        enemy.GetComponent<EnemyBase>().SetManager(gameObject.GetComponent<EnemyManager>());
        enemy.GetComponent<EnemyBase>().SetPlayer(player);
        NowEnemyList.Add(enemy);
    }

    private Vector3 CreatePos()
    {
        Vector3 vPos;

        vPos = new Vector3(Random.Range(-InstantiateX, InstantiateX), 1.0f, Random.Range(-InstantiateZ, InstantiateZ));
        return vPos;
    }
}
