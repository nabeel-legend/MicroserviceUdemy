using AutoMapper;
using Mango.Service.CouponAPI.Data;
using Mango.Service.CouponAPI.Model;
using Mango.Service.CouponAPI.Model.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mango.Service.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;


        public CouponAPIController(AppDbContext db,IMapper mapper1)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper1;
        }


        // GET: api/<CouponAPIController>
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                //purpose ienumerator is to get the record and using in foreach loop
                IEnumerable<Coupon> couponlist = _db.Coupons.ToList();
                _responseDto.Result= _mapper.Map<IEnumerable<CouponDto>>(couponlist);
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        // GET api/<CouponAPIController>/5
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon couponlist = _db.Coupons.First(u=>u.CouponId==id);
                _responseDto.Result = _mapper.Map<CouponDto>(couponlist);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpGet]
        [Route("{CouponCode/{code}")]
        public ResponseDto GetCouponCOde(string code)
        {
            try
            {
                Coupon couponlist = _db.Coupons.First(u => u.CouponCode.ToLower() == code);
                _responseDto.Result = _mapper.Map<CouponDto>(couponlist);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost]
        [Route("CreateCoupon")]
        public ResponseDto CraeteCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto); 
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        [Route("UpdateCoupon")]
        public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("DeleteCoupon")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }



    }
}
