using Core_NET6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core_NET6.Data
{
    // Tämä luokka pitää periyttää DB Contextista, joka on Entity Framwork Coren sisällä
    public class ApplicationDbContext : DbContext
    {
        // constructot
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)   // otetaan vastaan options ja siirretään base luokkaan, joka on DbContext
        {

        }
        public DbSet<Movie> Movies { get; set; }   //  Tämä luo Movie taulun Movies ja sillä on neljä saraketta, jotka loimme Movie luokkaan
    }
}
