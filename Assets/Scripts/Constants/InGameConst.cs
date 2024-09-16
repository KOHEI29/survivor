public class InGameConst
{
    //インゲームの状態
    public enum State
    {
        DEFAULT = -1,
        Play,
        Skill,
        GameOver
    }
    //スキルタイプ
    public enum SkillType
    {
        DEFAULT = -1,
        BLINK,
        MELEE,
    }
    //スキル枠数
    public const int SkillInventoryCount = 4;
    //スキル枠描画用
    public const float SkillInventoryLeftX = 50f;
    public const float SkillInventoryOffsetX = 95f;
    //スキル入手画面で出る選択肢の数
    public const int SkillSelectCount = 2;
}