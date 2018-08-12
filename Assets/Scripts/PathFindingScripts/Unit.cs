using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    #region Variables
    public Vector2 myTarget;
    public float speed = 5;
    private Vector2[] path;
    private int targetIndex;
    #endregion

    //Askerlerimize yolunu bulup ilerleme komutunu başlattığımız metod.
    public void GoToPosition()
    {
        PathRequestManager.RequestPath(transform.position, myTarget, OnPathFound);
    }

    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];

        targetIndex = 0;

        while (true)
        {
            if ((Vector2)transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    if(GameObject.Find("MovementStoperImage"))
                        GameObject.Find("MovementStoperImage").SetActive(false);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed*Time.deltaTime);
            yield return null;
        }
    }

    //Askerin gideceği yolu çizmek için.
    private void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
