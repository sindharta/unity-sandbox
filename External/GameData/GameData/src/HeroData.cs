using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData {

    public enum HeroJob {
        WARRIOR = 0,
        MAGICIAN,
        GUARDIAN,
    }

    [ProtoContract(SkipConstructor = true, ImplicitFields = ImplicitFields.AllFields)]
    public class HeroData {
        public uint     ID;
        public uint     HP;
        public uint     MP;
        public string   Name;
        
        //public Color    FavoriteColor;
        //public Vector3  WeaponPos; 

        [ProtoMember(6, AsReference = true)]
        public List<HeroSkillData> Skills;

        [ProtoMember(7), DefaultValue(HeroJob.WARRIOR)]
        public HeroJob Job;
    }

}

