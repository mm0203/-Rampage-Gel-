//======================================================================
// AID_PlayerEffect.cs
//======================================================================
// 開発履歴
//
// 2022/03/28 author：竹尾　応急　プレイヤーのエフェクト発生
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AID_PlayerSE : MonoBehaviour
{
    [Header("発生SE")]
    [SerializeField] List<AudioClip> PlayerSEList;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // テスト
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartPlayerSE(0);
        }
    }

    public void StartPlayerSE(int listnum)
    {
        audioSource.PlayOneShot(PlayerSEList[listnum]);
    }
}
