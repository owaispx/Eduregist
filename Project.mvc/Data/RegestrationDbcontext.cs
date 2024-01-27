using Microsoft.EntityFrameworkCore;   // importing dependencies 


using Project.mvc.Models;   //importing models 

public class RegestrationDbcontext : DbContext     //inheritance
{
    public RegestrationDbcontext(DbContextOptions<RegestrationDbcontext> options)
        : base(options)
    {

    }

    public DbSet<Intern> Interns { get; set; }


}
