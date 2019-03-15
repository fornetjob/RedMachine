using System.Collections.Generic;

namespace Assets.Scripts.Features.Pooling
{
    public class Pool<T>: IPool
        where T: IPoolItem, new()
    {
        private Queue<T>
           _destroyed = new Queue<T>();

        private Dictionary<int, T>
            _dict = new Dictionary<int, T>();

        public T Single()
        {
            if (Items.Count != 1)
            {
                throw new System.ArgumentOutOfRangeException("count");
            }

            return Items[0];
        }

        public List<T> Items = new List<T>();

        public bool ContainsId(int id)
        {
            return _dict.ContainsKey(id);
        }

        public T GetById(int id)
        {
            return _dict[id];
        }

        public T Create(int id)
        {
            T item;

            if (_destroyed.Count == 0)
            {
                item = new T();
            }
            else
            {
                item = _destroyed.Dequeue();
            }

            item.Id = id;
            item.Attach(this);

            Items.Add(item);

            _dict.Add(id, item);

            return item;
        }

        void IPool.Destroy(IPoolItem item)
        {
            T itemToDestroy = (T)item;

            Items.Remove(itemToDestroy);
            _dict.Remove(item.Id);

            _destroyed.Enqueue(itemToDestroy);
        }

        IPoolItem IPool.GetById(int id)
        {
            return _dict[id];
        }
    }
}
