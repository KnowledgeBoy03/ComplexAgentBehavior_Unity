using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pursuerScript : MonoBehaviour
{
    SphereCollider detection;
    NavMeshAgent agent;
    public GameObject speaker;
    public Transform target;
    private int id = 0;
    public bool suspect;
    public bool suspectPlay;
    public bool transmission;
    public bool transmissionPlay;
    public bool ambush;
    public bool ambushPlay;
    public bool found;
    public bool foundPlay;

    void Start()
    {
        detection = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        suspect = false;
        suspectPlay = false;
        transmission = false;
        transmissionPlay = false;
        ambush = false;
        ambushPlay = false;
        found = false;
        foundPlay = false;
        print(gameObject.name);
        if (gameObject.CompareTag("1"))
        {
            //set the path to 0 
            id = 1;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap0Pref"), 1f);
            agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(-22, -3));
        }
        else if (gameObject.CompareTag("2"))
        {
            id = 2;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap1Pref"), 1f);
            agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(-22, -3));
        }
        else if (gameObject.CompareTag("3"))
        {
            id = 3;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap2Pref"), 1f);
            agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(3, 22));
        }
        else if (gameObject.CompareTag("4"))
        {
            id = 4;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap3Pref"), 1f);
            agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(3, 22));
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
            detection.radius = 10.0f;
            agent.destination = target.position;
        }
        
        if (suspect)
        {
            detection.radius = 7.5f;
            if (suspectPlay == false)
            {
                speaker.GetComponent<speakerScript>().Warning();
                suspectPlay = true;
            }
        }

        if (transmission)
        {
            suspectPlay = true;
            speaker.GetComponent<speakerScript>().Respond();
            suspect = true;
        }

        if (ambush)
        {
            foundPlay = true;
            speaker.GetComponent<speakerScript>().Ready();
            found = true;
        }

        if (!found)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (id==1)
                {
                    agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(-22, -3));
                }
                else if (id == 2)
                {
                    agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(-22, -3));
                }
                else if (id == 3)
                {
                    agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(3, 22));
                }
                else if (id == 4)
                {
                    agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(3, 22));

                }
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

        if (other.CompareTag("1") || other.CompareTag("2") || other.CompareTag("3") || other.CompareTag("4"))
        {
            if (found == true && other.GetComponent<pursuerScript>().found == false)
            {
                other.gameObject.GetComponent<pursuerScript>().ambush = true;
            }
            else if (suspect == true && other.GetComponent<pursuerScript>().suspect == false)
            {
                other.gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
                other.gameObject.GetComponent<pursuerScript>().transmission = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Evader"))
        {
            found = true;
            suspect = false;
            agent.destination = other.transform.position;
        }
        if (other.CompareTag("1") || other.CompareTag("2") || other.CompareTag("3") || other.CompareTag("4"))
        {
            if (found == true && other.GetComponent<pursuerScript>().found == false)
            {
                other.gameObject.GetComponent<NavMeshAgent>().destination = agent.destination;
                other.gameObject.GetComponent<pursuerScript>().found = true;
            }
            else if (suspect == true && other.GetComponent<pursuerScript>().suspect == false)
            {
                other.gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            }
        }
    }

    private void PlaySpeaker()
    {
        speaker.GetComponent<speakerScript>().Reveal();
    }
}
