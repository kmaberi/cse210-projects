using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int CommentCount => Comments.Count;

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Comments ({CommentCount}):");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var videos = new List<Video>
        {
            new Video("Epic Mountain Biking", "John Doe", 300)
            {
                Comments = new List<Comment>
                {
                    new Comment("Alice", "Awesome ride!"),
                    new Comment("Bob", "Where is this trail?"),
                    new Comment("Charlie", "Great footage!")
                }
            },
            new Video("Cooking Pasta", "Chef Emma", 480)
            {
                Comments = new List<Comment>
                {
                    new Comment("Dave", "Yummy!"),
                    new Comment("Fiona", "I’ll try this tonight."),
                    new Comment("Gary", "More sauce tips please!"),
                    new Comment("Helen", "Loved it.")
                }
            },
            new Video("Tech Review - Smartwatch X", "Tech Guy", 600)
            {
                Comments = new List<Comment>
                {
                    new Comment("Ivan", "Helpful review."),
                    new Comment("Jack", "I’m buying one now."),
                    new Comment("Kelly", "What about battery life?")
                }
            }
        };

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
