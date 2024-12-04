using api_burguer_mania.Models;
using Microsoft.EntityFrameworkCore;

namespace api_burguer_mania.Context;

public class BurguerManiaDbContext : DbContext {
    public BurguerManiaDbContext(DbContextOptions<BurguerManiaDbContext> options) : base(options) {

    }
    public DbSet<User> Users {get; set;}

    public DbSet<Product> Products {get; set;}

    public DbSet<Category> Categories {get; set;}

    public DbSet<Status> Status {get; set;}

    public DbSet<Order> Orders {get; set;}
    public DbSet<OrderProduct> OrdersProducts {get; set;}
    public DbSet<OrderUser> OrdersUsers {get; set;}
}