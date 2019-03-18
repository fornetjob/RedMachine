using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Editor.Entities.Systems
{
    public class SystemsTest
    {
        [Test]
        public void Order()
        {
            var context = new Context();

            context.systems.Add(new TestOrderSystem().SetOrder(int.MaxValue));
            context.systems.Add(new TestOrderSystem().SetOrder(int.MaxValue - 1));
            context.systems.Add(new TestOrderSystem());

            var field = context.systems.GetType().GetField("_updateSystems", BindingFlags.Instance | BindingFlags.NonPublic);

            List<IUpdateSystem> updates = (List<IUpdateSystem>)field.GetValue(context.systems);

            Assert.NotNull(updates);

            Assert.AreEqual(updates[0].GetOrder(), 0);
            Assert.AreEqual(updates[1].GetOrder(), int.MaxValue - 1);
            Assert.AreEqual(updates[2].GetOrder(), int.MaxValue);
        }
    }
}
