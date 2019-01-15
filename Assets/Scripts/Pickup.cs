using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public bool doubleWeapon; 
    public int ammoAmount;
    public int healAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (gameObject.CompareTag("AmmoPickup"))
            {
                if (gameObject.name.Contains("Torrent"))
                {
                    TorrentSpawner torrentSpawner = col.GetComponent<TorrentSpawner>();
                    torrentSpawner.IncreaseTorrent(ammoAmount);
                }
            } else if (gameObject.CompareTag("HealthPickup")) 
            {
                Player player = col.GetComponent<Player>();
                player.Heal(healAmount);
            } else
            {
                Player player = col.GetComponent<Player>();
                player.ChangeWeapon(weaponToEquip, doubleWeapon);
            }

            Destroy(gameObject);
        }
    }
}
