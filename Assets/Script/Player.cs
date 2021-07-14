using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum MoShi
    {
        PuTong,
        MiWu,
        HeiYe
    }

    private static Player player;
    public static Player player_Main {
        get {
            if (player == null) {
                player = new GameObject("Player").AddComponent<Player>();
                player.diTu_FuJi = GameObject.Find("DiTu").transform;
                player.player_FuJi = GameObject.Find("Player_LuJing").transform;
                player.ui = GameObject.Find("UI").GetComponent<UI_KongZhi>();
            }
            return player;
        }
    }
    public static bool Player_IsNull(){
        if (player == null){
            return false;
        }
        else {
            return true;
        }
    }

    public Transform diTu_FuJi;
    public Transform player_FuJi;

    public UI_KongZhi ui;

    public bool IsKaiShi = false;
    public MoShi moShi;
    public int size;
    public int Size {
        get
        {
            if (size < 3)
                size = 3;
            if (size > 41)
                size = 41;
            if (size % 2 != 1)
                size += 1;
            return size;
        }
        set { size = value; }
    }
    public int QiDian {
        get { return (Size * (Size - 1)) + ZhongDian; }
    }
    public int ZhongDian {
        get { return Size / 2; }
    }



    public int DangQian;
    public bool IsQianZhi;
    public float Play_Time;
    public List<DiTuGe> DiTu;
    public List<Image> Player_Ge;

    public List<int> ShengCheng_LuJing;            //路径储存
    public List<int> ShengCheng_LuJing_ShiBai;     //
    public List<int> WeiShiYou_GeiZi;
    List<int> TianChuong_LuJing;


    void Start()
    {
        
    }

    void Update()
    {
        if (IsKaiShi && DangQian != ZhongDian)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                KongZhi_Main(0, moShi);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                KongZhi_Main(1, moShi);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                KongZhi_Main(2, moShi);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                KongZhi_Main(3, moShi);
            }
        }
        else if (DangQian == ZhongDian && IsKaiShi)
        {
            ui.YouXiJieShu();
            IsKaiShi = false;
        }
        if (IsKaiShi)
        {
            Play_Time += Time.deltaTime;
            ui.Set_Player_Time(Play_Time);
        }
    }

    public void ChuShiHua(int size , MoShi moShi) {
        ShanChu();
        Size = size;
        this.moShi = moShi;
        IsKaiShi = true;
        DiTu = new List<DiTuGe>();
        Player_Ge = new List<Image>();
        WeiShiYou_GeiZi = new List<int>();
        ShengCheng_LuJing = new List<int>();
        ShengCheng_LuJing_ShiBai = new List<int>();
        ShengCheng_LuJing.Add(QiDian);
        DangQian = QiDian;
        DiTuGe_ChuShiHua();
        Player_ChuShiHua();
        ShengCheng_LuJin();
        ShengCheng_TianChuong();
        IsKaiShi = true;
        Play_Time = 0;
    }

    public void KongZhi_Main(int Key, MoShi moShi){
        int zc = KongZhi(Key);
        if (zc != DangQian)
        {
            switch (moShi)
            {
                case MoShi.PuTong:
                    Player_Ge[DangQian].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                    Player_Ge[zc].sprite = Sprite_Manager.Main.Get_Sprite("Player");
                    DangQian = zc;
                    break;
                case MoShi.MiWu:
                    Player_Ge[DangQian].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                    Player_Ge[zc].sprite = Sprite_Manager.Main.Get_Sprite("Player");
                    MiWu_Play(zc);
                    DangQian = zc;
                    break;
                case MoShi.HeiYe:
                    HeiYei_Play(zc);
                    Player_Ge[DangQian].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                    Player_Ge[zc].sprite = Sprite_Manager.Main.Get_Sprite("Player");
                    DangQian = zc;
                    break;
            }
        }
    }

    int KongZhi(int Key) {
        int JieGuo = DangQian;
        switch (Key)
        {
            case 0:
                if (DiTu[DangQian].Shang)
                {
                    JieGuo -= Size;
                }
                break;
            case 1:
                if (DiTu[DangQian].Xia)
                {
                    JieGuo += Size;
                }
                break;
            case 2:
                if (DiTu[DangQian].Zuo)
                {
                    JieGuo -= 1;
                }
                break;
            case 3:
                if (DiTu[DangQian].You)
                {
                    JieGuo += 1;
                }
                break;
        }
        return JieGuo;
    }
    public void ShanChu() {
        if (DiTu!= null && DiTu.Count > 0)
        {
            for (int i = DiTu.Count - 1; i > -1 ; i--)
            {
                GameObject.Destroy(DiTu[i].getZi_obj);
            }
            for (int i = Player_Ge.Count - 1; i > -1; i--)
            {
                GameObject.Destroy(Player_Ge[i].gameObject);
            }
        }
        
    }

    public void MiWu_Play(int DangQian)
    {
        for (int i = -3; i < 4; i++)
        {
            if (DangQian + (i * Size) > 0 && DangQian + (i * Size) < Size * Size)
            {

                for (int j = -3; j < 4; j++)
                {
                    if (DangQian % Size + j >= 0 && (DangQian % Size) + j < Size && DangQian + (i * Size) + j != ZhongDian && Player_Ge[DangQian + (i * Size) + j].sprite == Sprite_Manager.Main.Get_Sprite("Kuang"))
                    {
                        Player_Ge[DangQian + (i * Size) + j].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                    }
                }
            }
        }
    }
    public void HeiYei_Play(int DangQian)
    {
        for (int i = -4; i < 5; i++)
        {
            if (DangQian + (i * Size) > 0 && DangQian + (i * Size) < Size * Size)
            {

                for (int j = -4; j < 5; j++)
                {
                    if ((DangQian % Size + j >= 0 && (DangQian % Size) + j < Size && DangQian + (i * Size) + j != ZhongDian)&& (Player_Ge[DangQian + (i * Size) + j].sprite == Sprite_Manager.Main.Get_Sprite("Null") || Player_Ge[DangQian + (i * Size) + j].sprite == Sprite_Manager.Main.Get_Sprite("Kuang")))
                    {
                        if (i == -4 ||  i == 4 || j == -4 || j ==4)
                        { 
                            Player_Ge[DangQian + (i * Size) + j].sprite = Sprite_Manager.Main.Get_Sprite("Kuang");
                            Player_Ge[DangQian + (i * Size) + j].color = new Color(0,222,255,255);
                           
                        }
                        else {
                            Player_Ge[DangQian + (i * Size) + j].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                        }

                    }
                }
            }
        }
    }

    public void XianShi_LuJing() {
        Queue<int> GeZhi = new Queue<int>();
        List<int> GeZhi_JiLu = new List<int>();
        List<int> GeZhi_FuJi_JiLu = new List<int>();
        List<int> lujing = new List<int>();
        GeZhi.Enqueue(DangQian);
        GeZhi_JiLu.Add(DangQian);
        GeZhi_FuJi_JiLu.Add(-1);
        int ZhiZhen=GeZhi.Dequeue();
        while (ZhiZhen != ZhongDian)
        {
            if (DiTu[ZhiZhen].Shang && !GeZhi_JiLu.Contains(ZhiZhen - size))
            {
                GeZhi.Enqueue(ZhiZhen - size);
                GeZhi_JiLu.Add(ZhiZhen - size);
                GeZhi_FuJi_JiLu.Add(ZhiZhen);
            }
            if (DiTu[ZhiZhen].Xia && !GeZhi_JiLu.Contains(ZhiZhen + size))
            {
                GeZhi.Enqueue(ZhiZhen + size);
                GeZhi_JiLu.Add(ZhiZhen + size);
                GeZhi_FuJi_JiLu.Add(ZhiZhen);
            }
            if (DiTu[ZhiZhen].Zuo && !GeZhi_JiLu.Contains(ZhiZhen - 1))
            {
                GeZhi.Enqueue(ZhiZhen - 1);
                GeZhi_JiLu.Add(ZhiZhen - 1);
                GeZhi_FuJi_JiLu.Add(ZhiZhen);
            }
            if (DiTu[ZhiZhen].You && !GeZhi_JiLu.Contains(ZhiZhen+1))
            {
                GeZhi.Enqueue(ZhiZhen + 1);
                GeZhi_JiLu.Add(ZhiZhen + 1);
                GeZhi_FuJi_JiLu.Add(ZhiZhen);
            }
            ZhiZhen = GeZhi.Dequeue();
        }
        lujing.Add(ZhiZhen);
        while (GeZhi_FuJi_JiLu[GeZhi_JiLu.IndexOf(ZhiZhen)] != -1)
        {
            ZhiZhen = GeZhi_FuJi_JiLu[GeZhi_JiLu.IndexOf(ZhiZhen)];
            lujing.Add(ZhiZhen);
        }
        for (int i = 1; i < lujing.Count; i++)
        {
            if (lujing[i] != DangQian)
            {
                if ((lujing[i]- lujing[i-1]>1&& lujing[i] - lujing[i + 1] < -1)|| (lujing[i] - lujing[i + 1] > 1 && lujing[i] - lujing[i - 1] < -1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_ShangXia");
                    MiWu_Play(lujing[i]);
                }
                else if ((lujing[i] - lujing[i - 1] > 1 && lujing[i] - lujing[i + 1] == -1)|| (lujing[i] - lujing[i + 1] > 1 && lujing[i] - lujing[i - 1] == -1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_ShangYou");
                    MiWu_Play(lujing[i]);
                }
                else if ((lujing[i] - lujing[i - 1] > 1 && lujing[i] - lujing[i + 1] == 1)|| (lujing[i] - lujing[i + 1] > 1 && lujing[i] - lujing[i - 1] == 1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_ShangZuo");
                    MiWu_Play(lujing[i]);
                }
                else if ((lujing[i] - lujing[i - 1] <-1 && lujing[i] - lujing[i + 1] == -1)|| (lujing[i] - lujing[i + 1] < -1 && lujing[i] - lujing[i - 1] == -1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_XiaYou");
                    MiWu_Play(lujing[i]);
                }
                else if ((lujing[i] - lujing[i - 1] <-1 && lujing[i] - lujing[i + 1] == 1)|| (lujing[i] - lujing[i + 1] < -1 && lujing[i] - lujing[i - 1] == 1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_XiaZuo");
                    MiWu_Play(lujing[i]);
                }
                else if ((lujing[i] - lujing[i - 1] == 1 && lujing[i] - lujing[i + 1] == -1)|| (lujing[i] - lujing[i + 1] == 1 && lujing[i] - lujing[i - 1] == -1))
                {
                    Player_Ge[lujing[i]].sprite = Sprite_Manager.Main.Get_Sprite("xian_ZuoYou");
                    MiWu_Play(lujing[i]);
                }
            }
        }
    }

    #region 初始化地图元素
    //地图层初始化
    public void DiTuGe_ChuShiHua() {
        diTu_FuJi.GetComponent<GridLayoutGroup>().constraintCount = Size;
        float ZiLei_Size = diTu_FuJi.GetComponent<RectTransform>().rect.height / Size;
        diTu_FuJi.GetComponent<GridLayoutGroup>().cellSize = new Vector2(ZiLei_Size, ZiLei_Size);
        for (int i = 0; i < Size * Size; i++)
        {
            WeiShiYou_GeiZi.Add(i);
            if (i == 0)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), false, true, false, true));
            else if (i < size - 1)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), false, true, true, true));
            else if (i == size - 1)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), false, true, true, false));
            else if (i == size * (size - 1))
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, false, false, true));
            else if (i == (size * size) - 1)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, false, true, false));
            else if (i % size == 0)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, true, false, true));
            else if (i % size == size - 1)
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, true, true, false));
            else if (i < (size * size) - 1 && i > size * (size - 1))
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, false, true, true));
            else
                DiTu.Add(new DiTuGe(new GameObject(i.ToString()), true, true, true, true));
            DiTu[i].getZi_obj.transform.SetParent(diTu_FuJi);
            DiTu[i].getZi_obj.transform.localScale = new Vector3(1, 1, 1);
        }
        WeiShiYou_GeiZi.Remove(QiDian);
    }
    //玩家层初始化
    public void Player_ChuShiHua() {
        player_FuJi.GetComponent<GridLayoutGroup>().constraintCount = Size;
        float ZiLei_Size = diTu_FuJi.GetComponent<RectTransform>().sizeDelta.x / Size;
        player_FuJi.GetComponent<GridLayoutGroup>().cellSize = new Vector2(ZiLei_Size, ZiLei_Size);
        for (int i = 0; i < Size * Size; i++)
        {
            Player_Ge.Add(new GameObject(i.ToString()).AddComponent<Image>());
            Player_Ge[i].transform.SetParent(player_FuJi);
            Player_Ge[i].transform.localScale = new Vector3(1, 1, 1);
            if (moShi == MoShi.PuTong)
            {
                Player_Ge[i].sprite = Sprite_Manager.Main.Get_Sprite("Null");
                Player_Ge[i].color = new Color(0, 222, 255, 255);
            }
            else
            {
                Player_Ge[i].sprite = Sprite_Manager.Main.Get_Sprite("Kuang");
                Player_Ge[i].color = new Color(0, 222, 255, 255);
            }

        }
        Player_Ge[ZhongDian].sprite = Sprite_Manager.Main.Get_Sprite("ZhongDian");
        Player_Ge[QiDian].sprite = Sprite_Manager.Main.Get_Sprite("Player");
    }
    #endregion

    //生成路径并显示
    public void ShengCheng_LuJin() {
        int DangQian;
        int XiaYiGe;
        while (ShengCheng_LuJing[ShengCheng_LuJing.Count - 1] != ZhongDian)
        {
            UpDate_LuJing_ShengCheng();
        }
        for (int i = 0; i < DiTu.Count; i++)
        {
            DiTu[i].Set_FangXiang(false, false, false, false);
        }
        for (int i = 0; i < ShengCheng_LuJing.Count; i++)
        {
            DangQian = ShengCheng_LuJing[i];
            if (i != ShengCheng_LuJing.Count - 1)
            {
                XiaYiGe = ShengCheng_LuJing[i + 1];
                if (DangQian > XiaYiGe && DangQian - 1 == XiaYiGe)
                {
                    DiTu[DangQian].Zuo = true;
                    DiTu[XiaYiGe].You = true;
                }
                else if (DangQian > XiaYiGe)
                {
                    DiTu[DangQian].Shang = true;
                    DiTu[XiaYiGe].Xia = true;
                }
                else if (DangQian < XiaYiGe && DangQian + 1 == XiaYiGe)
                {
                    DiTu[DangQian].You = true;
                    DiTu[XiaYiGe].Zuo = true;
                }
                else
                {
                    DiTu[DangQian].Xia = true;
                    DiTu[XiaYiGe].Shang = true;
                }
            }
        }
    }
    //生成可通行路径
    void UpDate_LuJing_ShengCheng() {
        int ZhiZhen = ShengCheng_LuJing[ShengCheng_LuJing.Count - 1];
        bool shang, xia, zuo, you;
        shang = DiTu[ZhiZhen].Shang;
        xia = DiTu[ZhiZhen].Xia;
        zuo = DiTu[ZhiZhen].Zuo;
        you = DiTu[ZhiZhen].You;
        int suiJi = Random.Range(0, 4);
        //当处于地图边缘且被记录时 远离起点
        if (IsQianZhi && (ZhiZhen == (Size * Size) - 1 || ZhiZhen == Size * (Size - 1)))
        {
            LuJing_TianJia(shang,-Size);
            IsQianZhi = false;
        }
        else if (IsQianZhi && ((ZhiZhen < Size / 2 || ZhiZhen > Size * (Size - 1) + Size / 2)))
        {
            LuJing_TianJia(you,1);
            IsQianZhi = false;
        }
        else if (IsQianZhi && ((ZhiZhen > Size / 2 && ZhiZhen < Size) || (ZhiZhen > Size * (Size - 1) + Size / 2 && ZhiZhen < Size * (Size - 1) - 1)))
        {
            LuJing_TianJia(zuo,-1);
            IsQianZhi = false;
        }
        else if (IsQianZhi && (ZhiZhen % Size == 0 || ZhiZhen % Size == Size - 1))
        {
            LuJing_TianJia(shang,-Size);
            IsQianZhi = false;
        }
        else 
        {
            switch (suiJi)
            {
                case 0: LuJing_TianJia(shang, -size); break;
                case 1: LuJing_TianJia(xia, size); break;
                case 2: LuJing_TianJia(zuo, -1); break;
                case 3: LuJing_TianJia(you, 1); break;
            }
        }
    }
    //添加路径格
    void LuJing_TianJia(bool Shang, int ShuLiang) {
        if (Shang)
        {
            int XiaYiGe = ShengCheng_LuJing[ShengCheng_LuJing.Count - 1] + ShuLiang;
            if (!ShengCheng_LuJing.Contains(XiaYiGe) && !ShengCheng_LuJing_ShiBai.Contains(XiaYiGe))
            {
                ShengCheng_LuJing.Add(XiaYiGe);
                WeiShiYou_GeiZi.Remove(XiaYiGe);
                IsQianZhi = true;
            }
            else
            {
                ShengCheng_LuJing_ShiBai.Add(XiaYiGe);
                if (ShengCheng_LuJing_ShiBai.Contains(ShengCheng_LuJing[ShengCheng_LuJing.Count - 1]))
                {
                    ShengCheng_LuJing_ShiBai.Remove(ShengCheng_LuJing[ShengCheng_LuJing.Count - 1]);
                }
                WeiShiYou_GeiZi.Add(ShengCheng_LuJing[ShengCheng_LuJing.Count - 1]);
                ShengCheng_LuJing.Remove(ShengCheng_LuJing[ShengCheng_LuJing.Count - 1]);
            }
        }
    }
    //填充随机地图
    public void ShengCheng_TianChuong() {
        if (WeiShiYou_GeiZi.Count > 0)
        {
            TianChuong_LuJing = new List<int>();
            ShengCheng_LuJing_ShiBai = new List<int>();
            TianChuong_LuJing.Add(WeiShiYou_GeiZi[0]);
            WeiShiYou_GeiZi.Remove(TianChuong_LuJing[TianChuong_LuJing.Count - 1]);
        }
        while (ShengCheng_LuJing.Count != Size * Size)
        {
            UpDate_ShengCheng_TianChong();
        }
    }
    //写入地图信息
    void TianChong_XianShi() {
        int DangQian;
        int XiaYiGe;
        for (int i = 0; i < TianChuong_LuJing.Count; i++)
        {
            DangQian = TianChuong_LuJing[i];
            if (i != TianChuong_LuJing.Count - 1)
            {
                XiaYiGe = TianChuong_LuJing[i + 1];
                if (DangQian > XiaYiGe && DangQian - 1 == XiaYiGe)
                {
                    DiTu[DangQian].Zuo = true;
                    DiTu[XiaYiGe].You = true;
                }
                else if (DangQian > XiaYiGe)
                {
                    DiTu[DangQian].Shang = true;
                    DiTu[XiaYiGe].Xia = true;
                }
                else if (DangQian < XiaYiGe && DangQian + 1 == XiaYiGe)
                {
                    DiTu[DangQian].You = true;
                    DiTu[XiaYiGe].Zuo = true;
                }
                else
                {
                    DiTu[DangQian].Xia = true;
                    DiTu[XiaYiGe].Shang = true;
                }
            }
        }
    }
    //生成填充路径
    void UpDate_ShengCheng_TianChong(){
        int ZhiZhen;
        int suiJi;
        suiJi = Random.Range(0, 4);
        if (TianChuong_LuJing.Count != 0)
        {
            ZhiZhen = TianChuong_LuJing[TianChuong_LuJing.Count - 1];
            if (ZhiZhen == (Size * Size) - 1 || ZhiZhen == Size * (Size - 1))
            {
                TianChuong_TianJia(-Size);
            }
            else if (ZhiZhen < Size / 2 || ZhiZhen > Size * (Size - 1) + Size / 2)
            {
                TianChuong_TianJia(1);
            }
            else if ((ZhiZhen > Size / 2 && ZhiZhen < Size) || (ZhiZhen > Size * (Size - 1) + Size / 2 && ZhiZhen < Size * (Size - 1) - 1))
            {
                TianChuong_TianJia(-1);
            }
            else if (ZhiZhen % Size == 0 || ZhiZhen % Size == Size - 1)
            {
                TianChuong_TianJia(-Size);
            }
            else
            {
                switch (suiJi)
                {
                    case 0: TianChuong_TianJia(-size); break;
                    case 1: TianChuong_TianJia(size); break;
                    case 2: TianChuong_TianJia(-1); break;
                    case 3: TianChuong_TianJia(1); break;
                }
            }
        }
        
    }
    //填充路径格
    void TianChuong_TianJia(int ShuLiang)
    {
        int XiaYiGe = TianChuong_LuJing[TianChuong_LuJing.Count - 1] + ShuLiang;
        if(ShengCheng_LuJing.Contains(XiaYiGe))
        {
            ShengCheng_LuJing.AddRange(TianChuong_LuJing);
            TianChuong_LuJing.Add(XiaYiGe);
            TianChong_XianShi();
            if (WeiShiYou_GeiZi.Count > 0)
            {
                TianChuong_LuJing = new List<int>();
                ShengCheng_LuJing_ShiBai = new List<int>();
                TianChuong_LuJing.Add(WeiShiYou_GeiZi[0]);
                WeiShiYou_GeiZi.Remove(TianChuong_LuJing[TianChuong_LuJing.Count - 1]);
            }
        }
        else if(!TianChuong_LuJing.Contains(XiaYiGe) && WeiShiYou_GeiZi.Contains(XiaYiGe) && !ShengCheng_LuJing_ShiBai.Contains(XiaYiGe))
        {
            TianChuong_LuJing.Add(XiaYiGe);
            WeiShiYou_GeiZi.Remove(XiaYiGe);
        }
        else
        {
            if (XiaYiGe < Size*Size )
            {
                if (!ShengCheng_LuJing_ShiBai.Contains(XiaYiGe))
                {
                    ShengCheng_LuJing_ShiBai.Add(XiaYiGe);
                }
                if (ShengCheng_LuJing_ShiBai.Contains(TianChuong_LuJing[TianChuong_LuJing.Count - 1]))
                {
                    ShengCheng_LuJing_ShiBai.Remove(TianChuong_LuJing[TianChuong_LuJing.Count - 1]);
                }
                WeiShiYou_GeiZi.Add(TianChuong_LuJing[TianChuong_LuJing.Count - 1]);
                TianChuong_LuJing.Remove(TianChuong_LuJing[TianChuong_LuJing.Count - 1]);
            }
            ChuShiHua_MiWuMoShi();
        }
        
    }

    public void ChuShiHua_MiWuMoShi() {
        MiWu_Play(QiDian);
        Player_Ge[QiDian].sprite = Sprite_Manager.Main.Get_Sprite("Player");
    }
}
