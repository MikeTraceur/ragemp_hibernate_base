using FluentNHibernate.Mapping;
using LoginResource.Domain;

namespace LoginResource.Mappings
{
    class UserCharacterMapping : ClassMap<UserCharacter>
    {
        public UserCharacterMapping()
        {
            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Z);
            Map(x => x.RX);
            Map(x => x.RY);
            Map(x => x.RZ);
            Map(x => x.CSlot1);
            References(x => x.user, "userId").Cascade.None();
            Table("user_character");
        }
    }
}
