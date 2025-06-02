using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] public GameObject PainelGameOver;
    [SerializeField] public GameObject MapaGameplay;
    [SerializeField] public text txtscore;
    [SerializeField] public text txtHS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("MoveSnake");
        SetFood();       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && moveDirection != Direction.DOWN)
        {
            moveDirection = Direction.UP;
        }

        if(Input.GetKeyDown(KeyCode.A) && moveDirection != Direction.RIGHT)
        {
            moveDirection = Direction.LEFT;
        }

        if(Input.GetKeyDown(KeyCode.S) && moveDirection != Direction.UP)
        {
            moveDirection = Direction.DOWN;
        }

        if(Input.GetKeyDown(KeyCode.D) && moveDirection != Direction.LEFT)
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

        switch(moveDirection)
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

        foreach (Transform t in Tail)
        {
            Vector3 temp = t.position;
            t.position = lastPos;
            lastPos = temp;
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
        score += 1
        txtscore.text = score.ToString();
        SetFood();

    }

    void SetFood()
    {
        int x = Random.Range((columns - 1)/2*-1, (columns - 1)/2*1);
        int y = Random.Range((lines - 1)/2*-1, (lines - 1)/2*1);

        Food.position = new Vector2(x * step, y * step); 
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        MapaGameplay.SetActive(false);
        PainelGameOver.SetActive(true);
        Debug.Log("Game Over");
    }

}
