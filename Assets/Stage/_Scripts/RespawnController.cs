using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField, Header("リスポーン座標オブジェクト")] public GameObject respawnPosObject;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーと衝突");

            SoundManager.Instance.playDamageSE();

            StartCoroutine(Respawn(other.gameObject));
        }
    }

    private IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(1);

        //一度消す
        player.gameObject.SetActive(false);
        //リスポーン座標に移動
        player.gameObject.transform.position = respawnPosObject.transform.position;
        //再表示
        player.gameObject.SetActive(true);        
    }
}
