﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Server.Model
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        // Mettez ici les propriété de votre livre: Nom, Autheur, Prix, Contenu et Genres associés
        // N'oublier pas qu'un livre peut avoir plusieur genres
        public string Author{ get; set; }
        public string Name { get; set; } = string.Empty;

        public string Content { get; set; }

        public decimal Price { get; set; }

        public List<Genre> Genres { get; set; }
    }

    public class BookView
    {
        public int Id { get; set; }

        public string Author{ get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
