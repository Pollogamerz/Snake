using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    float step;
    Controls controls;
    Vector3 lastPos;
    bool growingPending;
    Rigidbody m_Rigidbody;
    float m_Speed;
    public int contador;
    public Text contadorT;
    

    public GameObject tailPrefab;
    public GameObject foodPrefab;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject topSide;
    public GameObject bottomSide;
    public List<Transform> tail = new List<Transform>();
    public GameObject[] cuboPruebas;

    enum Controls
    {
        up,
        down,
        left,
        right
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 8.0f;
        //step = GetComponent<SpriteRenderer>().bounds.size.x;
        StartCoroutine(MoveCoroutine());
        CreateFood();
        
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //controls = Controls.right;
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //controls = Controls.left;
            m_Rigidbody.velocity = -transform.forward * m_Speed;
        }
    

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //controls = Controls.up;
            m_Rigidbody.velocity = transform.up * m_Speed;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //controls = Controls.down;
            m_Rigidbody.velocity = -transform.up * m_Speed;
        }
    }

    private void Move()
{
    lastPos = transform.position;
    Vector3 nextPos = Vector3.zero;

        if (controls == Controls.left)
        {
            nextPos = Vector3.left;
        }

        else if (controls == Controls.right)
        {
            nextPos = Vector3.right;
        }

        else if (controls == Controls.up)
        {
            nextPos = Vector3.up;
        }
        
        else if (controls == Controls.down)
        {
            nextPos = Vector3.down;
        }

    transform.position += nextPos * step;
    MoveTail();
    }

    void MoveTail()
    {
    for (int i = 0; i < tail.Count; i++)
    {
        Vector3 temp = tail[i].position;
        tail[i].position = lastPos;
        lastPos = temp;
    }
    if (growingPending) CreateTail();
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Move();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Food")
        {
            print("colision");
            growingPending = true;
            Destroy(collision.gameObject);
            int randomValue = Random.Range(0, cuboPruebas.Length);
            cuboPruebas[randomValue].GetComponent<Spawn>().CreateCubes();
            contador = contador + 1;
            contadorT.text = "Marcador " + contador;

        }
        else if (tag == "Limit" || tag == "Tail" && name != "Tail0")
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (tag == "win")
        {
            if (contador == 0)
            {
                SceneManager.LoadScene("lol");
            }
            if (contador > 1 && contador < 10)
            {
                SceneManager.LoadScene("normal");
            }

            if (contador > 10 && contador < 15)
            {
                SceneManager.LoadScene("chayanne");
            }

            if (contador > 15)
            {
                SceneManager.LoadScene("mamada");
            }

        }
    }
  

        void CreateTail()
        {
            GameObject newTail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
            newTail.name = "Tail" + tail.Count;
            tail.Add(newTail.transform);
            growingPending = false;
        }

    void CreateFood()
        {

            Vector2 pos = new Vector2 (Random.Range(leftSide.transform.position.x, rightSide.transform.position.x), Random.Range(topSide.transform.position.y, bottomSide.transform.position.y));
            Instantiate(foodPrefab, pos, Quaternion.identity);
        }
    
}

