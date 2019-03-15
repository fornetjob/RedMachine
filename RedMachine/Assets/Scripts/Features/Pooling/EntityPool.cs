using Assets.Scripts.Features.Identities;

namespace Assets.Scripts.Features.Pooling
{
    public class EntityPool: PoolBase<Entity>
    {
        #region Services

        private IdentityService
            _identity;

        #endregion

        private readonly Context
            _context;

        public EntityPool(Context context)
        {
            _context = context;

            _identity = _context.services.identity;
        }

        public Entity NewEntity(int id = -1)
        {
            if (id == -1)
            {
                id = _identity.NewId();
            }

            var entity = Create(id);

            ((IAttachContext)entity).Attach(_context);

            return entity;
        }
    }
}
