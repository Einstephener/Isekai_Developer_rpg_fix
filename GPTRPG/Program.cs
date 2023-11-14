using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.IO;


internal class Program
{

    //아이템 리스트
    private static List<Item> items = new List<Item>();


    //아이템들 선언
    private static Item ring1;
    private static Item ring2;
    private static Item sword1;
    private static Item sword2;
    private static Item sword3;
    private static Item armor1;
    private static Item armor2;
    private static Item armor3;


    private static Character player1;


    public static int Damage;


    public int level = player1.Level;
    public static int experienceCap;


    public static List<LevelRange> levelRanges = new List<LevelRange>();

    //필요레벨업 수치
    public void Start()
    {
        levelRanges = new List<LevelRange>
        {
        new LevelRange { startLevel = 1, endLevel = 5, experienceCapIncrease = 10 },
        new LevelRange { startLevel = 5, endLevel = 10, experienceCapIncrease = 50 },
        new LevelRange { startLevel = 10, endLevel = 50, experienceCapIncrease = 100 },
        new LevelRange { startLevel = 50, endLevel = 100, experienceCapIncrease = 500 }
        };
        // 리스트 0번째의 경험치량 불러오기
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    //경험치 추가
    public static void IncreaseExperience(Character player1, int amount)
    {
        // 들어오는 경험치 더하기
        player1.Experience += amount;

        // 레벨업 하는지 확인
        LevelUpChecker(player1);
    }

    //경험치가 필요경험치 수치를 전부 채우면 레벨업
    public static void LevelUpChecker(Character player1)
    {
        // 만약 경험치가 꽉찬다면?
        if (player1.Experience >= experienceCap)
        {
            // 레벨 업
            player1.Level++;
            // 경험치 줄여주기
            player1.Experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {

                // 만약 현재 레벨이 리스트안에 있는 startLevel보다 크고 endLevel보다 적으면 실행하고 빠져나오기
                // 간단하게 0번 리스트에 조건이 맞으면 나오고 초기화

                if (player1.Level >= range.startLevel && player1.Level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
        }
    }

    //시작
    static void Main(string[] args)
    {
        //게임이 실행되면 데이터 먼저 세팅
        GameDataSetting();
        //시작화면으로 이동
        StartScene();
    }

    //게임 데이터 준비
    static void GameDataSetting()
    {
        //콘솔창 이름 변경
        Console.Title = "ISEKAI DEVELOPER";

        // 캐릭터 정보 세팅
        player1 = new Character("Dobby", "용사", 1, 10, 10, 100, 1500);

        // 아이템 정보 세팅
        ring1 = new Item("수호 반지", 0, 500, 3000, 0, "고대의 반지. 방어력을 늘려준다.");
        ring2 = new Item("마법 반지", 500, 0, 3000, 0, "고대의 반지. 공격력을 늘려준다.");
        sword1 = new Item("부러진 검", 10, 0, 100, 1, "끝이 부러진 검. 사과를 깎을 수 있을 것 같다.");
        sword2 = new Item("기사의 검", 50, 10, 1000, 1, "중심이 잘 잡힌 검. 떠돌이 기사가 쓰던 검인듯 하다.");
        sword3 = new Item("용사의 검", 700, 50, 5000, 1, "전대 용사가 쓰던 검. 관리가 잘 되어있다.");
        armor1 = new Item("천 갑옷", 0, 10, 100, 2, "허름한 천갑옷. 화살이 박혔던 자국이 있다.");
        armor2 = new Item("사슬 갑옷", 10, 50, 1000, 2, "얇은 사슬 갑옷. 사용감이 있다.");
        armor3 = new Item("판금 갑옷", 100, 700, 5000, 2, "무겁고 두꺼운 갑옷. 대부분의 칼로는 흠집도 안날 것 같다.");
        //리스트에 아이템들 추가
        items.Add(ring1);
        items.Add(ring2);
        items.Add(sword1);
        items.Add(sword2);
        items.Add(sword3);
        items.Add(armor1);
        items.Add(armor2);
        items.Add(armor3);

    }

    //시작화면
    static void StartScene()
    {
        Console.Clear();
        //시작화면
        Console.BackgroundColor = ConsoleColor.Blue; //회색배경
        Console.ForegroundColor = ConsoleColor.White; //흰 글씨
        Console.SetWindowSize(82, 21); //타이틀에 맞춰 화면 크기 변경

        Console.WriteLine("                                                                                  ");
        Console.WriteLine("                ####     ####    ######   ##  ##     ##      ####                 ");
        Console.WriteLine("                 ##     ##  ##   ##       ## ##     ####      ##                  ");
        Console.WriteLine("                 ##     ##       ##       ####     ##  ##     ##                  ");
        Console.WriteLine("                 ##      ####    ####     ###      ######     ##                  ");
        Console.WriteLine("                 ##         ##   ##       ####     ##  ##     ##                  ");
        Console.WriteLine("                 ##     ##  ##   ##       ## ##    ##  ##     ##                  ");
        Console.WriteLine("                ####     ####    ######   ##  ##   ##  ##    ####                 ");
        Console.WriteLine("                                                                                  ");
        Console.WriteLine("  ####     ######   ##  ##   ######   ##        ####    #####    ######   #####   ");
        Console.WriteLine("  ## ##    ##       ##  ##   ##       ##       ##  ##   ##  ##   ##       ##  ##  ");
        Console.WriteLine("  ##  ##   ##       ##  ##   ##       ##       ##  ##   ##  ##   ##       ##  ##  ");
        Console.WriteLine("  ##  ##   ####     ##  ##   ####     ##       ##  ##   #####    ####     #####   ");
        Console.WriteLine("  ##  ##   ##       ##  ##   ##       ##       ##  ##   ##       ##       ####    ");
        Console.WriteLine("  ## ##    ##        ####    ##       ##       ##  ##   ##       ##       ## ##   ");
        Console.WriteLine("  ####     ######     ##     ######   ######    ####    ##       ######   ##  ##  ");
        Console.WriteLine("                                                                                  ");
        Console.WriteLine("                                                                                  ");
        Console.WriteLine("                         Press Any Key to start the game.                         ");
        Console.WriteLine("                                                                                  ");
        Console.Write("                                                                              >>");

        Console.ReadKey();
        ShowPreView();//줄거기로 이동

    }

    //줄거리
    static void ShowPreView()
    {
        Console.Clear();
        //줄거리 설명
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("\t당신은 게임을 개발하던중 갑작스럽게 정신을 잃었다!");
        Console.WriteLine();
        Console.WriteLine("\t눈을 뜬 당신의 앞에 있는 건 상...상태창?!");
        Console.WriteLine();
        Console.WriteLine("\t당신은 마왕을 무찌르면 원래 세계로 돌아갈 수 있다!");
        Console.WriteLine();
        Console.WriteLine("\t퇴근하고 넷플릭스 보고 싶은 당신! 어서 마왕을 무찌르러 가자!");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("\t\tPress 1 to next.");
        Console.Write("\t>>");

        int input = CheckValidInput(1, 1);
        if (input == 1)
        {
            Console.ResetColor(); //배경색 초기화
            ShowVillageFirst(); //마을 입구로 이동      
        }

    }

    //마을 입구
    static void ShowVillageFirst()
    {
        Console.Clear();
        //화면 크기 확장
        Console.SetWindowSize(90, 31);
        //마을 입구
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("68%8QU89        |..|:  _//=========================;.         cce * 88oobbb    ");
        Console.WriteLine("O0896%68Oo    |||. l  /=/===========================;.      C8O8*8Q8P*Ob o8oo ");
        Console.WriteLine("a%0C88i%o   || ,, ' /==/=============================;.   /dOB*9*OLS*UOpugO9*Db ");
        Console.WriteLine("PQ%OO8OO'  ||, '  |.. |  |                       | ||     CO*9O0*89PBCOPL*SOBB*");
        Console.WriteLine("8OUC%CBO%b |||,,, |.. |  |                       | ||     Cgg*bU8*UO*OOd*UOdcbb");
        Console.WriteLine("89Y||//OO  |||||| |.. |  |      ___________      | || ,,,,,, 6O*U  /p   gc*U*dpP ");
        Console.WriteLine("  |||        |||| |:. |  |      |##X##X##||      | || ||||||,    //   /d*uUP*  , ");
        Console.WriteLine(" ,|||,       `||| |:: |  | = -  |XX:XX:XX||      | || ||||||||,   ////_ |   ,,|| ");
        Console.WriteLine(":                   ''|  |      |.::..::.|| =    | || ||||||||||  |||// ,,,|||||");
        Console.WriteLine("::                    |__|______|........||______|_||             |||||  ");
        Console.WriteLine(":::.                               :  :                '        .||||||||   ");
        Console.WriteLine("::::::.                     , .                                  ");
        Console.WriteLine("::%::::::.   ''      .           :   `                                  ");
        Console.WriteLine("::::::%:::::....,   :  . : ` .                  ");
        Console.WriteLine(":%:::::::::%::::::::.........:...:.:...:..:...........................:::::: ");
        Console.WriteLine("::::::%::::::::::%:::::::::::%:::::::::::::::%:::::::::::::::%::::%::::%:::: ");
        Console.WriteLine("`::::::::::%::::::::::::%::::::::::::::%::::::::::::%:::::%::::::::::::::%:: ");
        Console.WriteLine();
        Console.WriteLine(" 마을에 오신 용사님을 환영합니다.");
        Console.WriteLine(" 이곳에서 던전에 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine(" 1. 상태창");
        Console.WriteLine();
        Console.WriteLine(" 2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine(" 3. 마을 시설물 이용");
        Console.WriteLine();
        Console.WriteLine(" 4. 포탈");
        Console.WriteLine();
        Console.Write(" >>");

        int input = CheckValidInput(1, 4);
        switch (input)
        {
            case 1:
                //상태창
                DisplayMyInfo();
                break;

            case 2:
                //인벤토리
                DisplayInventory(player1);
                break;
            case 3:
                //마을창 띄우기
                ShowVillage();
                break;
            case 4:
                //던전 포탈 사용
                DungeonPotal();
                break;
        }
    }

    //마을 시설물
    static void ShowVillage()
    {
        Console.Clear();
        //마을 시설물
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("68%8QU89        |..|:  _//=========================;.         cce * 88oobbb    ");
        Console.WriteLine("O0896%68Oo    |||. l  /=/===========================;.      C8O8*8Q8P*Ob o8oo ");
        Console.WriteLine("a%0C88i%o   || ,, ' /==/=============================;.   /dOB*9*OLS*UOpugO9*Db ");
        Console.WriteLine("PQ%OO8OO'  ||, '  |.. |  |                       | ||     CO*9O0*89PBCOPL*SOBB*");
        Console.WriteLine("8OUC%CBO%b |||,,, |.. |  |                       | ||     Cgg*bU8*UO*OOd*UOdcbb");
        Console.WriteLine("89Y||//OO  |||||| |.. |  |      ___________      | || ,,,,,, 6O*U  /p   gc*U*dpP ");
        Console.WriteLine("  |||        |||| |:. |  |      |##X##X##||      | || ||||||,    //   /d*uUP*  , ");
        Console.WriteLine(" ,|||,       `||| |:: |  | = -  |XX:XX:XX||      | || ||||||||,   ////_ |   ,,|| ");
        Console.WriteLine(":                   ''|  |      |.::..::.|| =    | || ||||||||||  |||// ,,,|||||");
        Console.WriteLine("::                    |__|______|........||______|_||             |||||  ");
        Console.WriteLine(":::.                               :  :                '        .||||||||   ");
        Console.WriteLine("::::::.                     , .                                  ");
        Console.WriteLine("::%::::::.   ''      .           :   `                                  ");
        Console.WriteLine("::::::%:::::....,   :  . : ` .                  ");
        Console.WriteLine(":%:::::::::%::::::::.........:...:.:...:..:...........................:::::: ");
        Console.WriteLine("::::::%::::::::::%:::::::::::%:::::::::::::::%:::::::::::::::%::::%::::%:::: ");
        Console.WriteLine("`::::::::::%::::::::::::%::::::::::::::%::::::::::::%:::::%::::::::::::::%:: ");
        Console.WriteLine();
        Console.WriteLine(" 이용하실 수 있는 마을 시설들입니다.");
        Console.WriteLine();
        Console.WriteLine(" 1. 마을여관");
        Console.WriteLine();
        Console.WriteLine(" 2. 상점");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                //마을 입구로 돌아가기
                ShowVillageFirst();
                break;
            case 1:
                //마을 여관으로 이동
                Motel();
                break;
            case 2:
                // 상점으로 이동
                Shop();
                break;

        }
    }

    //마을 여관
    static void Motel()
    {
        Console.Clear();

        Console.WriteLine("                          (     )                ");
        Console.WriteLine("                           (   )                 ");
        Console.WriteLine("                            (  )                 ");
        Console.WriteLine("                            (  )                 ");
        Console.WriteLine("                            )  )                 ");
        Console.WriteLine("                           (  (                  ");
        Console.WriteLine("                            (_)                  ");
        Console.WriteLine("                    ________[_]________          ");
        Console.WriteLine("                   /|        ______    |         ");
        Console.WriteLine("                  /|_|       |    /|    |        ");
        Console.WriteLine("                 /|___|       |__/  |    |       ");
        Console.WriteLine("                /|_____|       | |[]|     |      ");
        Console.WriteLine("               /|_______|       ||__|      |     ");
        Console.WriteLine("              / XXXXXXXXXX|                  |   ");
        Console.WriteLine("             | _I_II  I__I_|__________________|  ");
        Console.WriteLine("               I_I | I__I_____[]_ | _[]_____I    ");
        Console.WriteLine("               I_II  I__I_____[] _| _[]_____I    ");
        Console.WriteLine("               I II__I I       XXXXXXX      I    ");
        Console.WriteLine("            ~~~~~;   ;~~~~~~~~~~~~~~~~~~~~~~~~   ");
        Console.WriteLine(" 이곳은 마을 여관입니다.");
        Console.WriteLine();
        Console.WriteLine(" 휴식을 취해 체력을 회복할 수 있습니다.");
        Console.WriteLine(" ==========================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 여관에서 체력 회복하기");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                //마을 입구로 돌아가기
                ShowVillageFirst();
                break;
            case 1:
                //체력 회복하기
                Healing(player1);
                break;
        }
    }
    //체력 회복하기
    static void Healing(Character player1)
    {
        Console.Clear();
        Console.WriteLine(" 여관에서 회복하시겠습니까?");
        Console.WriteLine(" 여관에서 회복시 체력이 전부 회복됩니다.");
        Console.WriteLine(" 숙박 비용은 100G입니다.");
        Console.WriteLine();
        Console.WriteLine(" 1. 예");
        Console.WriteLine(" 2. 아니요");
        Console.WriteLine();
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                //회복
                if (player1._gold >= 100)
                {
                    player1._gold -= 100; //골드 차감
                    Console.WriteLine(" 100G를 지불합니다.");
                    player1.Hp = 100; //체력 회복
                    Console.WriteLine();
                    Console.WriteLine(" 체력이 전부 회복되었습니다.");
                    Console.ReadKey();
                    Console.WriteLine(" Press Anykey to Exit");
                    ShowVillageFirst();
                }
                else
                { //골드 없을경우
                    Console.WriteLine(" 지불할 골드가 없습니다.");
                    Console.WriteLine(" 여관밖으로 쫒겨납니다.");
                    Console.WriteLine();
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                    ShowVillageFirst();
                }
                break;
            case 2:
                //뒤로
                ShowVillage();
                break;
        }
    }

    //상점
    static void Shop()
    {
        //상점입니다.
        Console.Clear();

        Console.WriteLine("                               _----_        ");
        Console.WriteLine("                              | _  _ |       ");
        Console.WriteLine("                              |  __  |       ");
        Console.WriteLine("                              ,'----'.       ");
        Console.WriteLine("                             |        |      ");
        Console.WriteLine(" -------------------------------------------- ");
        Console.WriteLine(" |       |                                    ");
        Console.WriteLine(" |  ,---.|---.,---.,---.                      ");
        Console.WriteLine(" |  `---.|   ||   ||   |                      ");
        Console.WriteLine(" |  `---'`   '`---'|---'                      ");
        Console.WriteLine(" |                 |                          ");
        Console.WriteLine(" -------------------------------------------- ");
        Console.WriteLine();
        Console.WriteLine(" 이곳은 상점입니다.");
        Console.WriteLine(" 장비와 회복아이템을 살 수 있습니다.");
        Console.WriteLine();
       
        Console.WriteLine($" 현재 소지금: {player1._gold}G"); //소지금 표시
        Console.WriteLine();
        Console.WriteLine("=====================================================================================");
        //반복문을 이용한 아이템 목록출력
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine($" {i + 1}. {item.ItemName} \t| 공격력 : {item.ItemAtk} \t| 방어력 : {item.ItemDef} \t| 가격: {item.ItemGold}G");

        }
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        Console.WriteLine(" 1. 아이템 구매하기");
        Console.WriteLine();
        Console.WriteLine(" 2. 구매목록 확인하기");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                //마을화면으로
                ShowVillageFirst();
                break;
            case 1:
                // 상점에서 아이템을 구매하는 메소드 호출
                BuyItem(player1);
                break;
            case 2:
                //구매 목록 확인
                DisplayInventoryInshop(player1);
                break;
        }
    }

