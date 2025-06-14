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

        context.Categories.AddRange(LoadSubCategories());
        context.SaveChanges();

        context.Products.AddRange(LoadProducts());
        context.SaveChanges();

        context.Attachments.AddRange(LoadAttachments());
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

    public static IEnumerable<Category> LoadSubCategories() => new List<Category>()
    {
        new Category{Name="Eyes", Description="Discover radiant, glowing skin with our premium facial care collection. From cleansers and serums to masks and moisturizers, we offer the best products to nourish, hydrate, and rejuvenate your complexion. Whether you're targeting acne, aging, or dryness, find the perfect skincare routine for a flawless face.", ImagePath = FileSettings.DefaultImagePath, ParentId = 1 },
        new Category{Name="Lips", Description="Pamper your skin from head to toe with our luxurious body care range. Indulge in silky body lotions, exfoliating scrubs, firming creams, and nourishing butters. Keep your skin soft, smooth, and beautifully scented every day.", ImagePath = FileSettings.DefaultImagePath, ParentId = 1},
    };

    public static IEnumerable<Attachment> LoadAttachments() => new List<Attachment>()
    {
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 1},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 1},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 2},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 2},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 3},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 4},
        new Attachment{Path=FileSettings.DefaultImagePath, ProductId = 5},
    };

    public static IEnumerable<Product> LoadProducts() => new List<Product>()
    {
        new Product {
            Name="Glow Revival Hydrating Serum – Deep Moisture & Radiance Boost",
            Description="Unlock your skin’s natural glow with our lightweight, fast-absorbing hydrating serum. Infused with hyaluronic acid, vitamin E, and botanical extracts, this serum deeply nourishes, plumps fine lines, and restores a dewy, youthful radiance. Perfect for all skin types, it’s your daily dose of hydration for a smoother, more luminous complexion.",
            Price="33.3$",
            CategoryId=1,
        },
        new Product {
            Name="Pure Renew Facial Cleanser – Gentle Exfoliation & Pore Refining",
            Description="Achieve a fresh, polished look with our gentle exfoliating cleanser. Formulated with natural fruit enzymes and micro-fine jojoba beads, it removes dead skin cells, unclogs pores, and smooths texture without irritation. Ideal for daily use, leaving skin soft, clear, and revitalized.",
            Price="11.3$",
            CategoryId=1,
        },
        new Product {
            Name="Age-Defy Overnight Cream – Repair & Rejuvenate While You Sleep",
            Description="Wake up to firmer, younger-looking skin with our luxurious overnight cream. Powered by retinol, peptides, and ceramides, it works deeply to reduce wrinkles, boost collagen, and restore elasticity. Perfect for nighttime renewal, leaving skin refreshed and radiant by morning.",
            Price="44.3$",
            CategoryId=1,
        },
        new Product {
            Name="Charcoal Detox Clay Mask – Deep Cleansing & Purifying Treatment",
            Description="Draw out impurities and reveal clearer skin with our purifying charcoal clay mask. Enriched with bentonite clay and tea tree oil, it absorbs excess oil, minimizes pores, and detoxifies for a balanced, refreshed complexion. Ideal for oily and acne-prone skin.",
            Price="5.3$",
            CategoryId=12,
        },
        new Product {
            Name="Radiant-C Brightening Serum – Even Tone & Illuminate Skin",
            Description="Fade dark spots and boost your natural glow with our potent Vitamin C serum. Packed with antioxidants and ferulic acid, it fights free radicals, evens skin tone, and enhances luminosity. Lightweight and fast-absorbing for a brighter, more even complexion.",
            Price="150.3$",
            CategoryId=12,
        },
    };
}