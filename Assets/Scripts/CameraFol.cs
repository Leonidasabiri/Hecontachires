using UnityEngine;

[ExecuteInEditMode]
public class CameraFol : MonoBehaviour
{
    public float followSpeed;
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
    }
}
