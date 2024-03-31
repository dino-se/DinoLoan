using System;
using System.Collections.Generic;

namespace dotnet_web_mvc.Entity;

public partial class Book
{
    public int Id { get; set; }

    public string Author { get; set; } = null!;

    public string Name { get; set; } = null!;
}
