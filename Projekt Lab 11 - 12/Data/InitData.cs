using Microsoft.AspNetCore.Identity;
using Projekt_Lab_11___12.Models.Entities;
using System;

namespace Projekt_Lab_11___12.Data
{
    public static class InitData
    {
        public async static Task Initialize(
            Projekt_Lab_11___12Context context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager
            )
        {

            string[] roleNames = { "Admin", "User", "Moderator" };

            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                }
            }

            var adminUser = await userManager.FindByEmailAsync("kubabonda@o2.pl");
            if (adminUser != null) {
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }


            if (context.Pickaxes.Any() || context.Shops.Any())
            {
                return;
            }

            var pickaxe1 = new Pickaxe
            {
                Name = "Podstawowy Kilof",
                ImageName = "pickaxeDefault.png",
                RequirmentLevel = 1,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 1
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 1
                }
            }
            };

            var pickaxe2 = new Pickaxe
            {
                Name = "pickaxe-0",
                ImageName = "pickaxe-0.png",
                RequirmentLevel = 2,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 3
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 2
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 1
                }
            }
            };

            var pickaxe3 = new Pickaxe
            {
                Name = "pickaxe-1",
                ImageName = "pickaxe-1.png",
                RequirmentLevel = 3,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 5
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 3
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 2
                }
            }
            };

            var pickaxe4 = new Pickaxe
            {
                Name = "pickaxe-2",
                ImageName = "pickaxe-2.png",
                RequirmentLevel = 1,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 5
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 3
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 2
                }
            }
            };

            var pickaxe5 = new Pickaxe
            {
                Name = "pickaxe-4",
                ImageName = "pickaxe-4.png",
                RequirmentLevel = 1,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 5
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 3
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 2
                }
            }
            };

            var pickaxeAdmin = new Pickaxe
            {
                Name = "pickaxe-admin",
                ImageName = "pickaxe-admin.png",
                RequirmentLevel = 1,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 1000
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 1000
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 1000
                }
            }
            };

            var pickaxe6 = new Pickaxe
            {
                Name = "pickaxe-3",
                ImageName = "pickaxe-3.png",
                RequirmentLevel = 3,
                PickaxeResourceMultipliers = new List<PickaxeResourceMultiplier>
            {
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Iron,
                    Value = 5
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Gold,
                    Value = 3
                },
                new PickaxeResourceMultiplier
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 2
                }
            }
            };

            var shop = new Shop();

            var shopPickaxe1 = new PickaxeShop
            {
                Pickaxe = pickaxe1,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Iron,
                    Value = 100
                }
            }
            };

            var shopPickaxe2 = new PickaxeShop
            {
                Pickaxe = pickaxe2,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Gold,
                    Value = 200
                },
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 5
                }
            }
            };

            var shopPickaxe3 = new PickaxeShop
            {
                Pickaxe = pickaxe3,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Gold,
                    Value = 100
                },
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 5
                }
            }
            };

            var shopPickaxe4 = new PickaxeShop
            {
                Pickaxe = pickaxe4,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Gold,
                    Value = 100
                },
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 5
                }
            }
            };

            var shopPickaxe5 = new PickaxeShop
            {
                Pickaxe = pickaxe5,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Gold,
                    Value = 100
                },
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 5
                }
            }
            };

            var shopPickaxe6 = new PickaxeShop
            {
                Pickaxe = pickaxe6,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Gold,
                    Value = 100
                },
                new PickaxeShopResourceCost
                {
                    ResourceType = ResourceType.Diamond,
                    Value = 5
                }
            }
            };

            var shopPickaxeAdmin = new PickaxeShop
            {
                Pickaxe = pickaxeAdmin,
                PickaxeShopResourceCosts = new List<PickaxeShopResourceCost>
            {
                
            }
            };

            shop.PickaxeShops.Add(shopPickaxe1);
            shop.PickaxeShops.Add(shopPickaxe2);
            shop.PickaxeShops.Add(shopPickaxe3);
            shop.PickaxeShops.Add(shopPickaxe4);
            shop.PickaxeShops.Add(shopPickaxe5);
            shop.PickaxeShops.Add(shopPickaxe6);
            shop.PickaxeShops.Add(shopPickaxeAdmin);


            context.Pickaxes.Add(pickaxe1);
            context.Pickaxes.Add(pickaxe2);
            context.Pickaxes.Add(pickaxe3);
            context.Pickaxes.Add(pickaxe4);
            context.Pickaxes.Add(pickaxe5);
            context.Pickaxes.Add(pickaxe6);
            context.Pickaxes.Add(pickaxeAdmin);

            context.Shops.Add(shop);

            context.PointForClicks.Add(new PointForClick { Level = 1, ResourceType = ResourceType.Iron, Amount = 1 });
            context.PointForClicks.Add(new PointForClick { Level = 2, ResourceType = ResourceType.Iron, Amount = 2 });
            context.PointForClicks.Add(new PointForClick { Level = 3, ResourceType = ResourceType.Iron, Amount = 3 });
            context.PointForClicks.Add(new PointForClick { Level = 4, ResourceType = ResourceType.Iron, Amount = 4 });
            context.PointForClicks.Add(new PointForClick { Level = 5, ResourceType = ResourceType.Iron, Amount = 5 });

            context.PointForClicks.Add(new PointForClick { Level = 1, ResourceType = ResourceType.Gold, Amount = 0.1 });
            context.PointForClicks.Add(new PointForClick { Level = 2, ResourceType = ResourceType.Gold, Amount = 0.2 });
            context.PointForClicks.Add(new PointForClick { Level = 3, ResourceType = ResourceType.Gold, Amount = 0.3 });
            context.PointForClicks.Add(new PointForClick { Level = 4, ResourceType = ResourceType.Gold, Amount = 0.4 });
            context.PointForClicks.Add(new PointForClick { Level = 5, ResourceType = ResourceType.Gold, Amount = 0.5 });

            context.PointForClicks.Add(new PointForClick { Level = 1, ResourceType = ResourceType.Diamond, Amount = 0.01 });
            context.PointForClicks.Add(new PointForClick { Level = 2, ResourceType = ResourceType.Diamond, Amount = 0.02 });
            context.PointForClicks.Add(new PointForClick { Level = 3, ResourceType = ResourceType.Diamond, Amount = 0.03 });
            context.PointForClicks.Add(new PointForClick { Level = 4, ResourceType = ResourceType.Diamond, Amount = 0.04 });
            context.PointForClicks.Add(new PointForClick { Level = 5, ResourceType = ResourceType.Diamond, Amount = 0.05 });


            var levelRequirements = new List<LevelRequirement>();

            var resourceTypes = new[] { ResourceType.Iron, ResourceType.Gold, ResourceType.Diamond };

            foreach (var mineResource in resourceTypes)
            {
                for (int level = 2; level <= 5; level++)
                {
                    foreach (var costResource in resourceTypes)
                    {
                        levelRequirements.Add(new LevelRequirement
                        {
                            Level = level,
                            MineResourceType = mineResource,
                            ResourceType = costResource,
                            Amount = Math.Round(level * 10 * (costResource == mineResource ? 1.5 : 1.0), 2)
                        });
                    }
                }
            }

            context.LevelRequirements.AddRange(levelRequirements);



           await context.SaveChangesAsync();
        }
    }
}
