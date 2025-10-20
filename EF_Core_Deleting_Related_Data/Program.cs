using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

Console.WriteLine("Hello, World!");

#region One to One Iliskisel Senaryolarda Veri Silme
#region Example 1
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//context.Addresses.Remove(person.Address);

//await context.SaveChangesAsync();


#endregion
#region Example 2
//Person? person2 = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 2);

//context.Persons.Remove(person2);

//await context.SaveChangesAsync();
#endregion
#endregion
#region One to Many Iliskisel Senaryolarda Veri Silme
#region Example 1
//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 2);

//Post? post =blog.Posts.FirstOrDefault(p => p.Id == 5);

//context.Posts.Remove(post);

//await context.SaveChangesAsync();
#endregion
#region Example 2
//Blog? blog = await context.Blogs.FindAsync(1);

//context.Blogs.Remove(blog);

//await context.SaveChangesAsync();

#endregion
#endregion
#region Many to Many Iliskisel Senaryolarda Veri Silme
#region Example 1
//Book? book1 = await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 1);

//Author? author2 = book1.Authors.FirstOrDefault(a => a.Id == 2);

//book1.Authors.Remove(author2);

//await context.SaveChangesAsync();
#endregion
#region Example 2
//Author? author1 = await context.Authors
//    .Include(a => a.Books)
//    .FirstOrDefaultAsync(a => a.Id == 1);

//Book? book2 = author1.Books.FirstOrDefault(a => a.Id == 2);

//author1.Books.Remove(book2);

//await context.SaveChangesAsync();
#endregion
#endregion

#region Saving
//Person person = new()
//{
//    Name = "Cem",
//    Address = new() { PersonAddress = "Denizli" }
//};

//Person person2 = new() { Name = "Murat" };

//await context.AddAsync(person);
//await context.AddAsync(person2);
//await context.SaveChangesAsync();

//Blog blog = new() { Name = "exampleBlog.com", Posts = new List<Post> { new() { Title = "Post1" }, new() { Title = "Post2" }, new() { Title = "Post3" } } };

//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();

//Book book1 = new() { BookName = "Book 1" };
//Book book2 = new() { BookName = "Book 2" };
//Book book3 = new() { BookName = "Book 3" };

//Author author1 = new() { AuthorName = "Author 1" };
//Author author2 = new() { AuthorName = "Author 2" };
//Author author3 = new() { AuthorName = "Author 3" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddRangeAsync(book1, book2, book3);
//await context.SaveChangesAsync();
#endregion


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }

}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }
    public Person Person { get; set; }

}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-V99G48T;Database=ApplicationDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
             .HasOne(a => a.Person)
             .WithOne(p => p.Address)
             .HasForeignKey<Address>(a => a.Id)
             .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .OnDelete(DeleteBehavior.Cascade);
            
    }

}