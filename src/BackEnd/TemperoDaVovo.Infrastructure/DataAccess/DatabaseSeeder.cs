using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Infrastructure.DataAccess;

public static class DatabaseSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Restaurants.Any())
            return;

        // ─────────────────────────────────────────────
        // RESTAURANTE
        // ─────────────────────────────────────────────
        var restaurant = new Restaurant(
            name: "Tempero da Vovó",
            phone: "24999999999",
            address: null,
            description: "Comida caseira com sabor de infância",
            restaurantCategory: RestaurantCategory.Brazilian
        );

        // formas de pagamento
        restaurant.SetPaymentWay(new List<PaymentWay>
        {
            PaymentWay.Pix,
            PaymentWay.Cash,
            PaymentWay.Card
        });

        db.Restaurants.Add(restaurant);
        db.SaveChanges();

        // ─────────────────────────────────────────────
        // CIDADE
        // ─────────────────────────────────────────────
        var city = new City(
            name: "Barra do Piraí",
            restaurantId: restaurant.Id
        );

        db.Cities.Add(city);
        db.SaveChanges();

        // ─────────────────────────────────────────────
        // BAIRROS
        // ─────────────────────────────────────────────
        var centro = new Neighborhood(
            name: "Centro",
            deliveryFee: 5,
            cityId: city.Id,
            baseDeliveryTimeInMinutes: 30
        );

        var oficina = new Neighborhood(
            name: "Oficina Velha",
            deliveryFee: 7,
            cityId: city.Id,
            baseDeliveryTimeInMinutes: 40
        );

        db.Neighborhoods.AddRange(centro, oficina);
        db.SaveChanges();

        // ─────────────────────────────────────────────
        // CATEGORIAS
        // ─────────────────────────────────────────────
        var marmita = new Category(
            name: "Marmitas",
            restaurantId: restaurant.Id
        );

        var bebidas = new Category(
            name: "Bebidas",
            restaurantId: restaurant.Id
        );

        db.Categories.AddRange(marmita, bebidas);
        db.SaveChanges();

        // ─────────────────────────────────────────────
        // PRODUTOS
        // ─────────────────────────────────────────────
        var p1 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Arroz, feijão e frango",
            Description = "Clássico brasileiro",
            Price = 20,
            CategoryId = marmita.Id,
            IsPaused = false
        };

        var p2 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Feijoada completa",
            Description = "Feijoada com acompanhamentos",
            Price = 30,
            CategoryId = marmita.Id,
            IsPaused = false
        };

        var p3 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Coca-Cola 2L",
            Description = "Refrigerante gelado",
            Price = 12,
            CategoryId = bebidas.Id,
            IsPaused = false
        };

        db.Products.AddRange(p1, p2, p3);
        db.SaveChanges();
    }
}