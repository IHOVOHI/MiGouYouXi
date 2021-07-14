using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Manager
{
    static Sprite_Manager main;
    public static Sprite_Manager Main {
        get {
            if (main == null) {
                main = new Sprite_Manager();
                main.sprites = new Dictionary<string, Sprite>();
            }
            return main;
        }
    }

    Dictionary<string, Sprite> sprites;
 
    public void Set_Sprite(string name,Sprite sprite) {
        if (sprites == null) sprites = new Dictionary<string, Sprite>();
        if (!sprites.ContainsKey(name))
        {
            sprites.Add(name, sprite);
        }
        else
        {
            Debug.Log("当前Sprite以存在 名称为："+name);
        }
    }

    public Sprite Get_Sprite(string name){
        Sprite sprite = null;
        if (sprites .ContainsKey(name))
        {
            sprite = sprites[name];
        }
        else
        {
            sprite = JiaZai_Sprite(name);
        }
        return sprite;
    }

    public Sprite JiaZai_Sprite(string name) {
        Sprite sprite = null;
        string LuJing = "Sprite/" + name;
        sprite = Resources.Load<Sprite>(LuJing);
        if (sprite != null)
        {
            sprites.Add(name,sprite);
        }
        else
        {
            Debug.Log("路径错误或物体不存在 名称为：" + name+ "    路径为：Resources/" + LuJing);
        }
        return sprite;
    }
}
