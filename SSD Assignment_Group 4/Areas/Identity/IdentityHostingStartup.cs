using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;

[assembly: HostingStartup(typeof(SSD_Assignment_Group_4.Areas.Identity.IdentityHostingStartup))]
namespace SSD_Assignment_Group_4.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}