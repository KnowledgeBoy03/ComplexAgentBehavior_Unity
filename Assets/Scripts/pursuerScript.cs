using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pursuerScript : MonoBehaviour
{
    CapsuleCollider detection;
    NavMeshAgent agent;
    GameObject speaker;
    public bool suspect;
    public bool suspectPlay;
    public bool found;
    public bool foundPlay;

    void Start()
    {
        detection = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        GameObject speaker = GameObject.FindGameObjectWithTag("Speaker");
        suspect = false;
        suspectPlay = false;
        found = false;
        foundPlay = false;
        if(gameObject.CompareTag("1")){
            //set the path to 0 
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap0Pref"), 1f);
        }
        else if(gameObject.CompareTag("2"))
        {
             agent.SetAreaCost(NavMesh.GetAreaFromName("Cap1Pref"), 1f);

        }else if(gameObject.CompareTag("3"))
        {
             agent.SetAreaCost(NavMesh.GetAreaFromName("Cap2Pref"), 1f);

        }else if(gameObject.CompareTag("4"))
        {
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap3Pref"), 1f);

        }

    }

    void Update()
    {
        if (found)
        {
            if (foundPlay == false)
            {
                PlaySpeaker();
                foundPlay = true;
            }
            suspect = false;
            detection.radius = 9.0f;
        }
        
        if (suspect)
        {
            if (suspectPlay == false)
            {
                speaker.GetComponent<speakerScript>().BackUp();
                suspectPlay = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Evader"))
        {
            found = true;
            suspect = false;
            agent.destination = other.transform.position;
        }
        else if (other.CompareTag("Bolt"))
        {
            if (found == false)
            {
                suspect = true;
                agent.destination = other.transform.position;
            }
        }

        if (other.CompareTag("Pursuer"))
        {
            if (found == true && other.GetComponent<pursuerScript>().found == false)
            {
                other.gameObject.GetComponent<CapsuleCollider>().radius = 9.0f;
                other.gameObject.GetComponent<NavMeshAgent>().destination = agent.destination;
                other.gameObject.GetComponent<pursuerScript>().found = true;
            }
            else if (suspect == true && other.GetComponent<pursuerScript>().suspect == false)
            {
                other.gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            }
        }

        if (other.CompareTag("Speaker"))
        {
            agent.destination = other.gameObject.GetComponent<speakerScript>().evaderPos.position;
            found = true;
        }
    }

    private void PlaySpeaker()
    {
        speaker.GetComponent<speakerScript>().Reveal();
    }
}
