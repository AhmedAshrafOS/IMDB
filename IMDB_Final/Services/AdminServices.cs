using IMDB_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_Final.Services
{
    public interface IAdminServices
    {
        bool Login(string AdminName, string Password);
    }

    public class AdminServices : IAdminServices
    {
        public AppDbContext context { get; set; }

        public AdminServices()
        {
            context = new AppDbContext();
        }


        public bool Login(string AdminName, string Password)
        {
            return context.Admins
                .Where(a => a.AdminName == AdminName && a.Password == Password)
                .Any();
        }
    }
}