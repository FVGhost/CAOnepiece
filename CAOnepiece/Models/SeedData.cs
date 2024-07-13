using CAOnepiece.Data;
using CAOnepiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CAOnepieceContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CAOnepieceContext>>()))
        {
            // Look for any movies.
            if (context.Fruit.Any())
            {
                return;   // DB has been seeded
            }
            context.Fruit.AddRange(
                new Fruit
                {
                    Name = "Gomu Gomu No Mi",
                    type = "Zoan Type Fruit",
                    Description = "Originally, the fruit was called Hito Hito no Mi, Model: Nika and classified as a Mythical Zoan-type fruit that allows one to transform into the legendary " +
                    "Sun God Nika (and gain his rubbery attributes), before being renamed and reclassified by the World Government to hide the truth.[9] In the present, " +
                    "only a few are aware of the fruit's true nature, and its existence was never known to be recorded anywhere.",
                    Price = 10.99M
                },
                new Fruit
                {
                    Name = "Moku Moku no Mi ",
                    type = "Logia Type Fruit",
                    Description = " allows the user to create, control, and transform into smoke at will, making the user a Smoke Human",
                    Price = 8.99M
                },
                new Fruit
                {
                    Name = "Tori Tori no Mi",
                    type = "Mythical Zoan Type Fruit",
                    Description = "allows its user to transform into a nue hybrid and a full nue at will.",
                    Price = 15.99M
                },
                new Fruit
                {
                    Name = "Hebi Hebi no Mi",
                    type = "Mythical Zoan Type Fruit ",
                    Description = " allows the user to transform into a hybrid and full version of the Yamata no Orochi, an eight-headed snake or dragon in Japanese mythology. ",
                    Price = 14.99M
                }
            );
            context.SaveChanges();
        }
    }
}