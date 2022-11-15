namespace Logger.Factories.Contracts
{
    using Logger.Core.Appenders.Contracts;
    using Logger.Core.Layouts.Contracts;

    public interface IAppenderFactory
    {
        IAppender CreateAppender(string appenderType, ILayout layout, string reportLevel);
    }
}
