using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspMvcExample.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMvcExample.Models
{
    public class DbSeeder
    {
        private readonly TradingContext _dbContext;

        public DbSeeder(TradingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool TrySeed()
        {
            bool seeded = false;
            if (!_dbContext.Clients.Any() && !_dbContext.Products.Any())
            {
                Seed();
                seeded = true;
            }

            return seeded;
        }

        private void Seed()
        {
            var genders = new List<Gender>()
            {
                new Gender("Неизвестен"),
                new Gender("Мужчина"),
                new Gender("Женщина")
            };
            _dbContext.Genders.AddRange(genders);

            var products = new List<Product>()
            {
                new Product("Форель", 699.99),
                new Product("Кока-кола 1л", 85),
                new Product("Говядина стейк 800г", 499.99),
                new Product("Пакет-майка полиэтилен", 3.80),
                new Product("Сок апельсиновый Я 1л", 110),
                new Product("Мышь компьютерная", 790),
                new Product("Жевачка Орбит без сахара", 19),
                new Product("Свердловская булочка", 30),
                new Product("Булочка с сыром", 45),
                new Product("Липтон 1л", 90),
                new Product("Фанта 2л", 150),
                new Product("Спрайт 1.5л", 120),
                new Product("Чипсы лейс", 100),
                new Product("Чипсы принглс", 155)
            };
            _dbContext.Products.AddRange(products);
            _dbContext.SaveChanges();

            int unknownGender = _dbContext.Genders.ToList().Min(g => g.Id);
            var clients = new List<Client>()
            {
                new Client("Иванов В.В.", unknownGender, DateTime.Now.AddYears(-30)),
                new Client("Иванова А.А.", unknownGender + 1, DateTime.Now.AddYears(-31)),
                new Client("Викторов Г.Г.", unknownGender + 2, DateTime.Now.AddYears(-32)),
                new Client("Петров Д.Д.", unknownGender + 2, DateTime.Now.AddYears(-33)),
                new Client("Вадина С.С.", unknownGender + 1, DateTime.Now.AddYears(-34)),
                new Client("Громов Б.Б.", unknownGender, DateTime.Now.AddYears(-35)),
                new Client("Шпильева Н.Н.", unknownGender + 1, DateTime.Now.AddYears(-36)),
            };
            _dbContext.Clients.AddRange(clients);
            _dbContext.SaveChanges();

            var receipts = new List<Receipt>()
            {
                new Receipt(1, DateTime.Now.AddDays(-1)),
                new Receipt(1, DateTime.Now.AddDays(-2)),
                new Receipt(2, DateTime.Now.AddDays(-3)),
                new Receipt(2, DateTime.Now.AddDays(-4)),
                new Receipt(3, DateTime.Now.AddDays(-5)),
                new Receipt(3, DateTime.Now.AddDays(-6)),
                new Receipt(4, DateTime.Now.AddDays(-7)),
                new Receipt(5, DateTime.Now.AddDays(-8)),
                new Receipt(5, DateTime.Now.AddDays(-9)),
                new Receipt(5, DateTime.Now.AddDays(-10)),
                new Receipt(6, DateTime.Now.AddDays(-11)),
                new Receipt(6, DateTime.Now.AddDays(-12)),
                new Receipt(7, DateTime.Now.AddDays(-13)),
                new Receipt(7, DateTime.Now.AddDays(-14)),
                new Receipt(7, DateTime.Now.AddDays(-15)),
            };
            _dbContext.Receipts.AddRange(receipts);
            _dbContext.SaveChanges();

            var rnd = new Random();
            int productCount = products.Count;
            int receiptCount = receipts.Count;
            var receiptPositions = new List<ReceiptPosition>();
            for (int i = 0; i < 150; i++)
            {
                receiptPositions.Add(new ReceiptPosition(rnd.Next(productCount) + 1, rnd.Next(receiptCount) + 1, rnd.Next(3) + 1));
            }
            _dbContext.ReceiptPositions.AddRange(receiptPositions);

            var dbReceipts = _dbContext.Receipts
                .Include(r => r.ReceiptPositions)
                    .ThenInclude(rp => rp.Product)
                .ToList();
            foreach (var receipt in dbReceipts)
            {
                receipt.Cost = receipt.ReceiptPositions.Select(rp => rp.Amount * rp.Product.Cost).Sum();
            }

            _dbContext.SaveChanges();
        }
    }
}
