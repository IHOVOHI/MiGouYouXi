using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiTuGe
{
    public GameObject getZi_obj;
    bool shang;
    bool xia;
    bool zuo;
    bool you;
    public bool Shang{
        get { return shang; }
        set {
            shang = value;
            getZi_obj.GetComponent<Image>().sprite = sprite_JiaZai(shang, xia, zuo, you);
        }
    }
    public bool Xia
    {
        get { return xia; }
        set
        {
            xia = value;
            getZi_obj.GetComponent<Image>().sprite = sprite_JiaZai(shang, xia, zuo, you);
        }
    }
    public bool Zuo
    {
        get { return zuo; }
        set
        {
            zuo = value;
            getZi_obj.GetComponent<Image>().sprite = sprite_JiaZai(shang, xia, zuo, you);
        }
    }
    public bool You
    {
        get { return you; }
        set
        {
            you = value;
            getZi_obj.GetComponent<Image>().sprite = sprite_JiaZai(shang, xia, zuo, you);
        }
    }

    public DiTuGe(GameObject getZi_obj, bool shang, bool xia, bool zuo, bool you)
    {
        this.getZi_obj = getZi_obj;
        this.shang = shang;
        this.xia = xia;
        this.zuo = zuo;
        this.you = you;
        Set_Obj_Sprite();
    }

    public void Set_FangXiang(bool shang, bool xia, bool zuo, bool you) {
        this.shang = shang;
        this.xia = xia;
        this.zuo = zuo;
        this.you = you;
        getZi_obj.GetComponent<Image>().sprite = sprite_JiaZai(shang, xia, zuo, you);
    }

    void Set_Obj_Sprite() { 
        Image image = getZi_obj.AddComponent<Image>();
        image.sprite = sprite_JiaZai(shang, xia, zuo, you);
        image.color = new Color(0, 0.8705883f, 1,1);
    }

    Sprite sprite_JiaZai(bool shang, bool xia, bool zuo, bool you) {
        Sprite sprite = null;
        if (shang==true && xia==true && zuo==true && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Jiao");
        else if(shang == false && xia == true && zuo == true && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Shang");
        else if (shang == true && xia == false && zuo == true && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Xia");
        else if (shang == true && xia == true && zuo == false && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Zuo");
        else if (shang == true && xia == true && zuo == true && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("You");
        else if (shang == false && xia == true && zuo == false && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Zuo_Shang");
        else if(shang == true && xia == false && zuo == false && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Zuo_Xia");
        else if(shang == false && xia == true && zuo == true && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("You_Shang");
        else if (shang == true && xia == false && zuo == true && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("You_Xia");
        else if (shang == true && xia == true && zuo == false && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("Zuo_You");
        else if (shang == false && xia == false && zuo == true && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("Shang_Xia");
        else if (shang == true && xia == false && zuo == false && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("Shang_Que");
        else if (shang == false && xia == true && zuo == false && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("Xia_Que");
        else if (shang == false && xia == false && zuo == true && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("Zuo_Que");
        else if (shang == false && xia == false && zuo == false && you == true)
            sprite = Sprite_Manager.Main.Get_Sprite("You_Que");
        else if (shang == false && xia == false && zuo == false && you == false)
            sprite = Sprite_Manager.Main.Get_Sprite("Kuang");
        return sprite;
    }
}
