using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour {

    public Transform target;//目标  

    private Transform mRoot;//相机  

    //[SerializeField]
    //private float moveSpeed = 5;

    private Vector3 offset;//目标与相机的距离  

    void Awake()
    {
        mRoot = this.transform;
        offset = mRoot.position - target.position;
    }

    void LateUpdate()
    {
        //mRoot.position = Vector3.Lerp(mRoot.position, target.position + offset, moveSpeed * Time.deltaTime);
        mRoot.position = target.position + offset;
    }
}
