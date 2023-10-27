using BackendApi.Auth.Models;
using BackendApi.Controllers;
using BackendApi.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BackendApi.Data.Dtos.Auth.AuthDtos;

namespace BackendApi.Test
{
    public class AuthControllerTests
    {
        [Test]
        public async Task RegisterUser_WithValidData_ReturnsCreatedAtActionResult()
        {
            var userManager = GetMockUserManager();
            var jwtTokenService = new Mock<IJwtTokenService>();
            var controller = new AuthController(userManager.Object, jwtTokenService.Object);
            var registerUserDto = new RegisterUserDto("test@example.com", "password");

            var result = await controller.RegisterUser(registerUserDto);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task RegisterUser_WithExistingUser_ReturnsBadRequest()
        {
            var userManager = GetMockUserManager(existingUser: new ShopUser());
            var jwtTokenService = new Mock<IJwtTokenService>();
            var controller = new AuthController(userManager.Object, jwtTokenService.Object);
            var registerUserDto = new RegisterUserDto("test@example.com", "password");

            var result = await controller.RegisterUser(registerUserDto);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }


        [Test]
        public async Task LoginUser_WithValidCredentials_ReturnsOk()
        {
            var userManager = GetMockUserManager(existingUser: new ShopUser());
            var jwtTokenService = new Mock<IJwtTokenService>();
            var controller = new AuthController(userManager.Object, jwtTokenService.Object);
            var loginUserDto = new LoginDto("existing@example.com", "password123");

            var result = await controller.Login(loginUserDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task LoginUser_WithInValidCredentials_ReturnsOk()
        {
            var userManager = GetMockUserManager(existingUser: new ShopUser(), true); // check password in mock
            var jwtTokenService = new Mock<IJwtTokenService>();
            var controller = new AuthController(userManager.Object, jwtTokenService.Object);
            var loginUserDto = new LoginDto("existing@example.com", "password123");

            var result = await controller.Login(loginUserDto);

            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
        }
        [Test]
        public async Task LoginUser_WithInvalidUser_ReturnsBadRequest()
        {
            var userManager = GetMockUserManager(existingUser: null); // No existing user
            var jwtTokenService = new Mock<IJwtTokenService>();
            var controller = new AuthController(userManager.Object, jwtTokenService.Object);
            var loginUserDto = new LoginDto("nonexistent@example.com", "nonexistent");

            var result = await controller.Login(loginUserDto);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }


        private Mock<UserManager<ShopUser>> GetMockUserManager(ShopUser existingUser = null, bool checkPassword = false)
        {
            var store = new Mock<IUserStore<ShopUser>>();
            var userManager = new Mock<UserManager<ShopUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Object.UserValidators.Add(new UserValidator<ShopUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<ShopUser>());

            userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingUser);
            userManager.Setup(u => u.CheckPasswordAsync(existingUser, It.IsAny<string>())).ReturnsAsync(!checkPassword);
            userManager.Setup(u => u.CreateAsync(It.IsAny<ShopUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            return userManager;
        }

    }
}
