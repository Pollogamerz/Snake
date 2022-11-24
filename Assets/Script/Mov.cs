using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public int GAP = 10;

    public GameObject BodyPrefabs;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionHisotory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        float SteerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * SteerDirection * SteerSpeed * Time.deltaTime);

        PositionHisotory.Insert(0, transform.position);

        int index = 0;
    }

    
    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefabs);
        BodyParts.Add(body);
    }
}

