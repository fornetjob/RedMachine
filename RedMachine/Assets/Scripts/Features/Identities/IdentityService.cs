namespace Assets.Scripts.Features.Identities
{
    public class IdentityService: IService, IAttachContext
    {
        private Entity
            _entity;

        public void Attach(Context context)
        {
            _entity = context.entities.NewEntity(0);
            _entity.Add(new IdentityComponent());
        }

        public int GetId()
        {
            return _entity.Get<IdentityComponent>().value.Id;
        }

        public int NewId()
        {
            var identity = _entity.Get<IdentityComponent>();

            identity.value.Id++;

            identity.Set(identity.value);

            return identity.value.Id;
        }
    }
}
