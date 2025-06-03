using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT, UP, DOWN
    }

    public Direction moveDirection;

    public float delayStep; // tempo entre os passos
    public float step; //tamanho do passo

    public Transform Head; //pega a posição da cabeça
    public List<Transform> Tail;
    private Vector3 lastPos;

    public Transform Food;
    public GameObject TailPrefab;


    public int lines = 13;
    public int columns = 29;

    private bool isGameOver = false;

    public GameObject PainelGameOver;
    public GameObject MapaGameplay;
    public GameObject PainelMenuPrincipal;
    public TMP_Text txtscore;
    public TMP_Text txtHS;
    private int score;
    private int hiscore;

    private List<Quaternion> TailRotations = new List<Quaternion>();

    public UltimoScore ultimoScoreScript;
    public AudioSource audioSourceSFXEAT;
    public AudioClip eatSound;
    public AudioSource audioSourceSFXHit;
    public AudioClip hitSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine("MoveSnake");
        SetFood();
        hiscore = PlayerPrefs.GetInt("hiscore");
        txtHS.text = "HS:" + hiscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Direction.DOWN)
        {
            moveDirection = Direction.UP;
        }

        if (Input.GetKeyDown(KeyCode.A) && moveDirection != Direction.RIGHT)
        {
            moveDirection = Direction.LEFT;
        }

        if (Input.GetKeyDown(KeyCode.S) && moveDirection != Direction.UP)
        {
            moveDirection = Direction.DOWN;
        }

        if (Input.GetKeyDown(KeyCode.D) && moveDirection != Direction.LEFT)
        {
            moveDirection = Direction.RIGHT;
        }

    }

    IEnumerator MoveSnake()
    {
        if (isGameOver)
            yield break;

        yield return new WaitForSeconds(delayStep);// aguarda do tempo de delayStep para realizar
        Vector3 nexPos = Vector3.zero;

        if (isGameOver)
            yield break;

        switch (moveDirection)
        {
            case Direction.DOWN:
                nexPos = Vector3.down;
                Head.rotation = Quaternion.Euler(0, 0, 180);
                break;

            case Direction.LEFT:
                nexPos = Vector3.left;
                Head.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case Direction.RIGHT:
                nexPos = Vector3.right;
                Head.rotation = Quaternion.Euler(0, 0, -90);
                break;

            case Direction.UP:
                nexPos = Vector3.up;
                Head.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        nexPos *= step; //multiplica a direção do movimento, pelo tamanho do movimento
        lastPos = Head.position;
        Head.position += nexPos; //move a cabeça em direção ao movimento determinado
        Quaternion lastRot = Head.rotation;

        foreach (Transform t in Tail)
        {
            Vector3 temp = t.position;
            Quaternion tempRot = t.rotation;

            t.position = lastPos;
            t.rotation = lastRot;

            lastPos = temp;
            lastRot = tempRot;
            t.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (!isGameOver)
            StartCoroutine("MoveSnake");

    }

    public void Eat()
    {
        //print("Comeu");

        Vector3 tailPosition = Head.position;
        if (Tail.Count > 0)
        {
            tailPosition = Tail[Tail.Count - 1].position;
        }

        GameObject temp = Instantiate(TailPrefab, tailPosition, transform.localRotation, MapaGameplay.transform);
        Tail.Add(temp.transform);
        TailRotations.Add(temp.transform.rotation);
        score += 1;
        txtscore.text = score.ToString();
       if (eatSound != null && audioSourceSFXEAT != null)
        {
        audioSourceSFXEAT.PlayOneShot(eatSound);
        }
        SetFood();

    }

    void SetFood()
    {
        int x = Random.Range((columns - 1) / 2 * -1, (columns - 1) / 2 * 1);
        int y = Random.Range((lines - 1) / 2 * -1, (lines - 1) / 2 * 1);

        Food.position = new Vector2(x * step, y * step);
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
            if (hitSound != null && audioSourceSFXHit != null)
            {
            audioSourceSFXHit.PlayOneShot(hitSound);
            }
        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
        PlayerPrefs.SetInt("lastscore", score);
        MapaGameplay.SetActive(false);
        PainelGameOver.SetActive(true);
         if (ultimoScoreScript != null)
        {
        ultimoScoreScript.AtualizarScore();
        }
        Debug.Log("Game Over");
    }

    public void Jogar()
    {
        Head.position = Vector3.zero;
        moveDirection = Direction.LEFT;
        foreach (Transform t in Tail)
        {
            Destroy(t.gameObject);
        }
        Tail.Clear();
        Head.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        score = 0;
        txtscore.text = "0";
        hiscore = PlayerPrefs.GetInt("hiscore");
        txtHS.text = "HS: " + hiscore.ToString();
        isGameOver = false;
        StopAllCoroutines();
        StartCoroutine("MoveSnake");
        PainelGameOver.SetActive(false);
        PainelMenuPrincipal.SetActive(false);
        MapaGameplay.SetActive(true);
        Time.timeScale = 1;
        SetFood();
    }

}
