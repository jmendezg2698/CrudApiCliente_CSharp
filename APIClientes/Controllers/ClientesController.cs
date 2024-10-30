using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIClientes.Data;
using APIClientes.Modelos;
using APIClientes.Repositorio;
using APIClientes.Modelos.DTO;
using Microsoft.AspNetCore.Authorization;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDTO _responseDTO;

        public ClientesController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _responseDTO = new ResponseDTO();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var lista = await _clienteRepositorio.GetClientes();
                _responseDTO.Result = lista;
                _responseDTO.DisplayMessage = "Lista De Clientes";
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.ErrorMessage = new List<string> { ex.ToString() };
            }
            return Ok(_responseDTO);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepositorio.GetClienteById(id);
            if (cliente == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "Cliente No Existe";
                return NotFound(_responseDTO);
            }
            _responseDTO.Result = cliente;
            _responseDTO.DisplayMessage = "Informacion Del Cliente";
            return Ok(_responseDTO);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDTO clienteDTO)
        {
            try
            {
                ClienteDTO cliente = await _clienteRepositorio.CreateUpdate(clienteDTO);
                _responseDTO.Result = cliente;
                return Ok(_responseDTO);
            }catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "Error Al Actualizar El Cliente";
                _responseDTO.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_responseDTO);
            }
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDTO clienteDTO)
        {
            try
            {
                ClienteDTO cliente = await _clienteRepositorio.CreateUpdate(clienteDTO);
                _responseDTO.Result = cliente;
                return CreatedAtAction("GetCliente", new { id = cliente.Id }, _responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "Error Al Actualizar El Cliente";
                _responseDTO.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_responseDTO);
            }

            
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                bool estadoEliminado = await _clienteRepositorio.DeleteCliente(id);
                if (estadoEliminado) {
                    _responseDTO.Result = estadoEliminado;
                    _responseDTO.DisplayMessage = "Cliente Eliminado Con Exito";
                    return Ok(_responseDTO);
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.DisplayMessage = "Error Al Eliminar Cliente";
                    return BadRequest(_responseDTO);
                }
            }catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_responseDTO);
            }
        }

      
    }
}
