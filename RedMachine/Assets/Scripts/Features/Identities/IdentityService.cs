namespace Assets.Scripts.Features.Identities
{
    public class IdentityService: IService, IAttachContext
    {
        #region Fields

        private ComponentPool<IdentityComponent>
            _identityPool;

        #endregion

        #region IAttachContext

        void IAttachContext.Attach(Context context)
        {
            _identityPool = context.services.pool.Provide<IdentityComponent>();

            _identityPool.Create();
        }

        #endregion

        #region Public methods

        public int NewId()
        {
            var identity = _identityPool.Single();

            identity.Identity++;

            return identity.Identity;
        }

        #endregion
    }
}