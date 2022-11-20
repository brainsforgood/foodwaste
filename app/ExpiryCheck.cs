using System.Collections;

namespace Expiry;

public readonly record struct Product(string name, DateTime expiry);

public class ExpiryCheck
{

    public static ArrayList TempCreateProducts()
    {
        ArrayList products = new ArrayList();

        products.Add(new Product("appel", DateTime.Parse("10 November 2022")));
        products.Add(new Product("banaan", DateTime.Parse("14 November 2022")));
        products.Add(new Product("aardappel", DateTime.Parse("15 November 2022")));
        products.Add(new Product("taart", DateTime.Parse("10 December 2022")));
        products.Add(new Product("gehakt", DateTime.Parse("14 December 2022")));
        products.Add(new Product("broccoli", DateTime.Parse("20 December 2022")));
        products.Add(new Product("pasta", DateTime.Parse("10 January 2022")));

        return products;
    }
}

