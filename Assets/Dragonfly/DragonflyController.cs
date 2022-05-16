using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyController : MonoBehaviour
{
    private Vector3 lookPosition;
    private GameObject FlyNet;
    private float initializationTime;


    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        FlyNet = GameObject.FindGameObjectsWithTag("FlyNet")[0];
        gameObject.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.Euler(0, 0, 0));
        lookPosition = GetRandomPosition();
        gameObject.transform.LookAt(2 * transform.position - lookPosition);
    }

    void Update()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
        if(timeSinceInitialization > 10)
        {
            Destroy(gameObject);
        }

        Move(lookPosition);
        if (transform.position.Equals(lookPosition))
        {
            lookPosition = GetRandomPosition();
            gameObject.transform.LookAt(2 * transform.position - lookPosition);
        }
    }

    private void Move(Vector3 endPosition)
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPosition, 30 * Time.deltaTime);
    }

    public IEnumerator Catch()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }

    private Vector3 GetRandomPosition()
    {
        int x = gameObject.transform.position.x < 0 ? Random.Range(10, 40) : -1 * Random.Range(10, 40);
        int y = Random.Range(10, 20);
        int z = Random.Range(10, 25);
        return new Vector3(x, y, z);
    }
}
