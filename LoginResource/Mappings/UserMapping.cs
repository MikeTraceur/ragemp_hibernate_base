using FluentNHibernate.Mapping;
using LoginResource.Domain;

namespace LoginResource.Mappings
{
    class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.Name);
            Map(x => x.Passwort);
            Map(x => x.SocialID);
            Map(x => x.WhitelistStatus);
            HasMany<UserCharacter>(x => x.UserCharacter).KeyColumn("userId").Inverse();
            Table("User");
        }
    }
}
