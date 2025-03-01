using Microsoft.EntityFrameworkCore;
using Phonebook.Models;
using static Phonebook.ConfigurationHelper;

namespace Phonebook;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
}