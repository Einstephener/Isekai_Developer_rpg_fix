
//캐릭터 클래스
public class Character
{
    public string Name { get; } //캐릭터 이름
    public string Job { get; } //캐릭터 직업
    public int Atk { get; private set; } //캐릭터 공격력
    public int Def { get; private set; } //캐릭터 방어력
    public int Hp { get; set; } //캐릭터 체력
    public int Experience { get; set; }

    public int _gold; //소유 골드
    public List<Item> Inventory { get; } = new List<Item>(); // 인벤토리 리스트 추가

    private int _level; // private 필드로 변경
    public int Level
    {
        get { return _level; }
        set
        {
            // Level이 변경될 때마다 레벨업 로직 수행
            for (int i = _level + 1; i <= value; i++)
            {
                Atk += 2 * (i - 1); // 레벨업 할 때마다 Atk가 2*(Level-1)만큼 증가
                Def += 2 * (i - 1); // 레벨업 할 때마다 Def가 2*(Level-1)만큼 증가
            }

            _level = value;
        }
    }
    public int Gold //골드 증감
    {
        get { return _gold; }
        set { _gold = value; }
    }


    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        _gold = gold;

    }

    //인벤토리에 아이템 추가
    public void AddToInventory(Item item)
    {
        //아이템이 처음 구매될때 isEquipped가 false로 고정
        item.IsEquipped = false;
        //인벤토리에 아이템 추가
        Inventory.Add(item);
    }
    
    //장비 장착
    public void EquipItem(int input)
    {
        //착용하려는 장비가 인벤토리 안에 있는 장비인가
        if (input > 0 && input <= Inventory.Count)
        {
            
            //배열은 0부터 시작하기 때문에 input-1
            var item = Inventory[input - 1];

            if (!item.IsEquipped) //착용여부가 false일때
            {
                
                item.IsEquipped = true;//착용여부를 true로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 장착했습니다.");
                //착용된 아이템의 스탯을 캐릭터 스탯에 적용
                
                Atk += item.ItemAtk;
                Def += item.ItemDef;
            }
            else
            {
                item.IsEquipped = false;//착용여부를 false로 바꿈
                Console.WriteLine($" [{item.ItemName}]을(를) 해제했습니다.");
                //착용 해제된 아이템의 스탯을 캐릭터 스탯에 적용
                Atk -= item.ItemAtk;
                Def -= item.ItemDef;
            }
        }
        else
        {
            Console.WriteLine(" 잘못된 인덱스입니다.");
        }
        
    }
}
