using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRuns
{
    public class UITestRunCaseMapping : MappingTestBase<UITestCase, UITestRunCase>
    {
        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_StartUrl() => Map(x => x.StartUrl, x => x.StartUrl);

        [Fact]
        public void ShouldMap_Start() => Map().Start.Should().BeNull();

        [Fact]
        public void ShouldMap_Duration() => Map().Duration.Should().BeNull();

        [Fact]
        public void ShouldMap_State() => Map().State.Should().Be(UITestEventState.NotStarted);

        [Fact]
        public void ShouldMap_Browser() => Map().Browser.Should().NotBeNull();

        [Fact]
        public void ShouldMap_Events() => Map(x => x.Events.Count, x => x.Events.Count);

        [Fact]
        public void ShouldMap_UITestRunId() => Map().UITestRunId.Should().Be(0);

        [Fact]
        public void ShouldMap_TestRun() => Map().TestRun.Should().BeNull();

        [Fact]
        public void ShouldMap_DefaultWaitForUIElement() => Map(x => x.DefaultWaitForUIElement, x => x.DefaultWaitForUIElement);

        [Fact]
        public void ShouldMap_AutomaticallyStarted() => Map(x => x.RunsPeriodically, x => x.AutomaticallyStarted);

        [Fact]
        public void ShouldMap_FailureSendedState() => Map().FailureSendedState.Should().Be(UITestRunCaseFailureSendedState.NotSended);
    }
}
