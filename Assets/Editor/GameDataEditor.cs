using UnityEngine;
using UnityEditor;
using GameData;
using System.IO;

public class GameDataEditor  {

    [MenuItem("GameData/Create")]
    public static void CreateGameData() {
        //prepare data
        HeroDataDic data_dic = new HeroDataDic();
        AddData (data_dic,1,"Crono");
        AddData (data_dic,2,"Magus");
        AddData (data_dic,3,"Marle");
        AddData (data_dic,4,"Lucca");
        AddData (data_dic,5,"Frog");
        
        //serialize
        GameDataSerializer gameDataSerializer = new GameDataSerializer();
        FileStream fs = new FileStream("Assets/Resources/GameData.bytes",FileMode.Create, FileAccess.Write, FileShare.None);
        gameDataSerializer.Serialize(fs,data_dic);       
        fs.Close ();
        EditorUtility.DisplayDialog("GameData","Game Data created successfully","Ok");
         
    }
     
    public static void AddData(HeroDataDic dic, uint key, string str) {
        HeroData new_data = new HeroData();
        new_data.ID = key;
        new_data.Name = str;
        new_data.HP = (uint) Random.Range(0,1000);
        new_data.MP = (uint) Random.Range(0,100);
        dic.Items.Add(key,new_data);
    }    
}
