using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TicketAPI.Models;
using TicketAPI.Resources.Request;
using TicketAPI.Resources.Response;
using TicketAPI.Services.Interface;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this._ticketService = ticketService;
            this._mapper = mapper;
        }

        [HttpGet("GetTickets")]
        public async Task<List<TicketResponse>> GetTickets()
        {
            var result = await this._ticketService.GetTickets();
            return _mapper.Map<List<Ticket>, List<TicketResponse>>(result);
        }

        [HttpGet("ScanTicket")]
        public async Task<string> ScanTicket(int id)
        {
            var result = await _ticketService.GetTikcetById(id);
            return result;
            ////return _mapper.Map<IList<Ticket>, IList<string>>(result);
        }

        [HttpPost("BuyTicket")]
        public async Task<IActionResult> BuyTicket(TicketRequest ticket)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<TicketRequest, Ticket>(ticket);
                var result = await _ticketService.BuyTicket(dto);
                if (result != null)
                {
                    var res = _mapper.Map<Ticket, TicketResponse>(result.Data);
                    return Ok(res);
                }
                return BadRequest("Something wrong.");
            }
            return BadRequest("Some Validation error occured.");

        }

        //[HttpGet("SingleTicket")]
        //public async Task<TicketResponse> SingleTicket(int id)
        //{
        //    var result = await _ticketService.GetTicket(id);
        //    var dto = _mapper.Map<Ticket, TicketResponse>(result.Data);
        //    return dto;
        //}

      
        [HttpGet("Print")]
        public ActionResult QrCodePrint(int maxCounts)
        {

            #region service call for assignment details

            List<TicketResponse> qrDatas = new List<TicketResponse>();

            TicketResponse qrData = new TicketResponse();
            qrData.Fullname = "Fullname";
            qrData.TicketType = "Ticket Type";
            qrData.Price = "Price";
            qrData.ExpiryDate = "Expired";
            qrDatas.Add(qrData);
            #endregion
            return File(GeneratePdf(qrDatas, maxCounts), "application/pdf");
        }

        [NonAction]
        public byte[] GeneratePdf(List<TicketResponse> qrDatas, int _maxColumn)
        {
            try
            {
                PdfPTable _pdfTable = new PdfPTable(_maxColumn);
                PdfPTable columnTable = new PdfPTable(1);
                List<TicketResponse> _qrDatas = new List<TicketResponse>();
                iTextSharp.text.Document _document = new iTextSharp.text.Document();
                iTextSharp.text.Font _fontStyle;
                MemoryStream _memoryStream = new MemoryStream();

                _qrDatas = qrDatas;
                _document.SetPageSize(PageSize.A4);
                _document.SetMargins(5f, 5f, 5f, 5f);

                _pdfTable.WidthPercentage = 100;
                _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
                PdfWriter docWrite = PdfWriter.GetInstance(_document, _memoryStream);

                _document.Open();
                float[] sizes = new float[_maxColumn];

                for (var i = 0; i < _maxColumn; i++)
                {
                    sizes[i] = 100;
                }

                _pdfTable.SetWidths(sizes);

                foreach (var _qrData in _qrDatas) //
                {
                    var paramQR = new Dictionary<EncodeHintType, object>();
                    paramQR.Add(EncodeHintType.CHARACTER_SET, CharacterSetECI.GetCharacterSetECIByName("UTF-8"));
                    BarcodeQRCode qrCodigo = new BarcodeQRCode(JsonConvert.SerializeObject(_qrData),
                    150, 150, paramQR);
                    iTextSharp.text.Image imgBarCode = qrCodigo.GetImage();
                    if (_maxColumn > 1)
                    {
                        imgBarCode.ScaleToFit(250f, 250f);
                    }

                    _pdfTable.AddCell(imgBarCode);
                }

                _document.Add(_pdfTable);
                _document.Close();

                return _memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                byte[] encryptedText = new byte[8];
                return encryptedText;
            }

        }
    }
}