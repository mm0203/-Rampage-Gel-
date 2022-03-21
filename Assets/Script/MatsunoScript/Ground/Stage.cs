using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // ステージサイズ
    int StageSize = 20;
    // 生成されたステージの数
    int StageIndex;
    // プレイヤー
    public Transform Target;
    // ステージプレハブ
    public GameObject[] stagenum;
    //スタート時にどのインデックスからステージを生成するのか
    public int FirstStageIndex;
    // 事前に生成しておくステージの数
    public int aheadStage;
    //生成したステージのリスト
    public List<GameObject> StageList = new List<GameObject>();

    void Start()
    {
        // 初期ステージ番号を設定(1 -1 => 0)
        StageIndex = FirstStageIndex - 1;
        // 初期にステージを生成(3)
        StageManager(aheadStage);
    }

    void Update()
    {
        // プレイヤーの位置からステージ番号を算出
        int targetPosIndex = (int)(Target.position.z / StageSize);

        // プレイヤーの現在いるステージ番号　+ 初期ステージ数がステージ番号を超えたら
        if (targetPosIndex + aheadStage > StageIndex)
        {
            StageManager(targetPosIndex + aheadStage);

        }


        Debug.Log("ステージ番号" + targetPosIndex);
        Debug.Log("ステージ番号" + StageIndex);
    }
    void StageManager(int maps)
    {
        // ステージ番号が
        if (maps <= StageIndex)
            return;

        // 指定したステージまで作成する
        for (int i = StageIndex + 1; i <= maps; i++)
        {

            GameObject stage = MakeStage(i);

            // リストにステージを追加
            StageList.Add(stage);
        }
        // 古いステージを削除する
        while (StageList.Count > aheadStage + 1)
        {
            DestroyStage();
        }

        StageIndex = maps;
    }
    // ステージを生成する
    GameObject MakeStage(int index)
    {
        int nextStage = Random.Range(0, stagenum.Length);

        // Z方向にステージを生成
        GameObject stageObject = (GameObject)Instantiate(stagenum[nextStage], new Vector3(0, 0, index * StageSize), Quaternion.identity);

        return stageObject;
    }


    void DestroyStage()
    {
        // リストからステージを削除する
        GameObject oldStage = StageList[0];
        StageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
