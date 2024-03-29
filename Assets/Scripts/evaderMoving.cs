﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evaderMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
	public float speed = 2.5f;
    private CharacterController cc;
    public GameObject bolt;
    public SphereCollider meep;
    private int i = 0;

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

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward + right), 0.025f);

		cc.Move(forward + right);
        if(Input.GetKey("left shift")){
            i++;
            if(speed <10){
                print("sprinting");
                speed += .2f;
                meep.radius = 4f;
                if (i%20 == 00) {
                    GameObject trail = Instantiate(bolt, (transform.position - transform.forward), transform.rotation);
                    Destroy(trail, 5);
                }

            }
            else
            {
                if (i % 20 == 00)
                {
                    GameObject trail = Instantiate(bolt, (transform.position - transform.forward), transform.rotation);
                    Destroy(trail, 5);
                }
            }
        }else{
            if(speed>5){
                speed-=.5f;
                meep.radius =1f;
            }
        }
    }
}
