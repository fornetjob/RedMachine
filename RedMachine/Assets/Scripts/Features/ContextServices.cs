using Assets.Scripts.Features.Configs;
using Assets.Scripts.Features.Identities;
using Assets.Scripts.Features.Pooling;
using Assets.Scripts.Features.Randoms;
using Assets.Scripts.Features.Resource;
using Assets.Scripts.Features.Times;
using System.Collections.Generic;

namespace Assets.Scripts.Features
{
    public class ContextServices: IService, IAttachContext
    {
        private List<IService>
            _services = new List<IService>();

        public ResourcesService resources;
        public ConfigService config;
        public IdentityService identity;
        public PoolService pool;
        public TimeService time;
        public RandomService random;

        public ContextServices()
        {
            resources = AddService(new ResourcesService());
            pool = AddService(new PoolService());
            identity = AddService(new IdentityService());

            config = AddService(new ConfigService(resources));

            time = AddService(new TimeService());

            random = AddService(new RandomService());
        }

        private T AddService<T>(T service)
            where T:IService
        {
            _services.Add(service);

            return service;
        }

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
    }
}
