using ProtoBuf;

namespace GameData {
    [ProtoContract(SkipConstructor = true, ImplicitFields = ImplicitFields.AllFields)]
    public class HeroSkillData {
        public string Name;
        public float Damage;
    }
}
