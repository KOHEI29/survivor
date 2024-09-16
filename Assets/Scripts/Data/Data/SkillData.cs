using UnityEngine;

public class SkillData : ISkillData
{
    public SkillData(int mstId,
                    int executerId,
                    Sprite icon,
                    string name,
                    string description,
                    int level,
                    int stackMax,
                    float coolTime)
    {
        MstId = mstId;
        ExecuterId = executerId;
        Icon = icon;
        Name = name;
        Description = description;
        Level = level;
        StackMax = stackMax;
        StackCurrent = stackMax;
        CoolTimeMax = coolTime;
        CoolTimeCurrent = StackMax == StackCurrent ? 0f : CoolTimeMax;
    }
    public SkillData(MstSkillData mst)
    {
        MstId = mst.Id;
        ExecuterId = mst.ExecuterId;
        Icon = mst.Icon;
        Name = mst.Name;
        Description = mst.Description;
        Level = 1;
        StackMax = mst.StackMax;
        StackCurrent = StackMax;
        CoolTimeMax = mst.CoolTime;
        CoolTimeCurrent = StackMax == StackCurrent ? 0f : CoolTimeMax;
    }
    public SkillData(MstSkillData mst, int level)
    {
        MstId = mst.Id;
        ExecuterId = mst.ExecuterId;
        Icon = mst.Icon;
        Name = mst.Name;
        Description = mst.Description;
        Level = level;
        StackMax = mst.StackMax;
        StackCurrent = StackMax;
        CoolTimeMax = mst.CoolTime;
        CoolTimeCurrent = StackMax == StackCurrent ? 0f : CoolTimeMax;
    }
    public int MstId { get; set; }
    public int ExecuterId { get; set; }
    public Sprite Icon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int StackMax { get; set; }
    public int StackCurrent { get; set; }
    public float CoolTimeMax { get; set; }
    public float CoolTimeCurrent { get; set; }
}
public interface ISkillData
{
    int MstId { get; }
    int ExecuterId { get; }
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
    int Level { get; }
    int StackMax { get; }
    int StackCurrent { get; }
    float CoolTimeMax { get; }
    float CoolTimeCurrent { get; }
}