using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pursuerScript : MonoBehaviour
{
    CapsuleCollider detection;
    NavMeshAgent agent;
    GameObject speaker;
    public bool found;

    void Start()
    {
        detection = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        GameObject speaker = GameObject.FindGameObjectWithTag("Speaker");
        found = false;
        if(gameObject.name == "1"){
            //set the path to 0 
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap0Pref"), 1f);
        }
        else if(gameObject.name =="2"){
             agent.SetAreaCost(NavMesh.GetAreaFromName("Cap1Pref"), 1f);

        }else if(gameObject.name =="3"){
             agent.SetAreaCost(NavMesh.GetAreaFromName("Cap2Pref"), 1f);

        }else if(gameObject.name =="4"){
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap3Pref"), 1f);

        }

    }

    void Update()
    {
        if (found)
        {
            detection.radius += 6.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Evader"))
        {
            found = true;
            agent.destination = other.transform.position;
        }
        if (other.CompareTag("Pursuer"))
        {
            if (found)
            {
                other.gameObject.GetComponent<CapsuleCollider>().radius += 6.0f;
                other.gameObject.GetComponent<NavMeshAgent>().destination = agent.destination;
                other.gameObject.GetComponent<pursuerScript>().found = true;
            }
        }
        if (other.CompareTag("Speaker"))
        {
            found = true;
            agent.destination = other.gameObject.GetComponent<speakerScript>().evaderPos.position;
        }
    }

    private void PlaySpeaker()
    {
        speaker.GetComponent<speakerScript>().Reveal();
    }
}
