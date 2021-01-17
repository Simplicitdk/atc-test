using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Atc.TestTools
{
    /// <summary>
    /// Provides auto-generated data specimens generated by AutoFixture as an extension
    /// to xUnit.net's Theory attribute.
    /// </summary>
    /// <remarks>
    /// NSubstitute is used when the type is abstract, or when the <see cref="SubstituteAttribute"/> is applied.
    /// </remarks>
    public sealed class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoNSubstituteDataAttribute"/> class.
        /// </summary>
        public AutoNSubstituteDataAttribute()
            : base(FixtureFactory.Create)
        {
        }
    }
}