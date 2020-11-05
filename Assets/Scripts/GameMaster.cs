using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    private static int _remainingLives = 3;
    [SerializeField]
    private int maxLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }
    public Transform playerPrefab;
    public Transform spawnPoint;

    public GameObject player;
    public Scene mainScene;
    // Start is called before the first frame update
    public int spawnDelay = 2;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject victoryUI;

    public Transform enemyDeathParticles;
    private float nextTimeToSearch = 0;



    [SerializeField]
    private AudioSource source;


    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        _remainingLives = maxLives;
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        if (!victoryUI.activeSelf)
        {
            gameOverUI.SetActive(true);

        }

    }
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives--;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());

        }

    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);

    }
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public void Victory()
    {
        Debug.Log("Victory");
        if (!gameOverUI.activeSelf)
        {
            victoryUI.SetActive(true);

        }

    }

    public void _KillEnemy(Enemy _enemy)
    {
        //Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_enemy.gameObject);
        player.GetComponent<Player>().AddScore(_enemy.enemyStats.pointsToGive);
        source.Play();

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

    public int getPlayerScore()
    {
        if (player != null)
        {
            return player.GetComponent<Player>().stats.score;

        }
        return 0;

    }

}
