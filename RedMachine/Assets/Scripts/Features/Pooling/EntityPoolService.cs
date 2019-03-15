using Assets.Scripts.Features.Identities;
using System.Collections.Generic;

namespace Assets.Scripts.Features.Pooling
{
    public class EntityPoolService : IService, IAttachContext
    {
        #region Services

        private IdentityService
            _identity;

        #endregion

        private Context
            _context;

        private Pool<Entity>
            _entities = new Pool<Entity>();

        void IAttachContext.Attach(Context context)
        {
            _context = context;

            _identity = context.services.identity;
        }

        public int Count
        {
            get
            {
                return _entities.Items.Count;
            }
        }

        public List<Entity> GetEntities()
        {
            return _entities.Items;
        }

        public bool IsExists(int id)
        {
            return _entities.ContainsId(id);
        }

        public Entity NewEntity(int id = -1)
        {
            if (id == -1)
            {
                id = _identity.NewId();
            }

            var entity = _entities.Create(id);

            ((IAttachContext)entity).Attach(_context);

            return entity;
        }
    }
}
