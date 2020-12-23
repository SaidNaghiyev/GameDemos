using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLine : MonoBehaviour
{
    public float minimumDistanceToFollow = 0.1f;
    private float distance = 0;
    public Transform PlayerTransform = null;
    private Vector3 pos;
    public float speed = 10f;
    public void moveTowardsPath(List<Vector3> list, float time)
    {
        StartCoroutine(delayMoveTowardsPath(list, time));
    }
    IEnumerator delayMoveTowardsPath(List<Vector3> list, float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < list.Count; i++)
        {

            distance = Vector3.Distance(PlayerTransform.transform.position, list[i]);
            while (distance > minimumDistanceToFollow)
            {
                Vector3 _lis = list[i];
                _lis.y = PlayerTransform.transform.position.y;
                pos = Vector3.MoveTowards(PlayerTransform.transform.position, _lis, speed * Time.deltaTime);
                PlayerTransform.transform.LookAt(pos);
                PlayerTransform.transform.position = pos;
                distance = Vector3.Distance(PlayerTransform.transform.position, _lis);
                yield return null;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishPoint"))
        {
            //UI won
            Manager.instance.winLevel();
        }
        else
        {
            if (other.CompareTag("Obstacle"))
            {
                //DIE
                StopAllCoroutines();
            }
        }
    }
}
