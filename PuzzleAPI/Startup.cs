using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PuzzleAPI.Models;
using Microsoft.EntityFrameworkCore;
using PuzzleAPI.DAL;
using PuzzleAPI.Interfaces;
using System.Reflection;
using System;
using System.IO;

namespace PuzzleAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Getting connectionstring from appsettings.json file.
            string connectionString = Configuration.GetConnectionString("ConStr");
            services.AddDbContext<DBContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ConStr")));

            //Adding scope of interface & class that is implementing that interface.
            services.AddScoped<IQuestionCategory, QuestionCategoryDAL>();
            services.AddScoped<IQuestions, QuestionsDAL>();
            services.AddScoped<IAnswers, AnswersDAL>();
            services.AddScoped<IUsers, UserDAL>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
