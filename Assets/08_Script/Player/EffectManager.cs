using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> EffectList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // エフェクト発生
    // 引数1:ループならtrue,falseなら一回
    // 引数2:発生させるオブジェクト
    // 引数3:発生場所
    // 引数4:発生させた際の角度
    // 引数5:発生遅延
    public IEnumerator CreateEfect(bool loop, GameObject obj, Vector3 pos, Quaternion roll, float time = 0)
    {
        // 遅延する
        yield return new WaitForSeconds(time);
        // 生成する
        Instantiate(obj, pos, roll);
        // ループするものだけ管理する
        if(loop)
        {
            EffectList.Add(obj);
        }
    }

    public void DestroyEffect()
    {
        
    }
}
