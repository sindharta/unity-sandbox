using ProtoBuf;
using System.Collections.Generic;

namespace GameData {

    [ProtoContract]
    public class HeroData {
        [ProtoMember(1)]
        public uint ID;
        [ProtoMember(2)]
        public uint HP;
        [ProtoMember(3)]
        public uint MP;
        [ProtoMember(4)]
        public string Name;
    }

    [ProtoContract]
    public class HeroDataDic {
        [ProtoMember(1)]
        public Dictionary<uint,HeroData> Items;

        public HeroDataDic() {
            Items = new Dictionary<uint,HeroData>();
        }
        
    }

}

