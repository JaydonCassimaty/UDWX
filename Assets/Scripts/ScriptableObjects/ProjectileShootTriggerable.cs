using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProjectileShootTriggerable : NetworkBehaviour {

    public GameObject projectile;                      // Rigidbody variable to hold a reference to our projectile prefab
    public Transform bulletSpawn;                           // Transform variable to hold the location where we will spawn our projectile
    public float projectileForce = 1f;                  // Float variable to hold the amount of force which we will apply to launch our projectiles

    public GameObject bulletPrefab;

    public void CmdLaunch()
    {
      var mana = GetComponent<Mana>();
  		var spellCost = 10;

  		if (mana.ReturnMana() > spellCost)
  		{
  			mana.UseMana(spellCost);
      //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
      var clonedBullet = Instantiate(projectile, new Vector3(bulletSpawn.transform.position.x,bulletSpawn.transform.position.y + 1f,bulletSpawn.transform.position.z) , Quaternion.identity);

      //Add force to the instantiated bullet, pushing it forward away from the bulletSpawn location, using projectile force for how hard to push it away
      // clonedBullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.up * projectileForce);
      clonedBullet.GetComponent<Rigidbody>().velocity = clonedBullet.transform.forward * 6;

      // Spawn the bullet on the Clients
      NetworkServer.Spawn(clonedBullet);

  		// Destroy the bullet after 2 seconds
  		Destroy(clonedBullet, 2.0f);
      }
    }
}
