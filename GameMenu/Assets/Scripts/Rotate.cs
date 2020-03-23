using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateTo = new Vector3(0.0f, 15.0f, 0.0f);
    public float rotationSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateTo * rotationSpeed * Time.deltaTime);
    }
}
