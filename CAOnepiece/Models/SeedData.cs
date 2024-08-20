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
            
            if (context.Fruit.Any())
            {
                return;   
            }
            context.Fruit.AddRange(
                new Fruit
                {
                    Name = "Gomu Gomu No Mi",
                    type = "Zoan Type Fruit",
                    Description = "Originally, the fruit was called Hito Hito no Mi, Model: Nika and classified as a Mythical Zoan-type fruit that allows one to transform into the legendary " +
                    "Sun God Nika (and gain his rubbery attributes), before being renamed and reclassified by the World Government to hide the truth.[9] In the present, " +
                    "only a few are aware of the fruit's true nature, and its existence was never known to be recorded anywhere.",
                    Price = 10.99M,
                    Rating = "Rare"
                },
                new Fruit
                {
                    Name = "Moku Moku no Mi ",
                    type = "Logia Type Fruit",
                    Description = " allows the user to create, control, and transform into smoke at will, making the user a Smoke Human",
                    Price = 8.99M,
                    Rating = "Epic"
                },
                new Fruit
                {
                    Name = "Tori Tori no Mi",
                    type = "Mythical Zoan Type Fruit",
                    Description = "allows its user to transform into a nue hybrid and a full nue at will.",
                    Price = 15.99M,
                    Rating = "Legendary"
                },
                new Fruit
                {
                    Name = "Hebi Hebi no Mi",
                    type = "Mythical Zoan Type Fruit ",
                    Description = " allows the user to transform into a hybrid and full version of the Yamata no Orochi, an eight-headed snake or dragon in Japanese mythology. ",
                    Price = 14.99M,
                    Rating = "Legendary"
                }
            );
            context.SaveChanges();
        }

        using (var context = new CAOnepieceContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CAOnepieceContext>>()))
        {

            if (context.Boss.Any())
            {
                return;
            }
            context.Boss.AddRange(
            new Boss
            {
               
                BossName = "Ryu",
                Description = "is a swordsman boss and spawns at Ryu's Palace, on Fishman Island. He uses 1 Sword Style and is equipped with his personal sword",

            },
        new Boss
        {
           
            BossName = "Neptune",
            Description = "is the main boss located at Fishman Island, in order to get to Fishman Island you need to follow the maze through Fishman Cave. Neptune has 2 drops",

        },
                new Boss
                {
                    
                    BossName = "Whitebeard",
                    Description = "Sea boss and is dormant at Marine Base G-1. He uses the Gura Gura no Mi and is equipped with Bisento and his personal cape.",

                },
                     new Boss
                     {
                         
                         BossName = "Crab King Cho",
                         Description = "The boss has 6,000 HP (per player), however, it passively negates a whopping 50% of all damage," +
                         " thus duplicating it's health. Additionally, when his health is reduced to half or lower, he will start slowly healing." +
                         "The Crab King deals 71 M1 damage and also has a huge vertical M1 hitbox. His spawn time is 10 minutes.He is surrounded by 5-20 of his Crab Minions. They each have 500 HP and deal 20 M1 damage..",

                     }


            ) ; 
            context.SaveChanges();
        }
        using (var context = new CAOnepieceContext(
    serviceProvider.GetRequiredService<
        DbContextOptions<CAOnepieceContext>>()))
        {

            if (context.Weapons.Any())
            {
                return;
            }
            context.Weapons.AddRange(
                new Weapon
                {
                    WeaponName = "Ryu's Katana",
                    Description = "Ryu's Katana is a Rare Sword Style-compatible sword with a 5% drop chance from Ryu at Ryu's Palace, on Fishman Island.",
                    BossId = 1

                },
            new Weapon
            {
                WeaponName = "Neptune's Trident",
                Description = "Neptune's Trident Is a Legendary sword with a 1% drop chance from Neptune at Fishman Island.It has 6 base M1 damage.The level requirement to trade it is 230+.",
                BossId = 2

            },
    new Weapon
    {
        WeaponName = "Bisento",
        Description = "The Bisento, also known as the Murakumogiri, is a Legendary sword with a 1% chance to drop from Captain Zhen at Marine Ford.It deals 13 base M1 damage.The level requirement to trade it is 270+.The damage and range is buffed with the Gura Gura no Mi.",
        BossId = 3

    },
      new Weapon
      {
          WeaponName = "Crab Cutlass",
          Description = "Crab Cutlass (1%) A Legendary rough-looking cutlass that is capable of powerful slashes.8 base M1 damage.",
          BossId = 4

      }


            ) ;
            context.SaveChanges();
        }
    }
}