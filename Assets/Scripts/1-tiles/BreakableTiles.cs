using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class BreakableTiles : MonoBehaviour
{
    [SerializeField] TileBase[] breakableTiles = null;
    [SerializeField] TileBase tileToBreakTo;

    public bool Contains(TileBase tile)
    {
        return breakableTiles.Contains(tile);
    }

    public TileBase[] GetBreakable()
    {
        return breakableTiles;
    }

    public TileBase GetTileToBreakTo()
    {
        return tileToBreakTo;
    }
}
