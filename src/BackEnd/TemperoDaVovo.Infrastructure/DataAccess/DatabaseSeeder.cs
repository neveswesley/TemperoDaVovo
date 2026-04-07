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

        var restaurantId = Guid.Parse("089364D2-0D9F-48E9-9535-F31CF78A3D5F");

        var address = new Address(
            zipCode: "54705370",
            state: "PE",
            city: "Capibaribe",
            neighborhood: "Capibaribe",
            street: "Rua São João do Piauí",
            number: "99",
            complement: "Ao lado do mercado"
        );

        var restaurant = new Restaurant(
            name: "Tempero da Vovó",
            phone: "11987321654",
            address: address,
            description: "O melhor sabor caseiro da região",
            restaurantCategory: RestaurantCategory.Brazilian
        );

        typeof(Restaurant)
            .GetProperty("Id")!
            .SetValue(restaurant, restaurantId);

       
        
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
        // USUÁRIO
        // ─────────────────────────────────────────────
        
        var user = new User
        {
            RestaurantId = restaurantId,
            Email = "neveswesley1997@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("senha1234"),
            Role = Role.Restaurant,
            IsEmailConfirmed = true
        };

        db.Users.Add(user);
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
        var marmitexGrande = new Category(
            name: "Marmitex Grande",
            restaurantId: restaurant.Id
        );

        var monteSeuMarmitex = new Category(
            name: "Monte seu Marmitex",
            restaurantId: restaurant.Id
        );

        db.Categories.AddRange(marmitexGrande, monteSeuMarmitex);
        db.SaveChanges();

        // ─────────────────────────────────────────────
        // PRODUTOS
        // ─────────────────────────────────────────────
        var p1 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Bife Acebolado",
            Description = "Bife macio ao molho de cebola caramelizada",
            Price = 22.99m,
            CategoryId = marmitexGrande.Id,
            IsPaused = false
        };

        var p2 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Filé de Frango Grelhado",
            Description = "Filé de frango temperado na brasa com ervas finas",
            Price = 19.99m,
            CategoryId = marmitexGrande.Id,
            IsPaused = false
        };

        var p3 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Strogonoff de Frango",
            Description = "Frango cremoso ao molho rosé com champignon",
            Price = 24.99m,
            CategoryId = marmitexGrande.Id,
            IsPaused = false
        };

        var p4 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Costelinha Suína",
            Description = "Costelinha suína ao molho barbecue com arroz e farofa",
            Price = 29.99m,
            CategoryId = marmitexGrande.Id,
            IsPaused = false
        };

        var p5 = new Product
        {
            RestaurantId = restaurant.Id,
            Name = "Isca de Peixe Frito",
            Description = "Iscas crocantes de peixe fresco com limão e tempero especial",
            Price = 26.99m,
            CategoryId = marmitexGrande.Id,
            IsPaused = false
        };

        db.Products.AddRange(p1, p2, p3, p4, p5);
        db.SaveChanges();
    }
}