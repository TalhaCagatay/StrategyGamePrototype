using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    #region Variables
    public bool displayGridGizmos;
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    #endregion

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    //Gridimizi oluşturuyoruz.
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        //Düğümlerimize verdiğimiz değerlere göre gridimizi 2 boyutlu olarak oluşturuyoruz.
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                //Oluşan düğüme bakıp overlap metoduyla çarptığı layera göre yürünüp yürünmeyecek bir yer olduğuna bakıyoruz.
                bool walkable = (Physics2D.OverlapCircle(worldPoint, nodeRadius, unWalkableMask) == null);

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
    //Buldunduğumuz düğümün etrafında yer alan komşularını bulan metod.
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        //Bulunduğumuz düğümün etrafına 3 e 3 lük tarayarak komşularını arıyoruz.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Kendimizi komşu olarak görmüyoruz.
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    //Node'ların (Düğümlerin) worldPosition'larını bulmamızı sağlayan metod.
    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        //(x+y/2)/y = x/y+.5f ==> x/y + .5f i hesaplamak daha iyi bir optimizasyon vericektir bize.

        //float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        //float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

        float percentX = worldPosition.x / gridWorldSize.x + .5f;
        float percentY = worldPosition.y / gridWorldSize.y + .5f;
        
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    //Scene ekranımızda hedefimize giden yoldaki wayPointleri çizdiğimiz metod.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null && displayGridGizmos)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - .1f));
                }
            }
    }

}
