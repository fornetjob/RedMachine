using Assets.Scripts.Features.Serialize;
using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Randoms;
using Assets.Scripts.Features.Resource;
using Assets.Scripts.Features.Times;
using Assets.Scripts.Features.Views;
using System.Collections.Generic;

namespace Assets.Scripts.Features
{
    public class ContextServices: IService, IAttachContext
    {
        #region Fields

        private List<IService>
            _services = new List<IService>();

        #endregion

        #region ctor

        public ContextServices(params IService[] services)
        {
            _services.AddRange(services);

            AddService<IResourcesService>(new ResourcesService());
            AddService(new PoolService());
            AddService(new IdentityService());
            AddService(new SerializeService());
            AddService(new TimeService());
            AddService(new RandomService());
            AddService(new ViewService());

            resources = ProvideService<IResourcesService>();
            pool = ProvideService<PoolService>();
            identity = ProvideService<IdentityService>();
            serialize = ProvideService<SerializeService>();
            time = ProvideService<TimeService>();
            random = ProvideService<RandomService>();
            view = ProvideService<ViewService>();
        }

        #endregion

        #region Properties

        public readonly IResourcesService resources;
        public readonly SerializeService serialize;
        public readonly IdentityService identity;
        public readonly PoolService pool;
        public readonly TimeService time;
        public readonly RandomService random;
        public readonly ViewService view;

        #endregion

        #region Public methods

        public void Attach(Context context)
        {
            for (int i = 0; i < _services.Count; i++)
            {
                var service = _services[i];

                if (service is IAttachContext)
                {
                    ((IAttachContext)service).Attach(context);
                }
            }
        }

        #endregion

        #region Private methods

        private T ProvideService<T>()
        {
            return (T)_services.Find(p => p is T);
        }

        private T AddService<T>(T service)
            where T:IService
        {
            var existService = ProvideService<T>();

            if (existService != null)
            {
                return existService;
            }

            _services.Add(service);

            return service;
        }

        #endregion
    }
}