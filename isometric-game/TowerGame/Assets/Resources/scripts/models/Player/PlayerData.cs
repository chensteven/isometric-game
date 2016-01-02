using UnityEngine;
using System.Collections;

/// <summary>
/// Game data for the player. Keeps record, and maintains the player's
/// level, experience, gold, minerals, and oil.
/// </summary>
public static class PlayerData
{

    private static int gold, minerals, oil, 
        level, experience, currentLvlExp;

    /// <summary>
    /// Static constructor for PlayerData.
    /// </summary>
    static PlayerData() {
        gold = Globals.InitialGold;
        minerals = Globals.InitialMinerals;
        oil = Globals.InitialOil;
        level = 0;
        experience = currentLvlExp = 0;
    }

    /// <summary>
    /// Increases the player's level by 1.
    /// </summary>
    public static void levelUp()
    {
        level++;
    }

    /// <summary>
    /// Increments the current player's experience by the given amount, and levels up the player
    /// if there is enough experience.
    /// </summary>
    /// <param name="exp">Experience to add</param>
    public static void experienceUp(int exp) {
        currentLvlExp += exp;
        experience += exp;

        int remainder;
        if (isCurLvlExpFull(out remainder)) {
            currentLvlExp = remainder;
            levelUp();
        }
    }

    /// <summary>
    /// Returns true if the experience points earned for the current level is
    /// enough to level up, otherwise returns false. The remainder is specified
    /// by the out parameter.
    /// </summary>
    /// <returns></returns>
    private static bool isCurLvlExpFull(out int carry) {
        var nextLvlExpReq = Globals.NextLevelExpRequirement(level);
        carry = nextLvlExpReq - currentLvlExp;
        return currentLvlExp >= nextLvlExpReq;
    }

}
