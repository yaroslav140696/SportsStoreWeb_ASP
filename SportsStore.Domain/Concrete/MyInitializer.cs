using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class MyInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            context.Products.Add(new Product { Name = "Футбольный мяч Adidas", Description = "Мяч для игры в футбол. Размер 5", Price = 120, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Футбольная форма Nike", Description = "Легкие и качественные шорты и футболка для игры в футбол", Price = 400, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Бейстбольная бита", Description = "Деревянная бита для игры в бейстбол", Price = 200, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Боксерские перчатки Revel", Description = "Качественные кожаные перчатки для бокса. Размер 12.", Price = 500, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Ракетка для настольного тенниса Donic", Description = "Ракетка для игроков среднего уровня", Price = 410, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Ракетка для настольного тенниса Atemi 500", Description = "Ракетка для игроков професионального уровня", Price = 1500, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Мяч для большого тенниса Slazenger Wimbledon", Description = "Набор теннисных мячей Slazenger Wimbledon Ultra-Vis содержит технологию HYDROGUARD, которая позволяет этому мячу на 70% лучше отталкивать влагу, чем обычный мяч.", Price = 370, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Боксерские перчатки Revel", Description = "Качественные кожаные перчатки для бокса. Размер 12.", Price = 500, Category = "ActiveSport" });
            context.Products.Add(new Product { Name = "Скейт", Description = "Доска для катания из прочных материалов", Price = 300, Category = "ActiveSport" });

            context.Products.Add(new Product { Name = "Кроссовки Adidas", Description = "Фирменные кроссовки для бега. Размеры 39-45", Price = 500, Category = "SportWear" });
            context.Products.Add(new Product { Name = "Кроссовки Nike", Description = "Кроссовки Nike, новая коллекция. Размеры 39-45", Price = 720, Category = "SportWear" });
            context.Products.Add(new Product { Name = "Хокейные коньки", Description = "Удобные коньки для игры в хокей. Размеры 39-45", Price = 340, Category = "SportWear" });
            context.Products.Add(new Product { Name = "Комплект мужского термобелья Craft Warm Multi 2-Pack M", Description = "Комплект мужского термобелья Craft Warm Multi 2-Pack M ', N'Keep Warm — самая теплая линейка базового слоя, обеспечивающая превосходную терморегуляцию и комфорт во умеренной физической активности при температуре воздуха от 0 ºC до -25 ºC.", Price = 1500, Category = "SportWear" });

            context.Products.Add(new Product { Name = "Лыжи Tisa Sport Step 180 см", Description = "Tisa Sport Step — классические лыжи для начинающих и любителей лыжных прогулок.", Price = 1600, Category = "WinterSport" });
            context.Products.Add(new Product { Name = "Лыжи Tisa Top Universal 192 см", Description = "Tisa Top Universal — универсальные лыжи для начинающих и любителей лыжных прогулок. Не требовательны по подготовке склона и технике.", Price = 1080, Category = "WinterSport" });

            context.Products.Add(new Product { Name = "Шахматная доска", Description = "Доска для игры в шахматы", Price = 380, Category = "BrainSport" });
            context.Products.Add(new Product { Name = "Набор для игры в Poker", Description = "Набор для игры в покер. В комплекте 5 колод карт,фишки, правила игры", Price = 366, Category = "BrainSport" });

            base.Seed(context);
    }
}
}
