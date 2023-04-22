using System;
using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace UI
{
    class Program
    {
        private static IServiceScopeFactory _serviceScopeFactory;
        static void Main(string[] args)
        {

            // Налаштування контейнера залежностей
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            // Встановлення _serviceScopeFactory
            _serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            // Отримання сервісів з контейнера
            var announcementService = serviceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            var tagService = serviceProvider.GetRequiredService<ITagService>();
            var userService = serviceProvider.GetRequiredService<IUserService>();

            // Получение экземпляра контекста базы данных
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MyProjectDbContext>();

            while (true)
            {
                Console.WriteLine("1. Add announcement\n2. Update announcement\n3. Deactivate announcement\n4. Delete announcement\n5. List announcements\n6. Add user\n7. Add category\n8. Add tag\n9. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddAnnouncement(userService, categoryService, tagService, announcementService);
                        break;
                    case 2:
                        UpdateAnnouncement(userService, announcementService);
                        break;
                    case 3:
                        DeactivateAnnouncement(userService, announcementService);
                        break;
                    case 4:
                        DeleteAnnouncement(userService, announcementService);
                        break;
                    case 5:
                        ListAnnouncements(_serviceScopeFactory);
                        break;
                    case 6:
                        AddUser(userService);
                        break;
                    case 7:
                        AddCategory(categoryService);
                        break;
                    case 8:
                        AddTag(tagService);
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void AddAnnouncement(IUserService userService, ICategoryService categoryService, ITagService tagService, IAnnouncementService announcementService)
        {
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());
            var category = categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter content: ");
            string content = Console.ReadLine();

            Console.Write("Enter comma-separated tag IDs: ");
            var tagIds = Console.ReadLine().Split(',');
            var tags = new List<Tag>();
            foreach (var id in tagIds)
            {
                int tagId = int.Parse(id);
                var tag = tagService.GetTagById(tagId);
                if (tag == null)
                {
                    Console.WriteLine($"Tag with ID {tagId} not found.");
                    return;
                }
                tags.Add(tag);
            }

            var announcement = new Announcement { UserId = user.Id, CategoryId = category.Id, Title = title, Description = content, Tags = tags };
            announcementService.AddAnnouncement(announcement);
            Console.WriteLine("Announcement added.");
        }

        private static void UpdateAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("Enter announcement ID: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Announcement not found.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("You can only update your own announcements.");
                return;
            }

            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new content: ");
            string newDescription = Console.ReadLine();

            announcement.Title = newTitle;
            announcement.Description = newDescription;

            announcementService.UpdateAnnouncement(announcement);
            Console.WriteLine("Announcement updated.");
        }

        private static void DeactivateAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("Enter announcement ID: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Announcement not found.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("You can only deactivate your own announcements.");
                return;
            }

            announcement.IsActive = false;
            announcementService.UpdateAnnouncement(announcement);
            Console.WriteLine("Announcement deactivated.");
        }

        private static void DeleteAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("Enter announcement ID: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Announcement not found.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("You can only delete your own announcements.");
                return;
            }

            announcementService.DeleteAnnouncement(announcement.Id);
            Console.WriteLine("Announcement deleted.");
        }

        private static void ListAnnouncements(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
            var announcements = announcementService.GetAllAnnouncements();
            Console.WriteLine("Announcements:");
            foreach (var announcement in announcements)
            {
                Console.WriteLine($"ID: {announcement.Id}, Title: {announcement.Title}, Content: {announcement.Description}, IsActive: {announcement.IsActive}");
            }
        }
        private static void AddUser(IUserService userService)
        {
            Console.Write("Enter user FirstName: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter user LastName: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();
            Console.Write("Enter user password: ");
            string password = Console.ReadLine();
            Console.Write("Enter user id: ");
            int id = int.Parse(Console.ReadLine());

            var user = new User {Id=id, FirstName = firstName, LastName = lastName, Email = email, PasswordHash = password };
            userService.AddUser(user);
            Console.WriteLine("User added.");
        }
        private static void AddCategory(ICategoryService categoryService)
        {
            Console.Write("Enter user Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter id: ");
            int id = int.Parse(Console.ReadLine());

            var category = new Category { Id = id, Name = name};
            categoryService.AddCategory(category);
            Console.WriteLine("Category added.");
        }
        private static void AddTag(ITagService tagService)
        {
            Console.Write("Enter tag name: ");
            string name = Console.ReadLine();
            Console.Write("Enter tag id: ");
            int id = int.Parse(Console.ReadLine());

            var tag = new Tag { Id = id, Name = name };
            tagService.AddTag(tag);
            Console.WriteLine("Tag added.");
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Реєстрація DbContext з конфігурацією провайдера SQLite
            services.AddDbContext<MyProjectDbContext>(options =>
                options.UseSqlite("Data Source=myDatabase.db"));

            // Реєстрація DbContext
            services.AddDbContext<MyProjectDbContext>();

            // Реєстрація UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
                new UnitOfWork(provider.GetRequiredService<MyProjectDbContext>()));

            // Реєстрація сервісів
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
        }
    }

}

