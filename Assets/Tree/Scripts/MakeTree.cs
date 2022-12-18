using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTree : MonoBehaviour
{
    [SerializeField] GameObject tree;
    [SerializeField] float MakeIntervalTime; //¶¬ŠÔŠu
    float Timecnt = 0.0f;

    GameObject player;

    bool flg = false;
    int posx = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        while (!flg)
        {
            posx = Random.Range(-14, 15);
            if (Mathf.Abs(posx - player.transform.position.x) >= 3 && posx != 1)
            {
                flg = true;
            }
        }

        int posy = Random.Range(-5, 7);
        Vector2 pos = new Vector2(posx, posy);

        Instantiate(tree, pos, Quaternion.identity);
        
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.transform.position.x);
        Timecnt += Time.deltaTime;
        if (MakeIntervalTime < Timecnt)
        {
            Timecnt = 0.0f;
            while (!flg)
            {
                posx = Random.Range(-14, 15);
                Debug.Log(Mathf.Abs(posx - player.transform.position.x));
                if(Mathf.Abs(posx - player.transform.position.x) >= 3 && posx != 1)
                {
                    flg = true;
                }
            }
            
            int posy = Random.Range(-5, 7);
            Vector2 pos = new Vector2(posx, posy);
            
            Instantiate(tree, pos, Quaternion.identity);

        }

        flg = false;
    }
}
