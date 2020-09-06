using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        sr.color = Color.green;
        player.closestTower = this.gameObject;
    }

    private void OnMouseExit()
    {
        sr.color = Color.white;
        player.closestTower = null;
        player.isClicked = false;
    }

    private void OnMouseDown()
    {
        player.isClicked = true;
    }

    private void OnMouseUp()
    {
        player.isClicked = false;
    }
}
