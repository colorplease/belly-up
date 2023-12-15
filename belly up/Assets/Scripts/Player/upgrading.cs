using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrading : MonoBehaviour
{
    public List<GameObject> upgradeCards = new List<GameObject>();
    public List<GameObject> godhoodCards = new List<GameObject>();
    public List<GameObject> endgameCards = new List<GameObject>();
    [SerializeField] gamemanager GAMING;
    [SerializeField]List<GameObject>activeCards = new List<GameObject>();    
    public void GenerateCards()
    {
        int i = 0;
        while (i < 3)
        {
            List<GameObject> tempCards = upgradeCards;
            var randomCardNumber = Random.Range(0, tempCards.Count);
            Instantiate(tempCards[randomCardNumber], Vector3.zero, Quaternion.identity, gameObject.transform);
            tempCards.RemoveAt(randomCardNumber);
            i++;
        }
    }

    public void ApplyUpgrade(int upgradeID)
    {
        switch(upgradeID)
        {
            case 0: // 0 Finger Fins: Shotgun Fire Rate increased by 100%. Increase Recoil by 50%
            GAMING.shotgunFireRate = 2;
            GAMING.shotgunKnockback = 1.5f;
            break;
        }
        for (int i = 0; i < upgradeCards.Count; i++)
        {
            if (upgradeCards[i].GetComponent<UpgradeCard>().id == upgradeID)
            {
                upgradeCards.Remove(upgradeCards[i]);
            }
        }
        GAMING.NOTUPGRADE();
    }
}
