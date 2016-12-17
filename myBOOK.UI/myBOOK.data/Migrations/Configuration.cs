namespace myBOOK.data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using static Books;

    internal sealed class Configuration : DbMigrationsConfiguration<myBOOK.data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "myBOOK.data.Context";
        }

        protected override void Seed(Context context)
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "myBook.data.Books.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string LineToRead;
                    while ((LineToRead = reader.ReadLine()) != null)
                    {
                        string[] a = LineToRead.Split(';');
                        var column = a[0];
                        var bookCheck = context._Book.Where(c => c.BookName == column);
                        Books book;
                        if (bookCheck.Count() == 0)
                        {
                            book = new Books
                            {
                                BookName = a[0],
                                Author = a[1],
                                Genre = (Books.Genres)(Convert.ToInt32(a[2]))

                            };
                            context._Book.AddOrUpdate(book);
                            context.SaveChanges();
                        }
                        else
                        {
                            book = bookCheck.First();
                        }


                    }
                }
            }

        }
    }
}
