using UnityEngine;

public class FirebarBehavior : MonoBehaviour
{
    //this represents the target to revolve around
    public Transform targetBlock;

    //true = clockwise
    //false = counter-clockwise
    //this is from the POV of the Main Camera
    public bool revolveClockwise;

    public float revolveSpeed;

    private Transform actualFirebar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualFirebar = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (revolveClockwise) {
            actualFirebar.transform.RotateAround(targetBlock.position, Vector3.forward, -1 * revolveSpeed * Time.deltaTime);
        } else {
            actualFirebar.transform.RotateAround(targetBlock.position, Vector3.forward, revolveSpeed * Time.deltaTime);
        }
    }
}
