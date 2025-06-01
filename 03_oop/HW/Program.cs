
using LibraryManagementSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LibraryManagementSystem
{
    // ======== BASIC ASSIGNMENT ========
    
    // TODO: Create the abstract base class LibraryItem with:
    // - Properties: Id, Title, PublicationYear
    // - Constructor that initializes these properties
    // - Abstract method: DisplayInfo()
    // - Virtual method: CalculateLateReturnFee(int daysLate) that returns decimal
    //   with a basic implementation of daysLate * 0.50m
    public abstract class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public LibraryItem(int id, string title, int publicationYear)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PublicationYear = publicationYear;
        }

        public abstract void DisplayInfo();

        public virtual decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.50m;
        }
    }

    // TODO: Create the IBorrowable interface with:
    // - Properties: BorrowDate (DateTime?), ReturnDate (DateTime?), IsAvailable (bool)
    // - Methods: Borrow(), Return()
    public interface IBorrowable
    {
        DateTime? BorrowDate { get; set; }
        DateTime? ReturnDate { get; set; }
        bool IsAvailable { get; set; }

        void Borrow();
        void Return();
    }

    // TODO: Create the Book class that inherits from LibraryItem and implements IBorrowable
    // - Add properties: Author, Pages, Genre
    // - Implement all required methods from the base class and interface
    // - Override CalculateLateReturnFee to return daysLate * 0.75m
    public class Book : LibraryItem, IBorrowable
    {
        public string Author { get; set; }
        public int Pages { get; set; }
        public string? Genre { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable { get; set; }

        public Book(int id, string title, int publicationYear, string author)
            : base(id, title, publicationYear)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            IsAvailable = true;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Book: {Title} (ID: {Id})");
            Console.WriteLine($"Author: {Author}, Year: {PublicationYear}, Pages: {Pages}, Genre: {Genre ?? "Not specified"}");
            Console.WriteLine($"Status: {(IsAvailable ? "Available" : "Borrowed")}");
            if (BorrowDate.HasValue)
                Console.WriteLine($"Borrowed on: {BorrowDate:yyyy-MM-dd}");
            if (ReturnDate.HasValue)
                Console.WriteLine($"Returned on: {ReturnDate:yyyy-MM-dd}");
            Console.WriteLine();
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.75m;
        }

        public void Borrow()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                BorrowDate = DateTime.Now;
                ReturnDate = null;
                Console.WriteLine($"Book '{Title}' has been borrowed on {BorrowDate:yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine($"Book '{Title}' is already borrowed.");
            }
        }

        public void Return()
        {
            if (!IsAvailable)
            {
                IsAvailable = true;
                ReturnDate = DateTime.Now;
                Console.WriteLine($"Book '{Title}' has been returned on {ReturnDate:yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine($"Book '{Title}' is already available.");
            }
        }
    }

    // TODO: Create the DVD class that inherits from LibraryItem and implements IBorrowable
    // - Add properties: Director, Runtime (in minutes), AgeRating
    // - Implement all required methods from the base class and interface
    // - Override CalculateLateReturnFee to return daysLate * 1.00m
    public class DVD : LibraryItem, IBorrowable
    {
        public string Director { get; set; }
        public int Runtime { get; set; }
        public string? AgeRating { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable { get; set; }

        public DVD(int id, string title, int publicationYear, string director)
            : base(id, title, publicationYear)
        {
            Director = director ?? throw new ArgumentNullException(nameof(director));
            IsAvailable = true;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"DVD: {Title} (ID: {Id})");
            Console.WriteLine($"Director: {Director}, Year: {PublicationYear}, Runtime: {Runtime} minutes, Age Rating: {AgeRating ?? "Not specified"}");
            Console.WriteLine($"Status: {(IsAvailable ? "Available" : "Borrowed")}");
            if (BorrowDate.HasValue)
                Console.WriteLine($"Borrowed on: {BorrowDate:yyyy-MM-dd}");
            if (ReturnDate.HasValue)
                Console.WriteLine($"Returned on: {ReturnDate:yyyy-MM-dd}");
            Console.WriteLine();
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 1.00m;
        }

        public void Borrow()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                BorrowDate = DateTime.Now;
                ReturnDate = null;
                Console.WriteLine($"DVD '{Title}' has been borrowed on {BorrowDate:yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine($"DVD '{Title}' is already borrowed.");
            }
        }

        public void Return()
        {
            if (!IsAvailable)
            {
                IsAvailable = true;
                ReturnDate = DateTime.Now;
                Console.WriteLine($"DVD '{Title}' has been returned on {ReturnDate:yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine($"DVD '{Title}' is already available.");
            }
        }
    }

    // TODO: Create the Magazine class that inherits from LibraryItem
    // - Add properties: IssueNumber, Publisher
    // - Implement all required methods from the base class
    // - Magazines don't need to implement IBorrowable (they typically can't be borrowed)
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public string? Publisher { get; set; }

        public Magazine(int id, string title, int publicationYear, int issueNumber)
            : base(id, title, publicationYear)
        {
            IssueNumber = issueNumber;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Magazine: {Title} (ID: {Id})");
            Console.WriteLine($"Issue: {IssueNumber}, Year: {PublicationYear}, Publisher: {Publisher ?? "Not specified"}");
            Console.WriteLine();
        }
    }

    // TODO: Create the Library class with:
    // - A list to store LibraryItems
    // - Methods: AddItem(), SearchByTitle(), DisplayAllItems()
    public class Library
    {
        private List<LibraryItem> _items;

        public Library()
        {
            _items = new List<LibraryItem>();
        }

        public void AddItem(LibraryItem item)
        {
            _items.Add(item);
            Console.WriteLine($"Added item: {item.Title}");
        }

        public LibraryItem? SearchByTitle(string title)
        {
            return _items.FirstOrDefault(item => item.Title.ContainsIgnoreCase(title));
        }

        public void DisplayAllItems()
        {
            Console.WriteLine("\n===== All Library Items =====");
            if (_items.Count == 0)
            {
                Console.WriteLine("No items in the library.");
                return;
            }
            foreach (var item in _items)
            {
                item.DisplayInfo();
            }
        }

        // Advanced: Update item title using ref parameter
        public bool UpdateItemTitle(int id, ref string newTitle)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return false;

            string oldTitle = item.Title;
            item.Title = newTitle;
            newTitle = oldTitle; // Update the ref parameter to the old title
            return true;
        }

        // Advanced: Get item reference (modified to avoid ref return issue)
        public LibraryItem? GetItemReference(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }
    }

    // ======== ADVANCED ASSIGNMENT ========
    
    // TODO (ADVANCED): Create a record type for tracking borrowing history
    // - Include: ItemId, Title, BorrowDate, ReturnDate, BorrowerName
    // - Add an init-only property: LibraryLocation
    public record BorrowRecord(
        int ItemId,
        string Title,
        DateTime BorrowDate,
        DateTime? ReturnDate,
        string BorrowerName)
        {
        public string? LibraryLocation { get; init; }

        public override string ToString()
        {
            return $"Borrow Record: {Title} (ID: {ItemId}), Borrowed by {BorrowerName} on {BorrowDate:yyyy-MM-dd}, " +
                   $"Location: {LibraryLocation ?? "Not specified"}, " +
                   $"Returned: {(ReturnDate.HasValue ? ReturnDate.Value.ToString("yyyy-MM-dd") : "Not returned")}";
        }
    }

    // TODO (ADVANCED): Create an extension method for string
    // - Create a method ContainsIgnoreCase() that checks if a string contains
    //   another string, ignoring case sensitivity
    public static class StringExtensions
{
    public static bool ContainsIgnoreCase(this string source, string value)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
            return false;
        return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}

