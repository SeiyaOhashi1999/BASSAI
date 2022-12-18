using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public MikiGameManager mikiGameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Miki miki))
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    void GameOver()
    {
        mikiGameManager.playOn = false;
        mikiGameManager.AllPlayOnOff(false);
        GameSystemManager.Instance.setGameOver();
    }

}
