using LitJson;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class UI_KongZhi : MonoBehaviour
{
    static UI_KongZhi main;
    public static UI_KongZhi Main {
        get {
            if (main == null)
            {
                main = new GameObject("UI_KongZhi").AddComponent<UI_KongZhi>();
            }
            return main;
        }
    }

    public GameObject ZhuCaiDan;
    public GameObject XianShi;
    public GameObject JieShu;
    public GameObject ZanTing;
    public GameObject KaiShi;
    public GameObject SheZhi;
    public GameObject CunDang1;
    public GameObject DuDuang;

    Button[] cunDuang_Button = new Button[5];
    Button[] duDuang_Button = new Button[5];

    Text time;
    Text Time
    {
        get {
            if (time == null)
                time = GameObject.Find("Time").GetComponent<Text>();
            return time;
        }
    }

    Text time_jieShu;
    Text Time_jieShu
    {
        get
        {
            if (time_jieShu == null)
                time_jieShu = GameObject.Find("Time_JieShu").GetComponent<Text>();
            return time_jieShu;
        }
    }

    //主菜单显示
    public void ZhuCaiDan_XianShi()
    {
        ZhuCaiDan.SetActive(true);
        XianShi.SetActive(false);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        CunDang1.SetActive(false);
        DuDuang.SetActive(false);
    }
    //新游戏显示
    public void KaiShiYouXi_XianShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(false);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(true);
        SheZhi.SetActive(false);
    }
    //存档游戏显示
    public void CunDangYouXi_XianShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(false);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        DuDuang.SetActive(true);
        Transform[] gameObjects = DuDuang.GetComponentsInChildren<Transform>();
        Transform[] transforms;
        Transform[] xinXi;
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + "CunDang" + (i+1).ToString() + ".json"))
            {
                for (int j = 0; j < gameObjects.Length; j++)
                {
                    if (gameObjects[j].name == "CunDang" + (i + 1).ToString())
                    {
                        transforms = gameObjects[j].GetComponentsInChildren<Transform>();
                        for (int x = 0; x < transforms.Length; x++)
                        {
                            if (transforms[x].name == "duDang")
                            {
                                duDuang_Button[i] = transforms[x].gameObject.GetComponent<Button>();
                                string json = File.ReadAllText(Application.persistentDataPath + "/" + "CunDang" + (i + 1).ToString() + ".json");
                                JsonData js = JsonMapper.ToObject(json);
                                xinXi = transforms[x].gameObject.GetComponentsInChildren<Transform>();
                                for (int y = 0; y < xinXi.Length; y++)
                                {
                                    if (xinXi[y].name == "moShi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = moShi_XianShi((int)js["moShi"]);
                                    }
                                    else if(xinXi[y].name == "yongShi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = Set_Time(float.Parse((string)js["shiJian"]));
                                    }
                                    else if(xinXi[y].name == "riQi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = File.GetLastWriteTime(Application.persistentDataPath + "/" + "CunDang" + (i + 1).ToString() + ".json").ToString();
                                    }
                                }
                            }
                            else if (transforms[x].name == "Image" )
                            {
                                transforms[x].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
            else//没有存档 隐藏存档对应的读档按钮
            {
                for (int j = 0; j < gameObjects.Length; j++)
                {
                    if (gameObjects[j].name == "CunDang" + (i + 1).ToString())
                    {
                        transforms = gameObjects[j].GetComponentsInChildren<Transform>();
                        for (int x = 0; x < transforms.Length; x++)
                        {
                            if (transforms[x].name == "duDang")
                            {
                                duDuang_Button[i] = transforms[x].gameObject.GetComponent<Button>();
                                transforms[x].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
    //读档窗口显示
    public void CunDang_XianShi()
    {
        CunDang1.SetActive(true);
        Player.player_Main.IsKaiShi = false;
        Transform[] gameObjects = CunDang1.GetComponentsInChildren<Transform>();
        Transform[] transforms;
        Transform[] xinXi;
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/" + "CunDang" + (i + 1).ToString() + ".json"))
            {
                for (int j = 0; j < gameObjects.Length; j++)
                {
                    if (gameObjects[j].name == "CunDang" + (i + 1).ToString())
                    {
                        transforms = gameObjects[j].GetComponentsInChildren<Transform>();
                        for (int x = 0; x < transforms.Length; x++)
                        {
                            if (transforms[x].name == "duDang")
                            {
                                cunDuang_Button[i] = transforms[x].gameObject.GetComponent<Button>();
                                string json = File.ReadAllText(Application.persistentDataPath + "/" + "CunDang" + (i + 1).ToString() + ".json");
                                JsonData js = JsonMapper.ToObject(json);
                                xinXi = transforms[x].gameObject.GetComponentsInChildren<Transform>();
                                for (int y = 0; y < xinXi.Length; y++)
                                {
                                    if (xinXi[y].name == "moShi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = moShi_XianShi((int)js["moShi"]);
                                    }
                                    else if (xinXi[y].name == "yongShi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = Set_Time(float.Parse((string)js["shiJian"]));
                                    }
                                    else if (xinXi[y].name == "riQi")
                                    {
                                        xinXi[y].GetComponent<Text>().text = File.GetLastWriteTime(Application.persistentDataPath + "/" + "CunDang" + (i + 1).ToString() + ".json").ToString();
                                    }
                                }
                            }
                            else if (transforms[x].name == "cunDang")
                            {
                                transforms[x].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
            else//没有存档 隐藏存档对应的读档按钮
            {
                for (int j = 0; j < gameObjects.Length; j++)
                {
                    if (gameObjects[j].name == "CunDang" + (i + 1).ToString())
                    {
                        transforms = gameObjects[j].GetComponentsInChildren<Transform>();
                        for (int x = 0; x < transforms.Length; x++)
                        {
                            if (transforms[x].name == "duDang")
                            {
                                cunDuang_Button[i] = transforms[x].gameObject.GetComponent<Button>();
                                transforms[x].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void CunDang_GuanBi() {
        CunDang1.SetActive(false);
        Player.player_Main.IsKaiShi = true;
    }

    public void LuJing_XianShi() {
        Player.player_Main.XianShi_LuJing();
        ZanTing.SetActive(false);
        Player.player_Main.IsKaiShi = true;
    }
    //新游戏生成
    public void JianDan_MoShi() {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(15,Player.MoShi.PuTong);
        Player.player_Main.IsKaiShi = true;
    }

    public void YiBan_MoShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(25, Player.MoShi.PuTong);
        Player.player_Main.IsKaiShi = true;
    }

    public void KunNam_MoShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(40, Player.MoShi.PuTong);
        Player.player_Main.IsKaiShi = true;
    }
    public void MiWu_MoShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(40, Player.MoShi.MiWu);
        Player.player_Main.IsKaiShi = true;
    }

    public void HeiYe_MoShi()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(40, Player.MoShi.HeiYe);
        Player.player_Main.IsKaiShi = true;
    }

    //暂停
    public void ZanTing_OnClick() {  
        ZanTing.SetActive(true);
        Player.player_Main.IsKaiShi = false;
    }
    //恢复
    public void HuiFu() {
        ZanTing.SetActive(false);
        Player.player_Main.IsKaiShi = true;
    }
    //设置
    public void SheZhi_OnClick()
    {
        SheZhi.SetActive(true);
        if (Player.Player_IsNull())
        {
            Player.player_Main.IsKaiShi = false;
        }
    }
    //设置关闭
    public void SheZhi_GuanBi()
    {
        SheZhi.SetActive(false);
        if (Player.Player_IsNull())
        {
            Player.player_Main.IsKaiShi = true;
        }
    }
    //退出游戏
    public void TuiChu() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    //游戏完成弹框
    public void YouXiJieShu() {
        JieShu.SetActive(true);
        Time_jieShu.text = "用时:"+Set_Time(Player.player_Main.Play_Time);
        Player.player_Main.IsKaiShi = false;
    }
    //再来一局
    public void ZaiLaiYiJu() {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        Player.player_Main.ChuShiHua(Player.player_Main.Size, Player.player_Main.moShi);
    }
    //下一关
    public void XiaYiGuan()
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        if (Player.player_Main.Size>40 && Player.player_Main.moShi!= Player.MoShi.HeiYe)
        {
            Player.player_Main.ChuShiHua(Player.player_Main.Size, Player.player_Main.moShi+1);
        }
        else if(Player.player_Main.Size == 15)
        {
            Player.player_Main.ChuShiHua(25, Player.player_Main.moShi);
        }
        else if(Player.player_Main.Size == 25)
        {
            Player.player_Main.ChuShiHua(41, Player.player_Main.moShi);
        }
        else
        {
            Player.player_Main.ChuShiHua(Player.player_Main.Size, Player.player_Main.moShi);
        }
    }

    public void DuDang(int weiZhi)
    {
        ZhuCaiDan.SetActive(false);
        XianShi.SetActive(true);
        JieShu.SetActive(false);
        ZanTing.SetActive(false);
        KaiShi.SetActive(false);
        SheZhi.SetActive(false);
        DuDuang.SetActive(false);
        CunDang cunDang = new CunDang();
        cunDang.duDuang(weiZhi);
    }

    public void CunDang(int weiZhi) {
        CunDang cunDang = new CunDang();
        cunDang.cunDang(weiZhi);
        CunDang_GuanBi();
        cunDuang_Button[weiZhi - 1].gameObject.SetActive(true);
        duDuang_Button[weiZhi - 1].gameObject.SetActive(true);
    }
    public void Set_Player_Time(float time) {
        Time.text = Set_Time(time);
    }

    string moShi_XianShi(int moShi) {
        switch (moShi)
        {
            case 1: return "简单模式";
            case 2: return "一般模式";
            case 3: return "困难模式";
            case 4: return "迷雾模式";
            case 5: return "黑夜模式";
        }
        return "";
    }

    string Set_Time(float time)
    {
        int zs = (int)time;
        string str;
        if (zs / 60 == 0)
        {
            str = "00:";
        }
        else if (zs / 60 < 10)
        {
            str = "0";
            str += zs / 60 + ":";
        }
        else
        {
            str = zs / 60 + ":";
        }
        if (zs % 60 < 10)
        {
            str += "0";
        }
        str += zs % 60;
        str += ":";
        str += (int)((time - zs) * 100);
        return str;
    }
}
