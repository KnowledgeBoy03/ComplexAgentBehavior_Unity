using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pursuerScript : MonoBehaviour
{
    CapsuleCollider detection;
    NavMeshAgent agent;
    GameObject speaker;
    private int id = 0;
    //private bool viablePatrol;
    public bool found;
    //private Vector3[] positions = new Vector3[4];

    void Start()
    {
        detection = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        GameObject speaker = GameObject.FindGameObjectWithTag("Speaker");
        found = false;
        print(gameObject.name);
        if(gameObject.name == "1"){
            //set the path to 0 
            id = 1;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap0Pref"), 1f);
            agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(-22, -3));
        }
        else if(gameObject.name =="2"){
            id = 2;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap1Pref"), 1f);
            agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(-22, -3));

        }
        else if(gameObject.name =="3"){
            id = 3;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap2Pref"), 1f);
            agent.destination = new Vector3(Random.Range(-22, -3), .5f, Random.Range(3, 22));

        }
        else if(gameObject.name =="4"){
            id = 4;
            agent.SetAreaCost(NavMesh.GetAreaFromName("Cap3Pref"), 1f);
            agent.destination = new Vector3(Random.Range(3, 22), .5f, Random.Range(3, 22));

        }
        //viablePatrol = true;
    }

    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        if (found)
        {
            detection.radius += 6.0f;
        }
        else if (!found)
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
