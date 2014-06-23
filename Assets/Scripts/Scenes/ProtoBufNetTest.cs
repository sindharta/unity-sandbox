using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameData;
using System.IO;


public class ProtoBufNetTest : MonoBehaviour {

    GameDatabase m_gameData;
    GameDataSerializer m_gameDataSerializer;

    // Use this for initialization
    void Start () {
        TextAsset textAsset = (TextAsset) Resources.Load("GameData", typeof(TextAsset));
        if (null==textAsset) {
            Debug.LogError("GameData not detected. Run GameData|Create first");
            return;
        }
        
        GameDataSerializer gameDataSerializer = new GameDataSerializer();
        if (null == gameDataSerializer) {
            Debug.LogError("Failed to initialize game data serializer");
            return;
        }
        
        MemoryStream s = new MemoryStream(textAsset.bytes);
        
        object data = gameDataSerializer.Deserialize(s,null,typeof(GameDatabase));
        m_gameData = (GameDatabase) (data);
        s.Close();
    } 

    //
    void OnGUI() {
        if (null == m_gameData || null== m_gameData.Hero || m_gameData.Hero.Count<=0)
            return;
           
        const int y_start = 10;
        const int y_diff  = 25;
        const int x_start = 10;
        const int x_diff  = 15;
        
        int y = y_start;
        var enumerator = m_gameData.Hero.GetEnumerator();
        while (enumerator.MoveNext()) {
            var cur = enumerator.Current;
            HeroData cur_data = cur.Value;
            int x = x_start;
            string info = string.Format("{0} {1} {2} {3}",cur_data.ID, cur_data.Name, cur_data.HP, cur_data.MP);
            GUI.Label(new Rect(x, y , 300, 30), info); y+= y_diff;
                        
            //list skills
            if (null==cur_data.Skills)
                continue;
            
            x+= x_diff;
            var skill_enumerator = cur_data.Skills.GetEnumerator();
            while (skill_enumerator.MoveNext()) {
                HeroSkillData cur_skill_data = skill_enumerator.Current;
                string skill_info = string.Format("{0} {1}",cur_skill_data.Name, cur_skill_data.Damage);
                GUI.Label(new Rect(x + x_diff, y, 300, 30), skill_info);y+= y_diff;
            }                                   
        }    
        
        if (GUI.Button(new Rect(150,15,300,25),"Change the first skill's name of the first hero")) {
            var skill_enumerator = m_gameData.Hero.GetEnumerator();
            skill_enumerator.MoveNext();
            HeroData first_hero = skill_enumerator.Current.Value;
            if (null==first_hero.Skills || first_hero.Skills.Count<=0) {
                Debug.LogWarning("The first hero doesn't have any skill");
                return;
            }
            
            HeroSkillData first_skill = first_hero.Skills[0];
            first_skill.Name = "AsReference works !";
        }
        
        GUI.Label(new Rect(320, 160, 300, 30), "When modifying GameData. Don't forget to: ");
        GUI.Label(new Rect(320, 190, 300, 30), "1. Rebuild the GameData Solution");
        GUI.Label(new Rect(320, 220, 300, 30), "2. Run GameDataSerializer project");
        
    }
}
