using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public int id;
    // 0 Finger Fins: Shotgun Fire Rate increased by 100%. Increase Recoil by 50%
    // 1 Razorfin Bullets: Shotgun Bullets pierce through fish. Recoil increased by 50%
    // 2 Unity Accurate Fluid Simulation: Reduces shotgun energy cost by 75%. Reduce dash distance by 25%
    //3 A Single Wire: Shotgun will now become an automatic weapon. Reduce dash distance by 50%
    //4 Blob-Split Bullets: Shotgun will now fire 50% more bullets. Reduce dash distance by 25%
    //5 Fish N’ Crits - Single-Shot bullets gain a 2% chance to shoot a HYPERBULLET that does 10x its normal damage, Recoil Increased by 25%
    //6 Smaller Flippers - Dash Energy Cost reduced by 50%, Recoil Increased by 25%
    //7 Polygon Collider 2D - Hitbox reduced by 15%, Increase Damage Taken by 25%
    //8 An Exact Experimental Caliber - Single-Shot Gun Damage increased by 100%, Single-shot Fire-rate decreased by 75%
    //9 Sword Tipped Fish: Single-Shot bullets now pierce through fish, Recoil Increased by 50%
    //10 Goldfish Memory: Combo doesn’t break upon first hit. Damage Taken increased by 50%
    //11 Dagon’s Organ: 50% increase to fish spawns, 50% decrease to fish health.
    //12 Carcharias’s Organ: Damage increases by 1% the higher your current combo is. Decreases default damage by 25%.
    //13 Sach’s Organ: Energy Regen speed increases by 1% the higher your current combo is. Decreases default energy regen by 25%.
    //14 Salar’s Organ: Decrease recoil by 1% the higher your current combo is, Increases default recoil by 25%
    //15 Torpediniformes’s Organ: Increase Max Energy by 1% the higher your current combo is, decrease default max energy by 25%
    //16 Unstoppable Force: All Weapons and Dashing do not cost Energy as long as combo counter remains above 100.
    //17 Conch Plating: When braking, reduce all damage taken by 75%. Decrease max speed and dash distance by 25%
    //18 Man O War Nematocyst: When braking, all fish that damage you count as a kill. Braking will break your combo.
    //19 Starfin Burster: When braking, 8 single-shot bullets will fire out of your shell. Increase braking cost by 25%
    //20 Immovable Object: Increase Max Energy by 200%. Reduce Max Speed by 50%.
    //21 Cod Cooker - Deal 15% more damage against cod. Fish Spawn Rate increased by 25%.
    //22 Swordfish Slayer - Deal 15% more damage against swordfish. Fish Spawn Rate increased by 25%.
    //23 Angler Annihilator - Deal 15% more damage against anglerfish. Fish Spawn Rate increased by 25%.
    //24 Blobfish Butcher - Deal 15% more damage against blobfish. Fish Spawn Rate increased by 25%.
    

    //1125 Punyama’s Blessing, God of Cowardice - Forfeit all upgrades to reset the difficulty back to the beginning.
    //1126 Francis’s Blessing, God of Gluttony - Forfeit all non-blessing-upgrades to bear two blessings at once.
    //1127 Ethan’s Blessing, God of Chance - Forfeit all upgrades to possess a random blessing each time you upgrade.
    //1128 Kaname’s Blessing, Goddess of Salvation - Forfeit all upgrades. Gain the ability to cheat death whenever a certain COMBO threshold is met. Threshold increases every revive.
    //1129 Megumin’s Blessing, Goddess of Inconsistency - Every kill has a slim chance to kill (almost) every fish on screen. Forfeit all upgrades.
    //1130 Orcinus Orca, Curse of Swordfish - Forfeit all upgrades to completely erase Swordfish from existence.
    //1131 Squalus Acanthias, Curse of Cod - Forfeit all upgrades to completely erase Cod from existence.
    //1132 Homo Sapien, Curse of Anglerfish - Forfeit all upgrades to completely erase Anglerfish from existence.
    //1133 Humanum Exitium, Curse of Blobfish - Forfeit all upgrades to completely erase Blobfish from existence.


    public void UpgradePicked()
    {
        print("told her get a friend");
    }
}
