using UnityEngine;

public class network_player : MonoBehaviour
{
    [SerializeField]
    Vector3 position;
    [SerializeField]
    Quaternion rotation;
    int action = 0;
    float health = 100;
    float ping = 0.001f; //network delay
    Vector3 targetPosition;
    Quaternion targetRotation;
    float snapRadius = 0.05f;// this is the radius where it just assumes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void PlayerJoin()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void networkUpdate()
    {
        if((transform.position - targetPosition).magnitude < snapRadius)
        {
            //close enough and snaps
        }
    }
}
