using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class pursuerScript : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent navvy;
    void Start()
    {
        navvy= GetComponent<NavMeshAgent>();
        if(gameObject.name == "1"){
            //set the path to 0 
            navvy.SetAreaCost(NavMesh.GetAreaFromName("Cap0Pref"), 1f);
        }
        else if(gameObject.name =="2"){
             navvy.SetAreaCost(NavMesh.GetAreaFromName("Cap1Pref"), 1f);

        }else if(gameObject.name =="3"){
             navvy.SetAreaCost(NavMesh.GetAreaFromName("Cap2Pref"), 1f);

        }else if(gameObject.name =="4"){
            navvy.SetAreaCost(NavMesh.GetAreaFromName("Cap3Pref"), 1f);

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
