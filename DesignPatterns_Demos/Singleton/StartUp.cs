
using Singleton;

var db = SingletonDataContainer.Instance;

Console.WriteLine(db.GetPopulation("Varna"));