using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour, ITakable, IDrop
{
    public ItemData SO;
    public Rigidbody rb;

    public void Take(Vector3 takePosition)
    {
        if(takePosition == null)
            takePosition = transform.position + new Vector3(0,1,0);

        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);

        //StartCoroutine(MoveToTarget(takerPosition, 1));
    }

    public void Drop()
    {
        Vector3 velocity = new Vector3(Random.Range(-1,1), 0, Random.Range(-1,1));
        velocity = velocity.normalized * 3;

        rb.AddForce(velocity, ForceMode.VelocityChange);
    }

    IEnumerator MoveToTarget(Vector3 endPosition, float timeToMove)
    {
        Vector3 startPosition = transform.position;
        float movingTime = 0;
        if(movingTime < timeToMove)
        {
            movingTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startPosition, endPosition, timeToMove / movingTime);

            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
    }



}
