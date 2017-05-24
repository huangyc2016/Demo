using System;
using System.Collections.Generic;
using System.Text;
using Hyc.Service.Dtos;
using AutoMapper;
using Hyc.Repository;
using Hyc.Model.TableModel;

namespace Hyc.Service
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }
        public List<ActionDto> GetAllList(int ControllerId)
        {

            var list = _actionRepository.RetriveAllEntity(ControllerId);

            //使用AutoMapper进行实体转换
            return Mapper.Map<List<ActionDto>>(list);
        }


        public ActionDto Get(int id)
        {
            var menus = _actionRepository.RetriveOneEntityById(id);
            return Mapper.Map<ActionDto>(menus);
        }

        public bool InsertOrUpdate(ActionDto dto)
        {
            if (dto.Id > 0)
            {
                return _actionRepository.UpdateEntity(Mapper.Map<Actions>(dto));
            }
            else
            {
                return _actionRepository.InsertEntity(Mapper.Map<Actions>(dto));
            }
        }

        public bool Delete(int Id)
        {
            return _actionRepository.DeleteEntityById(Id);
        }
    }
}
