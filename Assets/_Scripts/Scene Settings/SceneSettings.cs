﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SceneSettings", menuName = "Scene/Settings")]
[System.Serializable]
public class SceneSettings : ScriptableObject
{
    [Header("Scene Configuration")]
    public Scene scene;
    public SoundClip activeSoundClip;

    [Header("Scoreboard Labels")]
    public bool scoreLabelEnabled;
    public bool livesLabelEnabled;
    public bool highScoreLabelEnabled;

    [Header("Scene Buttons")]
    public bool endLabelActive;
    public bool restartButtonActive;
    public bool startButtonActive;
    public bool startLabelActive;
}
