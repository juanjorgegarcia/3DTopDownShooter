using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveUI : MonoBehaviour {

	[SerializeField]
	WaveSpawner spawner;

	[SerializeField]
	Animator waveAnimator;

	[SerializeField]
	Text waveCountdownText;

	[SerializeField]
	Text waveCountText;

	private WaveSpawner.SpawnState previousState;


	// Use this for initialization
	void Start () {
		if (spawner == null)
		{
			Debug.LogError("No spawner referenced!");
			this.enabled = false;
		}

		if (waveCountdownText == null)
		{
			Debug.LogError("No waveCountdownText referenced!");
			this.enabled = false;
		}
		if (waveCountText == null)
		{
			Debug.LogError("No waveCountText referenced!");
			this.enabled = false;
		}
	}

    //Update is called once per frame

    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.State;
    }

    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
				waveCountdownText.enabled = true;
				waveCountText.transform.parent.gameObject.SetActive(false) ;

            //Debug.Log("COUNTING");
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
    }

    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {

			waveCountdownText.enabled = false;
			waveCountText.transform.parent.gameObject.SetActive(true);
			waveCountText.text = spawner.NextWave.ToString();
            StartCoroutine(disableSpawningUI());
            //Debug.Log("SPAWNING");
        }
    }

    //  public void StartCountdown()
    //  {
    //StartCoroutine(UpdateCountingUI());
    //  }

    //IEnumerator UpdateCountingUI()
    //   {
    //	waveCountdownText.enabled = true;
    //       for (int i = 0; i < length; i++)
    //       {

    //       }
    //	waveCountdownText.text = "3";

    //       yield return new WaitForSeconds(1f);
    //	waveCountdownText.text = "2";

    //	yield return new WaitForSeconds(1f);		
    //	waveCountdownText.text = "1";

    //       yield return new WaitForSeconds(1f);
    //	waveCountdownText.enabled = false;

    //	StartCoroutine(UpdateSpawningUI());
    //}
    IEnumerator disableSpawningUI()
    {
        yield return new WaitForSeconds(1f);
        waveCountText.transform.parent.gameObject.SetActive(false);

    }
}
