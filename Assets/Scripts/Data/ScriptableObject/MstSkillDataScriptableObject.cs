using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MasterData/MstSkillDataScriptableObject")]
public class MstSkillDataScriptableObject : ScriptableObject
{
    [SerializeField]
    private List<MstSkillData> _data;
    public IReadOnlyList<MstSkillData> Data => _data;
}
