using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _curHealth;
        public int pointsToGive;
       
        public int damage = 20;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }

        }

        public void Init()
        {
            curHealth = maxHealth;
            pointsToGive = 50;

        }

    }
    public EnemyStats enemyStats = new EnemyStats();
    public GameObject player;

    public GameObject bullet;
    public GameObject bulletSpawnPoint;

    private float nextTimeToSearch = 0;


    private Renderer renderer;
    public float timeToColor = 1f;
    private Color defaultColor;

    private bool shot;
    public float waitTime = 1; 
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
         enemyStats.Init();
         player = GameObject.FindGameObjectWithTag("Player");
         renderer = transform.GetComponent<Renderer>();
         defaultColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z);
            this.transform.LookAt(targetPosition);

        }
        else
        {
            FindPlayer();
            return;
        }

        if (currentTime==0)
        {
            Shoot();
        }

        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }
        if (currentTime >=waitTime)
        {
            currentTime = 0;
        }

    }



    public void DamageEnemy(int dmg)
    {
        enemyStats.curHealth -= dmg;

        if (enemyStats.curHealth <= 0)
        {
            Debug.Log("KILL ENEMY");
            GameMaster.KillEnemy(this);

        }
        StartCoroutine(SwitchColor());

        //if (statusIndicator != null)
        //{
        // statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        //}
    }

    public void Shoot()
    {
        shot = true;
        Debug.Log("enemy shot");
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, transform.rotation);
        
    }


    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                player = searchResult;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }

    IEnumerator SwitchColor()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(timeToColor);
        renderer.material.color = defaultColor;

    }


}
