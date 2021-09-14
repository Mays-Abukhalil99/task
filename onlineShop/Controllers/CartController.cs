﻿using Microsoft.AspNetCore.Mvc;
using onlineShop.Model;
using onlineShop.Resource;
using onlineShop.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _ICartService;
        public CartController(ICartService iCartsService)
        {
            _ICartService = iCartsService;

        }
        //get all carts
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<CartResource>>>> GetAllAsync()
        {
            var response = await _ICartService.GetAll();
            return Ok(response);
        }
        //create new cart
        [HttpPost]
        public async Task<ActionResult<List<ServiceResponse<CartResource>>>> CreateAsync(CartModel newCart)
        {
            return Ok(await _ICartService.AddCart(newCart));
        }
        //get cart by id
        [HttpGet("(id)")]
        public async Task<ActionResult<ServiceResponse<CartResource>>> GetByIdAsync(int Id)
        {
            return Ok(await _ICartService.GetCartById(Id));
        }
        // change checjout to true
        [HttpPut("CheckOut")]
        public async Task<ActionResult<CartResource>> UpdateCheckOutAsync(int Id)
        {
            return Ok(await _ICartService.CartCheckOut(Id));
        }

        // get user's carts bu userId
       [HttpGet("UserId")]
        public async Task<ActionResult<ServiceResponse<CartResource>>> GetUserCartsAsync(int UserId)
        {
            return Ok(await _ICartService.GetUserCarts(UserId));
        }


        [HttpGet("UserCartCheckOut")]
        public async Task<ActionResult<ServiceResponse<CartResource>>> GetUserCartCheckOutAsync(int UserId)
        {
            return Ok(await _ICartService.GetUserCartCheckOut(UserId));
        }

        // [HttpGet("Id")]
        //public async Task<ActionResult<ServiceResponse<CartResource>>> GetUserCartAsync(int UserId)
        //{
        //  return Ok(await _ICartService.GetUserCart(UserId));
        //}
    }
}