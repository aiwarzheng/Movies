using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.WebUI.Models
{
    public class Cart
    {
        List<CartLine> lines = new List<CartLine>();
        public void AddLine(Movie movie, int num)
        {
            CartLine line = lines
                .Where(m=>m.Movie.ID == movie.ID)
                .FirstOrDefault();
            if (line == null)
            {
                lines.Add(new CartLine
                {
                    Movie = movie,
                    Quantity = num
                });
            }
            else
            {
                line.Quantity += num;
            }
        }

        public void RemoveLine(Movie movie)
        {
            lines.RemoveAll(m => m.Movie.ID == movie.ID);
        }

        public decimal CalcTotalPrice()
        {
            return lines.Sum(m => m.Movie.Price * m.Quantity);
        }

        public int CalcTotalCount()
        {
            return lines.Sum(m => m.Quantity);
        }

        public void Clear()
        {
            lines.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get
            {
                return lines;
            }
        }
    }

    public class CartLine
    {
        public Movie Movie { get; set; }
        public int Quantity { get; set; }
    }
}