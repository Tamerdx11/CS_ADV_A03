namespace CS_ADV_A03;

internal class Program
{
    static void Main(string[] args)
    {
        List<Book> books = [
            new("978-3-16-148410-0", "C# Programming", ["John ", " Doe"], new DateTime(2020, 1, 1), 29.99m),
            new("978-1-23-456789-7", "Advanced C#", ["Jane ", " Smith"], new DateTime(2021, 5, 15), 39.99m),
            new("978-0-12-345678-9", "C# in Depth", ["Mark ", " Johnson"], new DateTime(2019, 8, 20), 49.99m)
        ];
        // a: User-Defined Delegate
        BookDelegate titleDelegate = BookFunctions.GetTitle;
        Console.WriteLine("----------------- Books Titles -----------------");
        //LibraryEngine.ProccessBook(books, titleDelegate); // This line is commented out to avoid ambiguity with the Func<Book, string> overload
        LibraryEngine.ProccessBook(books, new Func<Book, string>(titleDelegate));

        // b Built-in Delegate
        Console.WriteLine("----------------- Books Authors -----------------");
        LibraryEngine.ProccessBook(books, BookFunctions.GetAuthors);

        // c: Anonymous Function
        Console.WriteLine("----------------- Books ISBN -----------------");
        LibraryEngine.ProccessBook(books, delegate(Book b) { return b.ISBN; });

        // d: Lambda Expression
        Console.WriteLine("----------------- Books Publication Date -----------------");
        LibraryEngine.ProccessBook(books, (b) => b.PublicationDate.ToString());

        Console.ReadKey();
    }
}

public class Book
{
    public string ISBN { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string[] Author { get; set; } = default!;
    public DateTime PublicationDate { get; set; } 
    public decimal Price { get; set; }

    public Book(string _ISBN, string _Title, string[] _Author, DateTime _PublicationDate, decimal _Price)
    {
        ISBN = _ISBN;
        Title = _Title;
        Author = _Author;
        PublicationDate = _PublicationDate;
        Price = _Price;
    }
    public override string ToString()
    {
        return $"{ISBN} - {Title} by {string.Join(", ", Author)} ({PublicationDate.ToShortDateString()}) - ${Price:F2}";
    }
}

public static class  BookFunctions
{
    public static string GetTitle(Book book) => book.Title;
    public static string GetAuthors(Book book) => string.Join(", ", book.Author);
    public static string GetPrice(Book book) => book.Price.ToString();
}

public delegate string BookDelegate(Book book);

public static class  LibraryEngine
{
    public static void ProccessBook(List<Book> bList, Func<Book, string> fPtr)
    {
        foreach (Book book in bList)
            Console.WriteLine(fPtr(book));
    }

    // ----- hidden for ambiguty whith the above method signature -----
    //public static void ProccessBook(List<Book> bList, BookDelegate fPtr) 
    //{
    //    foreach (Book book in bList)
    //        Console.WriteLine(fPtr(book));
    //}
}
