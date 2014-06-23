using ProtoBuf;
using System.Collections.Generic;

namespace GameData {

    [ProtoContract(SkipConstructor = true, ImplicitFields = ImplicitFields.AllFields)]
    public class GameDatabase {
        [ProtoMember(1)]
        public Dictionary<uint, HeroData> Hero;

        public void Init() {
            Hero = new Dictionary<uint, HeroData>();
        }

    }
}
