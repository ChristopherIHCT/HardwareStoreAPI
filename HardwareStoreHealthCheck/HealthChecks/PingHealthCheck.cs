using System.Net.NetworkInformation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HardwareStore.HealthCheckApi.HealthChecks
{
    public class PingHealthCheck : IHealthCheck
    {
        private readonly string _host;

        public PingHealthCheck(string host)
        {
            _host = host;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var ping = new Ping();

            HealthCheckResult result;

            try
            {
                var reply = await ping.SendPingAsync(_host, 10);

                switch (reply.Status)
                {
                    case IPStatus.Success:
                        result = HealthCheckResult.Healthy($"El host {_host} funciona a la perfeccion");
                        break;
                    case IPStatus.BadDestination:
                        result = HealthCheckResult.Unhealthy($"El host {_host} no es accesible");
                        break;
                    case IPStatus.TimedOut:
                    case IPStatus.TimeExceeded:
                        result = HealthCheckResult.Degraded($"El host {_host} no responde rapidamente");
                        break;
                    default:
                        result = HealthCheckResult.Unhealthy($"El host {_host} no es accesible de ninguna forma");
                        break;
                }
            }
            catch (Exception ex)
            {
                result = HealthCheckResult.Unhealthy(ex.Message);
            }

            return result;
        }
    }
}
