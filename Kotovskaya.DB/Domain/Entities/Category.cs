﻿using System.ComponentModel.DataAnnotations;

namespace Kotovskaya.DB.Domain.Entities;

public enum CategoryType
{
    Soapmaking,
    Candles,
    Cosmetics
}

public class Category
{
    [StringLength(150, MinimumLength = 5)]
    public string Id { get; init; } = new Guid().ToString();

    [StringLength(150, MinimumLength = 5)] 
    public string Name { get; init; } = "Категория";
    
    [StringLength(150, MinimumLength = 5)]
    public string? MsId { get; init; }
    
    public Category? ParentCategory { get; init; }
    
    public bool? IsVisible { get; init; }
    
    public CategoryType? Type { get; init; }
    
    public List<ProductEntity>? Products { get; set; }
}