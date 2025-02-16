using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // called only after all the other Update() funtions in the scene have been called
    void LateUpdate()
    {
        // waiting before moving the camera should eliminate any potential delays between player and camera movement
        if (!target)
            return;
        
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
