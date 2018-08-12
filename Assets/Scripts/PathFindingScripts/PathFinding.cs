using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour {

    [SerializeField]
    private GameObject movementStoperImage;

    PathRequestManager requestManager;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector2 startPos, Vector2 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    // Başlangıç ve hedef noktalarımıza göre yolumuzu buluyoruz.
    IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;

        //Node larımızı grid scriptimizde olan Node ların world pozisyonunu bulan scriptimize atarak pozisyonlarını alıyoruz.
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            // Listelerimizi oluşturuyoruz.
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            //Başlangıç Node'umuzu openSet listemize ilk eleman olarak atıyoruz.
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                //Yeni currentNode'umuz hedefimiz mi diye kontrol ediyoruz.
                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Yolumuz şu sürede bulundu : " + sw.ElapsedMilliseconds + "ms");
                    pathSuccess = true;
                    break;
                }
                //Tüm komşularımıza bakıp hangisine daha yakın olduğumuzu buluyoruz.
                foreach (Node neighbours in grid.GetNeighbours(currentNode))
                {
                    if (!neighbours.walkable || closedSet.Contains(neighbours))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbours);
                    if (newMovementCostToNeighbour < neighbours.gCost || !openSet.Contains(neighbours))
                    {
                        neighbours.gCost = newMovementCostToNeighbour;
                        neighbours.hCost = GetDistance(neighbours, targetNode);
                        neighbours.parent = currentNode;
                        //En yakın bulduğumuz komşu zaten openSet dizisinde mi diye bakıyoruz.
                        if (!openSet.Contains(neighbours))
                        {
                            openSet.Add(neighbours);
                        }
                        else
                            openSet.UpdateItem(neighbours);
                    }
                }
            }
        }
        else
            movementStoperImage.SetActive(false);


        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector2[] waypoints = SimplifyPath(path, endNode);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector2[] SimplifyPath(List<Node> path, Node endNode)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1].worldPosition);
            }
            directionOld = directionNew;
        }
        if (waypoints.Count < 1)
        {
            waypoints.Add(endNode.worldPosition);
        }
        return waypoints.ToArray();
    }

    //Verilen 2 düğüm arasındaki mesafeyi bulmaya yarayan metod.
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        // 14y + 10*(x-y)
        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
