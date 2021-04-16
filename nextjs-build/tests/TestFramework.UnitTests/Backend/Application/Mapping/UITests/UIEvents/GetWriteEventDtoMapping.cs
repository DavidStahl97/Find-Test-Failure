﻿using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class GetWriteEventDtoMapping : GetUIElementEventDtoMapping<WriteEvent, GetWriteEventDto>
    {
        protected override string TypeDiscriminator => "GetWriteEventDto";

        [Fact]
        public void ShouldMap_Input() => Map(x => x.Input, x => x.Input);

        [Fact]
        public void ShouldMap_GenerateUnique() => Map(x => x.GenerateUnique, x => x.GenerateUnique);
    }
}
