using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	
	
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;
	public Texture2D stickerTexture;
	public GameObject stickerPrefab;
	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update () {
		if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer))
		{
			if(decalHitWall){

				if (hit.transform.tag == "enemy")
				{
					hit.transform.GetComponent<HealthBar>().Hurt(2);
				}
				
				Vector3 hitNormal = hit.normal;
				Quaternion rotation = Quaternion.FromToRotation(Vector3.zero, hitNormal);
				
				GameObject sticker = Instantiate(stickerPrefab, hit.point, rotation);
				
				SpriteRenderer renderer = sticker.transform.AddComponent<SpriteRenderer>();
				renderer.sprite = Sprite.Create(stickerTexture, new Rect(0, 0, stickerTexture.width, stickerTexture.height), new Vector2(0.5f, 0.5f));
				renderer.transform.position = hit.point;
				renderer.transform.rotation = rotation;
				
				renderer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			}		
			Destroy(gameObject);
		}
		Destroy(gameObject, 0.1f);
	}

	
}
