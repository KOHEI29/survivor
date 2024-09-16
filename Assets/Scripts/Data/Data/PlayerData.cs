public class PlayerData : IPlayerData
{
    public PlayerData(int level,
                    int exp,
                    int reqExp,
                    float time,
                    int hpCurrent,
                    int hpMax,
                    InGameConst.State state)
    {
        Level = level;
        Exp = exp;
        ReqExp = reqExp;
        Time = time;
        HpCurrent = hpCurrent;
        HpMax = hpMax;
        CurrentState = state;
    }
    public int Level { get; set; }
    public int Exp { get; set; }
    public int ReqExp { get; set; }
    public float Time { get; set; }
    public int HpCurrent { get; set; }
    public int HpMax { get; set; }
    public InGameConst.State CurrentState { get; set; }
}
public interface IPlayerData
{
    int Level { get; }
    int Exp { get; }
    int ReqExp { get; }
    float Time { get; }
    int HpCurrent { get; }
    int HpMax { get; }
    InGameConst.State CurrentState { get; }
}