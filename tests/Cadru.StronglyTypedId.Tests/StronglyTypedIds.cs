using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Cadru.StronglyTypedId;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

[assembly: StronglyTypedIdDefaults(BackingType = BackingType.Guid)]

namespace Cadru.StronglyTypedId.Tests
{
    [StronglyTypedId(BackingType = BackingType.Char)]
    public partial record struct PopulationId { }

    [StronglyTypedId]
    public partial struct EnvironmentId { }

    [StronglyTypedId(BackingType = BackingType.Guid)]
    public partial class LicenseId { }

    [StronglyTypedId(BackingType = BackingType.Byte)]
    public partial record struct OrganizationId { }
    public readonly partial record struct CustomerId(Guid Value) { }
    public partial record class UserId(Guid Value) { }
    public partial record class ClassId(Guid Value) { }


    [StronglyTypedId(BackingType =  BackingType.Guid, Converters = StronglyTypedIdConverter.SystemTextJson)]
    public partial record struct TestId { }

    [StronglyTypedId(BackingType =  BackingType.Int)]
    public partial record struct TestIntId { }

    [StronglyTypedId(BackingType =  BackingType.Long)]
    public partial record struct TestLongId { }

    [StronglyTypedId(BackingType =  BackingType.Short)]
    public partial record struct TestShortId { }

    [StronglyTypedId(BackingType =  BackingType.String)]
    public partial record struct TestStringId { }

    [StronglyTypedId(BackingType =  BackingType.NullableString)]
    public partial record struct TestNullableStringId { }

    [StronglyTypedId]
    public partial record struct NathanId { }

    [StronglyTypedId]
    public partial record TestRecordId { }

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var a = EnvironmentId.New();
            var b = LicenseId.New();
            var c = TestIntId.New();
            var d = TestNullableStringId.New();
            var e = TestStringId.New("test");
            var f = OrganizationId.Empty;
            var g = TestRecordId.Empty;
            var h = new CustomerId();
            var j = new UserId(Guid.NewGuid());

            var x = JsonSerializer.Serialize(a);

            OrganizationId i = default;
            i = new();

            a = new();

            g.Deconstruct(out var v);

            h = new();

            //g = new()
            f = new();
            a = new();
            //b = new();
            c = new();
            c = TestIntId.Parse("3");
            c = new(3);
            e = new();
        }
    }
}
