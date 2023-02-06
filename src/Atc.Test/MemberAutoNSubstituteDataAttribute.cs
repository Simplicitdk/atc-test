namespace Atc.Test;

/// <summary>
/// Provides a data source for a data theory, with the data coming from
/// one of the following sources and combined with auto-generated data
/// specimens generated by AutoFixture and NSubstitute:
/// <list type="number">
/// <item>A static property</item>
/// <item>A static field</item>
/// <item>A static method (with parameters)</item>
/// </list>
/// The member must return something compatible with
/// IEnumerable&lt;object[]&gt; with the test data.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[DataDiscoverer("Xunit.Sdk.MemberDataDiscoverer", "xunit.core")]
public sealed class MemberAutoNSubstituteDataAttribute : MemberDataAttributeBase
{
    public MemberAutoNSubstituteDataAttribute(string memberName, params object[] parameters)
        : base(memberName, parameters)
    {
    }

    protected override object[] ConvertDataItem(MethodInfo testMethod, object item)
    {
        if (item is not object[] values)
        {
            throw new ArgumentException(
                $"Property {MemberName} on {MemberType.Name} yielded an item that is not an object[]",
                nameof(item));
        }

        var fixture = FixtureFactory.Create();

        return values
            .Concat(testMethod
                .GetParameters()
                .Skip(values.Length)
                .Select(p => GetSpecimen(fixture, p)))
            .ToArray();
    }

    private static object GetSpecimen(
        IFixture fixture,
        ParameterInfo parameter)
    {
        var attributes = parameter
            .GetCustomAttributes()
            .OfType<IParameterCustomizationSource>()
            .OrderBy(x => x is FrozenAttribute);

        foreach (var attribute in attributes)
        {
            attribute
                .GetCustomization(parameter)
                .Customize(fixture);
        }

        return new SpecimenContext(fixture)
            .Resolve(parameter);
    }
}