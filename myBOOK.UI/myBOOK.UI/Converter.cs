﻿using myBOOK.data;
using myBOOK.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myBOOK.UI
{
    class Converter
    {
        IRepository repo = Factory.Default.GetRepository();

        public List<BookView> ConvertToBookView(List<Books> list)
        {
            var resultList = new List<BookView>();
            for (int i = 0; i < list.Count; i++)
            {
                resultList.Add(new BookView
                {
                    BookName = list[i].BookName,
                    Author = list[i].Author,
                    Rating = repo.ViewRatingForABook(list[i].BookName, list[i].Author)
                });
            }
            return resultList;
        }

        public Books ConvertToBook(BookView bv)
        {
            var b = new Books
            {
                BookName = bv.BookName,
                Author = bv.Author,
            };
            return b;
        }    
    }
}
