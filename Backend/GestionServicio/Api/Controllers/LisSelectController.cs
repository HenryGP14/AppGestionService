﻿using Api.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/list")]
    [ApiController]
    [Authorize]
    public class LisSelectController : ControllerBase
    {
        private readonly IListSelectService _listSelectService;

        public LisSelectController(IListSelectService listSelectService)
        {
            _listSelectService = listSelectService;
        }

        [HttpGet("attentionstatus")]
        public async Task<ActionResult> GetAttentionstatus()
        {
            var response = await _listSelectService.GetAttentionstatus();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("attentiontype")]
        public async Task<ActionResult> GetAttentiontype()
        {
            var response = await _listSelectService.GetAttentiontype();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("methodpayment")]
        public async Task<ActionResult> GetMethodpayment()
        {
            var response = await _listSelectService.GetMethodpayment();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("rol")]
        public async Task<ActionResult> GetRol()
        {
            var response = await _listSelectService.GetRol();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("statuscontract")]
        public async Task<ActionResult> GetStatuscontract()
        {
            var response = await _listSelectService.GetStatuscontract();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("userstatus")]
        public async Task<ActionResult> GetUserstatus()
        {
            var response = await _listSelectService.GetUserstatus();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("cash/user")]
        public async Task<ActionResult> GetCashByUser()
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _listSelectService.GetCashByUser(userIdClaim);
            return StatusCode(response.StatusCode, response);
        }
    }
}
