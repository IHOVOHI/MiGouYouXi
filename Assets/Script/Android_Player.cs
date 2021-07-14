using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Android_Player : MonoBehaviour
{
    public Button shang;
    public Button xia;
    public Button zuo;
    public Button you;
    bool KaiShi;
    float time;
    int fangXiang;
    float yanChi = 0.2f;


    public void Butter_OnPointerDown(int fangXiang)
    {
        if (Player.player_Main.IsKaiShi)
        {
            this.fangXiang = fangXiang;
            KaiShi = true;
            time = Time.time + yanChi;
            Player.player_Main.KongZhi_Main(fangXiang, Player.player_Main.moShi);
        }
        
    }

    public void Update()
    {
        if (Time.time>time && KaiShi && Player.player_Main.IsKaiShi)
        {
            Player.player_Main.KongZhi_Main(this.fangXiang, Player.player_Main.moShi);
        }
    }

    public void Butter_OnPointerUp()
    {
        KaiShi = false;
    }

    public void Butter_OnPointerExit()
    {
        KaiShi = false;
    }
    
}