using UnityEngine;

public class ExtraLifeMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    transform.Rotate(new Vector3 (30,0,30)*Time.deltaTime);
    }
}
