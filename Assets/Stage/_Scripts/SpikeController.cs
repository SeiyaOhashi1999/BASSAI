using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField, Header("?d???????i10??1?b?j")] int freezeCount;

    private Rigidbody2D player_rb;

    // Start is called before the first frame update   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))        
        {
			var pl = other.gameObject.GetComponent<Player>();
			if (pl != null && pl.isDamaged == false)
			{
				Debug.Log("?v???C???[??????");
                SoundManager.Instance.playDamageSE();

                player_rb = other.gameObject.GetComponent<Rigidbody2D>();

				//???_???[?W?t???Otrue
				pl.isDamaged = true;
				//???????~????            
				//other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				//other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

				pl.setVelocityZero();

				//?`?J?`?J?_??
				StartCoroutine(Blink(other.gameObject));
			}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl != null && pl.isDamaged == false)
            {
                Debug.Log("?v???C???[??????");
                SoundManager.Instance.playDamageSE();

                player_rb = collision.gameObject.GetComponent<Rigidbody2D>();

                //???_???[?W?t???Otrue
                pl.isDamaged = true;
                //???????~????            
                //other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

                pl.setVelocityZero();

                //?`?J?`?J?_??
                StartCoroutine(Blink(collision.gameObject));
            }
        }
    }


    IEnumerator Blink(GameObject player)
    {
        int count = 0;
       
        while (count < freezeCount)
        {            
            //?X?v???C?g???\???A???\??????????????
            player.GetComponent<SpriteRenderer>().enabled = !player.GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.1f);

            count++;
        }

        player.gameObject.GetComponent<Player>().isDamaged = false;        

        Debug.Log("?_???I??");
        yield break;
    }
}
