﻿namespace Places_Service.Models;

public class Photo
{
    public int Height { get; set; }
    public List<string> HtmlAttributions { get; set; }
    public string PhotoReference { get; set; }
    public int Width { get; set; }
}