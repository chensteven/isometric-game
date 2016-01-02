using UnityEngine;
using System.Collections;

public static class Globals {

    #region Player Data

    public const int MaxLevel = 20;

    public const int InitialGold = 5;

    public const int InitialMinerals = 0;

    public const int InitialOil = 0;

    /// <summary>
    /// Returns the amount of experience points required to level up for the current level.
    /// </summary>
    /// <param name="currentLvl">Player's current level</param>
    /// <returns></returns>
    public static int NextLevelExpRequirement(int currentLvl)
    {
        return 2 * currentLvl + 20;
    }

    #endregion

    #region Camera

    public const float MaxCameraZoom = 20;

    public const float MinCameraZoom = 0.2f;

    public const float CameraZoomSpeed = 0.1f;

    public const float CameraPanSpeed = 0.1f;

    #endregion




}
