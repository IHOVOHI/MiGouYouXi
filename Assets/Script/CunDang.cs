using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class CunDang
{
    string url = Application.persistentDataPath + "/";

    //存档生成
    public void cunDang(int WeiZhi) {
        string str = url + "CunDang" + WeiZhi + ".json";
        string json = "";
        if (File.Exists(str))
        {
            File.Delete(str);
        }
        JsonData js = new JsonData();
        js["moShi"] = moShi_PanDuan(Player.player_Main.size,Player.player_Main.moShi);
        js["qiDian"] = Player.player_Main.DangQian;
        js["shiJian"] = Player.player_Main.Play_Time.ToString();
        for (int i = 0; i < Player.player_Main.DiTu.Count; i++)
        {
            json += diTu_PanDuan(Player.player_Main.DiTu[i].Shang, Player.player_Main.DiTu[i].Xia, Player.player_Main.DiTu[i].Zuo, Player.player_Main.DiTu[i].You);
            if (i != Player.player_Main.DiTu.Count-1)
            {
                json += ",";
            }
        }
        js["diTu"] = json;
        File.AppendAllText(str, js.ToJson());
    }

    //读档生成
    public void duDuang(int WeiZhi) {
        Player.player_Main.ShanChu();
        Player.player_Main.WeiShiYou_GeiZi = new List<int>();
        Player.player_Main.DiTu = new List<DiTuGe>();
        Player.player_Main.Player_Ge = new List<Image>();
        string str = url + "CunDang" + WeiZhi + ".json";
        string json = File.ReadAllText(str);
        JsonData js = JsonMapper.ToObject(json);
        Player.player_Main.moShi = moShi_PanDuan((int)js["moShi"],out Player.player_Main.size);
        Player.player_Main.DiTuGe_ChuShiHua();
        Player.player_Main.Player_ChuShiHua();
        Player.player_Main.Play_Time =float.Parse((string)js["shiJian"]);
        string ditu = (string)js["diTu"];
        string[] diTuS = ditu.Split(',');
        for (int i = 0; i < diTuS.Length; i++)
        {
            diTu_PanDuan(i,int.Parse(diTuS[i]));
        }
        Player.player_Main.DangQian = (int)js["qiDian"];
        if (Player.player_Main.moShi == Player.MoShi.PuTong)
        {
            Player.player_Main.Player_Ge[Player.player_Main.QiDian].sprite = Sprite_Manager.Main.Get_Sprite("Null");
        }
        else
        {
            Player.player_Main.Player_Ge[Player.player_Main.QiDian].sprite = Sprite_Manager.Main.Get_Sprite("Kuang");
        }
        Player.player_Main.MiWu_Play(Player.player_Main.DangQian);
        Player.player_Main.Player_Ge[Player.player_Main.DangQian].sprite = Sprite_Manager.Main.Get_Sprite("Player");
        Player.player_Main.IsKaiShi = true;
    }

    //存档时模式信息编码
    private int moShi_PanDuan(int size,Player.MoShi mo) {
        int moShi=0;
        switch (mo)
        {
            case Player.MoShi.PuTong:
                if (size == 15)
                    moShi = 1;
                else if (size == 25)
                    moShi = 2;
                else
                    moShi = 3;
                break;
            case Player.MoShi.MiWu:
                moShi = 4; break;
            case Player.MoShi.HeiYe:
                moShi = 5; break;
            default:
                break;
        }
        return moShi;
    }
    //读档时模式信息解码
    private Player.MoShi moShi_PanDuan(int moShi,out int Size )
    {
        Size = 0;
        switch (moShi)
        {
            case 1: Size = 15; return Player.MoShi.PuTong;
            case 2: Size = 25; return Player.MoShi.PuTong;
            case 3: Size = 41; return Player.MoShi.PuTong;
            case 4: Size = 41; return Player.MoShi.MiWu;
            case 5: Size = 41; return Player.MoShi.HeiYe;
        }
        return Player.MoShi.PuTong;
    }
    //存档时地图信息编码
    private string diTu_PanDuan(bool shang,bool xia,bool zuo ,bool you) {
        string srt ="";
        if (shang && xia && zuo && you)
            srt = "1";
        else if (!shang && !xia && !zuo && !you)
            srt = "2";
        else if (shang && xia && !zuo && !you)
            srt = "3";
        else if (!shang && !xia && zuo && you)
            srt = "4";
        else if (!shang && xia && zuo && you)
            srt = "5";
        else if (shang && !xia && zuo && you)
            srt = "6";
        else if (shang && xia && !zuo && you)
            srt = "7";
        else if (shang && xia && zuo && !you)
            srt = "8";
        else if (!shang && xia && !zuo && you)
            srt = "9";
        else if (!shang && xia && zuo && !you)
            srt = "10";
        else if (shang && !xia && !zuo && you)
            srt = "11";
        else if (shang && !xia && zuo && !you)
            srt = "12";
        else if (shang && !xia && !zuo && !you)
            srt = "13";
        else if (!shang && xia && !zuo && !you)
            srt = "14";
        else if (!shang && !xia && zuo && !you)
            srt = "15";
        else if (!shang && !xia && !zuo && you)
            srt = "16";
        return srt;
    }
    //读档时地图信息解码
    private void diTu_PanDuan(int name,int Id)
    {
        switch (Id)
        {
            case 1: Player.player_Main.DiTu[name].Set_FangXiang( true, true, true, true);break ;
            case 2: Player.player_Main.DiTu[name].Set_FangXiang(false, false, false, false);break ;
            case 3: Player.player_Main.DiTu[name].Set_FangXiang(true, true, false, false);break ;
            case 4: Player.player_Main.DiTu[name].Set_FangXiang(false, false, true, true);break ;
            case 5: Player.player_Main.DiTu[name].Set_FangXiang(false, true, true, true);break ;
            case 6: Player.player_Main.DiTu[name].Set_FangXiang(true, false, true, true);break ;
            case 7: Player.player_Main.DiTu[name].Set_FangXiang(true, true, false, true);break ;
            case 8: Player.player_Main.DiTu[name].Set_FangXiang(true, true, true, false);break ;
            case 9: Player.player_Main.DiTu[name].Set_FangXiang(false, true, false, true);break ;
            case 10: Player.player_Main.DiTu[name].Set_FangXiang(false, true, true, false);break ;
            case 11: Player.player_Main.DiTu[name].Set_FangXiang(true, false, false, true);break ;
            case 12: Player.player_Main.DiTu[name].Set_FangXiang(true, false, true, false);break ;
            case 13: Player.player_Main.DiTu[name].Set_FangXiang(true, false, false, false);break;
            case 14: Player.player_Main.DiTu[name].Set_FangXiang(false, true, false, false); break;
            case 15: Player.player_Main.DiTu[name].Set_FangXiang(false, false, true, false); break;
            case 16: Player.player_Main.DiTu[name].Set_FangXiang(false, false, false, true); break;
            default:
                break;
        }
    }
}
