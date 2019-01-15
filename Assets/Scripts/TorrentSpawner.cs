using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrentSpawner : MonoBehaviour
{
    public GameObject torrent;
    public float timeToDie;
    public float timeBtwSpawn;
    private int torrentsAmount;

    private float spawnTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= spawnTime)
            {
                if (torrentsAmount > 0)
                {
                    spawnTime = Time.time + timeBtwSpawn;
                    torrentsAmount--;
                    GameObject newTorrent = Instantiate(torrent, transform.position, transform.rotation);
                    Destroy(newTorrent, timeToDie);
                }
            }
        }
    }

    internal void IncreaseTorrent(int amount)
    {
        torrentsAmount += amount;
    }
}
