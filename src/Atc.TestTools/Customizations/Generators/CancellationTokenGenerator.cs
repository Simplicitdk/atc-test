using System.Threading;
using AutoFixture.Kernel;

namespace Atc.TestTools.Customizations.Generators
{
    /// <summary>
    /// Responsible for generating <see cref="CancellationToken"/> instances
    /// that has not been canceled.
    /// </summary>
    public class CancellationTokenGenerator : ISpecimenBuilder
    {
        /// <inheritdoc/>
        public object Create(object request, ISpecimenContext context)
        {
            if (!request.IsRequestFor<CancellationToken>())
            {
                return new NoSpecimen();
            }

            return new CancellationToken(canceled: false);
        }
    }
}