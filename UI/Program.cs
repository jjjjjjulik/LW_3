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
            var serviceProvider = new ServiceCollection()
    .AddDbContext<MyProjectDbContext>(options =>
        options.UseSqlServer("Server=localhost;Database=MyProjectDbContext;Trusted_Connection=True;"))
    .AddScoped<IUnitOfWork, DAL.UnitOfWork>() // зареєструвати IUnitOfWork з DAL.UnitOfWork
    .AddScoped<IAnnouncementService, AnnouncementService>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<ITagService, TagService>()
    .BuildServiceProvider();
            _serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();

            while (true)
            {
                Console.WriteLine("1. Add announcement\n2. Update announcement\n3. Deactivate announcement\n4. Delete announcement\n5. List announcements\n6. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddAnnouncement();
                        break;
                    case 2:
                        UpdateAnnouncement();
                        break;
                    case 3:
                        DeactivateAnnouncement();
                        break;
                    case 4:
                        DeleteAnnouncement();
                        break;
                    case 5:
                        ListAnnouncements();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void AddAnnouncement()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
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

        private static void UpdateAnnouncement()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
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

        private static void DeactivateAnnouncement()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
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

        private static void DeleteAnnouncement()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
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

        private static void ListAnnouncements()
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
    }
}

