

namespace Logger.Factories.Contracts
{
    using Logger.Core.Layouts.Contracts;


    public interface ILayoutFactory
    {
        ILayout CreateLayout(string layoutType);
    }
}
