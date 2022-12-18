using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������
public class MoveBrockController : MonoBehaviour
{
    [SerializeField, Header("���E�ړ�")] public bool isHorizon;
    [SerializeField, Header("�㉺�ړ�")] public bool isVertical;

    private Vector3 targetPos;

    [SerializeField,Header("�ړ��͈�"), Range(0, 3)]public float speed;

    private void Start()
    {
        //���g�̃|�W�V�����擾
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
