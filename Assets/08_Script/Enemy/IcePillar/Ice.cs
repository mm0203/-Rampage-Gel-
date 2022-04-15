//======================================================================
// Ice.cs
//======================================================================
// 開発履歴
//
// 2022/04/15 author：小椋駿 製作開始　氷柱処理
//
//======================================================================

using UnityEngine;

public class Ice : MonoBehaviour
{
    // 氷柱の時間
    float fLifeTime = 6.0f;

    // 攻撃サークルが出てから氷柱が出る時間
    float fAttackStart = 1.0f;

    GameObject player;
    GameObject enemy;
    GameObject AttackCircle, TimeCircle;

    // 1フレームで拡大するサイズ
    float fScale;

    public void SetPlayer(GameObject obj) { player = obj; }
    public void SetEnemy(GameObject obj) { enemy = obj; }
    public void SetCircle(GameObject obj) { AttackCircle = obj; }


    //----------------------------------
    // 初期化
    //----------------------------------
    private void Start()
    {
        // 広がるサークル生成
        TimeCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        
        // 初期値はゼロ
        TimeCircle.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        // 1フレームで拡大するサイズを計算
        fScale = AttackCircle.transform.localScale.x / (fAttackStart * 50.0f);

        // 攻撃サークル生成
        AttackCircle = Instantiate(AttackCircle, new Vector3(player.transform.position.x, 0.1f, player.transform.position.z), AttackCircle.transform.rotation);
        
        // サークルの透明度を下げる
        AttackCircle.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 125);

    }

    //----------------------------------
    // 更新
    //----------------------------------
    void Update()
    {
        Destroy(gameObject, fLifeTime);

        // サークル消滅
        Destroy(AttackCircle, fAttackStart);
        Destroy(TimeCircle, fAttackStart);

        // サークル消滅後、キューブ表示
        if(AttackCircle == null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }

    }

    private void FixedUpdate()
    {
        // サークルの拡大
        if (TimeCircle != null)
            TimeCircle.transform.localScale = new Vector3(TimeCircle.transform.localScale.x + fScale, TimeCircle.transform.localScale.y + fScale, 1.0f);
    }
}
