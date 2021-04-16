﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestCases
{
    public class GetUITestCaseDtoMapping : MappingTestBase<UITestCase, GetUITestCaseDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_StartUrl() => Map(x => x.StartUrl, x => x.StartUrl);

        [Fact]
        public void ShouldMap_DefaultWaitForUIElement() => Map(x => x.DefaultWaitForUIElement, x => x.DefaultWaitForUIElement);

        [Fact]
        public void ShouldMap_RunsPeriodically() => Map(x => x.RunsPeriodically, x => x.RunsPeriodically);
    }
}
