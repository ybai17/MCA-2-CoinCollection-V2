using UnityEngine;

public class FlagBehavior : MonoBehaviour
{

    public static bool ObjectiveReached {get; set;}
    public Material newFlagMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectiveReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinBehavior.ScoreTotal >= 75)
        {
            UpdateFlag();
        }
    }

    void UpdateFlag()
    {
        transform.GetChild(0).transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = newFlagMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            ObjectiveReached = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            ObjectiveReached = false;
        }
    }
}
