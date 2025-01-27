﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }
        [Display(Name = "Autheur")]
        public String Author { get; set; }
        [Required]
        [Display(Name = "Prix")]
        public Decimal Price { get; set; }

        [Required]
        [Display(Name = "Contenu")]
        public String Content { get; set; }
        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = null;
            ListBooks = this.libraryDbContext.Books.Include(x => x.Genres).Where(b => b.Price > 0).ToList();

            return View(ListBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                List<Genre> genres = new List<Genre>();
                foreach (var gen in book.Genres)
                {
                    var g = libraryDbContext.Genre.Find(gen);
                    if (g != null) genres.Add(g);
                }
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                libraryDbContext.Add(new Book() {
                    Name = book.Name, 
                    Author = book.Author,
                    Price = book.Price,
                    Genres = genres,
                    Content = book.Content
                });
                libraryDbContext.SaveChanges();
                return  RedirectToAction(nameof(Success));
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = libraryDbContext.Genre } );
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = libraryDbContext.Books.Find(id);
            if (book != null) {
                 libraryDbContext.Books.Remove(book);
                 libraryDbContext.SaveChanges();
            }
            return  RedirectToAction(nameof(Success));
        }
    }
}
