namespace Api1.HealthChecks
{
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using System.Threading;
    using System.Threading.Tasks;

    public class WeirdHealthCheck : IHealthCheck
    {
        private readonly int startSecond;
        private readonly int endSecond;
        private readonly string goodMessage;
        private readonly string badMessage;

        public WeirdHealthCheck(int startSecond, int endSecond, string goodMessage, string badMessage)
        {
            this.startSecond = startSecond;
            this.endSecond = endSecond;
            this.goodMessage = goodMessage;
            this.badMessage = badMessage;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int second = DateTime.Now.Second;

            if (second > this.startSecond && second <= this.endSecond)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(this.badMessage));
            }

            return Task.FromResult(HealthCheckResult.Healthy(this.goodMessage));
        }
    }
}
