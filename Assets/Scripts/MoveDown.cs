using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public static float fallingSpeed = 4;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(0, 0, Random.Range(-20, 20));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.isGamePlayable)
        {
            transform.position = transform.position + new Vector3(0, -1 * fallingSpeed * Time.deltaTime, 0);
        }
    }
}
