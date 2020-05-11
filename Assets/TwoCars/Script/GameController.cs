using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float scrollSpeed = 5f;

    public Text txtScore;
    public Button btnPause;

    public bool isGameOver = false;
    public bool isPause = false;
    public int score;

    public AudioClip audioGetPoint, audioHit;
    public AudioSource audioSource;
    public bool isSound = true;

    private float timeLastSpawn = 0;
    public float spawnRate = 1.5f;

    public Transform parentRed01, parentRed02, parentBlue01, parentBlue02;
    public Enemy wallRed, wallBlue, pointRed, pointBlue;

    private float randomBlue = 0, randomRed = 0;

    public GameObject particalRed, particalBlue;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        randomBlue = Random.Range(-1f, 5f);
        randomRed = Random.Range(-1f, 5f);
        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();
        btnPause.onClick.AddListener(onPause);
        StartGameDialog.instance.showUI();

        this.transform.DOLocalJump(Vector3.zero, 20, 3, 3f).OnComplete(this.Stress);
    }

    int[] a = new int [] { 0, 0, 0, 0, 0, 0, 0, 0 };
    private void Stress()
    {
        for(int i = 2 - 1; i >= 0; --i)
        {
            Task.Run(this.Stressful);
        }
        
    }

    private void Stressful()
    {
        int sum = 0;
        for (int i = int.MaxValue; i >= 0; --i)
        {
            sum *= (i + 1);
        }
    }

    void Update()
    {
        if (!isPause)
        {
            timeLastSpawn += Time.deltaTime;
            if (!isGameOver && timeLastSpawn > spawnRate)
            {
                timeLastSpawn = 0;
                // spawnPoint
                CreateObject(wallRed, pointRed, parentRed01, parentRed02, randomRed);
                CreateObject(wallBlue, pointBlue, parentBlue01, parentBlue02, randomBlue);
            }
        }
    }

    private void CreateObject(Enemy wall, Enemy point, Transform parent01, Transform parent02, float random)
    {
        Enemy go = (Random.value > 0.5) ? wall : point;
        Transform parentTransform = (Random.value > 0.5) ? parent01 : parent02;
        Instantiate(go, new Vector3(parentTransform.position.x, parentTransform.position.y + random, 0), Quaternion.identity);
    }

    private void onPause()
    {
        if (!isGameOver)
        {
            PauseGameDialog.instance.showUI();
        }
    }

    public void CarScored()
    {
        if (isGameOver)
            return;
        AudioHit(true);
        score++;
        if (score % 20 == 0)
        {
            GameHarder();
        }
        txtScore.text = score.ToString();
    }

    private void GameHarder()
    {
        
        if(spawnRate <= 0.8f)
        {
            spawnRate = 0.8f;
        }
        else
        {
            spawnRate -= 0.1f;
        }       
   
        if (scrollSpeed >= 10f)
        {
            scrollSpeed = 10f;
        }
        else
        {
            DOTween.To(
            () => scrollSpeed, //// function get data source (the number in the beginning)
            (x) => scrollSpeed = x,// function to get data, with x is the (learping) number
            scrollSpeed + 1,// target number
            1f// do you understand
            );// another way
        }
    }

    public void GameOver()
    {
        audioSource.Stop();
        isGameOver = true;
        Invoke("ReadyShowGameOver", 2f);
    }

    private void ReadyShowGameOver()
    {
        GameOverDialog.instance.showUI();
    }

    [ContextMenu("clear")]
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    //audio
    public void AudioHit(bool isPoint)
    {
        if(isSound)
        {
            if (isPoint)
            {
                audioSource.PlayOneShot(audioGetPoint, 0.2f);
            }
            else
            {
                audioSource.PlayOneShot(audioHit, 1f);
            }
        }
    }
}
