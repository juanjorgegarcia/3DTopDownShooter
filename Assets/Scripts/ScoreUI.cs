
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour
{
    private Text scoreText;
    // Start is called before the first frame update

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + GameMaster.gm.getPlayerScore().ToString();
    }
}
