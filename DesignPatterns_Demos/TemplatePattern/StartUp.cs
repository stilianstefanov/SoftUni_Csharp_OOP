namespace TemplatePattern
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Bread sourDough = new SourDough();
            sourDough.Make();

            Bread twelveGrain = new TwelveGrain();
            twelveGrain.Make();

            Bread wholeWheat = new WholeWheat();
            wholeWheat.Make();
        }
    }
}