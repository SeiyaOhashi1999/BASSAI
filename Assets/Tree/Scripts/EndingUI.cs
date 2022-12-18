using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int posx = Random.Range(0, 1280);
            int posy = Random.Range(0, 720);
            Instantiate(flower2, new Vector3(posx,posy, 0), Quaternion.identity);
        }
    }
}
