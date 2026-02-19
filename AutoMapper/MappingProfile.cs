using AutoMapper;
using BookStorePerfApi.DTOs;
using BookStorePerfApi.DTOs.Authors;
using BookStorePerfApi.DTOs.Books;
using BookStorePerfApi.DTOs.Customers;
using BookStorePerfApi.DTOs.OrderItems;
using BookStorePerfApi.DTOs.Orders;
using BookStorePerfApi.Entities;

namespace BookStorePerfApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAuthorDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorDto, Author>().ReverseMap();
            CreateMap<AuthorResponseDto, Author>().ReverseMap();

            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<BookResponseDto, Book>().ReverseMap();

            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerResponseDto, Customer>().ReverseMap();

            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<UpdateOrderDto, Order>().ReverseMap();
            CreateMap<UpdateOrderDto, Order>().ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<ResponseOrderItemDto, OrderItem>().ReverseMap();

        }
    }
}
