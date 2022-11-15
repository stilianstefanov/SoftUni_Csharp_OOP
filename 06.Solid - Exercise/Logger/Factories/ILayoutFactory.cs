namespace Logger.Factories
{
    using Logger.CustomLayouts;
    using Contracts;

    using Logger.Core.Layouts;
    using Logger.Core.Layouts.Contracts;
    

    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string layoutType)
        {
            switch (layoutType)
            {
                case "SimpleLayout":
                    return new SimpleLayout();
                case "XmlLayout":
                    return new XmlLayout();
                default:
                    return null;
            }
        }
    }
}
