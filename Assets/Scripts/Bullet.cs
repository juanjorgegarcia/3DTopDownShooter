using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float maxDistance;
    private GameObject triggeringEnemy;

    public int damage=20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance>=5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().DamageEnemy(this.damage);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            Debug.Log("acertou o player");
            other.gameObject.GetComponent<Player>().DamagePlayer(this.damage);
            Destroy(this.gameObject);
        }
        if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
