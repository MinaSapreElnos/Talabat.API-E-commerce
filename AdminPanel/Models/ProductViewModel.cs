using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models;

namespace AdminPanel.Models
{
    public class ProductViewModel 
    {
		//      public int Id { get; set; }
		//[Required(ErrorMessage = "Name is Required")]

		//public string Name { get; set; }
		//[Required(ErrorMessage = "Description is Required")]

		//public string Description { get; set; }

		//public IFormFile Image { get; set; }

		//public string PictureUrl { get; set; }


		//[Required(ErrorMessage = "Price is Required")]
		//[Range(1, 100000)]
		//public decimal Price { get; set; }

		//[Required(ErrorMessage = "ProductTypeId is Required")]

		//public int ProductTypeId { get; set; }

		//public ProductType ProductType { get; set; }



		//[Required(ErrorMessage = "ProductBrand Id is Required")]
		//public int BrandId { get; set; }

		//public ProductBrand Brand { get; set; }

		//public int CategoryId { get; set; }
		//public ProductCategory Category { get; set; }

		public int Id { get; set; }



		public string Name { get; set; }

		public string Description { get; set; } = null!;

		public IFormFile Image { get; set; }

		public string? PictureUrl { get; set; }

		public decimal Price { get; set; }

		public int BrandId { get; set; }
		public string? Brand { get; set; }

		public int CategoryId { get; set; }
		public string? Category { get; set; }

	}
}