// TODO (ADVANCED): Create a generic collection to avoid boxing/unboxing
    // - Create a class LibraryItemCollection<T> where T : LibraryItem
    // - Implement methods: Add(), GetItem(), Count property
public class LibraryItemCollection<T> where T : LibraryItem
{
    private List<T> _items;

    public LibraryItemCollection()
    {
        _items = new List<T>();
    }

    public void Add(T item)
    {
        _items.Add(item);
    }

    public T GetItem(int index)
    {
        if (index < 0 || index >= _items.Count)
            throw new ArgumentOutOfRangeException(nameof(index));
        return _items[index];
    }

    public int Count => _items.Count;
}

class Program
{
    static void Main()
    {
        // Create library
        var library = new Library();

        // Add items
        var book1 = new Book(1, "The Great Gatsby", 1925, "F. Scott Fitzgerald")
        {
            Genre = "Classic Fiction",
            Pages = 180
        };

        var book2 = new Book(2, "Clean Code", 2008, "Robert C. Martin")
        {
            Genre = "Programming",
            Pages = 464
        };

        var dvd1 = new DVD(3, "Inception", 2010, "Christopher Nolan")
        {
            Runtime = 148,
            AgeRating = "PG-13"
        };

        var magazine1 = new Magazine(4, "National Geographic", 2023, 56)
        {
            Publisher = "National Geographic Partners"
        };

        library.AddItem(book1);
        library.AddItem(book2);
        library.AddItem(dvd1);
        library.AddItem(magazine1);

        // Display all items
        library.DisplayAllItems();

        // Borrow and return demonstration
        Console.WriteLine("\n===== Borrowing Demonstration =====");
        book1.Borrow();
        dvd1.Borrow();

        // Try to borrow again
        book1.Borrow();

        // Display changed status
        Console.WriteLine("\n===== Updated Status =====");
        book1.DisplayInfo();
        dvd1.DisplayInfo();

        // Return item
        Console.WriteLine("\n===== Return Demonstration =====");
        book1.Return();

        // Search for an item
        Console.WriteLine("\n===== Search Demonstration =====");
        var foundItem = library.SearchByTitle("clean"); // Case-insensitive search
        if (foundItem != null)
        {
            Console.WriteLine("Found item:");
            foundItem.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Item not found");
        }
// ======== ADVANCED FEATURES DEMONSTRATION ========
            // Uncomment and implement these sections for the advanced assignment
            
        // Advanced features demonstration
        if (ShouldRunAdvancedFeatures())
        {
            // Boxing/Unboxing performance comparison
            Console.WriteLine("\n===== ADVANCED: Boxing/Unboxing Performance =====");

            var standardList = new ArrayList();
            var genericList = new List<int>();
            var customCollection = new LibraryItemCollection<Book>();

            const int iterations = 1_000_000;

            // Measure ArrayList performance (with boxing)
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                standardList.Add(i);
            }

            int sum1 = 0;
            foreach (int i in standardList)
            {
                sum1 += i;
            }
            stopwatch.Stop();
            Console.WriteLine($"ArrayList time (with boxing): {stopwatch.ElapsedMilliseconds}ms");

            // Measure generic List<T> performance (no boxing)
            stopwatch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                genericList.Add(i);
            }

