using ProtoBuf;
using System.Collections.Generic;

namespace GameData {

    [ProtoContract(SkipConstructor = true, ImplicitFields = ImplicitFields.AllFields)]
    public class HeroData {
        public uint ID;
        public uint HP;
        public uint MP;
        public string Name;

        [ProtoMember(5, AsReference = true)]
        public List<HeroSkillData> Skills;

    }

}

