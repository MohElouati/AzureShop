using DomainModel;

namespace DAL
{
    public static class DbSeeder
    {
        public static void Seed(AzureShopContext context)
        {
            if (!context.Produits.Any())
            {
                context.Produits.AddRange(
                    new Produit { Nom = "T-shirt", Description = "Coton bio", Prix = 19.99m, UnitsInStock = 100 },
                    new Produit { Nom = "Sac à dos", Description = "Imperméable", Prix = 49.99m, UnitsInStock = 50 },
                    new Produit { Nom = "Casque audio", Description = "Bluetooth", Prix = 89.90m, UnitsInStock = 30 },
                    new Produit { Nom = "Sweat capuche", Description = "Coupe oversize", Prix = 59.90m, UnitsInStock = 80 },
                    new Produit { Nom = "Mug", Description = "En céramique, 350ml", Prix = 12.00m, UnitsInStock = 150 }
                );
                context.SaveChanges();
            }

            if (!context.Clients.Any())
            {
                var p1 = context.Produits.First(p => p.Nom == "T-shirt");
                var p2 = context.Produits.First(p => p.Nom == "Casque audio");
                var p3 = context.Produits.First(p => p.Nom == "Mug");

                var clients = new List<Client>
                {
                    new Client
                    {
                        FirstName = "Alice", LastName = "Dupont", DateOfBirth = new DateTime(1995, 5, 15),
                        Adresses = new() { new Adresse { Street = "123 rue de Paris", City = "Clermont", ZipCode = "63000", Country = "France" } },
                        Commandes = new()
                        {
                            new Commande
                            {
                                DateCommande = DateTime.Now.AddDays(-2),
                                CommandeProduits = new()
                                {
                                    new CommandeProduits { ProduitID = p1.ProduitID, Quantity = 2 },
                                    new CommandeProduits { ProduitID = p2.ProduitID, Quantity = 1 }
                                }
                            }
                        }
                    },
                    new Client
                    {
                        FirstName = "Bob", LastName = "Martin", DateOfBirth = new DateTime(1988, 11, 22),
                        Adresses = new() { new Adresse { Street = "456 avenue des Lilas", City = "Lyon", ZipCode = "69000", Country = "France" } }
                    },
                    new Client
                    {
                        FirstName = "Claire", LastName = "Moreau", DateOfBirth = new DateTime(1990, 3, 8),
                        Adresses = new() { new Adresse { Street = "12 chemin du lac", City = "Annecy", ZipCode = "74000", Country = "France" } }
                    },
                    new Client
                    {
                        FirstName = "David", LastName = "Bernard", DateOfBirth = new DateTime(1985, 7, 22),
                        Adresses = new() { new Adresse { Street = "89 rue des Arts", City = "Paris", ZipCode = "75010", Country = "France" } },
                        Commandes = new()
                        {
                            new Commande
                            {
                                DateCommande = DateTime.Now.AddDays(-5),
                                CommandeProduits = new()
                                {
                                    new CommandeProduits { ProduitID = p3.ProduitID, Quantity = 5 }
                                }
                            }
                        }
                    },
                    new Client
                    {
                        FirstName = "Emma", LastName = "Leclerc", DateOfBirth = new DateTime(2000, 1, 1),
                        Adresses = new() { new Adresse { Street = "Place de la République", City = "Bordeaux", ZipCode = "33000", Country = "France" } }
                    },
                    new Client
                    {
                        FirstName = "Florian", LastName = "Dumas", DateOfBirth = new DateTime(1992, 4, 18),
                        Adresses = new() { new Adresse { Street = "Rue Victor Hugo", City = "Toulouse", ZipCode = "31000", Country = "France" } }
                    }
                };

                context.Clients.AddRange(clients);
                context.SaveChanges();
            }
        }
    }
}
