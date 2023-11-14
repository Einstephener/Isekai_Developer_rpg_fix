public class Monster
{
    public string MonsterName { get; } //몬스터 이름
    public string HowHarder { get; } //출현 난이도
    public int MonsterAtk { get; } //몬스터 공격력
    public int MonsterHp { get; set; } //몬스터 체력
    public int MonsterExperience { get; } //처치 경험치
    public int MonsterGold { get; } //처치 골드

    public Monster(string _monsterName, string howHarder, int monsterAtk, int monsterHp, int monsterExperience, int monsterGold)
    {
        MonsterName = _monsterName;
        HowHarder = howHarder;
        MonsterAtk = monsterAtk;
        MonsterHp = monsterHp;
        MonsterExperience = monsterExperience;
        MonsterGold = monsterGold;
    }
}
