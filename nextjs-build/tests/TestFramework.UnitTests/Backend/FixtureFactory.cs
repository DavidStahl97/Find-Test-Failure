using AutoFixture;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.UnitTests.Backend
{
    class FixtureFactory
    {
        public static IFixture GetCustomizedFixture()
            => new Fixture().Customize(new CompositeCustomization(new Customize()));

        class Customize : ICustomization
        {
            void ICustomization.Customize(IFixture fixture)
            {
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                fixture.Customize<UIElement>(x => x
                    .With(p => p.Name, fixture.CreateString(50))
                    .With(p => p.FindBy, fixture.CreateString(255))
                    .Without(p => p.Page));

                fixture.Customize<ClickEvent>(x => x
                    .Without(p => p.UIElement));

                fixture.Customize<WriteEvent>(x => x
                    .Without(p => p.UIElement));

                fixture.Customizations.Add(new TypeRelay(typeof(UIEvent), typeof(ClickEvent)));
                fixture.Customizations.Add(new TypeRelay(typeof(UIEvent), typeof(WaitEvent)));
                fixture.Customizations.Add(new TypeRelay(typeof(UIEvent), typeof(WriteEvent)));
            }
        }
    }
}
