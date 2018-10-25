using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Infra.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernStore.Api.Controllers
{
    public class BaseController : Controller
    {

        private readonly IUow _Uow;

        public BaseController(IUow Uow)
        {
            _Uow = Uow;        
        }

        public async Task<IActionResult> Response(object result,  IEnumerable<Notification> notificatios)
        {
            if(!notificatios.Any())
            {
                try
                {
                    _Uow.Commit();
                    return Ok(new
                    {
                        success = true,
                        data = result

                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = new[] { "Deu ruim aqui na API !!!" ,  "VISH", "FERROUs", ex.InnerException.Message }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notificatios
                });
            }

        }
    }
}