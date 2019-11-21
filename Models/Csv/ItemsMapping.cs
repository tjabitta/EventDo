using CsvHelper.Configuration;

namespace EvendoTest_v1.Search.Models.Csv
{
    public class ItemsMapping : ClassMap<ItemRecord>
    {
        public ItemsMapping()
        {
            Map(x => x.GTIN).Name("GTIN-14");
            Map(x => x.BrandName).Name("Brand Name");
            Map(x => x.Name).Name("Name");
            Map(x => x.Size).Name("Size");
            Map(x => x.Ingredients).Name("Ingredients");
            Map(x => x.ServingSize).Name("Serving Size");
            Map(x => x.ServingsPerContainer).Name("ServingsPerContainer");
            Map(x => x.ServingsPerContainer).Name("Servings Per Container");
            Map(x => x.Calories).Name("Calories");
            Map(x => x.FatCalories).Name("Fat Calories");
            Map(x => x.Fat).Name("Fat (g)");
            Map(x => x.SaturatedFat).Name("Saturated Fat (g)");
            Map(x => x.TransFat).Name("Trans Fat (g)");
            Map(x => x.PolyunsaturatedFat).Name("Polyunsaturated Fat (g)");
            Map(x => x.Monounsaturated).Name("Monounsaturated Fat (g)");
            Map(x => x.Cholesterol).Name("Cholesterol (mg)");
            Map(x => x.Sodium).Name("Sodium (mg)");
            Map(x => x.Potassium).Name("Potassium (mg)");
            Map(x => x.Carbohydrate).Name("Carbohydrate (g)");
            Map(x => x.Fiber).Name("Fiber (g)");
            Map(x => x.Sugars).Name("Sugars (g)");
            Map(x => x.Protein).Name("Protein (g)");
            Map(x => x.Author).Name("Author");
            Map(x => x.Publisher).Name("Publisher");
            Map(x => x.Pages).Name("Pages");
            Map(x => x.AlcoholByVolume).Name("Alcohol By Volume");
            Map(x => x.Images).Name("Images");
        }
    }

    public class ItemRecord
    {
    public string GTIN { get; set; }
    public string BrandName { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public string Ingredients { get; set; }
    public string ServingSize { get; set; }
    public string ServingsPerContainer { get; set; }
    public string Calories { get; set; }
    public string FatCalories { get; set; }
    public string Fat { get; set; }
    public string SaturatedFat { get; set; }
    public string TransFat { get; set; }
    public string PolyunsaturatedFat { get; set; }
    public string Monounsaturated { get; set; }
    public string Cholesterol { get; set; }
    public string Sodium { get; set; }
    public string Potassium { get; set; }
    public string Carbohydrate { get; set; }
    public string Fiber { get; set; }
    public string Sugars { get; set; }
    public string Protein { get; set; }
    public string Author { get; set; }
    public string Format { get; set; }
    public string Publisher { get; set; }
    public string Pages { get; set; }
    public string AlcoholByVolume { get; set; }
    public string Images { get; set; }
  }
}