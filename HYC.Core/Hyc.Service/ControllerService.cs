using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;
using Hyc.Model.TableModel;

namespace Hyc.Service
{
    public class ControllerService : IControllerService
    {
        private readonly IControllerRepository _controllerRepository;
        public ControllerService(IControllerRepository controllerRepository)
        {
            _controllerRepository = controllerRepository;
        }
        public List<ControllerDto> GetAllList()
        {

            var users = _controllerRepository.RetriveAllEntity();

            //使用AutoMapper进行实体转换
            return Mapper.Map<List<ControllerDto>>(users);
        }


        public ControllerDto Get(int id)
        {
            var menus = _controllerRepository.RetriveOneEntityById(id);
            return Mapper.Map<ControllerDto>(menus);
        }

        public bool InsertOrUpdate(ControllerDto dto)
        {
            if (dto.Id > 0)
            {
                return _controllerRepository.UpdateEntity(Mapper.Map<Controllers>(dto));
            }
            else
            {
                return _controllerRepository.InsertEntity(Mapper.Map<Controllers>(dto));
            }
        }

        public bool Delete(int Id)
        {
            return _controllerRepository.DeleteEntityById(Id);
        }
    }
}
