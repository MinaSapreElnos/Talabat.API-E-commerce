using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Models;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.ProductSpac;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IprodectServise prodectServise;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _BrandRepo;
        private readonly IGenericRepository<ProductCategory> _CategoryRepo;

        public ProductController(
            //IGenericRepository<ProductBrand> BrandRepo,
            //IGenericRepository<ProductCategory> CategoryRepo,
            //IGenericRepository<Product> ProductRepo ,
            IprodectServise prodectServise ,
            IMapper mapper )
        {
            this.prodectServise = prodectServise;

            //_productRepo = ProductRepo;
            _mapper = mapper;
            //_BrandRepo = BrandRepo;
            //_CategoryRepo = CategoryRepo;
        }

        
        [HttpGet]
        public async Task<ActionResult<Pagianation<ProductToReturnDto>>> GetProducts( [FromQuery] ProductSpacificationParams SpacPrams)
        {
            

            var products = await prodectServise.GetProductsAsync(SpacPrams);

            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);

           

            var Count = await prodectServise.GetCountAsync(SpacPrams);


            return Ok( new Pagianation<ProductToReturnDto>(SpacPrams.PageIndex,SpacPrams.PageSize, Data , Count) );
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
        {
            
            var Product = await prodectServise.GetProductAsync(id); 

            if(Product is null)
            {  

                return NotFound(new APIRespons(404)); 
            }

            return Ok(_mapper.Map<Product,ProductToReturnDto>(Product)); 

        }



        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var Brands = await prodectServise.GetBrandsAcync();

            return Ok(Brands);
        }


        [HttpGet("Category")]

        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategory()
        {
            var Categorys = await prodectServise.GetCategoriesAcync();

            return Ok(Categorys);
        }




    }
}