    //구매한 아이템 인벤토리로 옮기기
    static void BuyItem(Character player)
    {
        Console.Clear();
        Console.WriteLine(" 구매할 아이템을 선택하세요:");
        Console.WriteLine($" 현재 소지금: {player1._gold}");
        Console.WriteLine();
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            Console.WriteLine($" {i + 1}. {item.ItemName} \t| 공격력 : {item.ItemAtk} \t| 방어력 : {item.ItemDef} \t| 가격: {item.ItemGold}G");
            Console.WriteLine();
        }
        Console.WriteLine("=====================================================================================");
        Console.WriteLine();

        //1~7의 숫자를 입력하면 0~6번째의 아이템을 구매
        int itemIndex = CheckValidInput(1, 7) - 1;

        Item selectedItem = items[itemIndex]; // 선택한 아이템 가져오기

        // 플레이어의 골드가 아이템 가격보다 많은지 확인
        if (player.Gold >= selectedItem.ItemGold)
        {
            player.Gold -= selectedItem.ItemGold; // 골드 차감
            player.AddToInventory(selectedItem); // 인벤토리에 아이템 추가
            items.Remove(selectedItem);//선택한 아이템 제거
            Console.WriteLine($" {selectedItem.ItemName}을(를) 구매했습니다!");
        }
        else
        {
            Console.WriteLine(" 골드가 부족합니다!");
        }

