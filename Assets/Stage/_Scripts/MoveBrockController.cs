using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//動かす
public class MoveBrockController : MonoBehaviour
{
    [SerializeField, Header("左右移動")] public bool isHorizon;
    [SerializeField, Header("上下移動")] public bool isVertical;

    private Vector3 targetPos;

    [SerializeField,Header("移動範囲"), Range(0, 3)]public float speed;

    private void Start()
    {
        //自身のポジション取得
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHorizon)
        {
            transform.position = new Vector3(Mathf.Sin(Time.time) * speed + targetPos.x, targetPos.y, targetPos.z);
        }

        else if (isVertical)
        {
            transform.position = new Vector3(targetPos.x, Mathf.Sin(Time.time) * speed + targetPos.y, targetPos.z);
        }
    }
}
