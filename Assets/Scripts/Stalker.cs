using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stalker : MonoBehaviour
{

    public GameObject target = null;
    private NavMeshAgent navMeshAgent = null;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.gameObject.GetComponent<Player>().DamagePlayer(this.damage);

            Destroy(this.gameObject);
        }

    }
}
