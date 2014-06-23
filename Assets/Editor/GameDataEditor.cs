using UnityEngine;
using UnityEditor;
using GameData;
using System.IO;
using System.Collections.Generic;

public class GameDataEditor  {

    [MenuItem("GameData/Create")]
    public static void CreateGameData() {
        //prepare data
        GameDatabase database = new GameDatabase();
        database.Init();
        HeroData crono = AddHeroData (database,1,"Crono", HeroJob.WARRIOR);
        HeroData magus = AddHeroData (database,2,"Magus", HeroJob.MAGICIAN);
        AddHeroData (database,3,"Marle",HeroJob.MAGICIAN);
        AddHeroData (database,4,"Lucca",HeroJob.MAGICIAN);
        AddHeroData (database,5,"Frog" ,HeroJob.GUARDIAN);
        
        //Skills
        crono.Skills = new List<GameData.HeroSkillData>(3);
        magus.Skills = new List<GameData.HeroSkillData>(3);
        
        crono.Skills.Add(CreateHeroSkillData("Fire",10));
        crono.Skills.Add(CreateHeroSkillData("Blizzard",12));
        crono.Skills.Add(CreateHeroSkillData("Thunder",14));
        magus.Skills.Add(crono.Skills[0]);
        magus.Skills.Add(crono.Skills[1]);
        magus.Skills.Add(CreateHeroSkillData("Ultima",999999));
                    
        //serialize
        GameDataSerializer gameDataSerializer = new GameDataSerializer();
        FileStream fs = new FileStream("Assets/Resources/GameData.bytes",FileMode.Create, FileAccess.Write, FileShare.None);
        gameDataSerializer.Serialize(fs,database);       
        fs.Close ();
        EditorUtility.DisplayDialog("GameData","Game Data created successfully","Ok");
         
    }
     
    public static HeroData AddHeroData(GameDatabase database, uint key, string str, HeroJob job) {
        HeroData new_data = new HeroData();
        new_data.ID = key;
        new_data.Name = str;
        new_data.Job = job;
        new_data.HP = (uint) Random.Range(0,1000);
        new_data.MP = (uint) Random.Range(0,100);
        database.Hero.Add(key,new_data);
        return new_data;
    }    
    
    public static HeroSkillData CreateHeroSkillData(string name, float damage) {
        HeroSkillData new_skill_data = new HeroSkillData();
        new_skill_data.Name   = name;
        new_skill_data.Damage = damage;
        return new_skill_data;
    }
}
