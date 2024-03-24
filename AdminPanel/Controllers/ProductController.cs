using AdminPanel.Helper;
using AdminPanel.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Talabat.API.DTOs;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Spacifications.ProductSpac;

namespace AdminPanel.Controllers
{
   
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController( IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task< IActionResult> Index()
        {

			var Products = await unitOfWork.Repository<Product>().GetAllAsync() ;

            var MappingProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);  

            return View(MappingProducts);

        }


        
        //Create Product Get //

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create (ProductViewModel Model) 
        {
            if (ModelState.IsValid)
            {
                if (Model.Image != null )
                {
                    Model.PictureUrl = PictureSettings.UploadFile(Model.Image, "products");
                }
                else
                {
                    Model.PictureUrl = "images/products/sb-react1.png";

				}
                var MappedProduct = mapper.Map<ProductViewModel, Product>(Model);

                await unitOfWork.Repository<Product>().AddAcync(MappedProduct);

                await unitOfWork.CompleteAcync();

                return RedirectToAction("Index");
            }

            return View(Model);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetAsync(id);

            var MappedProduct = mapper.Map<Product, ProductViewModel>(product);

            return View(MappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id , ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if(ModelState.IsValid)
            {
                if(model.PictureUrl != null)
                {
                    //delete old photo 
                    PictureSettings.DeleteFile(model.PictureUrl, "products");

                    // upload new photo
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                else
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

                var MappedProduct = mapper.Map<ProductViewModel, Product>(model);

                unitOfWork.Repository<Product>().Update(MappedProduct);

                var result = await unitOfWork.CompleteAcync();

                if(result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);  
        }


        public async Task<IActionResult> Delete( int id)
        {
            var product = await unitOfWork.Repository<Product>().GetAsync(id);

            var MappedProduct = mapper.Map<Product, ProductViewModel>(product);

            return View(MappedProduct);
        }


        [HttpPost]

        public async Task<IActionResult> Delete (int id , ProductViewModel model)
        {
            if(id != model.Id) 
                return NotFound();
            try
            {
                var product = await unitOfWork.Repository<Product>().GetAsync(id);
                
                if(product.PictureUrl != null) 
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                     
                }
                unitOfWork.Repository<Product>().Delete(product);

                await unitOfWork.CompleteAcync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View (model);
            }
        }
    }

   
}
