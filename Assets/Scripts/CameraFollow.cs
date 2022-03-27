#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public int rightBound;
    public int leftBound;
    public int offset = 2;

    public void LateUpdate()
    {
        if(target.position.x >= rightBound || target.position.x <= leftBound)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + offset, -10);
        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y + offset, -10);
        }
    }
}