            int sum2 = 0;
            foreach (int i in genericList)
            {
                sum2 += i;
            }
            stopwatch.Stop();
            Console.WriteLine($"Generic List<T> time (no boxing): {stopwatch.ElapsedMilliseconds}ms");

            // Add books to custom collection
            var book3 = new Book(5, "The Hobbit", 1937, "J.R.R. Tolkien") { Pages = 310, Genre = "Fantasy" };
            var book4 = new Book(6, "1984", 1949, "George Orwell") { Pages = 328, Genre = "Dystopian" };

            customCollection.Add(book1);
            customCollection.Add(book3);
            customCollection.Add(book4);

            Console.WriteLine($"Books in custom collection: {customCollection.Count}");

            // Pattern matching demonstration
            Console.WriteLine("\n===== ADVANCED: Pattern Matching =====");

            var item = library.SearchByTitle("clean");

            string description = item switch
            {
                Book b when b.Pages > 400 => $"Long book: {b.Title} with {b.Pages} pages",
                Book b => $"Book: {b.Title} by {b.Author}",
                DVD d => $"DVD: {d.Title} directed by {d.Director}",
                Magazine m => $"Magazine: {m.Title}, Issue #{m.IssueNumber}",
                null => "No item found",
                _ => $"Unknown type: {item?.Title ?? "Unknown"}"
            };

            Console.WriteLine(description);

            // Item reference demonstration (modified to match GetItemReference)
            Console.WriteLine("\n===== ADVANCED: Item Reference =====");

            try
            {
                var itemRef = library.GetItemReference(1);
                if (itemRef != null)
                {
                    Console.WriteLine($"Before modification: {itemRef.Title}");
                    itemRef.Title += " (Modified)";
                    Console.WriteLine($"After modification: {itemRef.Title}");
                }
                else
                {
                    Console.WriteLine("Item with ID 1 not found.");
                }

                string title = "New Title";
                if (library.UpdateItemTitle(2, ref title))
                {
                    Console.WriteLine($"Updated title from '{title}' to 'New Title'");
                }
                else
                {
                    Console.WriteLine("Item with ID 2 not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Nullable types demonstration
            Console.WriteLine("\n===== ADVANCED: Nullable Types =====");

            Book? nullableBook = null;
            string bookTitle = nullableBook?.Title ?? "No title available";
            Console.WriteLine($"Nullable book title: {bookTitle}");

            var borrowedBook = library.SearchByTitle("gatsby") as Book;
            DateTime? dueDate = borrowedBook?.BorrowDate?.AddDays(14);
            Console.WriteLine($"Due date: {dueDate?.ToString("yyyy-MM-dd") ?? "Not borrowed"}");

            // Record type demonstration
            Console.WriteLine("\n===== ADVANCED: Record Type =====");

            var borrowRecord = new BorrowRecord(
                1,
                "The Great Gatsby",
                DateTime.Now.AddDays(-7),
                null,
                "John Smith")
            {
                LibraryLocation = "Main Branch"
            };

            Console.WriteLine(borrowRecord);

            var returnedRecord = borrowRecord with { ReturnDate = DateTime.Now };
            Console.WriteLine($"Original record: {borrowRecord}");
            Console.WriteLine($"Modified record: {returnedRecord}");

            // Test extension method
            string searchTerm = "code";
            bool contains = "Clean Code".ContainsIgnoreCase(searchTerm);
            Console.WriteLine($"Does 'Clean Code' contain '{searchTerm}'? {contains}");
        }
    }

    static bool ShouldRunAdvancedFeatures()
    {
        Console.WriteLine("\nWould you like to run the advanced features? (y/n)");
        return Console.ReadLine()?.ToLower() == "y";
    }
}
}