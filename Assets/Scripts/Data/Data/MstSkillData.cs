using System;
using UnityEngine;

[Serializable]
public record MstSkillData
{
    public int Id;
    public int ExecuterId;
    public Sprite Icon;
    public string Name;
    public string Description;
    public int StackMax;
    public float CoolTime;
}