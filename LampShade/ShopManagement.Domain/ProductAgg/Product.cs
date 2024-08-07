using _0_Framework.Domain;
using CommentManagement.Domain.CommentAgg;
using ShopManagement.Domain.PictureAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Domain.ProductAgg;

public class Product : EntityBase
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string ShortDescription { get; private set; }
    public string Description { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public long CategoryId { get; private set; } 
    public string Slug { get; private set; }
    public string Keyworks { get; private set; }
    public string MetaDescription { get; private set; }
    public ProductCategory Category { get; private set; }
    public List<ProductPicture> ProductPictures { get; private set; }
    

    public Product(string name, string code, string shortDescription,
        string description, string picture, string pictureAlt, string pictureTitle,
        long categoryId, string slug, string keyworks, string metaDescription)
    {
        Name = name;
        Code = code;
        ShortDescription = shortDescription;
        Description = description;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        CategoryId = categoryId;
        Slug = slug;
        Keyworks = keyworks;
        MetaDescription = metaDescription;
    }

    public void Edit(string name, string code, string shortDescription,
        string description, string picture, string pictureAlt, string pictureTitle,
        long categoryId, string slug, string keyworks, string metaDescription)
    {
        Name = name;
        Code = code;
        ShortDescription = shortDescription;
        Description = description;
        if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        CategoryId = categoryId;
        Slug = slug;
        Keyworks = keyworks;
        MetaDescription = metaDescription;
    }
}