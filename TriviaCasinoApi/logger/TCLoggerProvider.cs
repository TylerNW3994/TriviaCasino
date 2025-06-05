public class TCLoggerProvider : ILoggerProvider {
    private readonly ILoggerFactory _loggerFactory;

    public TCLoggerProvider(ILoggerFactory loggerFactory) {
        _loggerFactory = loggerFactory;
    }

    public ILogger CreateLogger(string categoryName) {
        return _loggerFactory.CreateLogger<TCLogger>();
    }

    public void Dispose() {
    }
}
