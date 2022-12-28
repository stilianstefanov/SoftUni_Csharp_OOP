namespace TemplatePattern
{
    public class SourDough : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the SourDough Bread (20 minutes)");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering Ingredients for SourDough Bread");
        }
    }
}
