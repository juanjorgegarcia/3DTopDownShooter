using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Variables
    public float movementSpeed;
    public GameObject camera;

    public GameObject bulletSpawnPoint;
    public float waitTime;

    public GameObject playerObj;
    public GameObject bullet;

    private Renderer renderer;
    public float timeToColor = 1f;
    private Color defaultColor;

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;
        private int _curHealth;

        public int score;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }

        }


        public void Init()
        {
            curHealth = maxHealth;
            score = 0;
        }
    }

    public PlayerStats stats = new PlayerStats();

    //Methods
    // Start is called before the first frame update
    void Start()
    {
        stats.Init();
        
        renderer = transform.GetChild(0).GetComponent<Renderer>();
        defaultColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray,out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }


        //Shooting

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
         void Shoot()
        {
            Instantiate(bullet.transform, bulletSpawnPoint.transform.position, playerObj.transform.rotation);
        }
       
    }

    public void DamagePlayer(int dmg)
    {
        stats.curHealth -= dmg;
        if (stats.curHealth <= 0)
        {
            Debug.Log("KILL PLAYER");
            GameMaster.KillPlayer(this);

        }
        StartCoroutine(SwitchColor());

        // if (statusIndicator != null)
        // {
        //  statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        // Debug.Log("DANO RECEBIDO: " + stats.curHealth);

        //}
        //Debug.Log("DANO RECEBIDO: " + stats.curHealth);

    }
    public void AddScore(int score)
    {
        stats.score += score;
        //Debug.Log("SCORE AUMENTADO: " + stats.score);

    }
    IEnumerator SwitchColor()
    {
        renderer.material.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(timeToColor);
        renderer.material.color = defaultColor;

    }
}
