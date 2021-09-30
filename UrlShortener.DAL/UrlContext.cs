using System;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL
{
    public class UrlContext: DbContext
    {
        private const string DbName = "Urls.db";
        public string DbPath { get; private set; }
        public DbSet<Url> Urls { get; set; }

        public UrlContext(DbContextOptions<UrlContext> options)
        : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}{DbName}";
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}