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

        context.Currencies.Add(new Currency { Name = "Syrian Pound", Rate = 10_000 });
        context.SaveChanges();
    }

    public static IEnumerable<Category> LoadCategories() => new List<Category>()
    {
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Facial", ImagePath = FileSettings.DefaultImagePath },   
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Body", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Hair", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Health", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Perfume", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Oil", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Makeup", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Accessories", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Gifts", ImagePath = FileSettings.DefaultImagePath},
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Men", ImagePath = FileSettings.DefaultImagePath},
    };

    public static IEnumerable<Category> LoadSubCategories() => new List<Category>()
    {
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Eyes", ImagePath = FileSettings.DefaultImagePath, ParentId = 1 },
        new Category{Color = "#FFFFFF", NameArabic = "عربي", Name="Lips", ImagePath = FileSettings.DefaultImagePath, ParentId = 1},
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
            Price="33.3",
            CategoryId=1,
        },
        new Product {
            Name="Pure Renew Facial Cleanser – Gentle Exfoliation & Pore Refining",
            Description="Achieve a fresh, polished look with our gentle exfoliating cleanser. Formulated with natural fruit enzymes and micro-fine jojoba beads, it removes dead skin cells, unclogs pores, and smooths texture without irritation. Ideal for daily use, leaving skin soft, clear, and revitalized.",
            Price="11.3",
            CategoryId=1,
        },
        new Product {
            Name="Age-Defy Overnight Cream – Repair & Rejuvenate While You Sleep",
            Description="Wake up to firmer, younger-looking skin with our luxurious overnight cream. Powered by retinol, peptides, and ceramides, it works deeply to reduce wrinkles, boost collagen, and restore elasticity. Perfect for nighttime renewal, leaving skin refreshed and radiant by morning.",
            Price="44.3",
            CategoryId=1,
        },
        new Product {
            Name="Charcoal Detox Clay Mask – Deep Cleansing & Purifying Treatment",
            Description="Draw out impurities and reveal clearer skin with our purifying charcoal clay mask. Enriched with bentonite clay and tea tree oil, it absorbs excess oil, minimizes pores, and detoxifies for a balanced, refreshed complexion. Ideal for oily and acne-prone skin.",
            Price="5.3",
            CategoryId=12,
        },
        new Product {
            Name="Radiant-C Brightening Serum – Even Tone & Illuminate Skin",
            Description="Fade dark spots and boost your natural glo w with our potent Vitamin C serum. Packed with antioxidants and ferulic acid, it fights free radicals, evens skin tone, and enhances luminosity. Lightweight and fast-absorbing for a brighter, more even complexion.",
            Price="150.3",
            CategoryId=12,
        },
    };
}