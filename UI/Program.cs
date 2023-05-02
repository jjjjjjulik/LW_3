using System;
using System.Text;
using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace UI
{
    class Program
    {
        // IServiceScopeFactory буде використовуватись для створення областей відповідальності сервісів
        private static IServiceScopeFactory _serviceScopeFactory;
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            // Налаштування контейнера залежностей
            var services = new ServiceCollection();
            // реєстрація сервісів та налаштування контейнера залежностей
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            // Встановлення _serviceScopeFactory
            _serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            // Отримання сервісів з контейнера
            var announcementService = serviceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            var tagService = serviceProvider.GetRequiredService<ITagService>();
            var userService = serviceProvider.GetRequiredService<IUserService>();

            // отримання екземпляра контексту баз даних
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MyProjectDbContext>();

            while (true)
            {
                Console.WriteLine("1. Додати оголошення\n2. Оновити оголошення\n3. Деактивувати оголошення\n4. Видалити оголошення\n5. Список оголошень\n6. Додати користувача\n7. Додати категорію\n8. Додати тег\n9. Вихід");
                Console.Write("Оберіть опцію: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAnnouncement(userService, categoryService, tagService, announcementService);
                        break;
                    case "2":
                        UpdateAnnouncement(userService, announcementService);
                        break;
                    case "3":
                        DeactivateAnnouncement(userService, announcementService);
                        break;
                    case "4":
                        DeleteAnnouncement(userService, announcementService);
                        break;
                    case "5":
                        ListAnnouncements(_serviceScopeFactory);
                        break;
                    case "6":
                        AddUser(userService);
                        break;
                    case "7":
                        AddCategory(categoryService);
                        break;
                    case "8":
                        AddTag(tagService);
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Невірний номер опції. Спробуйте ще раз.");
                        break;
                }
            }
        }
        // додає нове оголошення до бази даних
        private static void AddAnnouncement(IUserService userService, ICategoryService categoryService, ITagService tagService, IAnnouncementService announcementService)
        {
            Console.Write("Введіть ID користувача: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("Користувач не знайден.");
                return;
            }

            Console.Write("Введіть ID категорії: ");
            int categoryId = int.Parse(Console.ReadLine());
            var category = categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Категорія не знайдена.");
                return;
            }

            Console.Write("Введіть заголовок: ");
            string title = Console.ReadLine();

            Console.Write("Введіть контент: ");
            string content = Console.ReadLine();

            Console.Write("Введіть ID тегів через кому (1,2,3...): ");
            var tagIds = Console.ReadLine().Split(',');
            var tags = new List<Tag>();
            foreach (var id in tagIds)
            {
                int tagId = int.Parse(id);
                var tag = tagService.GetTagById(tagId);
                if (tag == null)
                {
                    Console.WriteLine($"Тег під ID {tagId} не знайден.");
                    return;
                }
                tags.Add(tag);
            }

            var announcement = new Announcement { UserId = user.Id, CategoryId = category.Id, Title = title, Description = content, Tags = tags, IsActive=  true };
            announcementService.AddAnnouncement(announcement);
            Console.WriteLine("Оголошення створено.");
        }
        // оновлює існуюче оголошення, яке належить конкретному користувачев
        private static void UpdateAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {

            Console.Write("Введіть ID користувача: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("Користувач не знайден.");
                return;
            }

            Console.Write("Введіть ID оголошення: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Оголошення не знайдено.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("Ви можете оновлювати лише власні оголошення.");
                return;
            }

            Console.Write("Введіть новий заголовок: ");
            string newTitle = Console.ReadLine();
            Console.Write("Введіть новий контент: ");
            string newDescription = Console.ReadLine();

            announcement.Title = newTitle;
            announcement.Description = newDescription;

            announcementService.UpdateAnnouncement(announcement);
            Console.WriteLine("Оголошення оновлено.");
        }
        // вимикає активність існуючого оголошення, яке належить конкретному користувачеві
        private static void DeactivateAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {
            Console.Write("Введіть ID користувача: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("Користувач не знайден.");
                return;
            }

            Console.Write("Введіть ID оголошення: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Оголошення не знайдено.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("Ви можете деактивувати лише власні оголошення.");
                return;
            }

            announcement.IsActive = false;
            announcementService.UpdateAnnouncement(announcement);
            Console.WriteLine("Оголошення деактивовано.");
        }
        // видаляє існуюче оголошення, яке належить конкретному користувачеві
        private static void DeleteAnnouncement(IUserService userService, IAnnouncementService announcementService)
        {
            Console.Write("Введіть ID користувача: ");
            int userId = int.Parse(Console.ReadLine());
            var user = userService.GetUserById(userId);
            if (user == null)
            {
                Console.WriteLine("Користувач не знайден.");
                return;
            }

            Console.Write("Введіть ID оголошення: ");
            int announcementId = int.Parse(Console.ReadLine());
            var announcement = announcementService.GetAnnouncementById(announcementId);
            if (announcement == null)
            {
                Console.WriteLine("Оголошення не знайдено.");
                return;
            }

            if (announcement.UserId != user.Id)
            {
                Console.WriteLine("Ви можете видаляти лише власні оголошення.");
                return;
            }

            announcementService.DeleteAnnouncement(announcement.Id);
            Console.WriteLine("Оголошення видалено.");
        }
        // виводить список усіх оголошень з бази даних
        private static void ListAnnouncements(IServiceScopeFactory serviceScopeFactory)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var announcementService = scope.ServiceProvider.GetRequiredService<IAnnouncementService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
            var announcements = announcementService.GetAllAnnouncements();
            Console.WriteLine("Список оголошень:");
            foreach (var announcement in announcements)
            {
                Console.WriteLine($"ID: {announcement.Id}, Заголовок: {announcement.Title}, Контент: {announcement.Description}, Активність: {announcement.IsActive}");
            }
        }
        // додавання нових користувачів до бази даних
        private static void AddUser(IUserService userService)
        {
            Console.Write("Введіть ім'я: ");
            string firstName = Console.ReadLine();
            Console.Write("Введіть прізвище: ");
            string lastName = Console.ReadLine();
            Console.Write("Введіть email: ");
            string email = Console.ReadLine();
            Console.Write("Введіть пароль: ");
            string password = Console.ReadLine();
            Console.Write("Введіть ID: ");
            int id = int.Parse(Console.ReadLine());

            var user = new User {Id=id, FirstName = firstName, LastName = lastName, Email = email, PasswordHash = password };
            userService.AddUser(user);
            Console.WriteLine("Користувач доданий.");
        }
        // додавання нових категорій до бази даних
        private static void AddCategory(ICategoryService categoryService)
        {
            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();
            Console.Write("Введіть ID: ");
            int id = int.Parse(Console.ReadLine());

            var category = new Category { Id = id, Name = name};
            categoryService.AddCategory(category);
            Console.WriteLine("Категорія додана.");
        }
        // додавання нових тегів до бази даних
        private static void AddTag(ITagService tagService)
        {
            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();
            Console.Write("Введіть ID: ");
            int id = int.Parse(Console.ReadLine());

            var tag = new Tag { Id = id, Name = name };
            tagService.AddTag(tag);
            Console.WriteLine("Тег доданий.");
        }
        // реєструє сервіси, що використовуються, включаючи об'єкт DbContext, який використовує провайдер SQLite та об'єкт UnitOfWork
        private static void ConfigureServices(IServiceCollection services)
        {

            // Реєстрація DbContext з конфігурацією провайдера SQLite
            services.AddDbContext<MyProjectDbContext>(options =>
                options.UseSqlite("Data Source=myDatabase.db"));


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

