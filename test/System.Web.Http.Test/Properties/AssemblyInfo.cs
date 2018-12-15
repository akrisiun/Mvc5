// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

[assembly: CLSCompliant(false)]


namespace Microsoft.TestCommon
{
    internal class FactAttribute : Xunit.FactAttribute
    {
    }

    internal class TheoryAttribute : Xunit.TheoryAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [CLSCompliant(false)]
    [DataDiscoverer("Xunit.Sdk.InlineDataDiscoverer", "xunit.core")]
    internal sealed class InlineDataAttribute : Xunit.Sdk.DataAttribute 
    {
        public InlineDataAttribute(params object[] data) {
            Wrap = new Xunit.InlineDataAttribute(data);
        }

        Xunit.InlineDataAttribute Wrap { get; set; }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod) => Wrap.GetData(testMethod);
    }

    internal class Assert : Assert1
    {
    }
}