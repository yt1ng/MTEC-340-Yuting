using UnityEngine;
using System.Collections;
using TMPro;

public class HealthBar : MonoBehaviour {

	public int maxHealth = 100;
	
	public TMP_Text healthText;
	
	private int currentHealth = 0;
	void Awake(){
		currentHealth = maxHealth;
		healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
	}

	public void Hurt(int damage)
	{
		currentHealth -= damage;
		healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
