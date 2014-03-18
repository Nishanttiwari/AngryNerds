namespace ForecastService
{

    using System.Collections.Generic;
    using TsExponentialSmoothing;

    public interface IForecaster
    {
        bool Forecast(int periodicityHint, List<double> data, int holdout, int range, string mode, double minValue, double maxValue, out IList<double> parameters, out IList<IScalarDistribution> forecast, out IList<double> residuals);
    }
}
