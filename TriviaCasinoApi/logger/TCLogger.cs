using Microsoft.Extensions.Logging;

public class TCLogger {
    private readonly ILogger _logger;

    public TCLogger(ILogger<TCLogger> logger) {
        _logger = logger;
    }

    public void LogInformation(string message) {
        _logger.LogInformation(message);
    }

    public void LogError(string message, Exception exception) {
        _logger.LogError(exception, message);
    }
}
