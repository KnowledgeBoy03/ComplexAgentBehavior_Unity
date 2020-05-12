using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evaderMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
	public float speed = 5f;
    private CharacterController cc;

    public SphereCollider meep; 

    void Start()
    {
        cc = GetComponent<CharacterController>();
        meep = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 forward = transform.forward * v * speed * Time.deltaTime;
		Vector3 right = transform.right * h * speed * Time.deltaTime;

		cc.Move(forward + right);
        if(Input.GetKey("left shift")){
            if(speed <10){
                speed += .2f;
                meep.radius += 3f;
            }
        }else{
            if(speed>5){
                speed-=.5f;
                meep.radius =1f;
            }
        }
        
        
    }
}
