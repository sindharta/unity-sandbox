using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace GameData {

    public enum HeroJob {
        WARRIOR = 0,
        MAGICIAN,
        GUARDIAN,
    }

    [ProtoContract(SkipConstructor = true)]
    public class HeroData {
        [ProtoMember(1)]
        public uint ID;
        [ProtoMember(2)]
        public uint HP;
        [ProtoMember(3)]
        public uint MP;
        [ProtoMember(4)]
        public string Name;        
        [ProtoMember(5, AsReference = true)]
        public List<HeroSkillData> Skills;
        [ProtoMember(6), DefaultValue(HeroJob.WARRIOR)]
        public HeroJob Job;
        [ProtoMember(7)]
        public Color FavoriteColor;
        [ProtoMember(8)]
        public Vector3 WeaponPos; 

    }

}

