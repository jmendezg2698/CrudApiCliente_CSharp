using APIClientes.Modelos;
using APIClientes.Modelos.DTO;
using APIClientes.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDTO _response;

        public UsersController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDTO();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register (UserDTO userDTO)
        {
            var res = await _userRepositorio.Register(
                new User
                {
                    UserName = userDTO.UserName
                }, userDTO.Password);
            if(res == -1 )
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario Ya Existe";
                return BadRequest(_response);
            }
            if(res == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error Al Crear El Usuario";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "Usuario Creado Con Exito";
            _response.Result = res;
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDTO userDTO)
        {
            var res = await _userRepositorio.Login(userDTO.UserName, userDTO.Password);
            if(res == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario No Existe";
                return BadRequest(_response);
            }
            if(res == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Password Incorrecta";
                return BadRequest(_response) ;
            }
            _response.Result = res;
            _response.DisplayMessage = "Usuario Conectado";
            return Ok(_response);
        }
    }
}
