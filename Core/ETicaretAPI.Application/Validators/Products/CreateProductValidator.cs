using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lüften isim alanını doldurunuz....")
                .MaximumLength(150)
                .MinimumLength(3)
                .WithMessage("Lüften ürün adını 3 ile 150 karakter arasında olmasına dikkat ediniz....");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stok bilgisi boş geçilemez")
                .Must(s => s >= 0)
                .WithMessage("Stok bilgisi negatif olamaz");
            
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Fiyat bilgisi boş geçilemez")
                .Must(s => s >= 0)
                .WithMessage("Fiyat bilgisi negatif olamaz");
        }
    }
}
