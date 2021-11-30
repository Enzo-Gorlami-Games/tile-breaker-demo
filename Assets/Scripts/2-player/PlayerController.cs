using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private const float EPSILON = 0.05f;

    [SerializeField] private float initSpeed = 5f;
    [SerializeField] private float miningSpeed = 1f;
    [SerializeField] private Transform movePoint;

    private float speed;

    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] BreakableTiles breakableTiles = null;


    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }


    // Use this for initialization
    void Start()
    {
        movePoint.parent = null;
        speed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed);
        if(Vector3.Distance(transform.position, movePoint.position) < EPSILON)
        {
            if(Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Math.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }

        }
        Vector3Int movePointCellPosition = tilemap.WorldToCell(movePoint.position);
        TileBase tileOnNewPosition = TileOnPosition(movePoint.position);
        if (allowedTiles.Contains(tileOnNewPosition))
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
            speed = initSpeed;
        }
        else if (breakableTiles.Contains(tileOnNewPosition))
        {
            movePoint.position = transform.position;
            if (Input.GetKey(KeyCode.Space))
            {
                tilemap.SetTile(movePointCellPosition, breakableTiles.GetTileToBreakTo());
                speed = miningSpeed;
            }
        }
        else
        {
            movePoint.position = transform.position;
        }
    }
}
