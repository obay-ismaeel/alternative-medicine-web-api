using AlternativeMedicine.App.Domain.Entities;
using AlternativeMedicine.App.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using System.Drawing.Text;

namespace AlternativeMedicine.App.DataAccess;

public static class DbSeeder
{   
    public static void CreateAndSeedDb(AppDbContext context)
    {
        //if (!context.Database.CanConnect())
        //{
        //}
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        //if(context.Categories.Count() is 0)
        //context.Attachments.AddRange(Attachments());
        //context.SaveChanges();

        context.Categories.AddRange(LoadCategories());
        context.SaveChanges();
    }

    public static IEnumerable<Category> LoadCategories() => new List<Category>()
    {
        new Category{Name="Facial", Description="Discover radiant, glowing skin with our premium facial care collection. From cleansers and serums to masks and moisturizers, we offer the best products to nourish, hydrate, and rejuvenate your complexion. Whether you're targeting acne, aging, or dryness, find the perfect skincare routine for a flawless face.", ImagePath = FileSettings.DefaultImagePath },   
        new Category{Name="Body", Description="Pamper your skin from head to toe with our luxurious body care range. Indulge in silky body lotions, exfoliating scrubs, firming creams, and nourishing butters. Keep your skin soft, smooth, and beautifully scented every day.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Hair", Description="Transform your locks with our high-quality hair care essentials. From shampoos and conditioners to treatments and styling products, we have everything you need for strong, shiny, and healthy hair—no matter your hair type or concern.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Health", Description="Prioritize your well-being with our selection of health-boosting products. Explore vitamins, supplements, natural remedies, and wellness essentials designed to support a balanced and vibrant lifestyle.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Perfume", Description="Find your signature scent with our exquisite collection of perfumes and colognes. From fresh and floral to bold and woody, we offer a variety of fragrances to match every mood and personality.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Oil", Description="Experience the power of nature with our premium oils. Whether you're looking for essential oils for aromatherapy, carrier oils for skincare, or hair oils for deep conditioning, our selection delivers pure, nourishing benefits.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Makeup", Description="Enhance your natural beauty with our high-performance makeup range. Discover long-lasting foundations, vibrant eyeshadows, bold lipsticks, and more—everything you need to create stunning looks for any occasion.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Accessories", Description="Complete your beauty and grooming routine with our stylish accessories. From makeup brushes and hair tools to travel cases and organizers, we have the perfect additions to elevate your self-care experience.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Gifts", Description="Surprise your loved ones (or yourself!) with our beautifully curated gift sets. Whether for birthdays, holidays, or just because, our selection of skincare, fragrance, and beauty bundles makes gifting effortless and delightful.", ImagePath = FileSettings.DefaultImagePath},
        new Category{Name="Men", Description="Designed for the modern man, our men’s grooming collection features premium skincare, haircare, shaving essentials, and colognes. Keep your look sharp, fresh, and confident with high-quality products tailored for men.", ImagePath = FileSettings.DefaultImagePath},
    };

    public static IEnumerable<Attachment> Attachments() => new List<Attachment>()
    {
        new Attachment{Path=FileSettings.DefaultImagePath},
    };
}