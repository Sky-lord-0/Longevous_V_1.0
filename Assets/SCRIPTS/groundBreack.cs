using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTilemap : MonoBehaviour
{
    public Tilemap tilemap;

    [Header("Tempo para quebrar")]
    public float breakTime = 0.3f;

    // controla quais blocos já estão quebrando
    private HashSet<Vector3Int> breakingTiles = new HashSet<Vector3Int>();

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            Vector3Int cellPos = tilemap.WorldToCell(hit.point);

            // se existe tile e ele ainda não começou a quebrar
            if (tilemap.HasTile(cellPos) && !breakingTiles.Contains(cellPos))
            {
                StartCoroutine(BreakTile(cellPos));
            }
        }
    }

    IEnumerator BreakTile(Vector3Int pos)
    {
        // marca esse bloco como quebrando
        breakingTiles.Add(pos);

        yield return new WaitForSeconds(breakTime);

        // remove só esse bloco
        tilemap.SetTile(pos, null);

        // libera ele da lista
        breakingTiles.Remove(pos);
    }
}