        Console.WriteLine(" Press Anykey to go Back.");
        Console.Write(">>");
        Console.ReadKey();
        Shop(); // 다시 상점으로 돌아가기

    }


    //던전 포탈
    static void DungeonPotal()
    {
        //던전으로 갈 수 있는 포탈입니다.
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine("\t @@@@@@@@@   @@@@@@");
        Console.WriteLine("\t @@@@@@@.  .  @@@@@");
        Console.WriteLine("\t @@@@@@  ;@@@  @@@@");
        Console.WriteLine("\t @@@@@   @@@@  @@@@");
        Console.WriteLine("\t @@@@@  @@@@@. @@@@");
        Console.WriteLine("\t @@@@. @@@@@@@ .@@@");
        Console.WriteLine("\t @@@@  @@@@@@. .@@@");
        Console.WriteLine("\t @@@@  @@@@@@. .@@@");
        Console.WriteLine("\t @@@@ @@@@@@@.  @@@");
        Console.WriteLine("\t @@@  @@@@@@@@ @@@@");
        Console.WriteLine("\t @@@. @@@@@@@@ @@@@");
        Console.WriteLine("\t @@@. @@@@@@@  @@@@");
        Console.WriteLine("\t @@@. @@@@@@@ @@@@@");
        Console.WriteLine("\t @@@@ @@@@@@  @@@@@");
        Console.WriteLine("\t @@@@ .@@@@   @@@@@");
        Console.WriteLine("\t @@@@  .@@   @@@@@@");
        Console.WriteLine("\t @@@@@  @   @@@@@@@");
        Console.WriteLine("\t @@@@@@    @@@@@@@@");
        Console.WriteLine();
        Console.WriteLine(" 던전으로 가는 포탈입니다.");
        Console.WriteLine(" 정말 던전으로 입장하시겠습니까?");
        Console.WriteLine();
        Console.WriteLine(" 1. 이지모드 입장");
        Console.WriteLine();
        Console.WriteLine(" 2. 노말모드 입장");
        Console.WriteLine();
        Console.WriteLine(" 3. 하드모드 입장(보스몹 등장)");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.WriteLine();
        Console.Write(">>");


        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                //마을 입구로 돌아가기
                ShowVillageFirst();
                break;
            case 1:
                //이지모드 입장
                DunGeonEasy();
                break;
            case 2:
                //노말모드 입장
                DunGeonNomal();
                break;
            case 3:
                //하드모드 입장
                DunGeonHard();
                break;

        }
    }

    //이지모드 입장
    static void DunGeonEasy()
    {
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" 이지모드 던전에 입장하시겠습니까?.");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" 권장 스텟은 ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("공격력 10 이상");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" 방어력 10 이상");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("입니다.");
        Console.WriteLine(" 이지모드 던전에서는 슬라임이 등장합니다.");
        Console.WriteLine();
        Console.WriteLine(" 입장하시겠습니까?");
        Console.WriteLine();
        Console.WriteLine(" 1. 예");
        Console.WriteLine();
        Console.WriteLine(" 2. 아니요");
        Console.WriteLine();

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                //이지모드 던전 입장
                EasyInDunGeon(player1);
                break;
            case 2:
                //뒤로
                DungeonPotal();
                break;
        }
    }
    //이지 모드 던전 입장 후
    static void EasyInDunGeon(Character player1)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 진행하려면 enter");
        Console.WriteLine();
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" 슬라임 등장!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();

        Console.WriteLine(" 전투 시작");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine(" 슬라임의 공격!");
        Damage = 30 - player1.Def; //몬스터의 공격 - 플레이어의 방어력 = 깎이는 체력
        if (Damage <= 0)
        {
            Damage = 0;
        }
        player1.Hp = player1.Hp - Damage;
        Console.WriteLine($" 체력이 {Damage} 감소했다.");
        Console.ReadKey();
        Console.WriteLine($" 당신의 남은 체력 : {player1.Hp}");

        if (player1.Hp > 0)
        {

            Console.WriteLine();
            Console.WriteLine(" 당신의 공격!");

            Console.WriteLine();
            Console.WriteLine(" 슬라임을 처치했습니다.");
            Console.WriteLine();
            Console.ReadKey();
            switch (player1.Atk) //공격력에 따른 추가 보상
            {
                case 30:
                    player1.Gold = player1.Gold + 300;
                    Console.WriteLine(" 300G 획득");

                    IncreaseExperience(player1, 60);
                    Console.WriteLine(" 60EXP 획득");
                    break;
                case 20:
                    player1.Gold = player1.Gold + 200;
                    Console.WriteLine(" 200G 획득");

                    IncreaseExperience(player1, 40);
                    Console.WriteLine(" 40EXP 획득");
                    break;
                case 10:
                    player1.Gold = player1.Gold + 100;
                    Console.WriteLine(" 100G 획득");

                    IncreaseExperience(player1, 20);
                    Console.WriteLine(" 20EXP 획득");
                    break;
                default:
                    player1.Gold = player1.Gold + 50;
                    Console.WriteLine(" 50G 획득");

                    IncreaseExperience(player1, 10);
                    Console.WriteLine(" 10EXP 획득");
                    break;
            }

            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine(" 던전을 클리어 하셨습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 이지모드 다시 하기");
            Console.WriteLine(" 2. 난이도 변경");
            Console.WriteLine(" 3. 마을로 귀환하기");
            Console.WriteLine(">>");


            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    //이지 던전 다시 돌기
                    EasyInDunGeon(player1);
                    break;
                case 2:
                    //던전포탈로 귀환
                    DungeonPotal();
                    break;
                case 3:
                    //마을로 귀환
                    ShowVillageFirst();
                    break;
            }
        }
        else
        {
            MedicalCost();
        }

    }

    //노말
    static void DunGeonNomal()
    {
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" 노말모드 던전에 입장하시겠습니까?.");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" 권장 스텟은 ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("공격력 600 이상");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" 방어력 600 이상");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("입니다.");
        Console.WriteLine(" 노말모드 던전에서는 고블린이 등장합니다.");
        Console.WriteLine();
        Console.WriteLine(" 입장하시겠습니까?");
        Console.WriteLine();
        Console.WriteLine(" 1. 예");
        Console.WriteLine();
        Console.WriteLine(" 2. 아니요");
        Console.WriteLine();

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                //노말모드 던전 입장
                NomalInDunGeon(player1);
                break;
            case 2:
                //뒤로
                DungeonPotal();
                break;
        }
    }
    //노말모드 던전 입장
    static void NomalInDunGeon(Character player1)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 진행하려면 enter");
        Console.WriteLine();
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" 고블린 등장!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();

        Console.WriteLine(" 전투 시작");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine(" 고블린의 공격!");
        Damage = 600 - player1.Def; //몬스터의 공격 - 플레이어의 방어력 = 깎이는 체력
        if (Damage <= 0)
        {
            Damage = 0;
        }
        player1.Hp = player1.Hp - Damage;
        Console.WriteLine($" 체력이 {Damage} 감소했다.");
        Console.ReadKey();

        if (player1.Hp > 0)
        {
            Console.WriteLine($" 당신의 남은 체력 : {player1.Hp}");
            Console.WriteLine();
            Console.WriteLine(" 당신의 공격!");
            Console.WriteLine();
            Console.WriteLine(" 고블린을 처치했습니다.");
            Console.WriteLine();
            Console.ReadKey();
            switch (player1.Atk) //공격력에 따른 추가 보상
            {
                case 1000:
                    player1.Gold = player1.Gold + 1000;
                    Console.WriteLine(" 1000G 획득");

                    IncreaseExperience(player1, 200);
                    Console.WriteLine(" 200EXP 획득");
                    break;
                case 800:
                    player1.Gold = player1.Gold + 750;
                    Console.WriteLine(" 750G 획득");

                    IncreaseExperience(player1, 150);
                    Console.WriteLine(" 150EXP 획득");
                    break;
                case 600:
                    Console.WriteLine(" 500G 획득");
                    player1.Gold = player1.Gold + 500;

                    Console.WriteLine(" 100EXP 획득");
                    IncreaseExperience(player1, 100);
                    break;
                default:
                    Console.WriteLine(" 250G 획득");
                    player1.Gold = player1.Gold + 250;

                    Console.WriteLine(" 50EXP 획득");
                    IncreaseExperience(player1, 50);
                    break;
            }
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine(" 던전을 클리어 하셨습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 노말모드 다시 하기");
            Console.WriteLine(" 2. 난이도 변경");
            Console.WriteLine(" 3. 마을로 귀환하기");
            Console.WriteLine(">>");


            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    //노말 던전 다시 돌기
                    NomalInDunGeon(player1);
                    break;
                case 2:
                    //던전포탈로 귀환
                    DungeonPotal();
                    break;
                case 3:
                    //마을로 귀환
                    ShowVillageFirst();
                    break;
            }
        }
        else
        {
            MedicalCost();
        }
    }

    //하드 던전
    static void DunGeonHard()
    {
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" 하드모드 던전에 입장하시겠습니까?.");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" 권장 스텟은 ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("공격력 3000 이상");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" 방어력 3000 이상");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("입니다.");
        Console.WriteLine(" 하드모드 던전에서는 발록이 등장합니다.");
        Console.WriteLine(" 발록을 처치하면 마왕성으로 가는 길이 열립니다.");
        Console.WriteLine();
        Console.WriteLine(" 입장하시겠습니까?");
        Console.WriteLine();
        Console.WriteLine(" 1. 예");
        Console.WriteLine();
        Console.WriteLine(" 2. 아니요");
        Console.WriteLine();

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                //하드모드 던전 입장
                HardInDunGeon(player1);
                break;
            case 2:
                //뒤로
                DungeonPotal();
                break;
        }
    }
    //하드모드 던전 입장
    static void HardInDunGeon(Character player1)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 진행하려면 enter");
        Console.WriteLine();
        Console.ReadKey();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" 발록 등장!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();

        Console.WriteLine(" 전투 시작");
        Console.WriteLine();
        Console.ReadKey();
        Console.WriteLine(" 발록의 공격!");
        Damage = 3000 - player1.Def; //몬스터의 공격 - 플레이어의 방어력 = 깎이는 체력
        if (Damage <= 0)
        {
            Damage = 0;
        }
        player1.Hp = player1.Hp - Damage;
        Console.WriteLine($" 체력이 {Damage} 감소했다.");
        Console.ReadKey();

        if (player1.Hp > 0)
        {
            Console.WriteLine($" 당신의 남은 체력 : {player1.Hp}");
            Console.WriteLine();
            Console.WriteLine(" 당신의 공격!");
            Console.WriteLine();
            Console.WriteLine(" 발록을 처치했습니다.");
            Console.WriteLine();
            Console.ReadKey();

            switch (player1.Atk) //공격력에 따른 추가 보상
            {
                case 6000:
                    Console.WriteLine(" 2000G 획득");
                    player1.Gold = player1.Gold + 2000;

                    Console.WriteLine(" 2000EXP 획득");
                    IncreaseExperience(player1, 2000);
                    break;
                case 5000:
                    Console.WriteLine(" 1500G 획득");
                    player1.Gold = player1.Gold + 1500;

                    Console.WriteLine(" 1500EXP 획득");
                    IncreaseExperience(player1, 1500);
                    break;
                case 3000:
                    Console.WriteLine(" 1000G 획득");
                    player1.Gold = player1.Gold + 1000;

                    Console.WriteLine(" 1000EXP 획득");
                    IncreaseExperience(player1, 1000);
                    break;
                default:
                    Console.WriteLine(" 500G 획득");
                    player1.Gold = player1.Gold + 500;

                    Console.WriteLine(" 500EXP 획득");
                    IncreaseExperience(player1, 500);
                    break;
            }
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine(" 던전을 클리어 하셨습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 하드모드 다시 하기");
            Console.WriteLine(" 2. 난이도 변경");
            Console.WriteLine(" 3. 마을로 귀환하기");
            Console.WriteLine(" 4. 마왕성으로 진입");
            Console.WriteLine(">>");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    //하드 던전 다시 돌기
                    HardInDunGeon(player1);
                    break;
                case 2:
                    //던전포탈로 귀환
                    DungeonPotal();
                    break;
                case 3:
                    //마을로 귀환
                    ShowVillageFirst();
                    break;
                case 4:
                    //마왕성으로 가기
                    GotoBoss(player1);
                    break;
            }
        }
        else
        {
            MedicalCost();
        }
    }

    //마왕성에서 보스잡기
    static void GotoBoss(Character player1)
    {
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" 정말 마왕성에 입장하시겠습니까?");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" 필요 입장 스탯:");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" 공격력: 10000  방어력: 10000");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(" 1. 예");
        Console.WriteLine(" 0. 아니요");
        int input = CheckValidInput(0, 1);
        if (input == 0)
        {
            //마을로 귀환
            ShowVillageFirst();
        }
        else 
        {
            if (player1.Atk >= 10000 || player1.Def >= 10000) //입장 가능
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(" 마왕성에 진입합니다.");
                Console.WriteLine();
                Console.WriteLine(" 왕좌에 마왕이 앉아있습니다.");
                Console.WriteLine();
                Console.WriteLine(" 마왕과의 전투!");
                if (player1.Atk <= 11000 || player1.Def <= 11000) //엔딩 분기점
                {
                    Console.WriteLine(" press any key to continue.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine(" 당신은 마왕과의 치열한 전투 끝에 승리하셨습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 집으로 돌아갈 수 있는 포탈이 눈앞에 나타납니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 하지만 당신은 전투중 얻은 부상으로 인해 곧 숨을 거두고 맙니다.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" Ending 1. 집에 가지 못한 개발자");
                }
                
                else if (player1.Atk <= 12000 || player1.Def <= 12000)
                {
                    Console.WriteLine(" press any key to continue.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine(" 당신은 마왕과의 전투에서 승리하셨습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 당신의 강한 일격에 마왕은 소멸되어 버렸습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 집으로 돌아갈 수 있는 포탈이 눈앞에 나타납니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 포탈을 향해 한발짝 내딛는 순간 목 뒤에서 강한 충격이 가해집니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 흐릿해지는 눈으로 뒤를 바라보니 여관주인이 서있었습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 여관주인이 중얼거립니다.");
                    Console.WriteLine();
                    Console.WriteLine(" \"드디어..나도 집에갈 수 있어...!\"");
                    Console.WriteLine();
                    Console.WriteLine(" 다시 눈을 떴을 때 당신은 여관주인이 되어있었습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 5년 후 당신의 눈 앞에 새로운 용사가 나타납니다.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" Ending 2. 되물림");
                }
                else
                {
                    Console.WriteLine(" press any key to continue.");
                    Console.ReadKey();
                    Console.WriteLine();
                    Console.WriteLine(" 당신은 마왕과의 전투에서 승리하셨습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 집으로 돌아갈 수 있는 포탈이 눈앞에 나타납니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 포탈을 탄 후 눈을 뜨니 모니터가 보입니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 당신은 이제 퇴근할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine(" 꿈이였나 싶었던 그때, 등 뒤에서 검의 감촉이 느껴집니다.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" True Ending. 퇴근하는 용사");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(" 당신은 마왕의 강함에 무릎을 꿇습니다.");
                Console.WriteLine(" 마왕이 당신의 오만함에 코웃음을 칩니다.");
                Console.WriteLine(" 마왕이 당신을 마왕성 밖으로 쫒아 냅니다.");
                Console.WriteLine(" 더 강해져서 돌아오십시오");
                Console.WriteLine();
                Console.WriteLine(" 강제로 마을로 귀환합니다.");
                Console.WriteLine(" press any key to continue.");
                Console.ReadKey();

                MedicalCost();
            }
        }
        
    }

    //치료비 강탈
    static void MedicalCost()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(" 당신은 마을의 여관에서 눈을 떳습니다.");
        Console.WriteLine(" 여관 주인이 치료비로 가진돈의 절반을 요구합니다.");
        Console.WriteLine(" 당신은 눈물을 머금고 치료비를 건냅니다.");
        int medicalCost = player1._gold / 2; //소지금 절반은 치료비
        player1._gold = player1._gold - medicalCost; 
        Console.WriteLine($" {medicalCost}를 건냅니다. 남은 골드: {player1._gold}");
        player1.Hp = 100;
        Console.WriteLine();
        Console.WriteLine(" 여관 밖으로 나갑니다.");
        Console.WriteLine(" press any key to continue.");
        Console.ReadKey();
        ShowVillage();
    }

    //상태창
    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태창");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine("====================================");
        Console.WriteLine($" Lv. {player1.Level.ToString("000")} ");
        Console.WriteLine($" {player1.Name} | {player1.Job} |");
        Console.WriteLine();
        Console.WriteLine($" 공격력 \t: {player1.Atk}");
        Console.WriteLine($" 방어력 \t: {player1.Def}");
        Console.WriteLine($" 체력 \t\t: {player1.Hp}");
        Console.WriteLine($" Gold \t\t: {player1.Gold} G");
        Console.WriteLine("====================================");
        Console.WriteLine();
        Console.WriteLine(" 0. 나가기");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                //마을로 돌아가기
                ShowVillageFirst();
                break;
        }
    }

    //인벤토리
    static void DisplayInventory(Character player)
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine(" [인벤토리]");
        Console.WriteLine($"\t\t\t\t\t\t[소지금:{player1._gold}G]");
        Console.WriteLine();
        Console.WriteLine("============================================================================");
        Console.WriteLine(" 소지중인 아이템 목록:");
        Console.WriteLine();
        Console.WriteLine("--------아이템 이름--------------------------아이템 설명---------------------");
        Console.WriteLine();
        //인벤토리 리스트에 있는 아이템들 나열
        for (int i = 0; i < player.Inventory.Count; i++)
        {
            var item = player.Inventory[i];
            string equippedStatus = item.IsEquipped ? "[E]" : ""; // 아이템이 장착되었는지 여부에 따라 [E] 표시 추가 없으면 공백
            Console.Write($"{i + 1}. ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{equippedStatus}");
            Console.ResetColor();
            Console.WriteLine($" \t {item.ItemName} \t | {item.ItemDescription}"); //아이템 부가 정보
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine("============================================================================");
        Console.WriteLine();
        Console.WriteLine(" 장착/해제를 원하는 아이템을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine(" 0. 뒤로가기");
        Console.Write(">>");

        int input = CheckValidInput(0, player.Inventory.Count);
        if (input > 0)
        {
            player.EquipItem(input);
            Console.WriteLine();
            Console.WriteLine("Press AnyKey");
            Console.ReadKey();
            //인벤토리창 새로고침
            DisplayInventory(player);
        }
        else
        {
            //마을로 돌아가기
            ShowVillageFirst();
        }
    }

    //상점에서 확인할 수 있는 인벤토리
    static void DisplayInventoryInshop(Character player)
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine(" [인벤토리]");
        Console.WriteLine();
        Console.WriteLine("====================================================================");
        Console.WriteLine(" 소지중인 아이템 목록:");
        Console.WriteLine();
        //아이템들 나열
        foreach (Item item in player.Inventory)
        {
            Console.WriteLine($" - {item.ItemName}");
        }

        Console.WriteLine();
        Console.WriteLine("====================================================================");
        Console.WriteLine();
        Console.WriteLine(" Press 0 to go back.");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        if (input == 0)
        {
            //상점창으로 돌아가기
            Shop();
        }
    }

    //입력 키 확인 메소드
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }
            //정해진 키가 아닌 키 입력시
            Console.WriteLine(" 잘못된 입력입니다.");
        }
    }

}

//레벨 범위 Class
public class LevelRange
{
    public int startLevel;
    public int endLevel;
    public int experienceCapIncrease;
}