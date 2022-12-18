using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField, Header("���X�|�[�����W�I�u�W�F�N�g")] public GameObject respawnPosObject;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[�ƏՓ�");

            SoundManager.Instance.playDamageSE();

            StartCoroutine(Respawn(other.gameObject));
        }
    }

    private IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(1);

        //��x����
        player.gameObject.SetActive(false);
        //���X�|�[�����W�Ɉړ�
        player.gameObject.transform.position = respawnPosObject.transform.position;
        //�ĕ\��
        player.gameObject.SetActive(true);        
    }
}
