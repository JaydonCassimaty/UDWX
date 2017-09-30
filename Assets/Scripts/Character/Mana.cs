using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Mana : NetworkBehaviour {

    public const float maxMana = 100;
		public float regen = .1f;

    [SyncVar(hook = "OnChangeMana")]
    public float currentMana = maxMana;

    public RectTransform manaBar;

		public void Update()
		{
			if (currentMana > maxMana)
			{
				currentMana = maxMana;
			}

			if (currentMana < maxMana)
			{
				currentMana += regen;
			}

			if (currentMana < 0)
			{
				currentMana = 0;
			}
		}

    public void UseMana(float amount)
    {
        if (!isServer)
            return;

        currentMana -= amount;
        if (currentMana <= 0)
        {
					Debug.Log("Mana Left: " + currentMana, gameObject);
        }
    }

		public float ReturnMana()
		{
			return currentMana;
		}

    void OnChangeMana(float currentMana)
    {
        manaBar.sizeDelta = new Vector2(currentMana, manaBar.sizeDelta.y);
    }
}
